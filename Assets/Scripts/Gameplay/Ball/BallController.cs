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
    #region Unity
    private void OnEnable()
    {
        StateHandler.Instance.AddStateListener(this);
        StateHandler.Instance.ChangeState(GameState.Direction);
    }

    private void OnDisable()
    {
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
    #endregion
}
