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
        private int battleEnteredCount;

        private int exitRoomFailedAttempts;

        private int timeStopUses;

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
                { DialogueTrigger.BattleEnded , HandleBattleEnded },
                { DialogueTrigger.EnterIslandLevel , HandleEnterIsland},
                { DialogueTrigger.EnterBattleZone , HandleEnterBattleZone},
                { DialogueTrigger.ExitRoomNotYet , HandleExitRoomNotYet},
                { DialogueTrigger.TimeStopTutorial , HandleTimeStopTutorial},
                { DialogueTrigger.UseTimeStop , HandleUseTimeStop}
            };
        }
        
        private void HandleBattleEnded(List<DialogueSO> dialogues)
        {
            if (dialogues.Count == 0) return;
            
            if (battleEndedCount == 0)
            {
                DialogueManager.Instance.PlayDialogue(dialogues[0]);
                battleEndedCount++;
            }
        }

        private void HandleEnterIsland(List<DialogueSO> dialogues)
        {
            if (dialogues.Count == 0) return;
            
            DialogueManager.Instance.PlayDialogue(dialogues[0]);
        }

        private void HandleEnterBattleZone(List<DialogueSO> dialogues)
        {
            if(dialogues.Count == 0) return;

            if (battleEnteredCount != 0) return;
            DialogueManager.Instance.PlayDialogue(dialogues[0]);
            battleEnteredCount++;
        }

        private void HandleExitRoomNotYet(List<DialogueSO> dialogues)
        {
            if(exitRoomFailedAttempts <= 0)
                DialogueManager.Instance.PlayDialogue(dialogues[0]);
            else
                DialogueManager.Instance.PlayDialogue(dialogues[1]);

            exitRoomFailedAttempts++;
        }

        private void HandleTimeStopTutorial(List<DialogueSO> dialogues)
        {
            DialogueManager.Instance.PlayDialogue(dialogues[0]);
        }

        private void HandleUseTimeStop(List<DialogueSO> dialogues)
        {
            if (timeStopUses > 0) return;
            timeStopUses++;
            
            DialogueManager.Instance.PlayDialogue(dialogues[0]);
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

        public void OnEnterIsland()
        {
            OnNotify(DialogueTrigger.EnterIslandLevel);
        }

        public void OnEnterBattleZone()
        {
            OnNotify(DialogueTrigger.EnterBattleZone);
        }

        public void OnExitRoomNotYet()
        {
            OnNotify(DialogueTrigger.ExitRoomNotYet);
        }

        public void OnStartTimeStopTutorial()
        {
            OnNotify(DialogueTrigger.TimeStopTutorial);
        }
        
        public void OnUseTimeStop()
        {
            OnNotify(DialogueTrigger.UseTimeStop);
        }
    }
}