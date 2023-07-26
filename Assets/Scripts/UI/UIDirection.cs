using UnityEngine;

public class UIDirection : MonoBehaviour
{
    [SerializeField] PingPongRotationTween pingPongRotationTween;
    [SerializeField] Transform arrow;

    #region Unity
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EventController.TriggerEvent(EventID.EVENT_DIRECTION_DECIDED, Vector3.Normalize(arrow.up));
            StateHandler.Instance.ChangeState(GameState.Placement);
        }
    }
    #endregion
}
