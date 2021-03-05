using System.Collections;
using UnityEngine;
namespace PowerGameStudio.UI
{

    public class Page : MonoBehaviour
    {
        #region Variables
        public static readonly string FLAG_ON = "On";
        public static readonly string FLAG_OFF = "Off";
        public static readonly string FLAG_RESET = "Reset";

        public PageType type;
        public bool debug;
        public bool useAnimation = false;
        private bool m_IsOn;

        public bool isOn
        {
            get
            {
                return m_IsOn;
            }
            private set
            {
                m_IsOn = value;
            }
        }
        public string targetState { get; private set; }

        private Animator m_animator;
        #endregion
        #region Builtin Methods
        private void OnEnable()
        {
            CheckAnimatorIntegrity();
        }
        #endregion
        #region Custom Methods
        #region Public Functions

        public void Animate(bool _on)
        {
            if(useAnimation)
            {
                m_animator.SetBool("on", _on);
                StopCoroutine("AwaitAnimation");
                StartCoroutine("AwaitAnimation", _on);
            }
            else
            {
                if(!_on)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        #endregion
        #region Private Functions
        private IEnumerator AwaitAnimation(bool _on)
        {
            targetState = _on ? FLAG_ON : FLAG_OFF;

            // wait for animator to reach the target state
            while(!m_animator.GetCurrentAnimatorStateInfo(0).IsName(targetState))
            {
                yield return null;
            }
            // wait for animator to finish animator
            while (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime <1)
            {
                yield return null;
            }

            targetState = FLAG_RESET;

            Log("Page [" + type + "] finished transitioning to " + (_on ? "on" : "off"));
            if(!_on)
            {
                gameObject.SetActive(false);
            }
        }

        private void CheckAnimatorIntegrity()
        {
            if(useAnimation)
            {
                m_animator = GetComponent<Animator>();
                if(!m_animator)
                {
                    LogWarning("You opted to animate a page ["+type+"] but no Animator component exists on the object");
                    return;
                }
            }
        }

        private void Log(string _msg)
        {
            if (!debug) return;
            Debug.Log("[Page]: " + _msg);
        }
        private void LogWarning(string _msg)
        {
            if (!debug) return;
            Debug.LogWarning("[Page]: " + _msg);
        }
        #endregion
        #endregion
    }

}