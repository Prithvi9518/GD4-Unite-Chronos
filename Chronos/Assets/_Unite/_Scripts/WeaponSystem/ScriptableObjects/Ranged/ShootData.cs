using System;
using UnityEngine;

namespace Unite.WeaponSystem
{
    [CreateAssetMenu(fileName = "ShootData", menuName = "Weapons/Shoot Data")]
    public class ShootData : ScriptableObject, ICloneable
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

        public void ModifyFireRate(float amount)
        {
            fireRate += amount;
        }
        
        public object Clone()
        {
            ShootData clone = CreateInstance<ShootData>();
            clone.hitMask = hitMask;
            clone.bulletSpread = bulletSpread;
            clone.fireRate = fireRate;
            
            return clone;
        }
    }
}

