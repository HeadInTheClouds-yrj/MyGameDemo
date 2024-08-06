using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstallGongFaBagButton : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 beforeTransformPosition;
    private Vector3 temp;
    private string gongFaId;
    private int gongFaLevel;
    private List<RectTransform> gongFas;
    [SerializeField] private Sprite empty;
    /// <summary>
    /// 当点击
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        beforeTransformPosition = transform.position;
    }
    /// <summary>
    /// 当开始移动
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnBeginDrag(PointerEventData eventData)
    {
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
        foreach (RectTransform gongFapzt in gongFas)
        {
            Vector3 buttonPosition = Camera.main.WorldToScreenPoint(gongFapzt.transform.position);
            float leftX = buttonPosition.x - gongFapzt.rect.width/2;
            float rightX = buttonPosition.x + gongFapzt.rect.width /2;
            float topY = buttonPosition.y + gongFapzt.rect.height /2;
            float bottomY = buttonPosition.y - gongFapzt.rect.height / 2;
            if (gongFapzt.transform.GetComponent<Button>().interactable&& eventData.position.x>leftX && eventData.position.x 
                < rightX && eventData.position.y > bottomY && eventData.position.y<topY)
            {

                //判断是否已经装备正在拖拽的功法，是则调换位置
                for (int i = 0; i < gongFas.Count; i++)
                {
                    if (gongFaId.Equals(PlayerManager.instance.playerData.installOrderGongFaIds[i]))
                    {
                        if (gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId != null
                            && !"empty".Equals(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId) 
                            &&!gongFaId.Equals(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId))
                        {
                            PlayerManager.instance.playerData.installOrderGongFaIds[i] = gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId;
                            gongFas[i].GetComponent<Image>().sprite = 
                                GongFaManager.instance.GetInitGongFaById(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId).gfInfo.gongFaInBattleIcon;
                            gongFas[i].Find("Name").GetComponent<TMP_Text>().text = 
                                GongFaManager.instance.GetInitGongFaById(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId).gfInfo.gongFaName;
                            GongFaManager.instance.RemoveGongFa(gongFaId, PlayerManager.instance.transform);
                        }
                        else
                        {
                            PlayerManager.instance.playerData.installOrderGongFaIds[i] = "empty";
                            GongFaManager.instance.RemoveGongFa(gongFaId, PlayerManager.instance.transform);
                            gongFas[i].GetComponent<Image>().sprite = empty;
                            gongFas[i].Find("Name").GetComponent<TMP_Text>().text = "无";
                            break;
                        }

                    }
                    else
                    {
                        if (gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId != null
                            && !"empty".Equals(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId)
                            && !gongFaId.Equals(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId))
                        {
                            GongFaManager.instance.RemoveGongFa(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId,PlayerManager.instance.transform);
                        }
                    }
                }
                gongFapzt.GetComponent<Image>().sprite = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.gongFaInBattleIcon;
                gongFapzt.Find("Name").GetComponent<TMP_Text>().text = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.gongFaName;
                PlayerManager.instance.playerData.installOrderGongFaIds[gongFapzt.GetComponent<InstallStaticGongFaUI>().InStaticGongFaIndex] = gongFaId;
                if (PlayerManager.instance.playerData.instaillGongFas.ContainsKey(gongFaId))
                {
                    PlayerManager.instance.playerData.instaillGongFas[gongFaId] = gongFaLevel;
                }
                else
                {
                    PlayerManager.instance.playerData.instaillGongFas.Add(gongFaId, gongFaLevel);
                }
                
                GongFaManager.instance.InstantiateGongFa(gongFaId, PlayerManager.instance.transform);
                EventManager.Instance.gongFaEvent.AddGongFa(gongFaId,PlayerManager.instance.transform);
                PropertyManuCtrl.instance.ListInstalledGongFaStaticImage();
                PropertyManuCtrl.instance.ListLearnedGongFaBag();
            }
        }
        transform.position = beforeTransformPosition;
    }
    public void SetGongFaButton(string gongFaId,int gongFalevel, List<RectTransform> gongFas)
    {
        this.gongFaId= gongFaId;
        this.gongFaLevel= gongFalevel;
        this.gongFas= gongFas;
    }

}
