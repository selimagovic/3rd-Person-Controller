using PowerGameStudio.Systems.Dialogue;
using PowerGameStudio.Systems.Quest;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PowerGameStudio.UI
{

    public class PGS_SceneLoaderHelper : MonoBehaviour
    {
        public static void CreateLoadScene()
        {
            GameObject sceneLoader = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/PGStudio_Tools/Art/Prefabs/Loading_Canvas.prefab");
            if (sceneLoader)
            {
                GameObject createGroup = Instantiate(sceneLoader);
                createGroup.name = "Loading_Scene";
            }
            else
            {
                EditorUtility.DisplayDialog("UI tools warning", "Cannot find Loading_Canvas.prefab", "OK");
            }


        }
        /// <summary>
        /// this will create npcDialogue system to scene
        /// </summary>
        public static void CreateNPCDialogue()
        {
            GameObject npcGroup = new GameObject("NPCDialogue_GRP");
            npcGroup.transform.position = new Vector3(0, 0, 0);

            GameObject createManager = new GameObject("DialogueManager");
            createManager.transform.SetParent(npcGroup.transform);
            createManager.AddComponent(typeof(PGS_DialogueManager));
            createManager.name = "DialogueManager";
            GameObject dialogueCamera = new GameObject("DialogueCamera");
            dialogueCamera.transform.SetParent(npcGroup.transform);
            dialogueCamera.AddComponent(typeof(Camera));
            GameObject npcCharacter = new GameObject("NPC_Character");
            npcCharacter.transform.SetParent(npcGroup.transform);
            npcCharacter.AddComponent(typeof(BoxCollider));
            npcCharacter.AddComponent(typeof(PGS_NPC));
            GameObject dialoguePanel = AssetDatabase.LoadAssetAtPath<GameObject>
                ("Assets/PGStudio_Tools/Art/Prefabs/DialoguePanel.prefab");
            if(dialoguePanel)
            {
                GameObject canvasGO = new GameObject("NPC_Canvas");
                canvasGO.transform.SetParent(npcGroup.transform);
                canvasGO.AddComponent(typeof(Canvas));
                canvasGO.AddComponent(typeof(CanvasScaler));
                canvasGO.AddComponent(typeof(GraphicRaycaster));
                GameObject createGroup = (GameObject)Instantiate(dialoguePanel);
                createGroup.name = "PanelDialogue";
                createGroup.transform.SetParent(canvasGO.transform);
            }
            else
            {
                EditorUtility.DisplayDialog("DialoguePanel tools warning", "Cannot find DialoguePanel.prefab", "OK");
            }

        }

        public static void CreateQuestSystem()
        {
            GameObject questGRP = new GameObject("QuestSystem_GRP");
            questGRP.transform.position = new Vector3(0, 0, 0);

            GameObject createManager = new GameObject("QuestManager");
            createManager.transform.SetParent(questGRP.transform);
            createManager.AddComponent(typeof(PGS_QuestManager));
            createManager.name = "QuestManager";
            GameObject createNPC = new GameObject("NPC");
            createNPC.transform.SetParent(questGRP.transform);
            createNPC.AddComponent(typeof(Systems.Quest.NPC));
            createNPC.name = "NPC";
            GameObject createPlayer = new GameObject("Player");
            createPlayer.transform.SetParent(questGRP.transform);
            createPlayer.AddComponent(typeof(PGS_Player));
            createPlayer.name = "Player";
        }

        public static void CreateMinimap()
        {
            GameObject minimapGRP = new GameObject("Minimap_GRP");
            minimapGRP.transform.position = new Vector3(0, 0, 0);
            GameObject minimapCanvas = AssetDatabase.LoadAssetAtPath<GameObject>
                ("Assets/PGStudio_Tools/Art/Prefabs/UI/MiniMapCanvas.prefab");
            if(minimapCanvas)
            {
                GameObject minmapCan = Instantiate(minimapCanvas);
                minmapCan.transform.SetParent(minimapGRP.transform);
                minmapCan.name = "MiniMapCanvas";
            }
            else
            {
                EditorUtility.DisplayDialog("Scene Tools warning", "Cannot find MiniMapCanvas.prefab", "OK");
            }
            GameObject fpsController = AssetDatabase.LoadAssetAtPath<GameObject>
                ("Assets/PGStudio_Tools/Art/Prefabs/Characters/PGS_FPS_Controller.prefab");

            if(fpsController)
            {
                GameObject fps = Instantiate(fpsController);
                fps.transform.SetParent(minimapGRP.transform);
                fps.name = "PGS_FPS_Controller";
            }
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.transform.position = new Vector3(0, 0, 0);
            floor.transform.localScale = new Vector3(10, 1, 10);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>("Assets/PGStudio_Tools/Art/Materials/Plane_MAT.mat");
            floor.GetComponent<MeshRenderer>().material = mat;
            floor.name = "Floor";

        }
        public static void CreateUICore()
        {
            GameObject ui = AssetDatabase.LoadAssetAtPath<GameObject>(
               "Assets/PGStudio_Tools/Art/Prefabs/UI/UI.prefab");
            if (ui)
            {
                GameObject createGroup = (GameObject)Instantiate(ui);
                createGroup.name = "UI";
            }
            else
            {
                EditorUtility.DisplayDialog("UI tools warning", "Cannot find Loading_Canvas.prefab", "OK");
            }

            GameObject test = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/PGStudio_Tools/Art/Prefabs/Tests/Test.prefab");

            if (test)
            {
                GameObject createGroup = (GameObject)Instantiate(test);
                createGroup.name = "TEST";
            }
            else
            {
                EditorUtility.DisplayDialog("UI tools warning", "Cannot find Test.prefab", "OK");
            }
        }

    }
}
