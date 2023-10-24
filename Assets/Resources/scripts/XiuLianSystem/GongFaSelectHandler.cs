
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GongFaSelectHandler : MonoBehaviour
{
    private GongFaInfoSO info;
    private CurrentXiuLianManu currentManu;
    private Image currentGongFaImage;
    [SerializeField] private TMP_Text displayNameText;
    [SerializeField] private GameObject gongFaEffectTextPrefab;
    [SerializeField] private Transform effectTextContent;
    [SerializeField] private TMP_Text pressedButtonText;
    public void SetInfo(GongFaInfoSO info)
    {
        this.info = info;
    }
    public void GongFaManuOnClick()
    {
        currentGongFaImage.sprite = info.gongFaInBattleIcon;
        displayNameText.text = info.gongFaDisplayName;
        gongFaEffectTextInit();
        if (currentManu.Equals(CurrentXiuLianManu.GongFaStudy))
        {
            pressedButtonText.text = "ÁìÎò";
        }
        else if (currentManu.Equals(CurrentXiuLianManu.GongFaLevelUp))
        {
            pressedButtonText.text = "¾«½ø";
        }
    }
    private void gongFaEffectTextInit()
    {
        for (int i = 0; i < info.gongFaMaxLevel; i++)
        {
            TMP_Text gongFaEffectTexts = Instantiate(gongFaEffectTextPrefab,effectTextContent).GetComponent<TMP_Text>();
            if (GongFaManager.instance.GetGongFaCurrentLevelById(PlayerManager.instance.playerData, info.id) == i)
            {
                if (currentManu.Equals(CurrentXiuLianManu.GongFaStudy) && i == 0)
                {
                    gongFaEffectTexts.text = info.gongFaEffectText[i];
                }
                else
                {
                    gongFaEffectTexts.color = Color.red;
                    gongFaEffectTexts.text = info.gongFaEffectText[i];
                }
            }
            else
            {
                gongFaEffectTexts.text = info.gongFaEffectText[i];
            }
        }
    }
    public void UpdateCurrentManuState(CurrentXiuLianManu currentManu)
    {
        this.currentManu = currentManu;
    }
}
