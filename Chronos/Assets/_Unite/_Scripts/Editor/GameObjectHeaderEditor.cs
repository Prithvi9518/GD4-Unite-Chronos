using UnityEditor;
using UnityEngine;
using Unite.Utils;

namespace Unite.EditorScripts
{
    // /// <summary>
    // /// GameObjectHeaderEditor
    // /// http://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header
    // /// </summary>
        [InitializeOnLoad]
    public class GameObjectHeaderEditor
    {
        static GameObjectHeaderEditor()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }
        private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject != null)
            {
                // Initialize hierarchyColor with a default value indicating no color should be applied
                Color hierarchyColor = default;
                bool isParent = false;

                // Check if the GameObject has a GameObjectHeader component
                GameObjectHeader gameObjectHeader = gameObject.GetComponent<GameObjectHeader>();
                if (gameObjectHeader != null)
                {
                    // This is a parent GameObject, so use the base color and flag it for label
                    hierarchyColor = gameObjectHeader.HeaderColor;
                    isParent = true;
                }
                else
                {
                    // If not, check if any of its parents have the GameObjectHeader component
                    Transform parent = gameObject.transform.parent;
                    while (parent != null)
                    {
                        GameObjectHeader parentHeader = parent.GetComponent<GameObjectHeader>();
                        if (parentHeader != null)
                        {
                            // Apply a tinted color for children
                            hierarchyColor = GetTintedColorByCategory(parentHeader.HeaderColor);
                            break; // Break the loop once a parent with a header is found
                        }
                        parent = parent.parent;
                    }
                }

                // Apply the determined color if not default
                if (hierarchyColor != default)
                {
                    EditorGUI.DrawRect(selectionRect, hierarchyColor);
                    // Additionally, if this is a parent GameObject, draw its label
                    if (isParent)
                    {
                        EditorGUI.DropShadowLabel(selectionRect, gameObject.name.ToUpperInvariant());
                    }
                }
            }
        }

        private static Color GetTintedColorByCategory(Color headerColor)
        {
            float tintFactor = 0.4f; // Adjust this value to get the desired tint level
            return headerColor * tintFactor; // Apply tint
        }
        
    }
}