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
    private int numRunnersAtStartingLine;
    private TimeSpan raceStartTime = TimeSpan.Zero;
    private readonly List<Runner> runners = new List<Runner>();

    private bool IsTimeToSetUpNewRace => futureRaces.Count > 0 && futureRaces.Peek().Time <= DateTime.Now.TimeOfDay;
    private bool IsTimeToStartRace => raceStartTime != TimeSpan.Zero && raceStartTime <= DateTime.Now.TimeOfDay;

    public GameObject announcer;
    public AudioClip announcerIntro;
    public AudioClip gunshotAndCommentary;
    public GameObject horsePrefab;
    public Transform horseParent;
    public float distanceBetweenHorses;
    public bool debugMode = false;

    public GameObject entryPoint;
    public GameObject exitPoint;

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
        if (IsTimeToSetUpNewRace)
        {
            currentRace = futureRaces.Dequeue();
            horseParent.ClearChildren();
            numRunnersAtStartingLine = 0;
            this.runners.Clear();

            var horseWidth = horsePrefab.GetComponent<Renderer>().bounds.size.z;
            var firstStallPosition = entryPoint.transform.position + new Vector3(0, 0, (startingGate.GetComponent<Renderer>().bounds.size.z / 2.0f) - (horseWidth / 2.0f));

            foreach (var horse in currentRace.Horses)
            {
                var horseIndex = currentRace.Horses.IndexOf(horse);
                var horsePosition = firstStallPosition + new Vector3(UnityEngine.Random.Range(0.0f, 2.0f), 0, firstStallPosition.z - horseIndex * (horseWidth + distanceBetweenHorses));
                var horseGameObject = Instantiate(horsePrefab, horsePosition, horsePrefab.transform.rotation, horseParent);
                horseGameObject.name = horse.Name;
                horseGameObject.GetComponent<Renderer>().material.color = horse.Color.ToUnityColor();

                SetHorseColor(horseGameObject, horse.Color.ToUnityColor());

                var runner = horseGameObject.GetComponent<Runner>();
                this.runners.Add(runner);
                runner.ArrivedAtStartingLine += Runner_ArrivedAtStartingLine;
                runner.Initialize(horse, startingGate.transform.position, firstCorner.transform.position, secondCorner.transform.position, thirdCorner.transform.position, finishLine.transform.position, exitPoint.transform.position);
                runner.WalkToStartingLine();
            }

            PlayAnnouncement(announcerIntro, 5.0f);
        }

        if (IsTimeToStartRace)
        {
            StartRace();
            raceStartTime = TimeSpan.Zero;
        }
    }

        
    private void Runner_ArrivedAtStartingLine(object sender, EventArgs e)
    {
        numRunnersAtStartingLine++;

        if (numRunnersAtStartingLine == currentRace?.Horses.Count)
        {
            raceStartTime = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(5.0);
        }
    }

    private void StartRace()
    {
        PlayAnnouncement(this.gunshotAndCommentary);

        foreach (var runner in this.runners)
        {
            runner.Run();
        }
    }

    private void PlayAnnouncement(AudioClip audioClip, float delay = 0.0f)
    {
        var announcerAudioSource = announcer?.GetComponent<AudioSource>();
        if (announcerAudioSource != null)
        {
            announcerAudioSource.clip = audioClip;
            announcerAudioSource.PlayDelayed(delay);
        }
    }

    private void SetHorseColor(GameObject horse, UnityEngine.Color color)
    {
        foreach (var path in new[] { "Horse_PBR/horse/saddle",
                                     "Horse_PBR/horse/saddle_lod",
                                     "Jockey_PBR/jockey/jockey_body",
                                     "Jockey_PBR/jockey/jockey_LOD" })
        {
            horse.transform.Find($"Horse_With_Jockey_PBR/{path}").GetComponent<Renderer>().material.SetColor("_Color", color);
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
