using TMPro;
using UnityEngine;

public class UILogin : MonoBehaviour
{
    [SerializeField] TMP_InputField textArea;

    #region Public
    public void OnClickStart()
    {
        PlayerDataController.pInstance.playerData.name = textArea.text;
        StateHandler.Instance.ChangeState(GameState.Direction);
        Destroy(gameObject);
    }
    #endregion
}
