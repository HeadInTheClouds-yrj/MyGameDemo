using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class XingHengZong : MonoBehaviour,Interactives
{
    public void ToTalk () {
        
    }
    public void EnterBuilding(){
        // SceneManager.LoadSceneAsync(2,LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(2,LoadSceneMode.Single);
    }
    public Sprite getAvatarSprite(){
        return null;
    }
    public void AddEventId(int id){

    }
}
