using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Freedom,
    Dialog,
    Fighting,
    OpenBag
}
public class GameControl : MonoBehaviour
{
    public GameState state = GameState.Freedom;
    [SerializeField] public PlayerContrllo PlayerContrllo;
    // Start is called before the first frame update
    void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.Freedom;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Freedom)
        {
            PlayerContrllo.HandleUpdate();
        }else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }else if (state==GameState.Fighting)
        {

        }else if (state==GameState.OpenBag)
        {

        }
    }
}
