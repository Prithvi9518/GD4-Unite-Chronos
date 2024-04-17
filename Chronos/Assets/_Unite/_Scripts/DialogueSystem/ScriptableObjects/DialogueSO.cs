﻿using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
    /// <summary>
    /// Stores all dialogue lines that make up a single dialogue sequence.
    /// <seealso cref="DialogueLine"/>
    /// </summary>
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