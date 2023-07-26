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
    }

    private void OnDisable()
    {
        StateHandler.Instance.RemoveStateListener(this);
    }

    #endregion

    #region Private
    private void HandleStateChange(GameState gameState)
    {
        foreach(BallStatesData ballStatesData in ballStateObjects)
        {
            if(ballStatesData.Equals(gameState))
            {
                ballStatesData.stateObject.gameObject.SetActive(true);
                ballStatesData.stateObject.GetComponent<ITakeData>().SendData(ball.transform);
            }

            ballStatesData.stateObject.gameObject.SetActive(false);
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
