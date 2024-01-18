using UnityEngine;
using UnityEditor;

namespace Unite.EditorScripts
{
    public class CopyComponents : MonoBehaviour
    {
        public GameObject sourceObject;
        public GameObject targetObject;

        void Start()
        {
            if (sourceObject == null || targetObject == null)
            {
                Debug.LogError("Source or target objects are not assigned!");
                return;
            }

            // Copy components from sourceObject to targetObject
            CopyAllComponents(sourceObject, targetObject);

            // Save the modified targetObject as a prefab
            SaveAsPrefab(targetObject, "Assets/_Unite/Prefabs/Enemies/ModifiedPrefab.prefab");
        }

        void CopyAllComponents(GameObject source, GameObject target)
        {
            // Get all components attached to the source object
            Component[] components = source.GetComponents(typeof(Component));

            // Iterate through each component and copy it to the target object
            foreach (Component component in components)
            {
                // Skip Transform component to avoid position/scale/rotation conflicts
                if (component.GetType() == typeof(Transform))
                    continue;

                // Copy the component to the target object
                Component copy = target.AddComponent(component.GetType());

                // Copy values from the source component to the target component (optional)
                // You may need to implement specific logic for each component type
                // For example, if it's a MonoBehaviour, you might want to copy public properties.

                // Uncomment the following line if you want to copy values (e.g., public properties)
                // CopyComponentValues(component, copy);
            }
        }

        void CopyComponentValues(Component source, Component target)
        {
            // This method should be expanded based on the specific components you're dealing with
            // For MonoBehaviour scripts, you may want to copy public properties
            // For other component types, additional logic may be needed

            // Example for MonoBehaviour:
            if (source is MonoBehaviour && target is MonoBehaviour)
            {
                MonoBehaviour sourceMono = (MonoBehaviour)source;
                MonoBehaviour targetMono = (MonoBehaviour)target;

                // Example: Copy a public property (change accordingly)
                // targetMono.publicProperty = sourceMono.publicProperty;
            }
        }

        void SaveAsPrefab(GameObject obj, string prefabPath)
        {
            // Create a prefab from the GameObject and save it as an asset
            GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(obj, prefabPath, InteractionMode.UserAction);
            Debug.Log("Prefab saved at: " + prefabPath);
        }
    }
}