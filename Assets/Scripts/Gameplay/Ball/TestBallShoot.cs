using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallShoot : MonoBehaviour
{
    [SerializeField] Rigidbody ball;
    [SerializeField] float forceScaler;

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_DIRECTION_DECIDED, HandleDirectionDecided);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_DIRECTION_DECIDED, HandleDirectionDecided);
    }

    private void HandleDirectionDecided(object arg)
    {
        Vector3 direction = (Vector3)arg;
        ball.isKinematic = false;
        direction.z = direction.y;
        direction.y = 0;
        ball.AddForce(direction * forceScaler);
    }
}
