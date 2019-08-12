using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Bas.ADayAtTheRaces.RaceResults
{
    [DataContract]
    public sealed class RaceResultsFile
    {
        private readonly DataContractSerializerSettings serializerSettings = new DataContractSerializerSettings()
        {
            KnownTypes = new[] { typeof(RaceResultsFile), typeof(RaceResult), typeof(Finish), typeof(Color) }
        };
        
        [DataMember]
        public Collection<RaceResult> RaceResults { get; private set; } = new Collection<RaceResult>();

        public void Save(string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

                using (var xmlWriter = new XmlTextWriter(filePath, Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;

                    var serializer = new DataContractSerializer(typeof(RaceResultsFile), serializerSettings);
                    serializer.WriteObject(xmlWriter, this);
                    xmlWriter.Close();
                }
            
        }

        public void Load(string filePath)
        {   
            try
            {
                using (var xmlReader = new XmlTextReader(filePath))
                {
                    var serializer = new DataContractSerializer(typeof(RaceResultsFile));
                    var newRaceResultsFile = serializer.ReadObject(xmlReader) as RaceResultsFile;

                    RaceResults = newRaceResultsFile.RaceResults;
                }
            }
            catch (FileNotFoundException)
            {
                // Do nothing, the file will be saved later, and RaceResults has already been initialized.
            }
            catch (DirectoryNotFoundException)
            {
                // Do nothing, the directory will be created when saved.
            }   
        }

        public void Populate()
        {
            var firstFinish = new Finish { HorseName = "Pinkie Pie", Position = 1, TotalTime = TimeSpan.FromSeconds(75.0), JockeyColor = new Color(1.0f, 0.0f, 0.0f) };
            var secondFinish = new Finish { HorseName = "Rainbow Dash", Position = 2, TotalTime = TimeSpan.FromSeconds(83.0), JockeyColor = new Color(0.0f, 1.0f, 0.0f) };
            var thirdFinish = new Finish { HorseName = "Applejack", Position = 3, TotalTime = TimeSpan.FromSeconds(90.0), JockeyColor = new Color(0.0f, 0.0f, 1.0f) };

            var firstRaceResult = new RaceResult { RaceTime = DateTime.Now.AddHours(-2.0) };
            firstRaceResult.Finishes.Add(firstFinish);
            firstRaceResult.Finishes.Add(secondFinish);
            firstRaceResult.Finishes.Add(thirdFinish);
            RaceResults.Add(firstRaceResult);

            var secondRaceResult = new RaceResult { RaceTime = DateTime.Now.AddHours(-1.0) };
            secondRaceResult.Finishes.Add(firstFinish);
            secondRaceResult.Finishes.Add(secondFinish);
            secondRaceResult.Finishes.Add(thirdFinish);
            RaceResults.Add(secondRaceResult);
        }
    }
}
