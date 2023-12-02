namespace Unite.WeaponSystem
{
    [System.Serializable]
    public class FireRateModifier : IGunModifier
    {
        private float amount;

        public FireRateModifier(float amount)
        {
            this.amount = amount;
        }
        
        public void Apply(GunData gun)
        {
            gun.ShootData.ModifyFireRate(amount);
        }
    }
}