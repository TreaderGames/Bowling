using UnityEngine;

public class BallInMotion : MonoBehaviour, ITakeData
{
    BallLaunchData ballData;

    #region Public
    public void SendData(object args)
    {
        ballData = (BallLaunchData)args;
        PlayTurn();
    }
    #endregion

    #region Private
    private void PlayTurn()
    {
        ballData.pBall.isKinematic = false;
        ballData.pBall.AddForce(ballData.pBallForce * ballData.GetBallDirection());
    }
    #endregion
}
