using InputSystem;
using Services;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
public class PauseMenu : MonoBehaviour
{
    IEventManager sceneLoader;
    InputActions inputActions;
    public GameObject pauseMenu;
    public void Awake()
    {
        sceneLoader = ServiceLocator.GetService<IEventManager>();
        inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
    }
    private void Start()
    {
        inputActions.Player.PauseMenu.performed += OnPaused;
    }

    private void OnDisable()
    {
        inputActions.Player.PauseMenu.performed -= OnPaused;
    }
    private void OnPaused(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
       
    }
    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1;
        sceneLoader.LoadScene(IEventManager.MainMenu);
    }
    public void OnExitButtonClicked()
    {
        // Quit the game
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
}