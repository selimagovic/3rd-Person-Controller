using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerGameStudio.Cameras
{
    public class PGS_BaseCamera : MonoBehaviour
    {
        #region Variables
        public Transform m_Target;
        #endregion
        #region Builtin Methods 
        private void Start()
        {
            HandleCamera();
           
        }
        private void LateUpdate()
        {
            HandleCamera();           
        }
        #endregion
        #region Helper Methods
        protected virtual void HandleCamera()
        {
            if (!m_Target)
            {
                Debug.Log("Please add target to camera!!!");
                return;
            }
        }
        #endregion
    } 
}
