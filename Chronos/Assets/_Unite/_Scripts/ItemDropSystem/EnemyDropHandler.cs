using UnityEngine;

namespace Unite.ItemDropSystem
{
    public class EnemyDropHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject item;

        [SerializeField]
        private Transform dropPoint;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private float yOffset;

        public void DropItems()
        {
            int probability = Random.Range(0, 2);
            if (probability == 0) return;
            
            Instantiate(item, dropPoint.position + (Vector3.up * yOffset), Quaternion.identity);
        }
    }
}