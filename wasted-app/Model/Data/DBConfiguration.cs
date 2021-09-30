using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace console_wasted_app.Model.Data
{
    internal sealed class DBConfiguration
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

            var configurationFilePathname = GetNameConfigurationFile();
            using (var r = new StreamReader(configurationFilePathname))
            {
                var jsonAsString = r.ReadToEnd();

                using (var document = JsonDocument.Parse(jsonAsString))
                {
                    var pathnameToDataDirectory = GetPathnameToDataDirectory(document);

                    PathToDataDirectory = Path.Combine(pathnameToDataDirectory.ToArray());

                    // Get the path to every data file
                    UpdatePathsToDataFiles(document);
                }
            }
        }

        private void UpdateInitialPathname()
        {
            var assemblyDirectory = System.IO.Directory.GetCurrentDirectory();
            var initialDirectory = Directory
                .GetParent(assemblyDirectory)
                .Parent
                .Parent;
            InitialPathname = initialDirectory.FullName;
        }

        private void UpdatePathsToDataFiles(JsonDocument document)
        {
            var suffix = document.RootElement.GetProperty("suffix").GetString();
            var foodsFile = document.RootElement.GetProperty("foods").GetString() + suffix;
            var restaurantsFile = document.RootElement.GetProperty("restaurants").GetString() + suffix;
            var typesOfFoodFile = document.RootElement.GetProperty("typesOfFood").GetString() + suffix;

            PathToFoodsFile = Path.Combine(PathToDataDirectory, foodsFile);
            PathToRestaurantsFile = Path.Combine(PathToDataDirectory, restaurantsFile);
            PathToTypesOfFoodFile = Path.Combine(PathToDataDirectory, typesOfFoodFile);
        }

        private string GetNameConfigurationFile()
        {
            var configurationFileName = Path.Combine("DB", "Configuration", "DBAsFile.json");
            var configurationFilePathname = Path.Combine(InitialPathname, configurationFileName);

            return configurationFilePathname;
        }

        private List<string> GetPathnameToDataDirectory(JsonDocument document)
        {
            var root = document.RootElement;
            var baseDirectory = root.GetProperty("baseDirectory");
            var toDataDirectoryIterator = baseDirectory.EnumerateArray();
            var pathnameToDataDirectory = new List<string>
            {
                InitialPathname
            };

            while (toDataDirectoryIterator.MoveNext())
            {
                var to = toDataDirectoryIterator.Current;
                pathnameToDataDirectory.Add(to.GetString());
            }

            return pathnameToDataDirectory;
        }

    }
}
