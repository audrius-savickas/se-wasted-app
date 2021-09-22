using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace console_wasted_app.Model.Data
{
    sealed class DBConfiguration
    {
        private static readonly Lazy<DBConfiguration> _instance
            = new Lazy<DBConfiguration>(() => new DBConfiguration());

        public string InitialPathname { get; set; }
        public string PathToDataDirectory { get; set; }
        public string PathToFoodsFile { get; set; }
        public string PathToRestaurantsFile { get; set; }
        public string PathToTypesOfFoodFile { get; set; }

        public static DBConfiguration Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private DBConfiguration()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.UpdateInitialPathname();

            string configurationFilePathname = this.GetNameConfigurationFile();
            using (StreamReader r = new StreamReader(configurationFilePathname))
            {
                string jsonAsString = r.ReadToEnd();

                using (JsonDocument document = JsonDocument.Parse(jsonAsString))
                {
                    List<string> pathnameToDataDirectory = this.GetPathnameToDataDirectory(document);

                    this.PathToDataDirectory = Path.Combine((string[])pathnameToDataDirectory.ToArray());

                    // Get the path to every data file
                    this.UpdatePathsToDataFiles(document);
                }
            }
        }

        private void UpdateInitialPathname()
        {
            string assemblyDirectory = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo initialDirectory = Directory
                .GetParent(assemblyDirectory)
                .Parent
                .Parent;
            this.InitialPathname = initialDirectory.FullName;
        }

        private void UpdatePathsToDataFiles( JsonDocument document )
        {
            string suffix = document.RootElement.GetProperty("suffix").GetString();
            string foodsFile = document.RootElement.GetProperty("foods").GetString() + suffix;
            string restaurantsFile = document.RootElement.GetProperty("restaurants").GetString() + suffix;
            string typesOfFoodFile = document.RootElement.GetProperty("typesOfFood").GetString() + suffix;

            this.PathToFoodsFile = Path.Combine(this.PathToDataDirectory, foodsFile);
            this.PathToRestaurantsFile = Path.Combine(this.PathToDataDirectory, restaurantsFile);
            this.PathToTypesOfFoodFile = Path.Combine(this.PathToDataDirectory, typesOfFoodFile);
        }

        private string GetNameConfigurationFile()
        {
            string configurationFileName = Path.Combine("DB", "Configuration", "DBAsFile.json");
            string configurationFilePathname = Path.Combine(this.InitialPathname, configurationFileName);

            return configurationFilePathname;
        }

        private List<string> GetPathnameToDataDirectory(JsonDocument document)
        {
            JsonElement root = document.RootElement;
            JsonElement baseDirectory = root.GetProperty("baseDirectory");
            JsonElement.ArrayEnumerator toDataDirectoryIterator = baseDirectory.EnumerateArray();
            List<string> pathnameToDataDirectory = new List<string>();
            pathnameToDataDirectory.Add(this.InitialPathname);

            while (toDataDirectoryIterator.MoveNext())
            {
                JsonElement to = toDataDirectoryIterator.Current;
                pathnameToDataDirectory.Add(to.GetString());
            }

            return pathnameToDataDirectory;
        }

    }
}
