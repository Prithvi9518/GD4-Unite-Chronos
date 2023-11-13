using UnityEditor;
using UnityEngine;

namespace Unite
{
    [CustomEditor(typeof(ObjectSpawner))]
    public class ObjectSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ObjectSpawner objectSpawner = (ObjectSpawner)target;

            if (GUILayout.Button("Spawn"))
            {
                objectSpawner.SpawnObjects();
            }

            if (GUILayout.Button("Clear"))
            {
                objectSpawner.Clear();
            }
        }
    }
}