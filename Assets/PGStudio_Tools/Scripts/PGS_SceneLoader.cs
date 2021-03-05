using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PGS_SceneLoader : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Image _progresBar = null;
    [SerializeField]
    private int _sceneIndex = 0;
    #endregion
    #region Builtin Methods
    private void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }
    #endregion
    #region Custom Methods
    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation levelProgress = SceneManager.LoadSceneAsync(_sceneIndex);

        while(levelProgress.progress<1)
        {
            _progresBar.fillAmount = levelProgress.progress;
            yield return new WaitForEndOfFrame();

        }
    }
    #endregion
}
