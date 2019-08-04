using Assets.Scripts;
using Bas.ADayAtTheRaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RaceManager : MonoBehaviour
{
    private const string configurationFileName = "ADayAtTheRaces.xml";
    private Queue<Race> futureRaces;
    private Race currentRace;
    private GameObject startingGate;

    public GameObject horsePrefab;
    public Transform horseParent;
    public float distanceBetweenHorses;
    public bool debugMode = false;

    public GameObject firstCorner;
    public GameObject secondCorner;
    public GameObject thirdCorner;
    public GameObject finishLine;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        startingGate = GameObject.Find("Starting Gate");
    }

    // Update is called once per frame
    void Update()
    {
        if (futureRaces.Count > 0 && futureRaces.Peek().Time <= DateTime.Now.TimeOfDay)
        {
            currentRace = futureRaces.Dequeue();

            horseParent.ClearChildren();

            var horseWidth = horsePrefab.GetComponent<Renderer>().bounds.size.z;
            var firstStallPosition = startingGate.transform.position + new Vector3(0,0,(startingGate.GetComponent<Renderer>().bounds.size.z / 2.0f) - (horseWidth /2.0f));

            foreach (var horse in currentRace.Horses)
            {
                var horseIndex = currentRace.Horses.IndexOf(horse);
                var horsePosition = firstStallPosition + new Vector3(0, 0, firstStallPosition.z - horseIndex * (horseWidth + distanceBetweenHorses));
                var horseGameObject = Instantiate(horsePrefab, horsePosition, horsePrefab.transform.rotation, horseParent);
                horseGameObject.name = horse.Name;
                horseGameObject.GetComponent<Renderer>().material.color = horse.Color.ToUnityColor();

                var runner = horseGameObject.GetComponent<Runner>();
                runner.FirstCornerPosition = firstCorner.transform.position;
                runner.SecondCornerPosition = secondCorner.transform.position;
                runner.ThirdCornerPosition = thirdCorner.transform.position;
                runner.FinishLinePosition = finishLine.transform.position;
                runner.Run(horse);                
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
