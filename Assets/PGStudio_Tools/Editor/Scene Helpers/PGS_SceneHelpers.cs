using System;
using UnityEngine;
namespace PowerGameStudio.Tools
{

    public static class PGS_SceneHelpers
    {
        #region Menu Methods
        /// <summary>
        /// Creates Groups
        /// </summary>
        /// 
        public static void CreateLevelGroup()
        {
            // Create the Main Level Manager Group
            GameObject levelGrp = new GameObject("Level_Manager");

            //create the sub Groups to hold certain types of Objects in the scene
            string[] groupNames = new string[] { "Lighting_GRP", "Geo_GRP", "FX_GRP", "Audio_GRP", "Post_GRP","Camera_GRP","UI_GRP","Managers_GRP", };
            Array.Sort(groupNames);
            CreateLevelGroups(levelGrp.transform, groupNames);

        }
        #endregion
        #region Utility Methods
        static void CreateLevelGroups(Transform levelManager, string[] groupNames)
        {
            if (levelManager && groupNames.Length > 0)
            {
                for (int i = 0; i < groupNames.Length; i++)                   
                {                   
                    GameObject curGroup = new GameObject(groupNames[i]);
                    curGroup.transform.SetParent(levelManager);
                }
            }
        }     
        #endregion
    }

}