using UnityEngine;

public class BallCollider : MonoBehaviour
{
    [SerializeField] LayerMask backTrayLayer;

    #region Unity
    private void OnCollisionEnter(Collision collision)
    {
        if(((1<<collision.gameObject.layer) & backTrayLayer) != 0)
        {
            EventController.TriggerEvent(EventID.EVENT_BALL_OUT_OF_PLAY);
        }
    }
    #endregion
}
