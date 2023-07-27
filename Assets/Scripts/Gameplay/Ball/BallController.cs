using System;
using UnityEngine;

public class BallController : MonoBehaviour, IStateListener
{
    [Serializable]
    public struct BallStatesData
    {
        public GameState state;
        public Component stateObject;
    }

    [SerializeField] Rigidbody ball;
    [SerializeField] BallStatesData[] ballStateObjects;
    [SerializeField] float ballMaxForce;

    BallData ballData;
    #region Unity
    private void Awake()
    {
        ballData = new BallData();
    }

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_DIRECTION_DECIDED, HandleDirectionDecided);
        EventController.StartListening(EventID.EVENT_FORCE_DECIDED, HandleForceDecided);

        StateHandler.Instance.AddStateListener(this);
        StateHandler.Instance.ChangeState(GameState.Direction);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_DIRECTION_DECIDED, HandleDirectionDecided); 
        EventController.StopListening(EventID.EVENT_FORCE_DECIDED, HandleForceDecided);

        StateHandler.Instance?.RemoveStateListener(this);
    }

    #endregion

    #region Private
    private void HandleStateChange(GameState gameState)
    {
        foreach(BallStatesData ballStatesData in ballStateObjects)
        {
            if (ballStatesData.state.Equals(gameState))
            {
                ballStatesData.stateObject.gameObject.SetActive(true);
                try
                {
                    ITakeData takeData = ballStatesData.stateObject as ITakeData;
                    takeData?.SendData(ball.transform);
                }
                finally
                {
                    //Do Nothing this just means the ball state object dosent need any data
                }
            }
            else
            {
                ballStatesData.stateObject.gameObject.SetActive(false);
            }
        }
    }

    #endregion

    #region Callbacks
    public void StateChanged(GameState gameState)
    {
        HandleStateChange(gameState);
    }

    private void HandleDirectionDecided(object arg)
    {
        ballData.UpdateBallDirection((Vector3)arg);
    }

    private void HandleForceDecided(object arg)
    {
        ballData.pBallForce = ballMaxForce * (float)arg;
    }
    #endregion
}
