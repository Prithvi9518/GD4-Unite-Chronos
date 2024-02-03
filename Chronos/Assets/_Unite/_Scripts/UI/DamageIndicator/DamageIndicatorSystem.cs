using System.Collections.Generic;
using UnityEngine;

namespace Unite.UI
{
    public class DamageIndicatorSystem : MonoBehaviour
    {
        [SerializeField]
        private DamageIndicator indicatorPrefab;

        [SerializeField]
        private RectTransform holder;
        
        private Transform player;
        private Camera cam;

        public Dictionary<Transform, DamageIndicator> Indicators = new();

        private void Start()
        {
            cam = Camera.main;
        }
        
        private bool InSightOfPlayer(Transform t)
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(t.position);
            return screenPoint is { z: > 0, x: > 0 and < 1, y: > 0 and < 1 };
        }

        public void TryCreateIndicator(Transform target)
        {
            // if (!InSightOfPlayer(target)) return;
            
            if (Indicators.ContainsKey(target))
            {
                Indicators[target].Restart();
                return;
            }

            DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
            newIndicator.Register(target, player, () => { Indicators.Remove(target);});
            
            Indicators.Add(target, newIndicator);
        }

        public void ListenToPlayerReadyEvent(Player.Player p)
        { 
            player = p.transform;
        }
    }
}