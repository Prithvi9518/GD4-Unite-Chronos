using UnityEngine;
using UnityEngine.AI;

namespace Unite.ItemDropSystem
{
    public class EnemyDropHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject item;

        [SerializeField]
        private Transform dropPoint;

        [SerializeField]
        private float yOffset;

        public void DropItems()
        {
            int probability = Random.Range(0, 2);
            if (probability == 0) return;

            if (NavMesh.SamplePosition(dropPoint.position, out var hit, 1000, -1))
            {
                Instantiate(item, hit.position + (Vector3.up * yOffset), Quaternion.identity);
            }
        }
    }
}