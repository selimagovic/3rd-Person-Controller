using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace PowerGameStudio.Tools
{

    public class PGS_ReplaceObjectsEditor : EditorWindow
    {
        #region Variables
        GameObject[] selectedObjects = new GameObject[0];
        GameObject wantedObject;
        #endregion
        #region Builtin Methods
        public static void LaunchReplacer()
        {
            var repWin = GetWindow<PGS_ReplaceObjectsEditor>();
            GUIContent gUIContent = new GUIContent("Replace Objects");
            repWin.titleContent = gUIContent;
            repWin.Show();
        }
        private void OnGUI()
        {
            GUILayout.Space(15);
            selectedObjects = Selection.gameObjects;
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Selected GOs: " + selectedObjects.Length.ToString("000"), EditorStyles.boldLabel);
            wantedObject = (GameObject)EditorGUILayout.ObjectField("Replace Object", wantedObject, typeof(GameObject), true);

            if (GUILayout.Button("Replace Selected Objects", GUILayout.ExpandWidth(true), GUILayout.Height(40)))
            {
                ReplaceSelectedObjects();
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            Repaint();
        }
        #endregion
        #region Custom Methods
        void ReplaceSelectedObjects()
        {
            //Check for selection count 
            if (selectedObjects.Length == 0)
            {
                CustomDialog("At least one object needs to be selected to replace with!");
                return;
            }
            //Check for replace Object
            if (!wantedObject)
            {
                CustomDialog("The replace object is empty, please assign something!");
                return;
            }

            for(int i=0; i<selectedObjects.Length; i++)
            {
                Transform selectTransform = selectedObjects[i].transform;
                GameObject newObject = Instantiate(wantedObject, selectTransform.position, selectTransform.rotation);
                newObject.transform.localScale = selectTransform.localScale;
                DestroyImmediate(selectedObjects[i]);
            }
            Debug.Log("Objects Replacing...");
        }

        void CustomDialog(string message)
        {
            EditorUtility.DisplayDialog("Replace Objects Warning", message, "OK");
        }
        
        #endregion
    }
}
