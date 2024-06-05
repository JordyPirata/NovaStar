using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour
{
    public Scrollbar scrollbar;
    public RectTransform contentPanel;
    public VerticalLayoutGroup layout;
    private List<WorldPanel> worlds = new();
    public GameObject worldPrefab;
    public void Update()
    {
        // Resize the panel
        contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, layout.preferredHeight);
    }
    public void CreateWorld()
    {
        GameObject world = Instantiate(worldPrefab, contentPanel);
        WorldPanel worldPanel = world.GetComponent<WorldPanel>();
        worlds.Add(worldPanel);
        scrollbar.value = 1;
    }
}
