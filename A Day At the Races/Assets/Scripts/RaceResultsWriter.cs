using Bas.ADayAtTheRaces;
using Bas.ADayAtTheRaces.RaceResults;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceResultsWriter : MonoBehaviour
{
    private readonly List<Finish> finishes = new List<Finish>();
    private Race currentRace;
    public TimeSpan GunshotTime { get; set; }

    public string filePath = "%appdata%\\A Day At The Races\\raceresults.xml";

    public void AddResult(Race race, Horse horse, TimeSpan time)
    {
        this.currentRace = race;

        var finish = new Finish()
        {
            HorseName = horse.Name,
            JockeyColor = race.JockeyColors[horse.Name],
            TotalTime = time - GunshotTime,
            Position = finishes.Count + 1
        };

        this.finishes.Add(finish);

        if (this.finishes.Count == race.Horses.Count)
        {
            SaveResults();
        }
    }

    private void SaveResults()
    {
        var raceResult = new RaceResult()
        {
            RaceTime = this.currentRace.Time
        };

        foreach (var finish in this.finishes)
        {
            raceResult.Finishes.Add(finish);
        }

        try
        {
            var expandedFilePath = Environment.ExpandEnvironmentVariables(this.filePath);

            var raceResultsFile = new RaceResultsFile();
            Debug.Log("Loading...");
            raceResultsFile.Load(expandedFilePath);
            Debug.Log("Loaded!");
            raceResultsFile.RaceResults.Add(raceResult);
            raceResultsFile.Save(expandedFilePath);
        }
        catch
        {
        }

        this.finishes.Clear();
        this.currentRace = null;
    }
}
