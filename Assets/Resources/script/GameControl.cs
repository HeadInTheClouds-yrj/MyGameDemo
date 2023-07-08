using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Freedom,
    Dailog,
    Fighting,
    OpenBag
}
public class GameControl : MonoBehaviour
{
    public GameState state;
    private PlayerContrllo PlayerContrllo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerContrllo = new PlayerContrllo();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Freedom)
        {
            PlayerContrllo.HandleUpdate();
        }else if (state == GameState.Dailog)
        {

        }else if (state==GameState.Fighting)
        {

        }else if (state==GameState.OpenBag)
        {

        }
    }
}
