namespace Unite.Utils
{
    using UnityEngine;
    using UnityEditor;
    
    /// <summary>
    /// Hierarchy Window Group Header
    /// http://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header
    /// </summary>
    /// <example>To create a folder make an empty with the name "*** folder name" in the hierarchy</example>
    [InitializeOnLoad] 
    public class GameObjectHeader : MonoBehaviour
    {
        private enum CategoryType
        {
            HierarchyOne,
            HierarchyTwo,
            HierarchyThree
        }

        [SerializeField]
        private CategoryType categoryType;
        private static readonly Color folderFillColor = Color.blue;
        private static readonly string singleCharFolderDelimiter = "*";
        private static readonly string folderDelimiter = $"{singleCharFolderDelimiter}{singleCharFolderDelimiter}{singleCharFolderDelimiter}";
    
        private GameObjectHeader()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }
    
        private void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            // var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            //
            // if (gameObject.GetComponent<GameObjectHeader>() != null)
            // {
            //     if (gameObject != null)
            //     {
            //         if (categoryType == CategoryType.HierarchyOne)
            //         {
            //             EditorGUI.DrawRect(selectionRect, folderFillColor);
            //             EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace(singleCharFolderDelimiter, "").ToUpperInvariant());
            //         }
            //         else
            //         {
            //             EditorGUI.DrawRect(selectionRect, Color.yellow);
            //             EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace(singleCharFolderDelimiter, "").ToUpperInvariant());
            //         }
            //
            //     }
            // }
           
        }
    }
    
    
}