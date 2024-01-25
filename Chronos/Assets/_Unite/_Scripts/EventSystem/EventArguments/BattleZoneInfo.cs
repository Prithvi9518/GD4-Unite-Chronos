namespace Unite.EventSystem
{
    public struct BattleZoneInfo
    {
        public readonly string BattleZoneName;
        public readonly int CurrentWave;

        public BattleZoneInfo(string name, int wave)
        {
            BattleZoneName = name;
            CurrentWave = wave;
        }
    }
}