using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillXiuLianUISelectHandler : MonoBehaviour
{
    private SkillInfoSO info;
    private CurrentXiuLianManu currentManu;
    private Image currentSelectImage;
    private TMP_Text displayNameText;
    [SerializeField] private GameObject XiuLianSampleEffectText;
    private Transform effectTextContent;
    private Button pressedButton;
    private TMP_Text pressedButtonText;
    [SerializeField] private Sprite InitializedSprite;
    public void SetInfo(SkillInfoSO info)
    {
        this.info = info;
    }
    private void PropertyInit()
    {
        displayNameText = UIManager.instance.GetUI("DiscipleBedroom", "DisplayName_N").GetComponent<TMP_Text>();
        effectTextContent = UIManager.instance.GetUI("DiscipleBedroom", "EffectContent_N").transform;
        pressedButton = UIManager.instance.GetUI("DiscipleBedroom", "PressedButton_N").GetComponent<Button>();
        pressedButtonText = UIManager.instance.GetUI("DiscipleBedroom", "PressedText_N").GetComponent<TMP_Text>();
        currentSelectImage = UIManager.instance.GetUI("DiscipleBedroom", "CurrentSelect_N").GetComponent<Image>();
    }
    public void SkillManuOnClick()
    {
        PropertyInit();
        currentSelectImage.sprite = info.skillInBattleIcon;
        displayNameText.text = info.skillDisplayName;
        SkillEffectTextInit();
        if (currentManu.Equals(CurrentXiuLianManu.ShenTongStudy))
        {
            pressedButton.interactable = true;
            pressedButtonText.text = "ÁìÎò";
            pressedButton.onClick.RemoveAllListeners();
            pressedButton.onClick.AddListener(StudySkill);
        }
        //else if (currentManu.Equals(CurrentXiuLianManu.SkillLevelUp))
        //{
        //    if (PlayerManager.instance.playerData.learnedSkills[info.id] == info.SkillMaxLevel)
        //    {
        //        pressedButton.interactable = false;
        //    }
        //    else
        //    {
        //        pressedButton.interactable = true;
        //        pressedButtonText.text = "¾«½ø";
        //        pressedButton.onClick.RemoveAllListeners();
        //        pressedButton.onClick.AddListener(LevelUPSkill);
        //        pressedButton.onClick.AddListener(SkillEffectTextInit);
        //    }
        //}
    }
    private void SkillEffectTextInit()
    {
        foreach (Transform item in effectTextContent)
        {
            Destroy(item.gameObject);
        }
        int i = 0;
        foreach (string item in info.skillEffectText)
        {
            GameObject gameObj = Instantiate(XiuLianSampleEffectText, effectTextContent) as GameObject;
            TMP_Text tmp_text = gameObj.transform.GetComponent<TMP_Text>();
            tmp_text.text = item;
            if (i == 0)
            {
                tmp_text.color = Color.red;
            }
            else if (i == 1)
            {
                tmp_text.color = Color.blue;
            }
            else if (i == 2)
            {
                tmp_text.color = Color.black;
            }
            i++;
        }
    }
    private void RoolBackSkillManu()
    {
        currentSelectImage.sprite = InitializedSprite;
        displayNameText.text = "";
        foreach (Transform item in effectTextContent)
        {
            Destroy(item.gameObject);
        }
        pressedButtonText.text = "";
    }
    private void StudySkill()
    {
        //XiuLianInteractManager.Instance.AddLearnedSkill(info.id);
        XiuLianInteractManager.Instance.AddLearnedSkill(info.id);
        //XiuLianInteractManager.Instance.RevomUnLearnedInBagSkillById(info.id);
        XiuLianInteractManager.Instance.RemoveUnlearnedInBagSkillById(info.id);
        //XiuLianInteractManager.Instance.AddLearnedSkillToPlayerData(info);
        XiuLianInteractManager.Instance.AddLearnedSkillToPlayerData(info);
        RoolBackSkillManu();
        pressedButton.onClick.RemoveListener(StudySkill);
        Destroy(this.gameObject);
    }
    private void LevelUPSkill()
    {
        //XiuLianInteractManager.Instance.SkillLevelUPToPlayerData(info);
    }
    public void UpdateCurrentManuState(CurrentXiuLianManu currentManu)
    {
        this.currentManu = currentManu;
    }
}
