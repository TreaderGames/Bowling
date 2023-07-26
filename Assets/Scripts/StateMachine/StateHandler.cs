using System.Collections.Generic;

public enum GameState
{
    None,
    Placement
}

public class StateHandler : Singleton<StateHandler>
{
    GameState currentState = GameState.None;
    List<IStateListener> stateListeners = new List<IStateListener>();

    #region Unity
    #endregion

    #region Public
    public void ChangeState(GameState gameState)
    {
        currentState = gameState;

        for (int i = 0; i < stateListeners.Count; i++)
        {
            stateListeners[i].StateChanged(gameState);
        }
    }

    public void AddStateListener(IStateListener stateListener)
    {
        stateListener.StateChanged(currentState);
        stateListeners.Add(stateListener);
    }
    public void RemoveStateListener(IStateListener stateListener)
    {
        stateListeners.Remove(stateListener);
    }
    #endregion
}
