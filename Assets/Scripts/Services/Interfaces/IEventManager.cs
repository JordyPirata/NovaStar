namespace Services.Interfaces
{
    public interface IEventManager {
        public const string MainMenu = "MainMenu";
        public const string Game = "Game";
        public const string DemoScene = "DemoScene";    
        void LoadScene(string sceneName);
    }
}