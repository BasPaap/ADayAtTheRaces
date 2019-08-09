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
        private readonly DataContractSerializerSettings serializerSettings = new DataContractSerializerSettings()
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
                Name = "Name",
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

            var princess = new Horse
            {
                Name = "Princess",
                ReactionSpeed = 0.1f,
                Reliability = 1.0f
            };

            princess.RunningPhases.Add(new RunningPhase(15.5, 0.7f));
            princess.RunningPhases.Add(new RunningPhase(40.0, 0.6f));
            princess.RunningPhases.Add(new RunningPhase(300, 1.0f));


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


            Horses.Add(straightward);
            Horses.Add(sirtThackery);
            Horses.Add(royalArch3);
            Horses.Add(uncleOrange);
            Horses.Add(peggiesPride);
            Horses.Add(guidingLight);

            Horses.Add(bigWonder);
            Horses.Add(secretGrace);
            Horses.Add(soldiersJoy);
            Horses.Add(theGosling);
            Horses.Add(balderdash);
            Horses.Add(trumpCard);

            Horses.Add(princess);
            Horses.Add(fitzgerald);
            Horses.Add(unityAsset);
            Horses.Add(bananabread);
            Horses.Add(pinkiePie);
            Horses.Add(littleJacob);


            var tenSecondsFromNow = DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(10));
            var firstRace = new Race(tenSecondsFromNow.Hours, tenSecondsFromNow.Minutes, tenSecondsFromNow.Seconds);
            firstRace.Horses.Add(straightward);
            firstRace.Horses.Add(sirtThackery);
            firstRace.Horses.Add(royalArch3);
            firstRace.Horses.Add(uncleOrange);
            firstRace.Horses.Add(peggiesPride);
            firstRace.Horses.Add(guidingLight);
            firstRace.HorseSpeeds[straightward] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[sirtThackery] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[royalArch3] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[uncleOrange] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[peggiesPride] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[guidingLight] = (1.0f, 1.0f);
            firstRace.JockeyColors[straightward] = redColor;
            firstRace.JockeyColors[sirtThackery] = greenColor;
            firstRace.JockeyColors[royalArch3] = blueColor;
            firstRace.JockeyColors[uncleOrange] = orangeColor;
            firstRace.JockeyColors[peggiesPride] = purpleColor;
            firstRace.JockeyColors[guidingLight] = mintColor;

            var twoMinutesFromNow = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(2));
            var secondRace = new Race(twoMinutesFromNow.Hours, twoMinutesFromNow.Minutes, twoMinutesFromNow.Seconds);
            secondRace.Horses.Add(bigWonder);
            secondRace.Horses.Add(secretGrace);
            secondRace.Horses.Add(soldiersJoy);
            secondRace.Horses.Add(theGosling);
            secondRace.Horses.Add(balderdash);
            secondRace.Horses.Add(trumpCard);
            secondRace.HorseSpeeds[bigWonder] = (1.0f, 1.0f);
            secondRace.HorseSpeeds[secretGrace] = (1.0f, 1.0f);
            secondRace.HorseSpeeds[soldiersJoy] = (1.0f, 1.0f);
            secondRace.HorseSpeeds[theGosling] = (1.0f, 1.0f);
            secondRace.HorseSpeeds[balderdash] = (1.0f, 1.0f);
            secondRace.HorseSpeeds[trumpCard] = (1.0f, 1.0f);
            secondRace.JockeyColors[bigWonder] = redColor;
            secondRace.JockeyColors[secretGrace] = greenColor;
            secondRace.JockeyColors[soldiersJoy] = blueColor;
            secondRace.JockeyColors[theGosling] = orangeColor;
            secondRace.JockeyColors[balderdash] = purpleColor;
            secondRace.JockeyColors[trumpCard] = mintColor;

            var fiveMinutesFromNow = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(5));
            var thirdRace = new Race(fiveMinutesFromNow.Hours, fiveMinutesFromNow.Minutes, fiveMinutesFromNow.Seconds);
            thirdRace.Horses.Add(princess);
            thirdRace.Horses.Add(fitzgerald);
            thirdRace.Horses.Add(unityAsset);
            thirdRace.Horses.Add(bananabread);
            thirdRace.Horses.Add(pinkiePie);
            thirdRace.Horses.Add(littleJacob);
            thirdRace.HorseSpeeds[princess] = (1.0f, 1.0f);
            thirdRace.HorseSpeeds[fitzgerald] = (1.0f, 1.0f);
            thirdRace.HorseSpeeds[unityAsset] = (1.0f, 1.0f);
            thirdRace.HorseSpeeds[bananabread] = (1.0f, 1.0f);
            thirdRace.HorseSpeeds[pinkiePie] = (1.0f, 1.0f);
            thirdRace.HorseSpeeds[littleJacob] = (1.0f, 1.0f);
            thirdRace.JockeyColors[princess] = redColor;
            thirdRace.JockeyColors[fitzgerald] = greenColor;
            thirdRace.JockeyColors[unityAsset] = blueColor;
            thirdRace.JockeyColors[bananabread] = orangeColor;
            thirdRace.JockeyColors[pinkiePie] = purpleColor;
            thirdRace.JockeyColors[littleJacob] = mintColor;

            Races.Add(firstRace);
            Races.Add(secondRace);
            Races.Add(thirdRace);
        }

        public void OldPopulate()
        {
            var rainbowDash = new Horse
            {
                Name = "Rainbow Dash",                
                ReactionSpeed = 0.2f,
                Reliability = 1.0f
            };

            rainbowDash.RunningPhases.Add(new RunningPhase(10.0, 0.5f));
            rainbowDash.RunningPhases.Add(new RunningPhase(30.0, 0.8f));
            rainbowDash.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var pinkiePie = new Horse
            {
                Name = "Pinkie Pie",
                ReactionSpeed = 1.0f,
                Reliability = 0.2f
            };

            pinkiePie.RunningPhases.Add(new RunningPhase(10.5, 1.0f));
            pinkiePie.RunningPhases.Add(new RunningPhase(20.0, 0.7f));
            pinkiePie.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var applejack = new Horse
            {
                Name = "Applejack",
                ReactionSpeed = 0.5f,
                Reliability = 0.5f
            };

            applejack.RunningPhases.Add(new RunningPhase(8.0, 0.7f));
            applejack.RunningPhases.Add(new RunningPhase(25.0, 0.85f));
            applejack.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var twilightSparkle = new Horse
            {
                Name = "Twilight Sparkle",
                ReactionSpeed = 0.5f,
                Reliability = 1.0f

            };

            twilightSparkle.RunningPhases.Add(new RunningPhase(10.0, 0.75f));
            twilightSparkle.RunningPhases.Add(new RunningPhase(15.0, 0.8f));
            twilightSparkle.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var fluttershy = new Horse
            {
                Name = "Fluttershy",
                ReactionSpeed = 0.1f,
                Reliability = 0.8f
            };

            fluttershy.RunningPhases.Add(new RunningPhase(8.0, 0.65f));
            fluttershy.RunningPhases.Add(new RunningPhase(10.0, 0.87f));
            fluttershy.RunningPhases.Add(new RunningPhase(300, 1.0f));

            var rarity = new Horse
            {
                Name = "Rarity",
                ReactionSpeed = 0.7f,
                Reliability = 0.6f
            };

            rarity.RunningPhases.Add(new RunningPhase(7.5, 0.7f));
            rarity.RunningPhases.Add(new RunningPhase(20.0, 0.8f));
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
            firstRace.HorseSpeeds[rainbowDash] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[pinkiePie] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[applejack] = (0.5f, 1.0f);
            firstRace.HorseSpeeds[twilightSparkle] = (1.0f, 1.5f);
            firstRace.HorseSpeeds[fluttershy] = (1.0f, 1.0f);
            firstRace.HorseSpeeds[rarity] = (1.0f, 1.0f);
            firstRace.JockeyColors[rainbowDash] = new Color(0x82, 0xC2, 0xE4);
            firstRace.JockeyColors[pinkiePie] = new Color(0xF6, 0xB8, 0xD5);
            firstRace.JockeyColors[applejack] = new Color(0xFC, 0xB7, 0x62);
            firstRace.JockeyColors[twilightSparkle] = new Color(0xD8, 0x67, 0xB4);
            firstRace.JockeyColors[fluttershy] = new Color(0xFD, 0xF5, 0xAA);
            firstRace.JockeyColors[rarity] = new Color(0xEA, 0xED, 0xF1);
            
            var fiveMinutesFromNow = tenSecondsFromNow.Add(TimeSpan.FromMinutes(5));
            var secondRace = new Race(fiveMinutesFromNow.Hours, fiveMinutesFromNow.Minutes, fiveMinutesFromNow.Seconds);
            secondRace.Horses.Add(rainbowDash);
            secondRace.Horses.Add(pinkiePie);
            secondRace.Horses.Add(applejack);
            secondRace.Horses.Add(twilightSparkle);
            secondRace.Horses.Add(fluttershy);
            secondRace.Horses.Add(rarity);
            secondRace.HorseSpeeds[rainbowDash] = (0.5f, 0.5f);
            secondRace.HorseSpeeds[pinkiePie] = (0.5f, 0.5f);
            secondRace.HorseSpeeds[applejack] = (0.5f, 1.0f);
            secondRace.HorseSpeeds[twilightSparkle] = (0.5f, 0.5f);
            secondRace.HorseSpeeds[fluttershy] = (0.5f, 0.5f);
            secondRace.HorseSpeeds[rarity] = (0.5f, 0.5f);
            secondRace.JockeyColors[rainbowDash] = new Color(0x82, 0xC2, 0xE4);
            secondRace.JockeyColors[pinkiePie] = new Color(0xF6, 0xB8, 0xD5);
            secondRace.JockeyColors[applejack] = new Color(0xFC, 0xB7, 0x62);
            secondRace.JockeyColors[twilightSparkle] = new Color(0xD8, 0x67, 0xB4);
            secondRace.JockeyColors[fluttershy] = new Color(0xFD, 0xF5, 0xAA);
            secondRace.JockeyColors[rarity] = new Color(0xEA, 0xED, 0xF1);

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
