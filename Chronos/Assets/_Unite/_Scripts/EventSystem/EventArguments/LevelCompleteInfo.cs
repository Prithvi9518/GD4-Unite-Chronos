using Unite.Core.Game;

namespace Unite.EventSystem
{
    public struct LevelCompleteInfo
    {
        public readonly GameLevel Level;
        public readonly float TimeTakenToComplete;

        public LevelCompleteInfo(GameLevel level, float timeTaken)
        {
            Level = level;
            TimeTakenToComplete = timeTaken;
        }
    }
}