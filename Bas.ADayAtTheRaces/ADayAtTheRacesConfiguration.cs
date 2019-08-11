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
            KnownTypes = new [] { typeof(ADayAtTheRacesConfiguration), typeof(Horse), typeof(RunningPhase), typeof(Color), typeof(Race), typeof(Speeds)}
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
            
            

            var tenSecondsFromNow = new TimeSpan(14, 0, 0);//  DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(30));
            var firstRace = new Race(tenSecondsFromNow.Hours, tenSecondsFromNow.Minutes, tenSecondsFromNow.Seconds);
            firstRace.Horses.Add(bigWonder);
            firstRace.Horses.Add(fallGuy);
            firstRace.Horses.Add(soldiersJoy);
            firstRace.Horses.Add(theGosling);
            firstRace.Horses.Add(balderdash);
            firstRace.Horses.Add(trumpCard);
            firstRace.HorseSpeeds[bigWonder] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[fallGuy] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[soldiersJoy] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[theGosling] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[balderdash] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[trumpCard] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.JockeyColors[bigWonder] = redColor;
            firstRace.JockeyColors[fallGuy] = greenColor;
            firstRace.JockeyColors[soldiersJoy] = blueColor;
            firstRace.JockeyColors[theGosling] = orangeColor;
            firstRace.JockeyColors[balderdash] = purpleColor;
            firstRace.JockeyColors[trumpCard] = mintColor;

            var twoMinutesFromNow = new TimeSpan(16, 0, 0);// DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(5));
            var secondRace = new Race(twoMinutesFromNow.Hours, twoMinutesFromNow.Minutes, twoMinutesFromNow.Seconds);
            secondRace.Horses.Add(sirtThackery);
            secondRace.Horses.Add(fitzgerald);
            secondRace.Horses.Add(unityAsset);
            secondRace.Horses.Add(bananabread);
            secondRace.Horses.Add(pinkiePie);
            secondRace.Horses.Add(littleJacob);
            secondRace.HorseSpeeds[sirtThackery] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[fitzgerald] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[unityAsset] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[bananabread] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[pinkiePie] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[littleJacob] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.JockeyColors[sirtThackery] = redColor;
            secondRace.JockeyColors[fitzgerald] = greenColor;
            secondRace.JockeyColors[unityAsset] = blueColor;
            secondRace.JockeyColors[bananabread] = orangeColor;
            secondRace.JockeyColors[pinkiePie] = purpleColor;
            secondRace.JockeyColors[littleJacob] = mintColor;

            var fiveMinutesFromNow = new TimeSpan(17, 0, 0);// DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(10));
            var thirdRace = new Race(fiveMinutesFromNow.Hours, fiveMinutesFromNow.Minutes, fiveMinutesFromNow.Seconds);
            thirdRace.Horses.Add(straightward);
            thirdRace.Horses.Add(royalArch3);
            thirdRace.Horses.Add(uncleOrange);
            thirdRace.Horses.Add(peggiesPride);
            thirdRace.Horses.Add(guidingLight);
            thirdRace.Horses.Add(secretGrace);
            thirdRace.HorseSpeeds[straightward] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeeds[royalArch3] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeeds[uncleOrange] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeeds[peggiesPride] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeeds[guidingLight] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.HorseSpeeds[secretGrace] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            thirdRace.JockeyColors[straightward] = redColor;
            thirdRace.JockeyColors[royalArch3] = greenColor;
            thirdRace.JockeyColors[uncleOrange] = blueColor;
            thirdRace.JockeyColors[peggiesPride] = orangeColor;
            thirdRace.JockeyColors[guidingLight] = purpleColor;
            thirdRace.JockeyColors[secretGrace] = mintColor;

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

            var tenSecondsFromNow = new TimeSpan(15, 0, 0); // DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(10));
            var firstRace = new Race(tenSecondsFromNow.Hours, tenSecondsFromNow.Minutes, tenSecondsFromNow.Seconds);
            firstRace.Horses.Add(rainbowDash);
            firstRace.Horses.Add(pinkiePie);
            firstRace.Horses.Add(applejack);
            firstRace.Horses.Add(twilightSparkle);
            firstRace.Horses.Add(fluttershy);
            firstRace.Horses.Add(rarity);
            firstRace.HorseSpeeds[rainbowDash] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[pinkiePie] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[applejack] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[twilightSparkle] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[fluttershy] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.HorseSpeeds[rarity] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            firstRace.JockeyColors[rainbowDash] = new Color(0x82, 0xC2, 0xE4);
            firstRace.JockeyColors[pinkiePie] = new Color(0xF6, 0xB8, 0xD5);
            firstRace.JockeyColors[applejack] = new Color(0xFC, 0xB7, 0x62);
            firstRace.JockeyColors[twilightSparkle] = new Color(0xD8, 0x67, 0xB4);
            firstRace.JockeyColors[fluttershy] = new Color(0xFD, 0xF5, 0xAA);
            firstRace.JockeyColors[rarity] = new Color(0xEA, 0xED, 0xF1);
            
            var fiveMinutesFromNow = new TimeSpan(17, 0, 0);// tenSecondsFromNow.Add(TimeSpan.FromMinutes(5));
            var secondRace = new Race(fiveMinutesFromNow.Hours, fiveMinutesFromNow.Minutes, fiveMinutesFromNow.Seconds);
            secondRace.Horses.Add(rainbowDash);
            secondRace.Horses.Add(pinkiePie);
            secondRace.Horses.Add(applejack);
            secondRace.Horses.Add(twilightSparkle);
            secondRace.Horses.Add(fluttershy);
            secondRace.Horses.Add(rarity);
            secondRace.HorseSpeeds[rainbowDash] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[pinkiePie] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[applejack] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[twilightSparkle] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[fluttershy] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
            secondRace.HorseSpeeds[rarity] = new Speeds { FirstLapSpeed = 1.0f, SecondLapSpeed = 1.0f };
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
