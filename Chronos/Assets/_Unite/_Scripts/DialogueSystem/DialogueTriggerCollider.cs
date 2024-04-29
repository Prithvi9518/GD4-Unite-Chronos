using UnityEngine;

namespace Unite.DialogueSystem
{
    /// <summary>
    /// Used to play dialogue when the player enters a trigger collider.
    /// </summary>
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