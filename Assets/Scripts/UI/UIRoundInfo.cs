using TMPro;
using UnityEngine;

public class UIRoundInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _roundText;
    [SerializeField] TextMeshProUGUI _materialText;
    [SerializeField] TextMeshProUGUI _pinsDropText;
    [SerializeField] TextMeshProUGUI _scoreText;

    #region Public
    public void Initialize(int round, PlayerDataController.PlayerRoundScore playerRoundScore)
    {
        _roundText.text = "Round " + round;
        _materialText.text = playerRoundScore.materialType.ToString();
        _pinsDropText.text = playerRoundScore.pinsDropped.ToString();
        _scoreText.text = playerRoundScore.score.ToString();
    }
    #endregion
}
