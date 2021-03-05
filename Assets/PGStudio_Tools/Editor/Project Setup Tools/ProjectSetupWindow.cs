using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace PowerGameStudio.Tools
{
    public class ProjectSetupWindow : EditorWindow
    {
        #region Variables
        static ProjectSetupWindow window;

        private string gameName = "Game";
        #endregion

        #region Main Methods
        public static void InitWindow()
        {
            window = EditorWindow.GetWindow<ProjectSetupWindow>("Project Setup");
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            gameName =EditorGUILayout.TextField("Game Name: ",gameName);

            EditorGUILayout.EndHorizontal();

            if(GUILayout.Button("Create Project Structure",GUILayout.Height(40),GUILayout.ExpandWidth(true)))
            {
                CreateProjectFolders();
            }

            if(window!=null)
                window.Repaint();
        }
        #endregion

        #region Custom Methods
        void CreateProjectFolders()
        {
            if (string.IsNullOrEmpty(gameName))
                return;

            if(gameName == "Game")
            {
                if(!EditorUtility.DisplayDialog("Project Setup Warning", "Do You Really want to call your project 'Game'", "Yes", "No"))
                {
                    CloseWindow();
                    return;
                }
            }

            string assetPath = Application.dataPath;
            string rootPath = assetPath + "/" + gameName;

            DirectoryInfo rootInfo = Directory.CreateDirectory(rootPath);
           
            // Create Sub Directories
            if(rootInfo.Exists)
            {
                CreateSubFolders(rootPath);
            }            
            AssetDatabase.Refresh();
            CloseWindow();
        }
        void CreateSubFolders(string rootPath)
        {
            DirectoryInfo rootinfo = null;
            List<string> folderNames = new List<string>();

            // Creating Directory for Art
            rootinfo = Directory.CreateDirectory(rootPath + "/Art");
            if (rootinfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Animation");
                folderNames.Add("Audio");
                folderNames.Add("Objects/Models");
                folderNames.Add("Materials");
                folderNames.Add("Prefabs");
                folderNames.Add("Textures");

                CreateFolders(rootPath + "/Art", folderNames);
            }


            // Creating Directory for Code
            rootinfo = Directory.CreateDirectory(rootPath + "/Scripts");
            if (rootinfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Editor");
                folderNames.Add("Shaders");
                CreateFolders(rootPath + "/Scripts", folderNames);
            }
            // Creating Directory for Resoruces
            rootinfo = Directory.CreateDirectory(rootPath + "/Resoruces");
            if (rootinfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Characters");
                folderNames.Add("Managers");
                folderNames.Add("Props");
                folderNames.Add("UI");

                CreateFolders(rootPath + "/Resoruces", folderNames);
            }
            // Creating Directory for Prefabs
            rootinfo = Directory.CreateDirectory(rootPath + "/Prefabs");
            if (rootinfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Characters");
                folderNames.Add("Props");
                folderNames.Add("UI");

                CreateFolders(rootPath + "/Prefabs", folderNames);
            }

            // Create Scenes
            DirectoryInfo sceneInfo = Directory.CreateDirectory(rootPath + "/Scenes");
            if (sceneInfo.Exists)
            {
                // Create base level scenes needed for a simple game
                Scene currentGameScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(currentGameScene, "Assets/"+gameName+"/Scenes/Game.unity", true);

                Scene currentMainScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(currentMainScene, "Assets/" + gameName + "/Scenes/Main.unity", true);

                Scene currentStartupScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(currentStartupScene, "Assets/" + gameName + "/Scenes/Startup.unity", true);
            }
            DirectoryInfo devSceneInfo = Directory.CreateDirectory(rootPath + "/Scenes/Development");
            if(devSceneInfo.Exists)
            {
                // Create base level scenes needed for a simple game
                Scene currentDevelopmentScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(currentDevelopmentScene, "Assets/"+gameName + "/Scenes/Development/_Development.unity", true);

            }
        }

        void CreateFolders(string aPath, List<string> folders)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                Directory.CreateDirectory(aPath + "/" + folders[i]);
            }

        }

        void CloseWindow()
        {
            if (window)
            {
                window.Close();
            }
        }
        #endregion
    }
}
