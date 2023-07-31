using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGame : MonoBehaviour, IStateListener
{
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _pinsDropped;
    [SerializeField] TextMeshProUGUI _totalScore;
    [SerializeField] TextMeshProUGUI _currentMaterial;

    [SerializeField] TextMeshProUGUI[] _roundScores;

    #region Unity

    private void OnEnable()
    {
        StateHandler.Instance.AddStateListener(this);
        EventController.StartListening(EventID.EVENT_MATCH_END, HandleMatchEnd);
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_MATCH_END, HandleMatchEnd);
        EventController.StopListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    #endregion

    #region Private

    private void UpdateValues()
    {
        _nameText.text = "Name: " + PlayerDataController.pInstance.playerData.name;
        _currentMaterial.text = "Material: " + PlayerDataController.pInstance.GetCurrentMaterial().ToString();

        UpdateTotals();
    }

    private void UpdateTotals()
    {
        int totalScore = 0, totalPins = 0;
        List<PlayerDataController.PlayerRoundScore> playerRoundScores = PlayerDataController.pInstance.GetPlayerRoundScores();

        for (int i = 0; i < playerRoundScores.Count; i++)
        {
            totalScore += playerRoundScores[i].score;
            totalPins += playerRoundScores[i].pinsDropped;

            _roundScores[i].gameObject.SetActive(true);
            _roundScores[i].text = "Round " + (i + 1) + ": " + playerRoundScores[i].score.ToString();
        }

        _totalScore.text = "Score: " + totalScore.ToString();
        _pinsDropped.text = "Pins: " + totalPins.ToString();
    }

    private void ResetData()
    {
        for (int i = 0; i < _roundScores.Length; i++)
        {
            _roundScores[i].gameObject.SetActive(false);
        }
        _totalScore.text = "Score: 0";
        _pinsDropped.text = "Pins: 0";
    }
    #endregion

    #region Callback

    private void HandleMatchEnd(object arg)
    {
        ResetData();
    }

    private void HandleTurnEnd(object arg)
    {
        UpdateValues();
    }

    public void StateChanged(GameState gameState)
    {
        StateHandler.Instance.RemoveStateListener(this);
        if(gameState == GameState.Direction)
        {
            UpdateValues();
        }
    }
    #endregion
}
