using System.Collections;
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

    [SerializeField] BallDataCollection.MaterialType[] defaultBallCollection = new BallDataCollection.MaterialType[GameConfig.MAX_TURNS];
    public PlayerData playerData;

    public static PlayerDataController pInstance { get; private set; }

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
}
