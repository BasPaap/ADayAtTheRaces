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

    public void AddResult(Race race, Horse horse, TimeSpan time)
    {
        this.currentRace = race;

        var finish = new Finish()
        {
            HorseName = horse.Name,
            TotalTime = time - race.Time,
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

        var raceResultsFile = new RaceResultsFile();
        raceResultsFile.Load(fileName);
        raceResultsFile.RaceResults.Add(raceResult);
        raceResultsFile.Save(fileName);

        this.finishes.Clear();
        this.currentRace = null;
    }
}
