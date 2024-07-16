using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstallSkillBagButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler,IPointerExitHandler
{
    private Vector3 beforeTransformPosition;
    private Vector3 temp;
    private List<RectTransform> skills;
    [SerializeField] private Sprite empty;
    [SerializeField] private GameObject mouseoverPanel;
    private GameObject instantatePanel;
    private SkillInfoSO info;
    private int i;

    /// <summary>
    /// 当开始移动
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeTransformPosition = transform.position;
        if (instantatePanel != null || !instantatePanel.IsDestroyed())
        {
            Destroy(instantatePanel);
        }
    }
    /// <summary>
    /// 拖拽时
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnDrag(PointerEventData eventData)
    {
        temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;
        transform.position = temp;
    }
    /// <summary>
    /// 松开时
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnEndDrag(PointerEventData eventData)
    {
        i = 0;
        foreach (RectTransform skillpzt in skills)
        {
            Vector3 buttonPosition = Camera.main.WorldToScreenPoint(skillpzt.transform.position);
            float leftX = buttonPosition.x - skillpzt.rect.width / 2;
            float rightX = buttonPosition.x + skillpzt.rect.width / 2;
            float topY = buttonPosition.y + skillpzt.rect.height / 2;
            float bottomY = buttonPosition.y - skillpzt.rect.height / 2;
            if (eventData.position.x > leftX && eventData.position.x
                < rightX && eventData.position.y > bottomY && eventData.position.y < topY)
            {

                for (int i = 0;i < skills.Count; i++)
                {
                    if (info.id.Equals(PlayerManager.instance.playerData.installOrderSkillIds[i]))
                    {
                        if (skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo() != null&&
                            skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo().id != "empty"&&
                            !skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo().id.Equals(info.id))
                        {
                            PlayerManager.instance.playerData.installOrderSkillIds[i] = skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo().id;
                            skills[i].GetComponent<InstalledSkillUIHandler>().SetSkillInfoSO(SkiilManager.Instance.GetSkillInfoSOById(
                                PlayerManager.instance.playerData.installOrderSkillIds[i]));
                            SkiilManager.Instance.SkillBindGrade(i, SkiilManager.Instance.GetSkillInfoSOById(
                                PlayerManager.instance.playerData.installOrderSkillIds[i]));
                            skills[i].GetComponent<Image>().sprite = skillpzt.GetComponent<Image>().sprite;
                            skills[i].Find("Name").GetComponent<TMP_Text>().text = skillpzt.Find("Name").GetComponent<TMP_Text>().text;
                        }
                        else
                        {
                            PlayerManager.instance.playerData.installOrderSkillIds[i] = "empty";
                            skills[i].GetComponent<Image>().sprite = null;
                            skills[i].Find("Name").GetComponent<TMP_Text>().text = "empty";
                            SkiilManager.Instance.SkillRemoveBindGrade(i, skills[i].GetComponent<InstalledSkillUIHandler>().GetCurrentInfo());
                            
                        }

                    }
                    else
                    {
                        if (skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo() != null &&
                            skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo().id != "empty" &&
                            !skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo().id.Equals(info.id))
                        {
                            if (PlayerManager.instance.playerData.installSkills.ContainsKey(skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo().id))
                            {
                                PlayerManager.instance.playerData.installSkills.Remove(skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo().id);
                            }
                            SkiilManager.Instance.SkillRemoveBindGrade(this.i, skillpzt.GetComponent<InstalledSkillUIHandler>().GetCurrentInfo());
                        }
                    }
                }
                SkiilManager.Instance.SkillBindGrade(i, info);
                skillpzt.GetComponent<Image>().sprite = info.skillInBattleIcon;
                skillpzt.Find("Name").GetComponent<TMP_Text>().text = info.skillName;
                PlayerManager.instance.playerData.installOrderSkillIds[i] = info.id;
                skillpzt.GetComponent<InstalledSkillUIHandler>().SetSkillInfoSO(info);
                if (!PlayerManager.instance.playerData.installSkills.ContainsKey(info.id))
                {
                    PlayerManager.instance.playerData.installSkills.Add(info.id,1);
                }
                PropertyManuCtrl.instance.ListInstalledSkillStaticImage();
                PropertyManuCtrl.instance.ListLearnedSkillBag();
            }
            i++;
        }
        transform.position = beforeTransformPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        if (instantatePanel == null || instantatePanel.IsDestroyed())
        {
            instantatePanel = Instantiate(mouseoverPanel, transform);
            TMP_Text[] tMP_Texts = instantatePanel.transform.GetComponentsInChildren<TMP_Text>();
            for (int i = 0; i < tMP_Texts.Length; i++)
            {
                tMP_Texts[i].text = info.skillEffectText[i];
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (instantatePanel != null || !instantatePanel.IsDestroyed())
        {
            Destroy(instantatePanel);
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (instantatePanel != null || !instantatePanel.IsDestroyed())
        {
            Destroy(instantatePanel);
        }
    }

    public void SetSkillMessage(SkillInfoSO info,List<RectTransform> skills)
    {
        this.info = info;
        this.skills = skills;
    }

}
