using UnityEngine;

public class BallForce : MonoBehaviour
{
    #region Unity
    private void OnEnable()
    {
        ScreenLoader.Instance.LoadScreen(ScreenType.Force, null);
    }

    private void OnDisable()
    {
        ScreenLoader.Instance?.RemoveCurrentScreen();
    }
    #endregion
}
