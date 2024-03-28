namespace Unite.EventSystem
{
    public struct BattleFinishedInfo
    {
        public readonly string BattleZoneName;
        public readonly float TimeTakenToComplete;

        public BattleFinishedInfo(string name, float timeTaken)
        {
            BattleZoneName = name;
            TimeTakenToComplete = timeTaken;
        }
    }
}