using UnityEngine;

namespace Unite.EventSystem
{
    public struct PlayerDiedInfo
    {
        public Vector3 DeathPosition;
        public string KilledByAttacker;
        public string KilledByAttack;

        public PlayerDiedInfo(Vector3 deathPos, string attacker, string attack)
        {
            DeathPosition = deathPos;
            KilledByAttacker = attacker;
            KilledByAttack = attack;
        }
    }
}