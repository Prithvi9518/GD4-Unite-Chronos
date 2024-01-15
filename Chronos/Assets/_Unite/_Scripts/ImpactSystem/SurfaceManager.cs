using System.Collections.Generic;
using Unite.SoundScripts;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.ImpactSystem
{
    public class SurfaceManager : MonoBehaviour
    {
        public static SurfaceManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one SurfaceManager active in scene. Destroying latest instance.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [SerializeField] private List<SurfaceType> surfaceTypes;
        [SerializeField] private Surface defaultSurface;

        private Dictionary<GameObject, ObjectPool<GameObject>> objectPools = new();
        
        public void HandleImpact(GameObject hitObject, Vector3 hitPos, Vector3 hitNormal, ImpactType impactType)
        {
            foreach (SurfaceImpactTypeEffect impactTypeEffect in defaultSurface.ImpactTypeEffects)
            {
                if (impactTypeEffect.ImpactType == impactType)
                {
                    PlayEffects(hitPos, hitNormal, impactTypeEffect.SurfaceEffect);
                }
            }
        }

        private void PlayEffects(Vector3 hitPos, Vector3 hitNormal, SurfaceEffect surfaceEffect)
        {
            foreach (SpawnObjectImpactEffect spawnObjectEffect in surfaceEffect.SpawnObjectEffects)
            {
                if (spawnObjectEffect.Prefab == null) continue;

                if (!objectPools.ContainsKey(spawnObjectEffect.Prefab))
                {
                    objectPools.Add(
                        spawnObjectEffect.Prefab,
                        new ObjectPool<GameObject>(() => Instantiate(spawnObjectEffect.Prefab))
                    );
                }

                GameObject instance = objectPools[spawnObjectEffect.Prefab].Get();
                instance.SetActive(true);
                instance.transform.position = hitPos + hitNormal * 0.001f;
                instance.transform.forward = hitNormal;
            }

            foreach (PlayAudioImpactEffect playAudioEffect in surfaceEffect.PlayAudioEffects)
            {
                AudioClip audioClip = playAudioEffect.AudioClips[Random.Range(0, playAudioEffect.AudioClips.Count)];

                SoundManager.Instance.PlaySoundAtPosition(
                    audioClip,
                    hitPos,
                    Random.Range(playAudioEffect.MinVolume, playAudioEffect.MaxVolume)
                );
            }
        }
    }
}