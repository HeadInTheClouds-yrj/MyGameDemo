using System;
using System.Diagnostics;
using static UnityEngine.EventSystems.EventTrigger;

public class EnimiesEvent
{
    public event Action<NpcCell> OnEnimyDie;
    public void EnimyDie(NpcCell npcCell)
    {
        if (OnEnimyDie != null)
        {
            OnEnimyDie(npcCell);
        }
    }
    public event Action CountWhenTheEnemyDies;
    public void CountTheEnemyDies()
    {
        if (CountWhenTheEnemyDies != null)
        {
            CountWhenTheEnemyDies();
        }
    }
}
