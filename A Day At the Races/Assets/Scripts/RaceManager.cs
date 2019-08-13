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
    
    private Queue<Race> futureRaces;
    private Race currentRace;
    private int numRunnersAtStartingLine;
    private DateTime? raceStartTime;
    private readonly List<Runner> runners = new List<Runner>();
    
    private bool IsTimeToSetUpNewRace => futureRaces.Count > 0 && futureRaces.Peek().Time <= DateTime.Now;
    private bool IsTimeToStartRace => raceStartTime.HasValue && raceStartTime.Value <= DateTime.Now;

    public string configurationFilePath = "%appdata%\\A Day At The Races\\ADayAtTheRaces.xml";

    public RaceResultsWriter raceResultsWriter;
    public GameObject announcer;
    public GameObject horsePrefab;
    public Transform horseParent;
    public float distanceBetweenHorses;

    [Tooltip("When enabled, reschedules the next race to start right after launch.")]
    public bool debugMode = false; 

    [Header("Track locations")]
    public GameObject entryPoint;
    public GameObject exitPoint;
    public GameObject startingGate;
    public GameObject firstCorner;
    public GameObject secondCorner;
    public GameObject thirdCorner;
    public GameObject finishLine;

    private float lastConfigurationFileWatchTime;
    private DateTime lastConfigurationFileWriteTime;

    // Start is called before the first frame update
    void Start()
    {        
        LoadData();        
    }

    private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
    {
        Debug.Log("File changed! Reloading data..");
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTimeToSetUpNewRace)
        {
            ResetRace();
            InstantiateHorses();
        }

        if (IsTimeToStartRace)
        {
            StartRace();
        }

        if (IsConfigurationFileUpdated())
        {
            Debug.Log("configuration file changed, loading data!");
            LoadData();
        }
    }

    private void InstantiateHorses()
    {
        var horseWidth = horsePrefab.GetComponent<Renderer>().bounds.size.z;
        var firstStallPosition = entryPoint.transform.position + new Vector3(0, 0, (startingGate.GetComponent<Renderer>().bounds.size.z / 2.0f) - (horseWidth / 2.0f)) + new Vector3(0.0f, 0.0f, -0.75f);
        foreach (var horse in currentRace.Horses)
        {
            InstantiateHorse(horse, horseWidth, firstStallPosition);
        }
    }

    private bool IsConfigurationFileUpdated()
    {
        const float configurationFileWatchInterval = 5.0f;
        if (Time.time - this.lastConfigurationFileWatchTime > configurationFileWatchInterval)
        {
            var expandedConfigurationFilePath = Environment.ExpandEnvironmentVariables(this.configurationFilePath);
            if (File.GetLastWriteTime(expandedConfigurationFilePath) > this.lastConfigurationFileWriteTime)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetRace()
    {
        currentRace = futureRaces.Dequeue();
        horseParent.ClearChildren();
        numRunnersAtStartingLine = 0;
        this.runners.Clear();
    }

    private void InstantiateHorse(Horse horse, float horseWidth, Vector3 firstStallPosition)
    {
        var horseIndex = currentRace.Horses.IndexOf(horse);
        var horsePosition = firstStallPosition + new Vector3(UnityEngine.Random.Range(0.0f, 2.0f), 0, firstStallPosition.z - horseIndex * (horseWidth + distanceBetweenHorses));
        var horseGameObject = Instantiate(horsePrefab, horsePosition, horsePrefab.transform.rotation, horseParent);
        horseGameObject.name = horse.Name;

        SetJockeyColor(horseGameObject, currentRace.JockeyColors[horse.Name].ToUnityColor());

        var runner = horseGameObject.GetComponent<Runner>();
        this.runners.Add(runner);
        runner.ArrivedAtStartingLine += Runner_ArrivedAtStartingLine;
        runner.ArrivedAtExitPosition += Runner_ArrivedAtExitPosition;
        runner.Finished += Runner_Finished;
        runner.Initialize(horse, currentRace.HorseSpeedModifiers[horse.Name].FirstLapSpeed, currentRace.HorseSpeedModifiers[horse.Name].SecondLapSpeed, startingGate.transform.position, firstCorner.transform.position, secondCorner.transform.position, thirdCorner.transform.position, finishLine.transform.position, exitPoint.transform.position);
        runner.WalkToStartingLine();
    }

    private void Runner_Finished(object sender, EventArgs e)
    {
        this.raceResultsWriter.AddResult(this.currentRace, (sender as Runner).Horse, DateTime.Now.TimeOfDay);
    }

    private void Runner_ArrivedAtExitPosition(object sender, EventArgs e)
    {
        var runner = sender as Runner;
                
        runner.ArrivedAtExitPosition -= Runner_ArrivedAtExitPosition;
        runner.ArrivedAtStartingLine -= Runner_ArrivedAtStartingLine;
        runner.Finished -= Runner_Finished;
        this.runners.Remove(sender as Runner);        
        UnityEngine.Object.Destroy(runner.gameObject);
    }

    private void Runner_ArrivedAtStartingLine(object sender, EventArgs e)
    {
        this.numRunnersAtStartingLine++;

        if (numRunnersAtStartingLine == currentRace?.Horses.Count)
        {
            announcer?.GetComponent<Announcer>().PlayAtTheGatesAnnouncement();
            raceStartTime = DateTime.Now.AddSeconds(5.0);
        }
    }

    private void StartRace()
    {
        raceStartTime = null;
        announcer?.GetComponent<Announcer>().PlayGunshotAndCommentaryAnnouncement();
        this.raceResultsWriter.GunshotTime = DateTime.Now.TimeOfDay;

        foreach (var runner in this.runners)
        {
            runner.Run();
        }
    }

    private void SetJockeyColor(GameObject horse, UnityEngine.Color color)
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
        var expandedConfigurationFilePath = Environment.ExpandEnvironmentVariables(this.configurationFilePath);

        EnsureConfigurationFileExists(expandedConfigurationFilePath);

        var configuration = new ADayAtTheRacesConfiguration();
        LoadConfigurationFile(configuration, expandedConfigurationFilePath);

        this.lastConfigurationFileWatchTime = Time.time;
        this.lastConfigurationFileWriteTime = File.GetLastWriteTime(expandedConfigurationFilePath);

        if (debugMode && currentRace == null)
        {
            RescheduleFirstRace(configuration);
        }

        futureRaces = new Queue<Race>((from r in configuration.Races
                                       where r.Time > DateTime.Now
                                       orderby r.Time
                                       select r).ToList());
    }

    private static void EnsureConfigurationFileExists(string expandedConfigurationFilePath)
    {
        if (!Directory.Exists(Path.GetDirectoryName(expandedConfigurationFilePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(expandedConfigurationFilePath));
        }

        if (!File.Exists(expandedConfigurationFilePath))
        {
            var newConfiguration = new ADayAtTheRacesConfiguration();
            newConfiguration.Populate();
            newConfiguration.Save(expandedConfigurationFilePath);
        }
    }

    private static void RescheduleFirstRace(ADayAtTheRacesConfiguration configuration)
    {
        var firstRace = configuration.Races.FirstOrDefault();
        if (firstRace != null)
        {
            firstRace.Time = DateTime.Now.AddSeconds(1.0);
        }
    }

    private static void LoadConfigurationFile(ADayAtTheRacesConfiguration configuration, string expandedConfigurationFilePath)
    {
        bool shouldRetry;
        var firstLoadAttemptTime = DateTime.Now;
        do
        {
            shouldRetry = false;

            try
            {
                configuration.Load(expandedConfigurationFilePath);
            }
            catch (IOException ex)
            {
                const int ERROR_SHARING_VIOLATION = -2147024864;
                if (ex.HResult == ERROR_SHARING_VIOLATION)
                {
                    if (DateTime.Now - firstLoadAttemptTime < TimeSpan.FromSeconds(5))
                    {
                        shouldRetry = true;
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
        } while (shouldRetry);
    }
}
