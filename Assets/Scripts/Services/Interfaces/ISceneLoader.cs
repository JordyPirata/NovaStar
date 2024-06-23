public interface ISceneLoader {
    public const string MainMenu = "MainM";
    public const string Game = "Game";
    void LoadScene(string sceneName);
}