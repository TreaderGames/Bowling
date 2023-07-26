using UnityEngine;

public class PingPongRotationTween : MonoBehaviour
{
    [SerializeField] Vector3 _pointARotation;
    [SerializeField] Vector3 _pointBRotation;
    [SerializeField] float _speed;
    [SerializeField] bool _canPlay = false;

    float _currentDelta = 0.5f;
    bool _ping;

    #region Unity;
    // Update is called once per frame
    void Update()
    {
        if(_canPlay)
        {
            PlayLoopAnimation();
        }
    }
    #endregion

    #region Public
    public void PlayPingPong()
    {
        _canPlay = true;
    }

    public void StopPingPong()
    {
        _canPlay = false;
    }
    #endregion

    #region Private 
    private void PlayLoopAnimation()
    {
        Vector3 targetRotation;
        Vector3 fromRotation;
        if (_currentDelta >= 1 || _currentDelta < 0)
        {
            _ping = !_ping;
            _currentDelta = 0;
        }

        targetRotation = _ping ? _pointARotation : _pointBRotation;
        fromRotation = !_ping ? _pointARotation : _pointBRotation;

        _currentDelta += Time.deltaTime * _speed;
        transform.localEulerAngles = Vector3.Lerp(fromRotation, targetRotation, _currentDelta);
    }
    #endregion
}
