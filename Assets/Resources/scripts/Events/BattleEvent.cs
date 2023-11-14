using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent
{
    public event Action OnBeforeBattleStart;
    public void BeforeBattleStart()
    {
        if (OnBeforeBattleStart != null)
        {
            OnBeforeBattleStart();
        }
    }
    public event Action OnBattleStarted;
    public void BattleStarted()
    {
        if (OnBattleStarted != null)
        {
            OnBattleStarted();
        }
    }
    public event Action OnBeforeBattleEnd;
    public void BeforeBattleEnd()
    {
        if (OnBeforeBattleEnd != null)
        {
            OnBeforeBattleEnd();
        }
    }
    public event Action OnBattleEnded;
    public void BattleEnded()
    {
        if (OnBattleStarted != null)
        {
            OnBattleEnded();
        }
    }
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
