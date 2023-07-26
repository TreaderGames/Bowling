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
    #endregion

    #region Public
    public void SendData(Object args)
    {
        Transform ball = (Transform)args;
        _moveTransform.StartMovement(_placementPlane.bounds, ball);
    }

    #endregion
}
