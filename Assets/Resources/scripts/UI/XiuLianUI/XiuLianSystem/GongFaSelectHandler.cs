
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
    private TMP_Text displayNameText;
    [SerializeField] private GameObject gongFaEffectTextPrefab;
    private Transform effectTextContent;
    private Button pressedButton;
    private TMP_Text pressedButtonText;
    [SerializeField] private Sprite InitializedSprite;
    public void SetInfo(GongFaInfoSO info)
    {
        this.info = info;
    }
    private void PropertyInit()
    {
        displayNameText = UIManager.instance.GetUI("DiscipleBedroom", "DisplayName_N").GetComponent<TMP_Text>();
        effectTextContent = UIManager.instance.GetUI("DiscipleBedroom", "EffectContent_N").transform;
        pressedButton = UIManager.instance.GetUI("DiscipleBedroom", "PressedButton_N").GetComponent<Button>();
        pressedButtonText = UIManager.instance.GetUI("DiscipleBedroom", "PressedText_N").GetComponent<TMP_Text>();
        currentGongFaImage = UIManager.instance.GetUI("DiscipleBedroom", "CurrentSelect_N").GetComponent<Image>();
    }
    public void GongFaManuOnClick()
    {
        PropertyInit();
        currentGongFaImage.sprite = info.gongFaInBattleIcon;
        displayNameText.text = info.gongFaDisplayName;
        GongFaEffectTextInit();
        if (currentManu.Equals(CurrentXiuLianManu.GongFaStudy))
        {
            pressedButton.interactable = true;
            pressedButtonText.text = "ÁìÎò";
            pressedButton.onClick.RemoveAllListeners();
            pressedButton.onClick.AddListener(StudyGongFa);
        }
        else if (currentManu.Equals(CurrentXiuLianManu.GongFaLevelUp))
        {
            if (PlayerManager.instance.playerData.learnedGongFas[info.id] == info.gongFaMaxLevel)
            {
                pressedButton.interactable = false;
            }
            else
            {
                pressedButton.interactable = true;
                pressedButtonText.text = "¾«½ø";
                pressedButton.onClick.RemoveAllListeners();
                pressedButton.onClick.AddListener(LevelUPGongFa);
                pressedButton.onClick.AddListener(GongFaEffectTextInit);
            }
        }
    }
    private void GongFaEffectTextInit()
    {
        foreach (Transform item in effectTextContent)
        {
            Destroy(item.gameObject);
        }
        for (int i = 1; i <= info.gongFaMaxLevel; i++)
        {
            TMP_Text gongFaEffectTexts = Instantiate(gongFaEffectTextPrefab,effectTextContent).GetComponent<TMP_Text>();
            if (GongFaManager.instance.GetGongFaCurrentLevelById(PlayerManager.instance.playerData, info.id) == i)
            {
                if (currentManu.Equals(CurrentXiuLianManu.GongFaLevelUp))
                {
                    gongFaEffectTexts.text = info.gongFaEffectText[i-1];
                    gongFaEffectTexts.color = Color.red;
                }
                else
                {
                    gongFaEffectTexts.text = info.gongFaEffectText[i-1];
                }
            }
            else
            {
                gongFaEffectTexts.text = info.gongFaEffectText[i-1];
            }
        }
    }
    private void RoolBackGongFaManu()
    {
        currentGongFaImage.sprite = InitializedSprite;
        displayNameText.text = "";
        foreach (Transform item in effectTextContent)
        {
            Destroy(item.gameObject);
        }
        pressedButtonText.text = "";
    }
    private void StudyGongFa()
    {
        XiuLianInteractManager.Instance.AddLearnedGongFa(info.id);
        XiuLianInteractManager.Instance.RemoveUnLearnedInBagGongFaById(info.id);
        XiuLianInteractManager.Instance.AddLearnedGongFaToPlayerData(info);
        RoolBackGongFaManu();
        pressedButton.onClick.RemoveListener(StudyGongFa);
        Destroy(this.gameObject);
    }
    private void LevelUPGongFa()
    {
        XiuLianInteractManager.Instance.GongFaLevelUPToPlayerData(info);
    }
    public void UpdateCurrentManuState(CurrentXiuLianManu currentManu)
    {
        this.currentManu = currentManu;
    }
}
