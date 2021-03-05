using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;

namespace PowerGameStudio.Tools
{
    public class PGS_ObjectRenameEditor : EditorWindow
    {
        #region Variables
        GameObject[] selected = new GameObject[0];
        string wantedPrefix;
        string wantedName;
        string wantedSuffix;
        bool addNumbering;
        
        #endregion

        #region Builtin Methods
        public static void LaunchRenamer()
        {
            var win = GetWindow<PGS_ObjectRenameEditor>();
            GUIContent content = new GUIContent("Rename Objects");
            win.titleContent = content;
            win.Show();
        }

        private void OnGUI()
        {
            // Get Current Selected Objects
            GUILayout.Space(15);
            selected = Selection.gameObjects;
            EditorGUILayout.LabelField("Selected: " + selected.Length.ToString("000"));

            //Display UI
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Space(10);
            wantedPrefix = EditorGUILayout.TextField("Prefix: ", wantedPrefix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            wantedName = EditorGUILayout.TextField("Name: ", wantedName, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            wantedSuffix = EditorGUILayout.TextField("Sufix: ", wantedSuffix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            addNumbering = EditorGUILayout.Toggle("Add Numbering?", addNumbering);
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();           
            
            if (GUILayout.Button("Rename Selected Objects",GUILayout.Height(45),GUILayout.ExpandWidth(true)))
            {
                RenameObjects();
            }
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
            GUILayout.Space(15);
            EditorGUILayout.EndHorizontal();
            Repaint();
        }
        #endregion

        #region Custom Methods
        void RenameObjects()
        {
            Array.Sort(selected, delegate (GameObject objA, GameObject objB) { return objA.name.CompareTo(objB.name); });

           
            for(int i=0; i<selected.Length; i++)
            {
                string finalName = string.Empty;

                if (wantedPrefix.Length > 0)
                {
                    finalName += wantedPrefix;
                }
                if(wantedName.Length>0)
                {
                    finalName +="_"+ wantedName;
                }
                if(wantedSuffix.Length>0)
                {
                    finalName += "_" + wantedSuffix;
                }
                if(addNumbering)
                {
                    finalName += "_" + i.ToString("000");
                }

                selected[i].name = finalName;
                //Debug.Log(selected[i].name);
            }
        }
        #endregion
    }
}
