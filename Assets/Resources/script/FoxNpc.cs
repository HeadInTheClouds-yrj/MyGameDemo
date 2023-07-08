using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxNpc : MonoBehaviour,Interactives
{
    [SerializeField]
    public Sprite npcIcon;
    private FoxDailog foxDailog;
    public Sprite getAvatarSprite()
    {
        if (npcIcon == null)
        {
            npcIcon = Resources.Load("data/Art/CharacterAvatar/Fox") as Sprite;
            return npcIcon;
        }
        else
        {
            return npcIcon;
        }
    }
    int index = 0;
    public void ToTalk()
    {
        if(index == 0)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(foxDailog.SayHello(),foxDailog.SayHelloSprites(this)));
            index++;
        }else if(index>0)
        {
            //StartCoroutine(DialogManager.Instance.ShowDialog(foxDailog.Task1Dailog()));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foxDailog = new FoxDailog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
