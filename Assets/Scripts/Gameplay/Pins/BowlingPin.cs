using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    Vector3 _initialPos;
    Quaternion _initRot;
    #region Unity
    private void Start()
    {
        _initialPos = transform.position;
        _initRot = transform.rotation;
    }
    #endregion

    #region Public
    public void ResetPin()
    {
        transform.position = _initialPos;
        transform.rotation = _initRot;
    }
    #endregion
}
