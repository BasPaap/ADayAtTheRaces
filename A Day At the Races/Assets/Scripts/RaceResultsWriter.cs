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

    public void AddResult(Race race, Horse horse, TimeSpan time)
    {
        this.currentRace = race;

        var finish = new Finish()
        {
            HorseName = horse.Name,
            JockeyColor = race.JockeyColors[horse],
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
        const string fileName = "raceresults.xml";

        var raceResult = new RaceResult()
        {
            RaceTime = DateTime.Today + this.currentRace.Time
        };

        foreach (var finish in this.finishes)
        {
            raceResult.Finishes.Add(finish);
        }

        try
        { 
            var raceResultsFile = new RaceResultsFile();
            Debug.Log("Loading...");
            raceResultsFile.Load(fileName);
            Debug.Log("Loaded!");
            raceResultsFile.RaceResults.Add(raceResult);
            raceResultsFile.Save(fileName);
        }
        catch
        {
        }

        this.finishes.Clear();
        this.currentRace = null;
    }
}
