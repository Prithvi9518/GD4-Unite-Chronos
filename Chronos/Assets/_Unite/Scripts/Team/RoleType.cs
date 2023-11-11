using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    [System.Serializable]
    public enum RoleType
    {
        // Game Design
        GD_LeadDesigner,

        GD_LevelDesigner,
        GD_Writer,
        GD_SystemDesigner,

        // Art
        ART_ConceptArtist,

        ART_3DModeler,
        ART_Animator,
        ART_TextureArtist,

        // Programming
        PROG_GameplayProgrammer,

        PROG_UXProgrammer,
        PROG_NetworkProgrammer,
        PROG_EngineProgrammer,

        // Sound Design
        SND_Composer,

        SND_SoundEffectsDesigner,
        SND_AudioEngineer,
        SND_VoiceOverDirector,

        // Quality Assurance
        QA_TestEngineer,

        QA_TestAnalyst,
        QA_TestLead,
        QA_AutomationEngineer,

        // Production
        PROD_Producer,

        PROD_AssociateProducer,
        PROD_ProductionAssistant,
        PROD_ScrumMaster,

        // Marketing
        MKT_MarketingStrategist,

        MKT_CommunityManager,
        MKT_PublicRelationsManager,
        MKT_BrandManager
    }
}
