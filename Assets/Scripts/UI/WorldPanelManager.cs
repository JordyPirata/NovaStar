using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Services.Interfaces;
using Services;

namespace UI
{
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
            StartCoroutine(ScrollToTop());
        }
        public void Update()
        {
            ResizeContent();
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
            StartCoroutine(ScrollToTop());
        }
        private void AddLiseners(WorldPanel worldPanel)
        {   
            worldPanel.editButton.onClick.AddListener(() => {
                    editPanel.SetActive(true);
                    gameObject.SetActive(false);
                    ServiceLocator.GetService<IWorldData>().SetWorld(worldPanel.game);
                }
            );
        }
        private WorldPanel InstantiateWorldPanel()
        {
            WorldPanel worldPanel = Instantiate(worldPrefab, contentPanel).GetComponent<WorldPanel>();
            StartCoroutine(ScrollToTop());
            return worldPanel;
        }
        private IEnumerator ScrollToTop()
        {
            yield return new WaitForSeconds(0.03f);
            scrollbar.value = 0;
        }
        private void ResizeContent()
        {
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, layout.preferredHeight);
        }
    }
}
