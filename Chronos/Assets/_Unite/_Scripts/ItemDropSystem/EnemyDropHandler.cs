using UnityEngine;

namespace Unite.ItemDropSystem
{
    public class EnemyDropHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject item;

        [SerializeField]
        private Transform dropPoint;

        public void DropItems()
        {
            int probability = Random.Range(0, 2);
            if (probability == 0) return;

            Instantiate(item, dropPoint.position, Quaternion.identity);
        }
    }
}