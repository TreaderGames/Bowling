using UnityEngine;

public class BallData
{
    private Vector3 _direction;

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
