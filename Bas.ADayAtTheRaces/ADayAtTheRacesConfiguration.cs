using System;
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
    [DataContract]
    public sealed class ADayAtTheRacesConfiguration
    {
        private DataContractSerializerSettings serializerSettings = new DataContractSerializerSettings()
        {
            PreserveObjectReferences = true,
            KnownTypes = new [] { typeof(ADayAtTheRacesConfiguration), typeof(Horse), typeof(RunningPhase), typeof(Color), typeof(Race) }
        };

        [DataMember]
        public Collection<Horse> Horses { get; private set; } = new Collection<Horse>();
        [DataMember]
        public Collection<Race> Races { get; private set; } = new Collection<Race>();

        public void Populate()
        {
            var rainbowDash = new Horse
            {
                Name = "Rainbow Dash",
                Color = new Color(0x82, 0xC2, 0xE4),
                ReactionSpeed = 0.2f,
                Reliability = 1.0f
            };

            rainbowDash.RunningPhases.Add(new RunningPhase(30.0, 0.2f));
            rainbowDash.RunningPhases.Add(new RunningPhase(60.0, 0.8f));
            rainbowDash.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var pinkiePie = new Horse
            {
                Name = "Pinkie Pie",
                Color = new Color(0xF6, 0xB8, 0xD5),
                ReactionSpeed = 1.0f,
                Reliability = 0.2f
            };

            pinkiePie.RunningPhases.Add(new RunningPhase(30.0, 0.2f));
            pinkiePie.RunningPhases.Add(new RunningPhase(60.0, 0.8f));
            pinkiePie.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var applejack = new Horse
            {
                Name = "Applejack",
                Color = new Color(0xFC, 0xB7, 0x62),
                ReactionSpeed = 0.5f,
                Reliability = 0.5f
            };

            applejack.RunningPhases.Add(new RunningPhase(30.0, 0.2f));
            applejack.RunningPhases.Add(new RunningPhase(60.0, 0.8f));
            applejack.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var twilightSparkle = new Horse
            {
                Name = "Twilight Sparkle",
                Color = new Color(0xD8, 0x67, 0xB4),
                ReactionSpeed = 0.5f,
                Reliability = 1.0f

            };

            twilightSparkle.RunningPhases.Add(new RunningPhase(30.0, 0.2f));
            twilightSparkle.RunningPhases.Add(new RunningPhase(60.0, 0.8f));
            twilightSparkle.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var fluttershy = new Horse
            {
                Name = "Fluttershy",
                Color = new Color(0xFD, 0xF5, 0xAA),
                ReactionSpeed = 0.1f,
                Reliability = 0.8f
            };

            fluttershy.RunningPhases.Add(new RunningPhase(30.0, 0.2f));
            fluttershy.RunningPhases.Add(new RunningPhase(60.0, 0.8f));
            fluttershy.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var rarity = new Horse
            {
                Name = "Rarity",
                Color = new Color(0xEA, 0xED, 0xF1),
                ReactionSpeed = 0.7f,
                Reliability = 0.6f
            };

            rarity.RunningPhases.Add(new RunningPhase(30.0, 0.2f));
            rarity.RunningPhases.Add(new RunningPhase(60.0, 0.8f));
            rarity.RunningPhases.Add(new RunningPhase(300, 1.0f));

            Horses.Add(rainbowDash);
            Horses.Add(pinkiePie);
            Horses.Add(applejack);
            Horses.Add(twilightSparkle);
            Horses.Add(fluttershy);
            Horses.Add(rarity);

            var tenSecondsFromNow = DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(10));
            var firstRace = new Race(tenSecondsFromNow.Hours, tenSecondsFromNow.Minutes, tenSecondsFromNow.Seconds);
            firstRace.Horses.Add(rainbowDash);
            firstRace.Horses.Add(pinkiePie);
            firstRace.Horses.Add(applejack);
            firstRace.Horses.Add(twilightSparkle);
            firstRace.Horses.Add(fluttershy);
            firstRace.Horses.Add(rarity);

            var fiveMinutesFromNow = tenSecondsFromNow.Add(TimeSpan.FromMinutes(5));
            var secondRace = new Race(fiveMinutesFromNow.Hours, fiveMinutesFromNow.Minutes, fiveMinutesFromNow.Seconds);
            secondRace.Horses.Add(rainbowDash);
            secondRace.Horses.Add(pinkiePie);
            secondRace.Horses.Add(applejack);
            secondRace.Horses.Add(twilightSparkle);
            secondRace.Horses.Add(fluttershy);
            secondRace.Horses.Add(rarity);

            Races.Add(firstRace);
            Races.Add(secondRace);
        }

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
