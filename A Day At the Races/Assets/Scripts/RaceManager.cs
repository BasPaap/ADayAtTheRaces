using Assets.Scripts;
using Bas.ADayAtTheRaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    private const string configurationFileName = "ADayAtTheRaces.xml";
    private List<Horse> horses;
    private Queue<Race> futureRaces;
    private Race currentRace;
    private GameObject startingGate;

    public GameObject horsePrefab;
    public Transform horseParent;
    public float horseDistance;
    public bool debugMode = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        startingGate = GameObject.Find("Starting Gate");
    }

    // Update is called once per frame
    void Update()
    {
        if (futureRaces.Peek().Time <= DateTime.Now.TimeOfDay)
        {
            currentRace = futureRaces.Dequeue();

            horseParent.ClearChildren();

            var horseWidth = horsePrefab.GetComponent<Renderer>().bounds.size.z;
            var firstStallPosition = startingGate.transform.position + new Vector3(0,0,(startingGate.GetComponent<Renderer>().bounds.size.z / 2.0f) - (horseWidth /2.0f));

            foreach (var horse in currentRace.Horses)
            {
                var horsePosition = firstStallPosition + new Vector3(0, 0, firstStallPosition.z - currentRace.Horses.IndexOf(horse) * (horseWidth + horseDistance));
                var horseGameObject = Instantiate(horsePrefab, horsePosition, horsePrefab.transform.rotation, horseParent);
                horseGameObject.GetComponent<Renderer>().material.color = horse.Color.ToUnityColor();
            }
        }
    }

    public void LoadData()
    {
        var configurationPath = Path.Combine(Application.dataPath, configurationFileName);
        ADayAtTheRacesConfiguration configuration = new ADayAtTheRacesConfiguration();

        if (!File.Exists(configurationPath))
        {
            configuration.Populate();
            configuration.Save(configurationPath);
        }
        else
        {
            configuration.Load(configurationPath);
        }

        horses = configuration.Horses.ToList();

        if (debugMode)
        {
            var firstRace = configuration.Races.FirstOrDefault();
            if (firstRace != null)
            {
                firstRace.Time = DateTime.Now.AddSeconds(1.0).TimeOfDay;
            }
        }

        futureRaces = new Queue<Race>((from r in configuration.Races
                                       where r.Time > DateTime.Now.TimeOfDay
                                       orderby r.Time
                                       select r).ToList());
    }
}
