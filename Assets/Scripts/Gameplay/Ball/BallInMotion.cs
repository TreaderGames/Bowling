using UnityEngine;

public class BallInMotion : MonoBehaviour, ITakeData
{
    BallData ballData;

    #region Public
    public void SendData(object args)
    {
        ballData = (BallData)args;
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
