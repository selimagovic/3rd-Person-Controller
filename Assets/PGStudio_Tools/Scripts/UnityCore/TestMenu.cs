using UnityEngine;
namespace PowerGameStudio.UI
{

    public class TestMenu : MonoBehaviour
    {

        #region Variables
        public PageController pageController;
        #endregion

#if UNITY_EDITOR
        #region Builtin Methods
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                pageController.TurnPageOn(PageType.Loading);
            }
            if (Input.GetKeyUp(KeyCode.G))
            {
                pageController.TurnPageOff(PageType.Loading);
            }

            if(Input.GetKeyUp(KeyCode.H))
            {
                pageController.TurnPageOff(PageType.Loading, PageType.Menu);
            }

            if (Input.GetKeyUp(KeyCode.J))
            {
                pageController.TurnPageOff(PageType.Loading, PageType.Menu,true);
            }
        }

        #endregion

        #region Custom Methods
        #endregion

#endif
    }

}