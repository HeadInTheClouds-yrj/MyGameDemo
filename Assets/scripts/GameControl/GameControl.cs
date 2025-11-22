using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private PlayerContrllo PlayerContrllo;
    private Dictionary<string,NpcCell> allNpcCell;
    public void Awake()
    {
        PlayerContrllo = PlayerManager.instance.GetComponent<PlayerContrllo>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadUptateNpcData;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadUptateNpcData;
    }
    private void OnSceneLoadUptateNpcData(Scene arg0, LoadSceneMode arg1)
    {
        allNpcCell = NpcManager.instance.getAllNpcCell();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.dialogEvent.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        EventManager.Instance.dialogEvent.OnHideDialog += () =>
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
        allNpcCell = NpcManager.instance.getAllNpcCell();
        if (allNpcCell == null)
        {
            allNpcCell = new Dictionary<string, NpcCell>();
        }
        UIManager.instance.InvokeCloseUI();

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
                if (npcCell!=null)
                {
                    npcCell.HandleUpdate();
                }
            }
            EventManager.Instance.gameStateEvent.ChangeGameState(State.BATTLE);
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }else if (state==GameState.Fighting)
        {

        }else if (state==GameState.OpenUI)
        {

        }
    }
}
