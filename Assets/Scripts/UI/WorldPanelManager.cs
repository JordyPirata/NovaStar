using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldPanelManager : MonoBehaviour
{
    public Scrollbar scrollbar;
    public RectTransform contentPanel;
    public VerticalLayoutGroup layout;
    public GameObject worldPrefab;
    public GameObject editPanel;
    public void Start()
    {
        LoadWorlds();
    }
    public void CreateWorld()
    {
        WorldPanel worldPanel = InstantiateWorldPanel();
        ResizeContent();
        worldPanel.CreateWorld();
        worldPanel.SaveWorld();
        // Add liseners to the buttons
        AddLiseners(worldPanel);
    }
    public void LoadWorlds()
    {
        string[] directories = Directory.GetDirectories(Application.persistentDataPath);
        foreach (string directory in directories)
        {
            WorldPanel worldPanel = InstantiateWorldPanel();
            worldPanel.LoadWorld(directory);
            worldPanel.SetWorld(worldPanel.game);
            // Add liseners to the buttons
            AddLiseners(worldPanel);
        }
        ResizeContent();
    }
    private void AddLiseners(WorldPanel worldPanel)
    {   
        worldPanel.editButton.onClick.AddListener(() => {
            editPanel.SetActive(true);
            gameObject.SetActive(false);
            }
        );
    }
    private WorldPanel InstantiateWorldPanel()
    {
        WorldPanel worldPanel = Instantiate(worldPrefab, contentPanel).GetComponent<WorldPanel>();  
        return worldPanel;
    }
    private void ResizeContent()
    {
        contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, layout.preferredHeight);
        // Scroll to the top
        scrollbar.value = 0;
    }
}
