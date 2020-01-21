namespace Shared.Models.Settings
{
    public class Settings
    {
        public Settings()
        {
            DataAccessSettings = new DataAccessSettings();
            StorageSettings = new StorageSettings();
            AppSettings = new AppSettings();
        }
        public DataAccessSettings DataAccessSettings { get; set; }
        public StorageSettings StorageSettings { get; set; }
        public AppSettings AppSettings { get; set; }
    }
}
