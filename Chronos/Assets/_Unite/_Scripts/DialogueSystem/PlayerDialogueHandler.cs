using System.Collections;
using System.Collections.Generic;
using Unite.Managers;
using UnityEngine;

namespace Unite.DialogueSystem
{
    /// <summary>
    /// Keeps track of specific conditions that must be met to play certain dialogues.
    ///
    /// Uses a dictionary mapping between DialogueTrigger enums and DialogueSO objects.
    /// Upon receiving an event with a DialogueTrigger enum value, it plays the appropriate dialogue.
    ///
    /// <seealso cref="DialogueTrigger"/>
    /// <seealso cref="DialogueTriggerMappingSO"/>
    /// </summary>
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

        private int islandBoundsReached;

        private int numTimesJournalOpened;

        private void Awake()
        {
            SetupDialogueMap();
            SetupDialogueTriggerHandlers();
        }

        private void OnEnable()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.OnResetPersistentValues += ResetTrackedVariables;
        }

        private void OnDisable()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.OnResetPersistentValues -= ResetTrackedVariables;
        }

        private void SetupDialogueMap()
        {
            dialogueMap = new();
            foreach (var mapping in dialogueTriggerMappings)
            {
                dialogueMap.Add(mapping.DialogueTrigger, mapping.Dialogues);
            }
        }

        private void ResetTrackedVariables()
        {
            battleEndedCount = 0;
            battleEnteredCount = 0;
            exitRoomFailedAttempts = 0;
            timeStopUses = 0;
            islandBoundsReached = 0;
            numTimesJournalOpened = 0;
        }

        /// <summary>
        /// Each DialogueTrigger value is mapped to an Action that is invoked when the handler is notified with the
        /// appropriate enum value.
        /// </summary>
        private void SetupDialogueTriggerHandlers()
        {
            dialogueTriggerHandlers = new Dictionary<DialogueTrigger, System.Action<List<DialogueSO>>>()
            {
                { DialogueTrigger.BattleEnded , HandleBattleEnded },
                { DialogueTrigger.EnterIslandLevel , HandleEnterIsland},
                { DialogueTrigger.EnterBattleZone , HandleEnterBattleZone},
                { DialogueTrigger.ExitRoomNotYet , HandleExitRoomNotYet},
                { DialogueTrigger.TimeStopTutorial , HandleTimeStopTutorial},
                { DialogueTrigger.UseTimeStop , HandleUseTimeStop},
                { DialogueTrigger.IslandBounds , HandleIslandBounds},
                { DialogueTrigger.OpenJournal , HandleOpenJournal}
            };
        }
        
        /// <summary>
        /// Plays a dialogue only for the first time a battle ends.
        /// </summary>
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

        /// <summary>
        /// Plays dialogue during the first time the player enters a battle zone.
        /// </summary>
        private void HandleEnterBattleZone(List<DialogueSO> dialogues)
        {
            if(dialogues.Count == 0) return;

            if (battleEnteredCount != 0) return;
            DialogueManager.Instance.PlayDialogue(dialogues[0]);
            battleEnteredCount++;
        }

        /// <summary>
        /// Tracks the number of failed attempts to exit the room level.
        /// Plays unique dialogues for the first 2 failed attempts.
        /// </summary>
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

        /// <summary>
        /// Plays dialogue after the player's first time using the time-stop ability.
        /// </summary>
        private void HandleUseTimeStop(List<DialogueSO> dialogues)
        {
            if (timeStopUses > 0) return;
            timeStopUses++;
            
            DialogueManager.Instance.PlayDialogue(dialogues[0]);
        }

        /// <summary>
        /// Tracks the number of times the player has tried to go outside the island's bounds.
        /// Plays different dialogues for the first 2 attempts at going out of bounds.
        /// </summary>
        private void HandleIslandBounds(List<DialogueSO> dialogues)
        {
            if (islandBoundsReached > 1) return;
            
            islandBoundsReached++;

            int index = 0;

            if (islandBoundsReached == 2)
                index = 1;
            
            DialogueManager.Instance.PlayDialogue(dialogues[index]);
        }

        /// <summary>
        /// Plays dialogue after the first time the journal has been opened.
        /// </summary>
        /// <param name="dialogues"></param>
        private void HandleOpenJournal(List<DialogueSO> dialogues)
        {
            if (numTimesJournalOpened > 0) return;
            StartCoroutine(FirstTimeJournalCoroutine(dialogues));
        }

        private IEnumerator FirstTimeJournalCoroutine(List<DialogueSO> dialogues)
        {
            yield return new WaitForSeconds(2.5f);
            numTimesJournalOpened++;
            DialogueManager.Instance.PlayDialogue(dialogues[0]);
        }

        /// <summary>
        /// Method called upon receiving a DialogueTrigger event.
        /// Gets the Action mapped to the received DialogueTrigger value, and
        /// invokes it.
        /// </summary>
        /// <param name="dialogueTrigger"></param>
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

        public void OnOpenJournal()
        {
            OnNotify(DialogueTrigger.OpenJournal);
        }
    }
}