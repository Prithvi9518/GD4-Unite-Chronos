using UnityEngine;

namespace Unite.WeaponSystem
{
    [CreateAssetMenu(fileName = "ShootData", menuName = "Weapons/Shoot Data")]
    public class ShootData : ScriptableObject
    {
        [SerializeField]
        private LayerMask hitMask;
        
        [SerializeField]
        private Vector3 bulletSpread;
        
        [SerializeField]
        private float fireRate;

        public LayerMask HitMask => hitMask;
        public Vector3 BulletSpread => bulletSpread;
        public float FireRate => fireRate;
    }
}

