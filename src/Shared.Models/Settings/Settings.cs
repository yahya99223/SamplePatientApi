namespace Shared.Models.Settings
{
    public class Settings
    {
        public Settings()
        {
            DataAccessSettings = new DataAccessSettings();
        }
        public DataAccessSettings DataAccessSettings { get; set; }
    }
}
