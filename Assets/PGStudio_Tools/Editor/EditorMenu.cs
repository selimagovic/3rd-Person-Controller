using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PowerGameStudio.Cameras;
using PowerGameStudio.UI;

namespace PowerGameStudio.Tools
{
    public class EditorMenu
    {
        [MenuItem("PGStudio/Project/Project Setup Window")]
        public static void InitProjectSetupTool()
        {
            ProjectSetupWindow.InitWindow();
        }
        [MenuItem("PGStudio/Level Tools/Group Objects %#g")]
        public static void initGroupObjects()
        {
            Debug.Log("Grpuping Objects");
            PGS_ObjectGrouperEditor.InitWindow();
        }
        [MenuItem("PGStudio/Scene Manager/Create UI Core Utility")]
        public static void CreateUICoreUtility()
        {

            PGS_SceneLoaderHelper.CreateUICore();
        }
        
        [MenuItem("PGStudio/Scene Manager/Create Level Group %#l")]
        public static void CreateLevelGroup()
        {
            Debug.Log("Creating Level Group");
            PGS_SceneHelpers.CreateLevelGroup();
        }
        [MenuItem("PGStudio/Scene Manager/Create Loading Screen")]
        public static void CreateLoadingScreen()
        {
            PGS_SceneLoaderHelper.CreateLoadScene();
        }
        [MenuItem("PGStudio/Systems/Create NPC Dialogue System")]
        public static void CreateNPCDialogueManager()
        {
            PGS_SceneLoaderHelper.CreateNPCDialogue();
        }
        [MenuItem("PGStudio/Systems/Create Quest System")]
        public static void CreateQuestSystem()
        {
            PGS_SceneLoaderHelper.CreateQuestSystem();
        }
        [MenuItem("PGStudio/Scene Manager/Create Minimap with FPS Controller")]
        public static void CreateMinimap()
        {
            PGS_SceneLoaderHelper.CreateMinimap();
        }
        [MenuItem("PGStudio/Level Tools/Rename Objects %#r")]
        public static void RenameSelectedObjects()
        {
            Debug.Log("Renaming Objects");
            PGS_ObjectRenameEditor.LaunchRenamer();
        }
        [MenuItem("PGStudio/Level Tools/Replace Selected Objects")]
        public static void ReplaceSelectedObjects()
        {
            Debug.Log("Launching Object Replacer");
            PGS_ReplaceObjectsEditor.LaunchReplacer();
        }
        [MenuItem("PGStudio/Cameras/Basic Follow Camera %#z")]
        public static void CreateBasicFollowCamera()
        {
            Debug.Log("Creating Basic Follow Camera");
            GameObject cameraGO = new GameObject("BasicFollowCamera", typeof(Camera),typeof(AudioListener), typeof(PGS_BasicFollowCamera));
            Camera cam = cameraGO.GetComponent<Camera>();
            cam.fieldOfView = 80f;
            cam.tag = "MainCamera";
            Selection.activeGameObject = cameraGO;            
        }
        [ExecuteInEditMode]
        [MenuItem("PGStudio/Cameras/Advanced Follow Camera %#x")]
        public static void CreateAdvancedFollowCamera()
        {
            Debug.Log("Creating Advanced Follow Camera ");
            GameObject cameraGO = new GameObject("AdvancedFollowCamera", typeof(Camera), typeof(AudioListener),typeof(PGS_AdvancedFollowCamera));
            Camera cam = cameraGO.GetComponent<Camera>();
            cam.fieldOfView = 80f;
            cam.tag = "MainCamera";
            Selection.activeGameObject = cameraGO;
            
        }
    } 
}
