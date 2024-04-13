using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RemoveUnderwaterTrees))]
public class RemoveUnderwaterTreesEditor : Editor
{
   public override void OnInspectorGUI()
   {
      DrawDefaultInspector();
      RemoveUnderwaterTrees underwaterTreesScript = (RemoveUnderwaterTrees)target;

      if (GUILayout.Button("Remove Underwater Trees"))
      {
         underwaterTreesScript.RemoveTreeUnderWater();
      }
   }
}
