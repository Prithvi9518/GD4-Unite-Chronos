using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue System/Dialogue SO")]
    public class DialogueSO : ScriptableObject
    {
        [SerializeField]
        private List<DialogueLine> lines;

        [SerializeField] private bool isQueued;
        [SerializeField] private bool overrideQueue;

        public List<DialogueLine> Lines => lines;
        public bool IsQueued => isQueued;
        public bool OverrideQueue => overrideQueue;
    }
}