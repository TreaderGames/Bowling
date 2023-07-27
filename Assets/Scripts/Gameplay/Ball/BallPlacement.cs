using UnityEngine;

public class BallPlacement : MonoBehaviour, ITakeData
{
    [SerializeField] Renderer _placementPlane;
    [SerializeField] MoveTransformWithinPlaneBounds _moveTransform;

    #region Unity
    private void OnDisable()
    {
        _moveTransform.StopMovement();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StateHandler.Instance.ChangeState(GameState.BallInMotion);
        }
    }
    #endregion

    #region Public
    public void SendData(object args)
    {
        Transform ball = (Transform)args;
        _moveTransform.StartMovement(_placementPlane.bounds, ball);
    }

    #endregion
}
