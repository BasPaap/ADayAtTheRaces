﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bas.ADayAtTheRaces
{
    /// <summary>
    /// Configuration for the application, including participating horses and planned races.
    /// </summary>
    [DataContract]
    public sealed class ADayAtTheRacesConfiguration
    {
        private readonly DataContractSerializerSettings serializerSettings = new DataContractSerializerSettings()
        {
            PreserveObjectReferences = true,
            KnownTypes = new [] { typeof(ADayAtTheRacesConfiguration), typeof(Horse), typeof(RunningPhase), typeof(Color), typeof(Race), typeof(LapSpeedModifier)}
        };

        [DataMember]
        public Collection<Horse> Horses { get; private set; } = new Collection<Horse>();
        [DataMember]
        public Collection<Race> Races { get; private set; } = new Collection<Race>();

        /// <summary>
        /// Populates the configuration structure with test data.
        /// </summary>
        public void Populate()
        {
            var redColor = new Color(230, 122, 120);
            var greenColor = new Color(226, 255, 87);
            var blueColor = new Color(125, 202, 232);
            var orangeColor = new Color(255, 188, 123);
            var purpleColor = new Color(207, 135, 221);
            var mintColor = new Color(39, 219, 180);

            var straightward = new Horse
            {
                Name = "Straightward",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };
            straightward.RunningPhases.Add(new RunningPhase(20.0, 0.6f));
            straightward.RunningPhases.Add(new RunningPhase(30.0, 0.8f));
            straightward.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var sirtThackery = new Horse
            {
                Name = "Sir Thackery",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };
            sirtThackery.RunningPhases.Add(new RunningPhase(10.5, 0.8f));
            sirtThackery.RunningPhases.Add(new RunningPhase(40.0, 0.6f));
            sirtThackery.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var royalArch3 = new Horse
            {
                Name = "Royal Arch III",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };
            royalArch3.RunningPhases.Add(new RunningPhase(28.0, 0.7f));
            royalArch3.RunningPhases.Add(new RunningPhase(25.0, 0.85f));
            royalArch3.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var uncleOrange = new Horse
            {
                Name = "Uncle Orange",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };
            uncleOrange.RunningPhases.Add(new RunningPhase(10.0, 0.65f));
            uncleOrange.RunningPhases.Add(new RunningPhase(15.0, 0.8f));
            uncleOrange.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var peggiesPride = new Horse
            {
                Name = "Peggy's Pride",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };
            peggiesPride.RunningPhases.Add(new RunningPhase(18.0, 0.65f));
            peggiesPride.RunningPhases.Add(new RunningPhase(30.0, 0.87f));
            peggiesPride.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var guidingLight = new Horse
            {
                Name = "Guiding Light",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            guidingLight.RunningPhases.Add(new RunningPhase(17.5, 0.7f));
            guidingLight.RunningPhases.Add(new RunningPhase(25.0, 0.8f));
            guidingLight.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var bigWonder = new Horse
            {
                Name = "Big Wonder",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            bigWonder.RunningPhases.Add(new RunningPhase(40.5, 0.7f));
            bigWonder.RunningPhases.Add(new RunningPhase(20.0, 0.55f));
            bigWonder.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var secretGrace = new Horse
            {
                Name = "Secret Grace",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            secretGrace.RunningPhases.Add(new RunningPhase(10.0, 0.67f));
            secretGrace.RunningPhases.Add(new RunningPhase(30.0, 0.8f));
            secretGrace.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var soldiersJoy = new Horse
            {
                Name = "Soldier's Joy",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            soldiersJoy.RunningPhases.Add(new RunningPhase(10.5, 0.8f));
            soldiersJoy.RunningPhases.Add(new RunningPhase(25.0, 0.64f));
            soldiersJoy.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var theGosling = new Horse
            {
                Name = "The Gosling",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            theGosling.RunningPhases.Add(new RunningPhase(21.5, 1.0f));
            theGosling.RunningPhases.Add(new RunningPhase(25.0, 0.64f));
            theGosling.RunningPhases.Add(new RunningPhase(300, 0.8f));

            var balderdash = new Horse
            {
                Name = "Balderdash",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            balderdash.RunningPhases.Add(new RunningPhase(10.5, 0.62f));
            balderdash.RunningPhases.Add(new RunningPhase(30.0, 0.8f));
            balderdash.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var trumpCard = new Horse
            {
                Name = "Trump Card",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            trumpCard.RunningPhases.Add(new RunningPhase(6.5, 0.9f));
            trumpCard.RunningPhases.Add(new RunningPhase(40.0, 0.6f));
            trumpCard.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var fallGuy = new Horse
            {
                Name = "Fall Guy",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            fallGuy.RunningPhases.Add(new RunningPhase(15.5, 0.7f));
            fallGuy.RunningPhases.Add(new RunningPhase(40.0, 0.6f));
            fallGuy.RunningPhases.Add(new RunningPhase(300, 1.0f));


            var fitzgerald = new Horse
            {
                Name = "Fitzgerald",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            fitzgerald.RunningPhases.Add(new RunningPhase(10.5, 0.8f));
            fitzgerald.RunningPhases.Add(new RunningPhase(30.0, 0.63f));
            fitzgerald.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var unityAsset = new Horse
            {
                Name = "Unity Asset",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            unityAsset.RunningPhases.Add(new RunningPhase(16.5, 0.7f));
            unityAsset.RunningPhases.Add(new RunningPhase(46.0, 0.85f));
            unityAsset.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var bananabread = new Horse
            {
                Name = "Bananabread",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            bananabread.RunningPhases.Add(new RunningPhase(25, 0.7f));
            bananabread.RunningPhases.Add(new RunningPhase(28.0, 0.8f));
            bananabread.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var pinkiePie = new Horse
            {
                Name = "Pinkie Pie",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            pinkiePie.RunningPhases.Add(new RunningPhase(20.0, 1.0f));
            pinkiePie.RunningPhases.Add(new RunningPhase(40.0, 0.55f));
            pinkiePie.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var littleJacob = new Horse
            {
                Name = "Little Jacob",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            littleJacob.RunningPhases.Add(new RunningPhase(10.5, 1.0f));
            littleJacob.RunningPhases.Add(new RunningPhase(50.0, 0.7f));
            littleJacob.RunningPhases.Add(new RunningPhase(300, 1.0f));



            Horses.Add(bigWonder);//1
            Horses.Add(fallGuy);//1
            Horses.Add(soldiersJoy);//1
            Horses.Add(theGosling);//1
            Horses.Add(balderdash);//1
            Horses.Add(trumpCard);//1

            Horses.Add(sirtThackery);//2
            Horses.Add(fitzgerald);//2
            Horses.Add(unityAsset);//2
            Horses.Add(bananabread);//2
            Horses.Add(pinkiePie);//2
            Horses.Add(littleJacob);//2
            
            Horses.Add(straightward);//3
            Horses.Add(royalArch3);//3
            Horses.Add(uncleOrange);//3
            Horses.Add(peggiesPride);//3
            Horses.Add(guidingLight);//3
            Horses.Add(secretGrace);//3



            var tenSecondsFromNow = DateTime.Now.AddSeconds(10);
            var firstRace = new Race(tenSecondsFromNow);
            firstRace.Horses.Add(bigWonder);
            firstRace.Horses.Add(fallGuy);
            firstRace.Horses.Add(soldiersJoy);
            firstRace.Horses.Add(theGosling);
            firstRace.Horses.Add(balderdash);
            firstRace.Horses.Add(trumpCard);
            firstRace.HorseSpeedModifiers[bigWonder.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeedModifiers[fallGuy.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeedModifiers[soldiersJoy.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeedModifiers[theGosling.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeedModifiers[balderdash.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeedModifiers[trumpCard.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.JockeyColors[bigWonder.Name] = redColor;
            firstRace.JockeyColors[fallGuy.Name] = greenColor;
            firstRace.JockeyColors[soldiersJoy.Name] = blueColor;
            firstRace.JockeyColors[theGosling.Name] = orangeColor;
            firstRace.JockeyColors[balderdash.Name] = purpleColor;
            firstRace.JockeyColors[trumpCard.Name] = mintColor;

            var twoMinutesFromNow = DateTime.Now.AddMinutes(2);
            var secondRace = new Race(twoMinutesFromNow);
            secondRace.Horses.Add(sirtThackery);
            secondRace.Horses.Add(fitzgerald);
            secondRace.Horses.Add(unityAsset);
            secondRace.Horses.Add(bananabread);
            secondRace.Horses.Add(pinkiePie);
            secondRace.Horses.Add(littleJacob);
            secondRace.HorseSpeedModifiers[sirtThackery.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeedModifiers[fitzgerald.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeedModifiers[unityAsset.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeedModifiers[bananabread.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeedModifiers[pinkiePie.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeedModifiers[littleJacob.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.JockeyColors[sirtThackery.Name] = redColor;
            secondRace.JockeyColors[fitzgerald.Name] = greenColor;
            secondRace.JockeyColors[unityAsset.Name] = blueColor;
            secondRace.JockeyColors[bananabread.Name] = orangeColor;
            secondRace.JockeyColors[pinkiePie.Name] = purpleColor;
            secondRace.JockeyColors[littleJacob.Name] = mintColor;

            var oneHourFromNow = DateTime.Now.AddHours(1);
            var thirdRace = new Race(oneHourFromNow);
            thirdRace.Horses.Add(straightward);
            thirdRace.Horses.Add(royalArch3);
            thirdRace.Horses.Add(uncleOrange);
            thirdRace.Horses.Add(peggiesPride);
            thirdRace.Horses.Add(guidingLight);
            thirdRace.Horses.Add(secretGrace);
            thirdRace.HorseSpeedModifiers[straightward.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeedModifiers[royalArch3.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeedModifiers[uncleOrange.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeedModifiers[peggiesPride.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeedModifiers[guidingLight.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeedModifiers[secretGrace.Name] = new LapSpeedModifier { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.JockeyColors[straightward.Name] = redColor;
            thirdRace.JockeyColors[royalArch3.Name] = greenColor;
            thirdRace.JockeyColors[uncleOrange.Name] = blueColor;
            thirdRace.JockeyColors[peggiesPride.Name] = orangeColor;
            thirdRace.JockeyColors[guidingLight.Name] = purpleColor;
            thirdRace.JockeyColors[secretGrace.Name] = mintColor;

            Races.Add(firstRace);
            Races.Add(secondRace);
            Races.Add(thirdRace);
        }

        /// <summary>
        /// Saves the configuration to the file provided in <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
                using (var xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;

                    var serializer = new DataContractSerializer(typeof(ADayAtTheRacesConfiguration), serializerSettings);
                    serializer.WriteObject(xmlWriter, this);
                    xmlWriter.Close();
                }
            
        }


        /// <summary>
        /// Loads the configuration from the file provided in <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
                using (var xmlReader = new XmlTextReader(fileName))
                {
                    var serializer = new DataContractSerializer(typeof(ADayAtTheRacesConfiguration));
                    var configuration = serializer.ReadObject(xmlReader) as ADayAtTheRacesConfiguration;

                    Horses = new Collection<Horse>(configuration.Horses);
                    Races = new Collection<Race>(configuration.Races);
                }
            
        }
    }
}
