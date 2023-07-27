using UnityEngine;

public class BallData
{
    private Vector3 _direction;
    private float _ballForce;

    public float pBallForce { get => _ballForce; set => _ballForce = value; }

    #region Public
    public Vector3 GetBallDirection()
    {
        return Vector3.Normalize(new Vector3(_direction.x, 0, _direction.y));
    }

    public void UpdateBallDirection(Vector3 direction)
    {
        _direction = direction;
    }
    #endregion
}
