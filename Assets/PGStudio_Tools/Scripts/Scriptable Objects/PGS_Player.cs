using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PowerGameStudio.Systems.Quest
{

    public class PGS_Player : MonoBehaviour
    {
        #region Variables
        public List<int> completedQuest;
        public List<int> activeQuest;


        #endregion
        #region Builtin Methods
        private void Start()
        {
            NPC.onQuestIssued += NPC_onQuestIssued;
        }

        private void NPC_onQuestIssued(int questID)
        {
            var id = PGS_QuestManager.Instance.RequestQuest(questID);
            if(id!= -1)
            {
                activeQuest.Add(id); 
                Debug.Log("Added Quest ID: "+id);

            }
            else
            {
                Debug.Log("Invalid Requrements");
            }
        }
        #endregion
        #region --Public Custom Methods--

        #endregion
        #region --Private Custom Methods--

        #endregion

    } 
}
