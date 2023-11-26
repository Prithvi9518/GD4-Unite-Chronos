using System.Collections.Generic;
using Unite.WeaponSystem;
using UnityEngine;

namespace Unite.Player
{
    [DisallowMultipleComponent]
    public class PlayerGunHandler : MonoBehaviour
    {
        [SerializeField]
        private GunType gunType;
        
        [SerializeField] 
        private Transform gunParent;

        [SerializeField]
        private List<GunData> guns;

        [Header("Filled at Runtime")] 
        [SerializeField]
        private GunData activeGun;

        public GunData ActiveGun => activeGun;

        private Dictionary<GunType, GunData> gunDictionary = new();
        private PlayerInputHandler inputHandler;

        private void Start()
        {
            SetupGunDictionary();
            GunData gun = gunDictionary[gunType];
            
            if (gun == null) return;
            activeGun = gun;
            gun.Spawn(gunParent, this);
        }

        private void Update()
        {
            if (inputHandler.IsShootActionPressed())
            {
                activeGun.Shoot();
            }
        }

        public void SetInputHandler(PlayerInputHandler playerInputHandler)
        {
            inputHandler = playerInputHandler;
        }

        private void SetupGunDictionary()
        {
            gunDictionary.Clear();
            foreach (GunData gun in guns)
            {
                gunDictionary.Add(gun.GunType, gun);
            }
        }
    }
}