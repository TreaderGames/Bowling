using UnityEngine;

public class BallDirection : MonoBehaviour
{
    #region Unity
    private void OnEnable()
    {
        ScreenLoader.Instance.LoadScreen(ScreenType.Direction, null);
    }

    private void OnDisable()
    {
        ScreenLoader.Instance?.RemoveCurrentScreen();
    }
    #endregion
}
