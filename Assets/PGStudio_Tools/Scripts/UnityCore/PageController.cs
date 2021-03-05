using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerGameStudio.UI
{
    public class PageController : MonoBehaviour
    {
        #region Variables
        public static PageController instance;

        public bool debug;
        public PageType entryPage;
        public Page[] pages;

        private Hashtable m_pages;
        #endregion
        #region Builtin Methods
        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                m_pages = new Hashtable();
                RegisterAllPages();
                if (entryPage != PageType.None)
                {
                    TurnPageOn(entryPage);
                }
            }

        }
        #endregion
        #region Custom Methods
        #region Public Functions
        public void TurnPageOn(PageType _type)
        {
            if (_type == PageType.None)
                return;
            if (!PageExists(_type))
            {
                LogWarning("You are trying to turn a page on [" + _type + "] that has not been registered");
                return;
            }
            Page _page = GetPage(_type);
            _page.gameObject.SetActive(true);
            _page.Animate(true);
        }
        public void TurnPageOff(PageType _off, PageType _on = PageType.None, bool _waitForExit = false)
        {
            if (_off == PageType.None) return;
            if (!PageExists(_off))
            {
                LogWarning("You are trying to turn a page off [" + _off + "] that has not been registered");
                return;
            }

            Page _offPage = GetPage(_off);
            if(_offPage.gameObject.activeSelf)
            {
                _offPage.Animate(false);
            }
            if(_on != PageType.None)
            {
                if (_waitForExit)
                {
                    Page _onPage = GetPage(_on);
                    StopCoroutine("WaitForPageExit");
                    StartCoroutine(WaitForPageExit(_onPage, _offPage));
                }
                else
                {
                    TurnPageOn(_on);
                }
            }
        }

        public bool PageIsOn(PageType _type)
        {
            if (!PageExists(_type))
            {
                LogWarning("You are trying to detect if a page is on [" + _type + "], but it has not been registered.");
                return false;
            }

            return GetPage(_type).isOn;
        }
        #endregion
        #region Private Functions

        private IEnumerator WaitForPageExit(Page _on, Page _off)
        {
            while (_off.targetState != Page.FLAG_RESET)
            {
                yield return null;
            }
            TurnPageOn(_on.type);
        }

        private void RegisterAllPages()
        {
            for (int i = 0; i < pages.Length; i++)
            {
                RegisterPage(pages[i]);
            }
        }
        private void RegisterPage(Page _page)
        {
            if (PageExists(_page.type))
            {
                LogWarning("You are trying to register a page on [" + _page.type + "] that has already been registered: " + _page.gameObject.name);
                return;
            }

            m_pages.Add(_page.type, _page);
            Log("Registered new Page [" + _page.type + "]");
        }
        private Page GetPage(PageType _type)
        {
            if (!PageExists(_type))
            {
                LogWarning("You are trying to get a page [" + _type + "] that has not been registered.");
                return null;
            }

            return (Page)m_pages[_type];
        }
        private bool PageExists(PageType _pageType)
        {
            return m_pages.ContainsKey(_pageType);
        }
        private void Log(string _msg)
        {
            if (!debug) return;
            Debug.Log("[PageController]: " + _msg);
        }
        private void LogWarning(string _msg)
        {
            if (!debug) return;
            Debug.LogWarning("[PageController]: " + _msg);
        }
        #endregion
        #endregion

    }
}

