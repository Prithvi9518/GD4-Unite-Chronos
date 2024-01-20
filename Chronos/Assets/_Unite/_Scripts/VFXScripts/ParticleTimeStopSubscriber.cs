using Unite.TimeStop;
using UnityEngine;

namespace Unite.VFXScripts
{
    public class ParticleTimeStopSubscriber : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private ParticleSystem particles;

        private bool initiallyStopped;
        private bool initiallyPaused;
        
        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            if (isTimeStopped)
            {
                initiallyStopped = particles.isStopped;
                initiallyPaused = particles.isPaused;
                
                if (initiallyPaused) return;
                particles.Pause();
            }
            else
            {
                if (initiallyStopped) return;
                if (initiallyPaused) return;
                particles.Play();
            }
        }
    }
}