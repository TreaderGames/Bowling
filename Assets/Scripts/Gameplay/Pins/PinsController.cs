using System;
using UnityEngine;

public class PinsController : MonoBehaviour
{
    [SerializeField] BowlingPin[] _bowlingPins;
    [SerializeField] float _pinDropThreshold = 0.8f;
    int _ballTouchCount = 0;
    int _pinDropCount = 0;

    #region Unity
    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    #endregion

    #region Private
    private void ResePins()
    {
        _ballTouchCount = 0;
        _pinDropCount = 0;
        for (int i = 0; i < _bowlingPins.Length; i++)
        {
            if (_bowlingPins[i].hasCollided)
            {
                _ballTouchCount++;
            }
            if(Vector3.Dot(_bowlingPins[i].transform.up, Vector3.up) < _pinDropThreshold)
            {
                _pinDropCount++;
            }

            _bowlingPins[i].ResetPin();
        }

        PlayerDataController.pInstance.UpdateScore(_pinDropCount, _ballTouchCount);
        Debug.LogError("Ball Touch Count: " + _ballTouchCount + " Pin drops: " + _pinDropCount);
    }
    #endregion

    #region Callbacks
    private void HandleTurnEnd(object arg)
    {
        ResePins();
    }
    #endregion
}
