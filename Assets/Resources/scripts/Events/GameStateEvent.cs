using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateEvent
{
    public Action<State> OnChangeGameState;
    public void ChangeGameState(State state)
    {
        if (OnChangeGameState != null)
        {
            OnChangeGameState(state);
        }
    }
}
