using UnityEngine;

public class MoveTransformWithinPlaneBounds : MonoBehaviour
{
    [SerializeField] float speed;

    bool _canMove = false;
    Transform _objectToMove;

    Vector3 _minBounds, _maxBounds;

    #region Unity
    private void Update()
    {
        if(_canMove)
        {
            DoMove();
        }
    }
    #endregion

    #region Public
    public void StartMovement(Bounds bounds, Transform transform)
    {
        _canMove = true;
        _objectToMove = transform;

        _objectToMove.position = bounds.center;
        _minBounds = bounds.min;
        _maxBounds = bounds.max;
    }

    public void StopMovement()
    {
        _canMove = false;
    }
    #endregion

    #region Private
    private void DoMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal == 0 && vertical == 0)
        {
            return;
        }

        Vector3 newPosition;
        newPosition.x = (horizontal * speed * Time.deltaTime) + _objectToMove.position.x;
        newPosition.y = vertical * speed * Time.deltaTime + _objectToMove.position.y;
        newPosition.z = _objectToMove.position.z;

        newPosition.x = Mathf.Clamp(newPosition.x, _minBounds.x, _maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, _minBounds.y, _maxBounds.y);

        _objectToMove.position = newPosition;
    }
    #endregion
}
