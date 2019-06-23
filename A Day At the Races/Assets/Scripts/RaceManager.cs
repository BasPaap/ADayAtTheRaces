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

    public GameObject horsePrefab;
    public Transform horseParent;
    public float horseDistance;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (futureRaces.Peek().Time <= DateTime.Now.TimeOfDay)
        {
            currentRace = futureRaces.Dequeue();

            horseParent.ClearChildren();

            foreach (var horse in currentRace.Horses)
            {
                const float horseWidth = 5.0f;
                var horsePosition = new Vector3(currentRace.Horses.IndexOf(horse) * (horseWidth + horseDistance), 0);
                var horseGameObject = Instantiate(horsePrefab, Vector3.zero, Quaternion.identity, horseParent);
                horseGameObject.GetComponent<Material>().color = horse.Color.ToUnityColor();
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
        futureRaces = new Queue<Race>((from r in configuration.Races
                                       where r.Time > DateTime.Now.TimeOfDay
                                       orderby r.Time
                                       select r).ToList());
    }
}
