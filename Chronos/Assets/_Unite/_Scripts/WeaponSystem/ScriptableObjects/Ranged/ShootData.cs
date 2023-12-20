using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.WeaponSystem
{
    [CreateAssetMenu(fileName = "ShootData", menuName = "Weapons/Shoot Data")]
    public class ShootData : ScriptableObject, ICloneable
    {
        [SerializeField]
        private LayerMask hitMask;

        [SerializeField]
        private BulletSpreadType bulletSpreadType;
        
        [Header("Simple Bullet Spread")]
        [SerializeField]
        private Vector3 bulletSpread;
        
        [SerializeField]
        private float fireRate;

        [SerializeField]
        private float recoilRecoverySpeed;
        
        [SerializeField]
        private float maxSpreadTime;

        public LayerMask HitMask => hitMask;
        public float FireRate => fireRate;
        public float RecoilRecoverySpeed => recoilRecoverySpeed;
        public float MaxSpreadTime => maxSpreadTime;

        public void ModifyFireRate(float amount)
        {
            fireRate += amount;
        }
        
        public Vector3 GetSpread(float shootTime = 0f)
        {
            Vector3 spread = Vector3.zero;

            if (bulletSpreadType == BulletSpreadType.Simple)
            {
                Vector3 spreadRange = new Vector3(
                    Random.Range(-bulletSpread.x, bulletSpread.x),
                    Random.Range(-bulletSpread.y, bulletSpread.y),
                    Random.Range(-bulletSpread.z, bulletSpread.z)
                );
                spread = Vector3.Lerp(Vector3.zero, spreadRange, Mathf.Clamp01(shootTime / maxSpreadTime));
            }
            
            return spread;
        }
        
        public object Clone()
        {
            ShootData clone = CreateInstance<ShootData>();
            clone.hitMask = hitMask;
            clone.bulletSpread = bulletSpread;
            clone.bulletSpreadType = bulletSpreadType;
            clone.fireRate = fireRate;
            clone.recoilRecoverySpeed = recoilRecoverySpeed;
            clone.maxSpreadTime = maxSpreadTime;
            
            return clone;
        }
    }
}

