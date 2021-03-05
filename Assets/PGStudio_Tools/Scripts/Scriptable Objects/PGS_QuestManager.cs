using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerGameStudio.QuestSystem;
using System.Linq;

namespace PowerGameStudio.Systems.Quest
{
    public class PGS_QuestManager : MonoBehaviour
    {
        #region Variables
        private static PGS_QuestManager _instance;

        //Designers Only
        [SerializeField]
        private List<PGS_Quest> _questData = new List<PGS_Quest>();

        private Dictionary<int, PGS_Quest> _quests = new Dictionary<int, PGS_Quest>();

        public static PGS_QuestManager Instance { get => _instance;}

        #endregion
        #region Builtin Methods
        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            for (int i = 0; i < _questData.Count; i++)
            {
                _quests.Add(_questData[i].id,_questData[i]);
            }

        }
        #endregion
        #region --Public Custom Methods--
        public int RequestQuest(int questId)
        {
            var quest = _quests[questId];

            //BRUTE FORCE IMPLEMENTATION
                //bool metRequirements = true;
            
            //check for requirements
            var requirements = quest.GetRequirements();

            //[2,3,7]
            var playerCompletedQuest = GameObject.FindObjectOfType<PGS_Player>().completedQuest;

            var requiremntesCheck = playerCompletedQuest.Except(requirements);

            if (requiremntesCheck.Count() > 0)
            {
                return -1;
                //havent met the requirements
            }
            else
            {
                return questId;
            }

            //BRUETE FORCE IMPLEMETATION
            /*
            for (int i = 0; i < playerCompletedQuest.Count; i++)
            {
                if (playerCompletedQuest.Contains(requirements[i]))
                {
                    continue;
                }
                else
                {
                    metRequirements = false;
                    Debug.Log("Quest is not completed: " + requirements[i]);
                    break;
                }
            }

            if (metRequirements == true)
            {
                return questId;
            }*/
            //fire of another event to stop the removal of questID being issued
            //return -1;
        }
        #endregion
        #region --Private Custom Methods--

        #endregion

    } 
}
