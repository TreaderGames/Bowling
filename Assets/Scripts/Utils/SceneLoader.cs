using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System;

public class SceneLoader : MonoBehaviour
{
    #region Unity
    private void Start()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);
        asyncOperation.completed += HandlePersistentLoaded;
    }
    #endregion

    #region Callback

    private void HandlePersistentLoaded(AsyncOperation obj)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        asyncOperation.completed += HandleMainSceneLoaded;
    }
    private void HandleMainSceneLoaded(AsyncOperation obj)
    {
        SceneManager.UnloadSceneAsync("Init");
    }
    #endregion
}
