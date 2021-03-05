using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Implementation of Managers in POWERGAMESTUDIO namspace
/// </summary>
namespace PowerGameStudio.Systems.Dialogue
{
    public class PGS_DialogueManager : MonoBehaviour
    {

        #region Variables
        private static PGS_DialogueManager _instance;
        public static PGS_DialogueManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("PGS_DialogueManager is null.");
                }
                return _instance;
            }
        }
        [SerializeField]
        private GameObject _dilogueCam = null;
        [SerializeField]
        private GameObject _dialoguePanel = null;
        [SerializeField]
        private Text _dialogueText = null;     

        private PGS_Dialogue _activeDialogue;
        private bool _engageNPC;
        private int _currentMessage = 0;
        #endregion
        #region Builtin Methods
        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            if (_engageNPC)
            {
                _dilogueCam.SetActive(true);
                _dialoguePanel.SetActive(true);
                _dialogueText.text = _activeDialogue.messages[_currentMessage];
            }
        }
        #endregion

        #region --Public Custom Methods--
        public void Next()
        {
            _currentMessage++;
            //if current message is grater than or equal to the lenght
            if(_currentMessage >= _activeDialogue.messages.Length)
            {
                //close all panels
                _dilogueCam.SetActive(false);
                _dialoguePanel.SetActive(false);
                _activeDialogue = null;
                _engageNPC = false;
                _currentMessage = 0;
                return;
            }

            _dialogueText.text = _activeDialogue.messages[_currentMessage];
        }
        public void ActiveNPC(PGS_Dialogue npcDialogue,bool canTalk)
        {
            _activeDialogue = npcDialogue;
            _engageNPC = canTalk;
        }
        #endregion

    } 
}
