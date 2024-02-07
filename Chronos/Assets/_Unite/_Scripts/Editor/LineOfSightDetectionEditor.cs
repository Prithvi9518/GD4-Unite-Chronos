using Unite.Detection;
using UnityEditor;
using UnityEngine;

namespace Unite.EditorScripts
{
    [CustomEditor(typeof(LineOfSightDetection))]
    public class LineOfSightDetectionEditor : Editor
    {
        private void OnSceneGUI()
        {
            LineOfSightDetection detectionHandler = (LineOfSightDetection)target;
            Vector3 targetPosition = detectionHandler.transform.position;
            
            Handles.color = Color.white;
            Handles.DrawWireArc(targetPosition,
                Vector3.up, Vector3.forward, 360, detectionHandler.DetectionRadius);

            Vector3 eulerAngles = detectionHandler.transform.eulerAngles;

            Vector3 viewAngle1 = DirectionFromAngle(eulerAngles.y,
                -detectionHandler.AngleInDegrees / 2);
            Vector3 viewAngle2 = DirectionFromAngle(eulerAngles.y,
                detectionHandler.AngleInDegrees / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(targetPosition, targetPosition + viewAngle1 * detectionHandler.DetectionRadius);
            Handles.DrawLine(targetPosition, targetPosition + viewAngle2 * detectionHandler.DetectionRadius);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}