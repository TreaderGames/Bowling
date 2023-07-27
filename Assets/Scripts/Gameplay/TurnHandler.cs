using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] float turnEndDelay = 5;

    float currentDelta;
    bool canRunDelay;
    #region Unity
    private void OnEnable()
    {
        currentDelta = 0;
        EventController.StartListening(EventID.EVENT_BALL_OUT_OF_PLAY, HandleBallOutOfPlay);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_BALL_OUT_OF_PLAY, HandleBallOutOfPlay);
    }

    private void Update()
    {
        if (canRunDelay)
        {
            currentDelta += Time.deltaTime;
            if(currentDelta >= turnEndDelay)
            {
                currentDelta = 0;
                canRunDelay = false;
                EventController.TriggerEvent(EventID.EVENT_TURN_END);
            }
        }
    }

    #endregion

    #region Callback
    private void HandleBallOutOfPlay(object arg)
    {
        canRunDelay = true;
    }
    #endregion
}
