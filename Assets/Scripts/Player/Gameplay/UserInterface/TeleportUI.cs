using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Gameplay.UserInterface
{
    public class TeleportUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI teleportNameText;
        public Action<string> OnClick;
        public string TeleportName
        {
            get => teleportNameText.text;
            set => teleportNameText.text = value;
        }

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClick?.Invoke(TeleportName));
        }
    }
}