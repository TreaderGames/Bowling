using System;
using UnityEngine;

public class PinsController : MonoBehaviour
{
    [SerializeField] BowlingPin[] _bowlingPins;

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
        for (int i = 0; i < _bowlingPins.Length; i++)
        {
            _bowlingPins[i].ResetPin();
        }
    }
    #endregion

    #region Callbacks
    private void HandleTurnEnd(object arg)
    {
        ResePins();
    }
    #endregion
}
