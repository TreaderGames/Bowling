using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    [Serializable]
    public class PlayerData
    {
        public BallDataCollection.MaterialType[] playerBallCollection;
        public string name;
    }

    public struct PlayerRoundScore
    {
        public BallDataCollection.MaterialType materialType;
        public int pinsDropped;
        public int score;

        public PlayerRoundScore(int inScore, int inPinsDropped, BallDataCollection.MaterialType inMaterialType)
        {
            materialType = inMaterialType;
            pinsDropped = inPinsDropped;
            score = inScore;
        }
    }

    [SerializeField] BallDataCollection.MaterialType[] defaultBallCollection = new BallDataCollection.MaterialType[GameConfig.MAX_TURNS];
    public PlayerData playerData;

    public static PlayerDataController pInstance { get; private set; }

    private List<PlayerRoundScore> playerRoundScores = new List<PlayerRoundScore>();

    BallDataCollection.MaterialType _currentMaterial;

    #region Unity
    private void Awake()
    {
        if(pInstance == null)
        {
            pInstance = this;
        }
    }

    private void Start()
    {
        PreparePlayerData();
    }
    #endregion

    #region Private
    private void PreparePlayerData()
    {
        if(PlayerPrefs.HasKey(PlayerPrefKeys.PREFS_KEY_PLAYER_DATA))
        {
            pInstance.playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PlayerPrefKeys.PREFS_KEY_PLAYER_DATA));
        }
        else
        {
            playerData.playerBallCollection = defaultBallCollection;
            PlayerPrefs.SetString(PlayerPrefKeys.PREFS_KEY_PLAYER_DATA, JsonUtility.ToJson(playerData));
            PlayerPrefs.Save();
        }
    }
    #endregion

    #region Public
    public void UpdateScore(int pinsDown, int pinsTouch)
    {
        if(playerRoundScores.Count >= GameConfig.MAX_TURNS)
        {
            playerRoundScores.Clear();
        }

        int pinTouchValue = _currentMaterial.Equals(BallDataCollection.MaterialType.Metal) ? GameConfig.METAL_TOUCH_VALUE : GameConfig.RUBBER_TOUCH_VALUE;
        int pinDownValue = _currentMaterial.Equals(BallDataCollection.MaterialType.Metal) ? GameConfig.METAL_DOWN_VALUE : GameConfig.METAL_DOWN_VALUE;
        int score = pinDownValue * pinsDown + pinTouchValue * pinsTouch;

        playerRoundScores.Add(new PlayerRoundScore(score, pinsDown, _currentMaterial));
    }

    public void UpdateCurrentMaterial(BallDataCollection.MaterialType materialType)
    {
        _currentMaterial = materialType;
    }

    public List<PlayerRoundScore> GetPlayerRoundScores()
    {
        return playerRoundScores;
    }
    #endregion
}
