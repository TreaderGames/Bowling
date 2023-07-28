using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    public bool hasCollided = false;

    Vector3 _initialPos;
    Quaternion _initRot;

    #region Unity
    private void Start()
    {
        _initialPos = transform.position;
        _initRot = transform.rotation;
        Debug.LogError("Rot Dist: " + Vector3.Dot(transform.up, Vector3.up));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Ball"))
        {
            hasCollided = true;
        }
    }
    #endregion

    #region Public
    public void ResetPin()
    {
        transform.position = _initialPos;
        transform.rotation = _initRot;
        hasCollided = false;
    }
    #endregion
}
