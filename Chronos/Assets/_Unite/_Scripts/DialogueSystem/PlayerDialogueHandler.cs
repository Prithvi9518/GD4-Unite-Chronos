using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
    public class PlayerDialogueHandler : MonoBehaviour
    {
        [SerializeField]
        private DialogueTriggerMappingSO[] dialogueTriggerMappings;

        private Dictionary<DialogueTrigger, List<DialogueSO>> dialogueMap;
        private Dictionary<DialogueTrigger, System.Action<List<DialogueSO>>> dialogueTriggerHandlers;

        private int battleEndedCount;

        private void Awake()
        {
            SetupDialogueMap();
            SetupDialogueTriggerHandlers();
        }

        private void SetupDialogueMap()
        {
            dialogueMap = new();
            foreach (var mapping in dialogueTriggerMappings)
            {
                dialogueMap.Add(mapping.DialogueTrigger, mapping.Dialogues);
            }
        }

        private void SetupDialogueTriggerHandlers()
        {
            dialogueTriggerHandlers = new Dictionary<DialogueTrigger, System.Action<List<DialogueSO>>>()
            {
                { DialogueTrigger.Test1, HandleTest1 },
                { DialogueTrigger.Test2, HandleTest2 },
                { DialogueTrigger.BattleEnded , HandleBattleEnded }
            };
        }
        
        private void HandleTest1(List<DialogueSO> dialogues)
        {
            Debug.Log($"test1 - {dialogues[0].Lines[0].Text}");
        }

        private void HandleTest2(List<DialogueSO> dialogues)
        {
            Debug.Log($"test2 - {dialogues[0].Lines[0].Text}");
        }

        private void HandleBattleEnded(List<DialogueSO> dialogues)
        {
            if (battleEndedCount == 0)
            {
                DialogueManager.Instance.PlayDialogue(dialogues[0]);
                battleEndedCount++;
            }
        }

        public void OnNotify(DialogueTrigger dialogueTrigger)
        {
            if (!dialogueTriggerHandlers.ContainsKey(dialogueTrigger)) return;
            
            if (dialogueMap.TryGetValue(dialogueTrigger, out List<DialogueSO> dialogues))
            {
                dialogueTriggerHandlers[dialogueTrigger](dialogues);
            }
        }

        public void OnBattleEnd()
        {
            OnNotify(DialogueTrigger.BattleEnded);
        }
    }
}