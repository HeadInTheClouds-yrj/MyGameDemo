using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    public QuestEvent questEvent;
    public EnimiesEvent enimiesEvent;
    public PlayerEvent playerEvent;
    public DialogEvent dialogEvent;
    public InputEvent inputEvent;
    private void Awake()
    {
        Instance = this;

        questEvent = new QuestEvent();
        enimiesEvent = new EnimiesEvent();
        playerEvent = new PlayerEvent();
        dialogEvent = new DialogEvent();
        inputEvent = new InputEvent();
    }
}
