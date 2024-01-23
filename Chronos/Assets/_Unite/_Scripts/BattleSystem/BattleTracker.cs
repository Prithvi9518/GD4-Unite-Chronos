namespace Unite.BattleSystem
{
    public static class BattleTracker
    {
        public static BattleZone CurrentBattleZone { get; private set; }
        
        public static void SetCurrentBattleZone(BattleZone zone)
        {
            CurrentBattleZone = zone;
        }
    }
}