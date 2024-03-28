using System.Collections.Generic;
using Unite.Managers;
using UnityEngine;

namespace Unite.UI
{
    public class DamageIndicatorSystem : MonoBehaviour
    {
        [SerializeField]
        private DamageIndicator indicatorPrefab;

        [SerializeField]
        private RectTransform holder;
        
        private Transform playerOrientation;
        private Camera cam;

        public Dictionary<Transform, DamageIndicator> Indicators = new();

        private void Start()
        {
            cam = Camera.main;
        }
        
        public void TryCreateIndicator(Transform target)
        {
            if (Indicators.ContainsKey(target))
            {
                Indicators[target].Restart();
                return;
            }
            
            DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
            
            if(playerOrientation == null)
                playerOrientation = GameManager.Instance.Player.Orientation;
            
            newIndicator.Register(target, playerOrientation, () => { Indicators.Remove(target);});
            
            Indicators.Add(target, newIndicator);
        }
    }
}