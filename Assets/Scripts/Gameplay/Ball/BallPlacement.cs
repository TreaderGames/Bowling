using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlacement : MonoBehaviour, ITakeData
{
    [SerializeField] Renderer placementPlane;

    Transform ball;

    #region Public
    public void SendData(Object args)
    {
        ball = (Transform)args;
    }

    #endregion

    #region Private
    #endregion
}
