using System.Collections.Generic;
using Unite.Core.DamageInterfaces;
using Unite.Core.Input;
using Unite.EventSystem;
using Unite.StatSystem;
using Unite.WeaponSystem;
using Unite.WeaponSystem.Modifiers;
using UnityEngine;

namespace Unite.Player
{
    [DisallowMultipleComponent]
    public class PlayerGunHandler : MonoBehaviour, IAttacker
    {
        [SerializeField]
        private StatTypeSO damageStatType;
        
        [SerializeField]
        private GunType gunType;
        
        [SerializeField] 
        private Transform gunParent;

        [SerializeField]
        private List<GunData> guns;

        [SerializeField] 
        private GameEvent onPlayerShootAction;

        [Header("Filled at Runtime")] 
        [SerializeField]
        private GunData activeGun;

        public GunData ActiveGun => activeGun;

        private Dictionary<GunType, GunData> gunDictionary = new();
        private PlayerStatsHandler statsHandler;

        private void Awake()
        {
            SetupGunDictionary();
            GunData gun = gunDictionary[gunType];
            
            if (gun == null) return;
            activeGun = gun.Clone() as GunData;
            if (activeGun == null) return;
            
            activeGun.Spawn(gunParent, this);
        }

        private void Update()
        {
            CheckAndHandleShootAction();
        }

        public void PerformSetup(PlayerStatsHandler playerStatsHandler)
        {
            statsHandler = playerStatsHandler;
            UpdateBaseDamageFromStats();
        }

        public void ApplyModifier(IGunModifier gunModifier)
        {
            gunModifier.Apply(activeGun);
        }

        public void UpdateBaseDamageFromStats()
        {
            activeGun.UpdateBaseDamage(statsHandler.GetStat(damageStatType).Value);
        }

        private void CheckAndHandleShootAction()
        {
            if (activeGun == null) return;

            bool isShootActionPressed = InputManager.Instance.IsShootActionPressed();
            activeGun.Tick(isShootActionPressed);

            if (!isShootActionPressed) return;
            onPlayerShootAction.Raise();
        }

        private void SetupGunDictionary()
        {
            gunDictionary.Clear();
            foreach (GunData gun in guns)
            {
                gunDictionary.Add(gun.GunType, gun);
            }
        }

        public string GetName()
        {
            return name;
        }
    }
}