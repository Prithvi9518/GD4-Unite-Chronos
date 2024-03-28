using System.Collections.Generic;
using Unite.Core.Game;
using Unite.EventSystem;
using Unite.Managers;
using UnityEngine;

namespace Unite.TimeTracking
{
    public class LevelTimeTracker : MonoBehaviour
    {
        [SerializeField]
        private GameLevelEvent onLevelStart;
        
        [SerializeField]
        private LevelCompleteInfoEvent onLevelComplete;
        
        private float timeLevelStarted;
        private float timeLevelEnded;

        private Dictionary<GameLevel, float> levelTimes = new();

        private void OnEnable()
        {
            GameManager.Instance.OnStartLevel_UpdateTimeTracking += OnLevelStart;
            GameManager.Instance.OnFinishLevel_UpdateTimeTracking += OnLevelFinish;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnStartLevel_UpdateTimeTracking -= OnLevelStart;
            GameManager.Instance.OnFinishLevel_UpdateTimeTracking -= OnLevelFinish;
        }

        private void OnLevelStart(GameLevel level)
        {
            timeLevelStarted = Time.realtimeSinceStartup;
            onLevelStart.Raise(level);
        }

        private void OnLevelFinish(GameLevel level)
        {
            timeLevelEnded = Time.realtimeSinceStartup;

            float timeDifference = timeLevelEnded - timeLevelStarted;
            
            levelTimes.Add(level, timeDifference);
            onLevelComplete.Raise(new LevelCompleteInfo(level, timeDifference));
        }
    }
}