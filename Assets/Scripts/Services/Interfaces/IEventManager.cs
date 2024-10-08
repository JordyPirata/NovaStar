namespace Services.Interfaces
{
    public interface IEventManager {
        public const string MainMenu = "MainMenu";
        public const string Game = "Game";
        void LoadScene(string sceneName);
    }
}