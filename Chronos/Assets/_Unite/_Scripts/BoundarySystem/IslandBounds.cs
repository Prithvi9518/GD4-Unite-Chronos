using Unite.DialogueSystem;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.BoundSystem
{
    /// <summary>
    /// Used to trigger a dialogue when the player collides with the island bounds.
    /// </summary>
    public class IslandBounds : MonoBehaviour
    {
        [SerializeField]
        private DialogueTrigger dialogueTrigger;
        
        [SerializeField]
        private DialogueTriggerEvent onBoundsReached;
        
        [SerializeField]
        private float interval = 5f;

        private float timer;

        private void Update()
        {
            timer += Time.unscaledDeltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(!other.collider.TryGetComponent(out Player.Player p)) return;
            
            if (timer < interval) return;
            
            onBoundsReached.Raise(dialogueTrigger);
            timer = 0;
        }
    }
}