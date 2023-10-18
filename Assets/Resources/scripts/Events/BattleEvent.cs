using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent
{
    public event Action<float> OnPlayerHit;
    public void PlayerHit(float value)
    {
        if (OnPlayerHit != null)
        {
            OnPlayerHit(value);
        }
    }
    public event Action<float> OnPlayerReduceLingQi;
    public void PlayerReduceLingQi(float value)
    {
        if (OnPlayerReduceLingQi != null)
        {
            OnPlayerReduceLingQi(value);
        }
    }
    public event Action<float> OnPlayerReduceHP;
    public void PlayerReduceHP(float value)
    {
        if (OnPlayerReduceHP != null)
        {
            OnPlayerReduceHP(value);
        }
    }
}
