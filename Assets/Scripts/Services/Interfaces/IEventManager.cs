namespace Services.Interfaces
{
    public interface IEventManager {
        public const string MainMenu = "MainMenu";
        public const string Game = "Game";
        public const string Game1 = "Game 1";    
        void LoadScene(string sceneName);
    }
}