using UnityEngine;

public class BallLaunchData
{
    private Vector3 _direction;

    public float pBallForce { get; set; }
    public Rigidbody pBall { get; private set; }

    #region Public
    public Vector3 GetBallDirection()
    {
        return Vector3.Normalize(new Vector3(_direction.x, 0, _direction.y));
    }

    public void SetBallRigidBody(Rigidbody rigidbody)
    {
        pBall = rigidbody;
    }

    public void UpdateBallDirection(Vector3 direction)
    {
        _direction = direction;
    }
    #endregion
}
