using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour
{
    public Scrollbar scrollbar;
    public RectTransform contentPanel;
    public VerticalLayoutGroup layout;
    public List<WorldPanel> worlds = new();
    public GameObject worldPrefab;
    public void Awake()
    {
        LoadWorlds();
    }
    public void Update()
    {
        // Resize the panel
        contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, layout.preferredHeight);
    }
    public void CreateWorld()
    {
        GameObject world = Instantiate(worldPrefab, contentPanel);
        WorldPanel worldPanel = world.GetComponent<WorldPanel>();
        worldPanel.CreateWorld(null);
        worldPanel.SaveWorld();
        worlds.Add(worldPanel);
        scrollbar.value = 1;
    }
    public void LoadWorlds()
    {
        string[] directories = Directory.GetDirectories(Application.persistentDataPath);
        foreach (string directory in directories)
        {
            WorldPanel world = Instantiate(worldPrefab, contentPanel).GetComponent<WorldPanel>();
            world.LoadWorld(directory);
            world.CreateWorld(world.game);
            worlds.Add(world);
        }
    }
}
