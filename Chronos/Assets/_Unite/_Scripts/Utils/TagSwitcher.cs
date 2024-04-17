using UnityEngine;

namespace Unite.Utils
{
    public class TagSwitcher : MonoBehaviour
    {
        [SerializeField] private string startingTag;
        [SerializeField] private string tagToSwitch;

        private void Awake()
        {
            gameObject.tag = startingTag;
        }

        public void SwitchTag()
        {
            gameObject.tag = tagToSwitch;
        }
    }
}