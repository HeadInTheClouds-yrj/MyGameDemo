using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FoxDailog
{
    public List<string> fistMeeting;
    public List<string> task1;
    public List<string> task2;
    public List<string> task3;
    public FoxDailog()
    {
        fistMeeting = new List<string>();
        task1 = new List<string>();
        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, "foxDialogue1.txt");
        FileInfo fileInfo = new FileInfo(fullPath);
        if (fileInfo.Exists)
        {
            StreamReader sr = new StreamReader(fullPath);
            for (int i = 0; i < 3; i++)
            {
                string s = sr.ReadLine();
                fistMeeting.Add(s);
            }
        }
        
        string fullPath2 = System.IO.Path.Combine(Application.streamingAssetsPath, "foxDialogue2.txt");
        FileInfo fileInfo2 = new FileInfo(fullPath2);
        if (fileInfo2.Exists)
        {
            StreamReader sr2 = new StreamReader(fullPath2);
            for (int i = 0; i < 2; i++)
            {
                string s2 = sr2.ReadLine();

                task1.Add(s2);
            }
        }
        
    }
    public List<string> SayHello()
    {
        return fistMeeting;
    }
    public List<Sprite> SayHelloSprites(FoxNpc foxNpc)
    {
        List<Sprite> sprites = new List<Sprite>
        {
            PlayerManager.instance.PlayerIcon,
            foxNpc.getAvatarSprite()
        };
        return sprites;
    }
    public List<string> Task1Dialog()
    {

        return task1;
    }
    public List<Sprite> Task1DialogSprites(FoxNpc foxNpc)
    {
        List<Sprite> sprites = new List<Sprite>
        {
            PlayerManager.instance.PlayerIcon,
            foxNpc.getAvatarSprite()
        };
        return sprites;
    }
}
