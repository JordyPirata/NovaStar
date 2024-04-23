using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour
{
    public VerticalLayoutGroup layout;

    public GameObject worldPanel;
    
    public void CreateWorld()
    {
        // Create a new world
        GameObject newWorld = Instantiate(worldPanel, layout.transform);
        // Set the new world to be the second last child of the layout
        newWorld.transform.SetSiblingIndex(layout.transform.childCount - 2);
    }
}
