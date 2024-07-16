using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Humanoid
{
    Data GetData();
    Dictionary<string, Data> GetTeams();
    Dictionary<string, Data> GetEnimies();
    List<Transform> GetOwnerTeams();
    List<Transform> GetEnemies();
}
