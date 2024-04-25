using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour
{
    public Scrollbar scrollbar;
    public RectTransform contentPanel;
    public VerticalLayoutGroup layout;
    private List<GameObject> worlds = new();
    public GameObject worldPrefab;
    public void Awake()
    {
        // Create a new world when the game starts
        for (int i = 0; i < 5; i++)
        {
            CreateWorld();
        }
        // Resize the panel
        StartCoroutine(ResizePanel());
        // Scroll to the bottom of the panel
        StartCoroutine(ScrollValue());
    }

    IEnumerator ScrollValue()
    {
        yield return new WaitForSeconds(0.25f);
        scrollbar.value = 1;
    }
    IEnumerator ResizePanel()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            // Resize the panel to fit the content
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, layout.preferredHeight);
        }
    }

    public void CreateWorld()
    {
        // Create a new world
        GameObject world = Instantiate(worldPrefab, contentPanel);
        worlds.Add(world);
    }
}
