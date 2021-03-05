using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PowerGameStudio.Systems.Quest
{
    public class NPC : MonoBehaviour
    {

        #region Variables
        public static event Action<int> onQuestIssued;

        [SerializeField]
        private List<int> _availableQuest = new List<int>();
        //private Queue<int> _availableQuest;
        #endregion
        #region Builtin Methods
        private void OnMouseDown()
        {

            //deliver a quest
            //try and give player the quest ID that's first available
            if (_availableQuest.Count > 0)
            {
                //give quest zero
                onQuestIssued?.Invoke(_availableQuest[0]);
                _availableQuest.Remove(0);
            }


        }
        #endregion
        #region --Public Custom Methods--

        #endregion
        #region --Private Custom Methods--

        #endregion
    }

}