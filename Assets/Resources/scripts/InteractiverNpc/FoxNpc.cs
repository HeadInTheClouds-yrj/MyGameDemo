using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxNpc : MonoBehaviour,Interactives
{
    [SerializeField]
    public Sprite npcIcon;
    private FoxDailog foxDialog;
    public Sprite getAvatarSprite()
    {
        if (npcIcon == null)
        {
            npcIcon = Resources.Load<Sprite>("data/Art/CharacterAvatar/Fox");
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
            StartCoroutine(DialogManager.Instance.ShowDialog(foxDialog.SayHello(),foxDialog.SayHelloSprites(this)));
            index++;
        }else if(index>0)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(foxDialog.Task1Dialog(),foxDialog.Task1DialogSprites(this)));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foxDialog = new FoxDailog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
