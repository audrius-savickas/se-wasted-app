using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Persistence.File
{
    public sealed class DBConfiguration
    {
        private static readonly Lazy<DBConfiguration> _instance
            = new Lazy<DBConfiguration>(() => new DBConfiguration());

        public string InitialPathname { get; set; }
        public string PathToDataDirectory { get; set; }
        public string PathToFoodsFile { get; set; }
        public string PathToRestaurantsFile { get; set; }
        public string PathToTypesOfFoodFile { get; set; }

        public static DBConfiguration Instance => _instance.Value;

        private DBConfiguration()
        {
            Initialize();
        }

        private void Initialize()
        {
            UpdateInitialPathname();

            string configurationFilePathname = GetNameConfigurationFile();

            using (StreamReader r = new StreamReader(configurationFilePathname))
            {
                string jsonAsString = r.ReadToEnd();

                using (JsonDocument document = JsonDocument.Parse(jsonAsString))
                {
                    List<string> pathnameToDataDirectory = GetPathnameToDataDirectory(document);

                    PathToDataDirectory = Path.Combine(pathnameToDataDirectory.ToArray());

                    // Get the path to every data file
                    UpdatePathsToDataFiles(document);
                }
            }
        }

        private void UpdateInitialPathname()
        {
            string assemblyDirectory = Directory.GetCurrentDirectory();

            int backendPosition = assemblyDirectory.IndexOf("backend", 0);
            string baseDirectory = assemblyDirectory.Substring(0, backendPosition + "backend".Length);
            InitialPathname = Path.Combine(baseDirectory, "Persistence.File");
        }

        private void UpdatePathsToDataFiles(JsonDocument document)
        {
            string suffix = document.RootElement.GetProperty("suffix").GetString();
            string foodsFile = document.RootElement.GetProperty("foods").GetString() + suffix;
            string restaurantsFile = document.RootElement.GetProperty("restaurants").GetString() + suffix;
            string typesOfFoodFile = document.RootElement.GetProperty("typesOfFood").GetString() + suffix;

            PathToFoodsFile = Path.Combine(PathToDataDirectory, foodsFile);
            PathToRestaurantsFile = Path.Combine(PathToDataDirectory, restaurantsFile);
            PathToTypesOfFoodFile = Path.Combine(PathToDataDirectory, typesOfFoodFile);
        }

        private string GetNameConfigurationFile()
        {
            string configurationFileName = Path.Combine("DB", "Configuration", "DBAsFile.json");
            string configurationFilePathname = Path.Combine(InitialPathname, configurationFileName);

            return configurationFilePathname;
        }

        private List<string> GetPathnameToDataDirectory(JsonDocument document)
        {
            JsonElement root = document.RootElement;
            JsonElement baseDirectory = root.GetProperty("baseDirectory");
            JsonElement.ArrayEnumerator toDataDirectoryIterator = baseDirectory.EnumerateArray();
            List<string> pathnameToDataDirectory = new List<string>
            {
                InitialPathname
            };

            while (toDataDirectoryIterator.MoveNext())
            {
                JsonElement to = toDataDirectoryIterator.Current;
                pathnameToDataDirectory.Add(to.GetString());
            }

            return pathnameToDataDirectory;
        }

    }
}
