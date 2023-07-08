using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FoxDailog
{
    public List<string> fistMeeting;
    public List<string> task1;
    public List<string> task2;
    public List<string> task3;
    public List<string> SayHello()
    {
        fistMeeting = new List<string>
        {
            "Fox: Hello, my name is Xiaoli.What's your name?",
            "Beastmaster: Hello, my name is xxx! Nice to meet you!",
            "Fox:Me too!"
        };
        return fistMeeting;
    }
    public List<Sprite> SayHelloSprites(FoxNpc foxNpc)
    {
        List<Sprite> sprites = new List<Sprite>
        {
            PlayerManager.instance.PlayerIcon,
            foxNpc.npcIcon
        };
        return sprites;
    }
    public List<string> Task1Dailog()
    {
        task1 = new List<string> 
        {
            "Fox:Good morning, warrior. Can you collect 5 herbs for me?",
            "select:",
            "Beastmaster:Sure! No problem!",
            "Beastmaster:Sorry, I have other matters to attend to."
        };
        return task1;
    }
}
