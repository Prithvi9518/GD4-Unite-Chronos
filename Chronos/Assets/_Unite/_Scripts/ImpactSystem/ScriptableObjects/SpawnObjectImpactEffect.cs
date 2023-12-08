using UnityEngine;

namespace Unite.ImpactSystem
{
    [CreateAssetMenu(fileName = "SpawnObjectEffect", menuName = "Impact System/Spawn Object Effect")]
    public class SpawnObjectImpactEffect : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        
        public GameObject Prefab => prefab;
    }
}