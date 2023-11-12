using UnityEngine;

namespace Unite
{
    public class ReferenceManager : MonoBehaviour
    {
        private static ReferenceManager instance;

        public Player Player { get; set; }

        public static ReferenceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("ReferenceManager");
                    instance = go.AddComponent<ReferenceManager>();
                    DontDestroyOnLoad(go);
                }

                return instance;
            }
        }
    }
}