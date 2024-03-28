using UnityEngine;

namespace Unite.WeaponSystem
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField]
        private Transform weaponParent;

        public Transform WeaponParent => weaponParent;
    }
}