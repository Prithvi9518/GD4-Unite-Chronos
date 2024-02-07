using TMPro;
using Unite.Core.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class InteractTextUI : MonoBehaviour
    {
        [SerializeField]
        private Transform holder;

        [SerializeField]
        private Image interactPromptImage;

        [SerializeField]
        private TextMeshProUGUI interactText;

        [SerializeField]
        private Sprite keyboardPromptSprite;

        [SerializeField]
        private Sprite xboxPromptSprite;

        [SerializeField]
        private Sprite playStationPromptSprite;

        private void Awake()
        {
            holder.gameObject.SetActive(false);
        }
        
        public void ShowInteractText(string text)
        {
            holder.gameObject.SetActive(true);
            interactText.text = text;
        }

        public void HideInteractText()
        {
            holder.gameObject.SetActive(false);
        }

        public void SetKeyboardPromptSprite()
        {
            interactPromptImage.sprite = keyboardPromptSprite;
        }

        public void SetGamepadPromptSprite(GamepadType gamepadType)
        {
            switch (gamepadType)
            {
                case GamepadType.Xbox:
                    interactPromptImage.sprite = xboxPromptSprite;
                    break;
                case GamepadType.PlayStation:
                    interactPromptImage.sprite = playStationPromptSprite;
                    break;
                case GamepadType.Unknown:
                    interactPromptImage.sprite = keyboardPromptSprite;
                    break;
            }
        }
    }
}