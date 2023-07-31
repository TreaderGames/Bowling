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

    [SerializeField] Rigidbody _ball;
    [SerializeField] Collider _ballCollider;
    [SerializeField] BallStatesData[] _ballStateObjects;
    [SerializeField] float _ballMaxForce;
    [SerializeField] BallDataCollection _ballDataCollection;

    BallLaunchData _ballData;
    Vector3 _ballStartPosition;
    BallDataCollection.MaterialType[] _materialTypes;
    int _currentBallIndex = 0;

    #region Unity
    private void Awake()
    {
        _ballData = new BallLaunchData();
        _ballData.SetBallRigidBody(_ball);
        _ballStartPosition = _ball.position;
    }

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_DIRECTION_DECIDED, HandleDirectionDecided);
        EventController.StartListening(EventID.EVENT_FORCE_DECIDED, HandleForceDecided);
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
        EventController.StartListening(EventID.EVENT_MATCH_END, HandleMatchEnd);

        StateHandler.Instance.AddStateListener(this);
        StateHandler.Instance.ChangeState(GameState.None);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_DIRECTION_DECIDED, HandleDirectionDecided); 
        EventController.StopListening(EventID.EVENT_FORCE_DECIDED, HandleForceDecided);
        EventController.StopListening(EventID.EVENT_TURN_END, HandleTurnEnd);
        EventController.StopListening(EventID.EVENT_MATCH_END, HandleMatchEnd);

        StateHandler.Instance?.RemoveStateListener(this);
    }

    private void Start()
    {
        _materialTypes = PlayerDataController.pInstance.playerData.playerBallCollection;
        System.Random random = new System.Random();
        random.Shuffle(_materialTypes);
        ResetBall();
    }

    #endregion

    #region Private
    private void HandleStateChange(GameState gameState)
    {
        foreach(BallStatesData ballStatesData in _ballStateObjects)
        {
            if (ballStatesData.state.Equals(gameState))
            {
                ballStatesData.stateObject.gameObject.SetActive(true);
                try
                {
                    ITakeData takeData = ballStatesData.stateObject as ITakeData;
                    if (gameState.Equals(GameState.Placement))
                    {
                        takeData?.SendData(_ball.transform);
                    }
                    else if (gameState.Equals(GameState.BallInMotion))
                    {
                        takeData?.SendData(_ballData);
                    }
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

    private void ResetBall()
    {
        if (_currentBallIndex < _materialTypes.Length)
        {
            BallDataCollection.BallData ballData = _ballDataCollection.GetBallData(_materialTypes[_currentBallIndex]);
            _ballCollider.sharedMaterial = ballData.physicMaterial;
            PlayerDataController.pInstance.UpdateCurrentMaterial(_materialTypes[_currentBallIndex]);
        }
        _ball.isKinematic = true;
        _ball.position = _ballStartPosition;
    }

    #endregion

    #region Callbacks
    public void StateChanged(GameState gameState)
    {
        HandleStateChange(gameState);
    }

    private void HandleDirectionDecided(object arg)
    {
        _ballData.UpdateBallDirection((Vector3)arg);
    }

    private void HandleForceDecided(object arg)
    {
        _ballData.pBallForce = _ballMaxForce * (float)arg;
    }

    private void HandleTurnEnd(object arg)
    {
        _currentBallIndex++;
        ResetBall();

        if (_currentBallIndex < GameConfig.MAX_TURNS)
        {
            StateHandler.Instance.ChangeState(GameState.Direction);
        }
        else
        {
            StateHandler.Instance.ChangeState(GameState.None);
        }
    }

    private void HandleMatchEnd(object arg)
    {
        _currentBallIndex = 0;
        ResetBall();
        ScreenLoader.Instance.LoadScreen(ScreenType.GameOver, null);
    }
    #endregion
}
