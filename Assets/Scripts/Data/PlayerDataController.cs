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
    public int pScore { get; private set; }
    public int pPinsCollapsed { get; private set; }

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
        int pinTouchValue = _currentMaterial.Equals(BallDataCollection.MaterialType.Metal) ? GameConfig.METAL_TOUCH_VALUE : GameConfig.RUBBER_TOUCH_VALUE;
        int pinDownValue = _currentMaterial.Equals(BallDataCollection.MaterialType.Metal) ? GameConfig.METAL_DOWN_VALUE : GameConfig.METAL_DOWN_VALUE;

        pScore += pinDownValue * pinsDown + pinTouchValue * pinsTouch;
        pPinsCollapsed += pinsDown;
    }

    public void UpdateCurrentMaterial(BallDataCollection.MaterialType materialType)
    {
        _currentMaterial = materialType;
    }
    #endregion
}
