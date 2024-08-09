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
        private IWorldData worldDataService;
        public void Awake()
        {
            worldDataService = ServiceLocator.GetService<IWorldData>();
        }
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
            worldPanel.CreateWorld();
            worldPanel.SaveWorld();
            // Add liseners to the buttons
            OnEditClick(worldPanel);
        }
        public void LoadWorlds()
        {
            string[] directories = Directory.GetDirectories(Application.persistentDataPath);
            foreach (string directory in directories)
            {
                WorldPanel worldPanel = InstantiateWorldPanel();
                worldPanel.ReadWorld(directory);
                // Add liseners to the buttons
                OnEditClick(worldPanel);
            }
            StartCoroutine(ScrollToTop());
        }
        private void OnEditClick(WorldPanel worldPanel)
        {   
            worldPanel.editButton.onClick.AddListener(() => {
                    worldDataService.SetWorld(worldPanel.game);
                    editPanel.SetActive(true);
                    gameObject.SetActive(false);
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
