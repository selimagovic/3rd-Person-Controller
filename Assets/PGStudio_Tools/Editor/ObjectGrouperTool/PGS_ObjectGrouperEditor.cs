using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace PowerGameStudio.Tools
{
    public class PGS_ObjectGrouperEditor : EditorWindow
    {

        #region Variables
        string wantedName = "Enter Name";
        private int curSelectionCount;
        private GameObject[] selectedObjects = new GameObject[0];
        #endregion
        #region Main Methods

        public static void InitWindow()
        {
            var win = GetWindow<PGS_ObjectGrouperEditor>("Group Selected");
            win.ShowUtility();

        }
        void OnGUI()
        {
            selectedObjects = Selection.gameObjects;
            curSelectionCount = selectedObjects.Length;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical();        
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Selection Count : " + curSelectionCount.ToString());
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Group Name :",EditorStyles.boldLabel);
            wantedName = EditorGUILayout.TextField(wantedName);
            GUILayout.Space(5);
            if (GUILayout.Button("Group Selected",GUILayout.ExpandWidth(true),GUILayout.Height(45)))
            {
                GroupSelectedObjects();
            }
            EditorGUILayout.Space();
            
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
            Repaint();
        }
        #endregion
        #region CustomMethods

        void GroupSelectedObjects()
        {
            if(selectedObjects.Length>0)
            {
                if(wantedName != "Enter Name")
                {
                    GameObject groupGO = new GameObject(wantedName+"_GRP");
                    for(int i=0; i<selectedObjects.Length; i++)
                    {
                        selectedObjects[i].transform.SetParent(groupGO.transform);
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Group Message", "You must provide a name for you group", "OK");
                }
               
            }
        }
        #endregion
    }
}
