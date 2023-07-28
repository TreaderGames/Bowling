using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] BallDataCollection.BallData[] defaultBallCollection = new BallDataCollection.BallData[GameConfig.MAX_TURNS];

    public BallDataCollection.BallData[] pPlayerBallCollection { get; private set; }
    public PlayerData pInstance { get; private set; }

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
            pInstance = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PlayerPrefKeys.PREFS_KEY_PLAYER_DATA));
        }
        else
        {
            pPlayerBallCollection = defaultBallCollection;
            PlayerPrefs.SetString(PlayerPrefKeys.PREFS_KEY_PLAYER_DATA, JsonUtility.ToJson(pInstance));
            PlayerPrefs.Save();
        }
    }
    #endregion
}
