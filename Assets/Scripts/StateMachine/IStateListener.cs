using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateListener
{
    public void StateChanged(GameState gameState);
}
