using Unite.SoundScripts;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "PlaySoundAction", menuName = "AI/Actions/PlaySoundAction")]
    public class PlaySoundAction : Action
    {
        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        [Range(0,1)]
        private float volume;
        
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            SoundEffectsManager.Instance.PlaySoundAtPosition(audioClip, baseStateMachine.transform.position, volume);
        }
    }
}