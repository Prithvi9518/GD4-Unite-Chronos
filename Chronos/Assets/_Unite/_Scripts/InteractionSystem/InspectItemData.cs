using System.Collections.Generic;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class InspectItemData
    {
        #region Fields
        
        /// <summary>
        /// State of whether the player is inspecting or not.
        /// </summary>
        private bool isExamining;
        
        /// <summary>
        /// The last recorded mouse position during examination.
        /// </summary>
        private Vector3 lastMousePosition;

        /// <summary>
        /// The currently examined object during examination.
        /// </summary>
        private Transform examinedObject;

        /// <summary>
        /// Original positions of interactable objects
        /// </summary>
        private Dictionary<Transform, Vector3> originalPositions;

        /// <summary>
        /// Original rotations of interactable objects
        /// </summary>
        private Dictionary<Transform, Quaternion> originalRotations;
        
        #endregion
        
        public InspectItemData()
        {
            this.isExamining = false;
            this.lastMousePosition = new Vector3();
            this.examinedObject = null;
            this.originalPositions = new Dictionary<Transform, Vector3>();
            this.originalRotations = new Dictionary<Transform, Quaternion>();
        }

        #region Properties
        public bool IsExamining => isExamining;

        public Vector3 LastMousePosition
        {
            get => lastMousePosition;
            set => lastMousePosition = value;
        }
        public Transform ExaminedObject
        {
            get => examinedObject;
            set => examinedObject = value;
        }
        public Dictionary<Transform, Vector3> OriginalPositions => originalPositions;
        public Dictionary<Transform, Quaternion> OriginalRotations => originalRotations;
        #endregion
        
        /// <summary>
        /// Change the state of examining.
        /// </summary>
        public void ToggleExamination()
        {
            isExamining = !isExamining;
        }
    }
}