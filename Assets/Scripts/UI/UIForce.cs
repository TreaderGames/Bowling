using UnityEngine.UI;
using UnityEngine;

public class UIForce : MonoBehaviour
{
    [SerializeField] Scrollbar forceBar;
    [SerializeField] [Range(0.1f, 0.9f)] float maxPoint;
    [SerializeField] float barSpeed;

    bool canUpdate = true;

    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        forceBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndForceStage();
        }

        if(canUpdate)
        {
            UpdateHandle();
        }
    }
    #endregion

    #region Private

    private void UpdateHandle()
    {
        if (forceBar.value < 1)
        {
            forceBar.value += Time.deltaTime * barSpeed;
        }
        else
        {
            EndForceStage();
        }
    }

    private void EndForceStage()
    {
        float force = GetForceMultiplier(forceBar.value);
        //Debug.LogError("Force: " + force);
        EventController.TriggerEvent(EventID.EVENT_FORCE_DECIDED, force);
        StateHandler.Instance.ChangeState(GameState.Placement);
        canUpdate = false;
    }

    private float GetForceMultiplier(float value)
    {
        float multiplier = maxPoint / (Mathf.Abs(value - maxPoint) + maxPoint);
        //Debug.LogError("GetForceMultiplier: " + value + " maxPoint:" + maxPoint + " multiplier: " + multiplier);
        return multiplier;
    }
    #endregion
}
