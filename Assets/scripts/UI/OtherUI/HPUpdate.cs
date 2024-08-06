using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUpdate : MonoBehaviour
{
    [SerializeField]private Image uI;
    [SerializeField]private TMP_Text playerhpui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uI.fillAmount = PlayerManager.instance.playerData.curenttHealth / PlayerManager.instance.playerData.maxHealth;
        playerhpui.text = PlayerManager.instance.playerData.curenttHealth.ToString() + "/" + PlayerManager.instance.playerData.maxHealth.ToString();
    }
}
