namespace Unite.DialogueSystem
{
    /// <summary>
    /// Used to map certain dialogues to enum values.
    /// Used in the PlayerDialogueHandler to play certain dialogues
    /// based on what enum value is used.
    /// 
    /// <seealso cref="DialogueTriggerMappingSO"/>
    /// <seealso cref="PlayerDialogueHandler"/>
    /// </summary>
    [System.Serializable]
    public enum DialogueTrigger
    {
        BattleEnded,
        EnterIslandLevel,
        EnterBattleZone,
        ExitRoomNotYet,
        TimeStopTutorial,
        UseTimeStop,
        IslandBounds,
        OpenJournal
    }
}