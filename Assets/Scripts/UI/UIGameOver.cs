using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] UIRoundInfo _roundInfoTemplate;
    [SerializeField] Transform _roundInfoParent;

    [SerializeField] TextMeshProUGUI _totalScoreText;

    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        PopulateRoundInfo();
    }
    #endregion

    #region Private
    private void PopulateRoundInfo()
    {
        int totalScore = 0;
        int index = 0;
        UIRoundInfo uIRoundInfo;
        List<PlayerDataController.PlayerRoundScore> playerRoundScores = PlayerDataController.pInstance.GetPlayerRoundScores();
        foreach(PlayerDataController.PlayerRoundScore playerRoundScore in playerRoundScores)
        {
            totalScore += playerRoundScore.score;
            uIRoundInfo = Instantiate<UIRoundInfo>(_roundInfoTemplate, _roundInfoParent);
            uIRoundInfo.Initialize(index, playerRoundScore);
            index++;
        }

        _totalScoreText.text = "Total score: " + totalScore;
    }
    #endregion

    #region Public
    public void OnClickPlayAgain()
    {
        StateHandler.Instance.ChangeState(GameState.Direction);
        ScreenLoader.Instance?.RemoveCurrentScreen();
    }
    #endregion
}
