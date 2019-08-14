using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Bas.ADayAtTheRaces.RaceResults
{
    /// <summary>
    /// Storage for the results of multiple races.
    /// </summary>
    [DataContract]
    public sealed class RaceResultsFile
    {
        private readonly DataContractSerializerSettings serializerSettings = new DataContractSerializerSettings()
        {
            KnownTypes = new[] { typeof(RaceResultsFile), typeof(RaceResult), typeof(Finish), typeof(Color) }
        };
        
        [DataMember]
        public Collection<RaceResult> RaceResults { get; private set; } = new Collection<RaceResult>();

        /// <summary>
        /// Saves the race results to a file provided in <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath">The path of the file to save the raceresults to.</param>
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

        /// <summary>
        /// Loads raceresults from the file provided in <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The path of the file to load the raceresults from.</param>
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

        /// <summary>
        /// Populates the data structure with test data.
        /// </summary>
        public void Populate()
        {
            var firstFinish = new Finish { HorseName = "Pinkie Pie", Position = 1, TotalTime = TimeSpan.FromSeconds(75.0), JockeyColor = new Color(255, 0, 0) };
            var secondFinish = new Finish { HorseName = "Rainbow Dash", Position = 2, TotalTime = TimeSpan.FromSeconds(83.0), JockeyColor = new Color(0, 255, 0) };
            var thirdFinish = new Finish { HorseName = "Applejack", Position = 3, TotalTime = TimeSpan.FromSeconds(90.0), JockeyColor = new Color(0, 0, 255) };

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
