using UnityEngine;

namespace PowerGameStudio.Systems.Dialogue
{
    public class PGS_NPC : MonoBehaviour
    {
        #region Variables  
        [SerializeField]
        private PGS_Dialogue _myDialogue = null;
        #endregion
        #region Builtin Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                //do i have anything to say?
                bool canTalk = _myDialogue.messages.Length > 0;
                PGS_DialogueManager.Instance.ActiveNPC(_myDialogue, canTalk);
            }
        }
        #endregion
    }
}
