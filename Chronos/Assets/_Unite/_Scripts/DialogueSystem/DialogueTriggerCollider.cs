using UnityEngine;

namespace Unite.DialogueSystem
{
    public class DialogueTriggerCollider : MonoBehaviour
    {
        [SerializeField]
        private DialogueSO dialogue;

        [SerializeField]
        private bool playOnlyOnce;

        private bool played;

        private void OnTriggerEnter(Collider other)
        {
            if (played) return;
            
            DialogueManager.Instance.PlayDialogue(dialogue);
            played = true;
        }
    }
}