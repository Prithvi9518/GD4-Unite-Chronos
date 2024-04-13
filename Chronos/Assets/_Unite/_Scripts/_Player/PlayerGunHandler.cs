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
    /// <summary>
    /// Handles storage, usage and switching of player guns.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerGunHandler : MonoBehaviour, IAttacker
    {
        [SerializeField]
        private StatTypeSO damageStatType;
        
        [SerializeField]
        private GunType gunType;
        
        [SerializeField]
        private List<GunData> guns;

        [SerializeField] 
        private GameEvent onPlayerShootAction;

        [Header("Filled at Runtime")] 
        [SerializeField]
        private GunData activeGun;

        public GunData ActiveGun => activeGun;

        private Transform gunParent;
        private Dictionary<GunType, GunData> gunDictionary = new();
        private PlayerStatsHandler statsHandler;

        private bool initializedGun;

        private void Awake()
        {
            SetupGunDictionary();
            GunData gun = gunDictionary[gunType];
            
            if (gun == null) return;
            activeGun = gun.Clone() as GunData;
        }

        private void Update()
        {
            CheckAndHandleShootAction();
        }

        /// <summary>
        /// Used to spawn gun at runtime when events are raised.
        /// </summary>
        public void InitializeActiveGun()
        {
            Debug.Log("PlayerGunHandler - InitializeActiveGun()");
            gunParent = Managers.GameManager.Instance.WeaponsHolder.WeaponParent;
            if (activeGun == null) return;
            
            activeGun.Spawn(gunParent, this);
            initializedGun = true;
        }

        public void PerformSetup(PlayerStatsHandler playerStatsHandler)
        {
            statsHandler = playerStatsHandler;
            UpdateBaseDamageFromStats();
        }

        /// <summary>
        /// Used when buffs are acquired by player, and adds modifiers to gun.
        /// </summary>
        public void ApplyModifier(IGunModifier gunModifier)
        {
            gunModifier.Apply(activeGun);
        }

        /// <summary>
        /// Used to update the gun's damage during player setup.
        /// </summary>
        public void UpdateBaseDamageFromStats()
        {
            activeGun.UpdateBaseDamage(statsHandler.GetStat(damageStatType).Value);
        }

        /// <summary>
        /// Queries InputManager class for whether the shoot action is pressed, and propagates
        /// the information to the active gun class.
        /// </summary>
        private void CheckAndHandleShootAction()
        {
            if (activeGun == null) return;
            if (!initializedGun) return;

            bool isShootActionPressed = InputManager.Instance.IsShootActionPressed();
            activeGun.Tick(isShootActionPressed, onPlayerShootAction);
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

        public Transform GetTransform()
        {
            return transform;
        }
    }
}