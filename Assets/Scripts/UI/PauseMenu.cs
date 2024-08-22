using Services;
using Services.Interfaces;
using UnityEngine;

namespace UI
{
public class PauseMenu : MonoBehaviour
{
    ISceneLoader sceneLoader;
    public void Awake()
    {
        sceneLoader = ServiceLocator.GetService<ISceneLoader>();
    }
    public void OnEnable()
    {
        Time.timeScale = 0;
    }
    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1;
        sceneLoader.LoadScene(ISceneLoader.MainMenu);
    }
    public void OnExitButtonClicked()
    {
        // Quit the game
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
}