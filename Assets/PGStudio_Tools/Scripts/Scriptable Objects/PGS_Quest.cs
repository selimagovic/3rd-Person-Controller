using System;
using UnityEngine;

namespace PowerGameStudio.QuestSystem
{
    [Serializable]
    public class Step
    {
        public string[] dialogues;
    }
    [CreateAssetMenu(fileName = "QuestData", menuName = "PGStudio/QuestBuilder")]
    public class PGS_Quest : ScriptableObject
    {
        #region Variables
        public int id;
        public string questName;
        public string description;
        public Step [] questSteps;
        public int reward;
        private int _currentStep = -1;
        [SerializeField]
        private int[] _reqiredQuestCompletion = null;
        #endregion
        #region --Public Custom Methods--
        public bool QuestComplete()
        {
            return _currentStep == questSteps.Length;
        }

        public int[] GetRequirements()
        {
            return _reqiredQuestCompletion;
        }
        #endregion
    } 
}
