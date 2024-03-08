using System.Collections.Generic;
using Unite.Enemies;
using Unite.EventSystem;
using Unite.InteractionSystem;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using UnityEngine.Analytics;
using Unite.DialogueSystem;
using Unite.Core.Game;

namespace Unite.Managers
{
    public class AnalyticsManager : MonoBehaviour
    {
        async void Start()
        {
            try
            {
                // Initialize Unity Services asynchronously
                await UnityServices.InitializeAsync();
                GiveConsent(); // Get user consent according to various legislations
            }
            catch (ConsentCheckException e)
            {
                Debug.Log(e.ToString());
            }
        }
        public void EnterNewRegion(string regionName)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Name", regionName);

            // Send analytics event
            SendAnalyticsEvent("EnteredNewRegion", data);
        }
        public void OnInteractWithInteractible(InteractibleObject interactible)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Name", interactible.DisplayName);

            // Send analytics event
            SendAnalyticsEvent("InteractWithInteractible", data);
        }

        public void OnInteractWithDialogueInteractible(DialogueSO dialogueInteractible)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Name", dialogueInteractible.name);

            // Send analytics event
            SendAnalyticsEvent("InteractWithDialogueInteractible", data);
        }

        public void JournalUsed()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            // Send analytics event
            SendAnalyticsEvent("JournalUsed", data);
        }

        public void DashUsed()
        {
            //Add more data related to time stop usage
            Dictionary<string, object> data = new Dictionary<string, object>();

            //Send analytics event
            SendAnalyticsEvent("DashUsed", data);
        }

        public void TimeStopUsed()
        {
            //Add more data related to time stop usage
            Dictionary<string, object> data = new Dictionary<string, object>();

            //Send analytics event
            SendAnalyticsEvent("TimeStopUsed", data);
        }

        public void EnemyDefeated(Enemy enemy)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Name", enemy.DisplayName);

            SendAnalyticsEvent("EnemyDefeated", data);
        }

        public void PlayerDied(PlayerDiedInfo info)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //data.Add("Death_Position_x", info.DeathPosition.x);
            //data.Add("Death_Position_y", info.DeathPosition.y);
            //data.Add("Death_Position_z", info.DeathPosition.z);
            data.Add("Killed_By_Enemy", info.KilledByAttacker);
            data.Add("Killed_By_Attack", info.KilledByAttack);

            // Send analytics event
            SendAnalyticsEvent("PlayerDied", data);
        }

        //public void PlayerReachedCheckpoint(Vector3 checkpointPosition)
        //{
        //    Dictionary<string, object> data = new Dictionary<string, object>();
        //    data.Add("Checkpoint_Position_x", checkpointPosition.x);
        //    data.Add("Checkpoint_Position_y", checkpointPosition.y);
        //    data.Add("Checkpoint_Position_z", checkpointPosition.z);

        //    // Add more data related to checkpoint reached event
        //    // Example: data.Add("Player_Health", player.Health);

        //    // Send analytics event
        //    SendAnalyticsEvent("PlayerReachedCheckpoint", data);
        //}

        public void LevelStarted(GameLevel gameLevel)
        {
            //Add more data related to time stop usage
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Name", gameLevel.name);

            //Send analytics event
            SendAnalyticsEvent("LevelStarted", data);
        }

        public void LevelFinished(LevelCompleteInfo levelCompleteInfo)
        {
            //Add more data related to time stop usage
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Name", levelCompleteInfo.Level.name);
            data.Add("TimeToComplete", levelCompleteInfo.TimeTakenToComplete);


            //Send analytics event
            SendAnalyticsEvent("LevelFinished", data);
        }

        private void OnLevelCompleted(string levelName)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "levelName", "level" + levelName}
            };

            // The ‘levelCompleted’ event will get cached locally
            // and sent during the next scheduled upload, within 1 minute
            SendAnalyticsEvent("LevelCompleted", parameters, true);
        }

        private void OnDestroy()
        {
            Analytics.FlushEvents();
        }

        public void GiveConsent()
        {
            // Call if consent has been given by the user
            AnalyticsService.Instance.StartDataCollection();
            //Debug.Log($"Consent has been provided. The SDK is now collecting data!");
        }

        private void SendAnalyticsEvent(string eventName, Dictionary<string, object> data, bool flushImmediately = false)
        {
            // Send analytics event
            AnalyticsService.Instance.CustomData(eventName, data);

            // Flush events immediately if specified
            if (flushImmediately)
            {
                AnalyticsService.Instance.Flush();
            }
        }
    }
}
