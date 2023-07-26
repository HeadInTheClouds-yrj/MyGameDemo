using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Freedom,
    Dialog,
    Fighting,
    OpenUI
}
public class GameControl : MonoBehaviour
{
    public GameState state = GameState.Freedom;
    [SerializeField] public PlayerContrllo PlayerContrllo;
    public Dictionary<string,NpcCell> allNpcCell;
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
        UIManager.instance.OpenUI += () =>
        {
            state = GameState.OpenUI;
        };
        UIManager.instance.CloseUI += () =>
        {
            if (state == GameState.OpenUI)
            {
                state = GameState.Freedom;
            }
        };
        if (allNpcCell == null)
        {
            allNpcCell = new Dictionary<string, NpcCell>();
        }
        allNpcCell = NpcManager.instance.getAllNpcCell();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Freedom)
        {
            PlayerContrllo.HandleUpdate();
            PlayerManager.instance.HandleUpdate();
            foreach (NpcCell npcCell in allNpcCell.Values)
            {
                npcCell.HandleUpdate();
            }
        }else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }else if (state==GameState.Fighting)
        {

        }else if (state==GameState.OpenUI)
        {

        }
    }
}
