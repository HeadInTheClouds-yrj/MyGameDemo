using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstallGongFaBagButton : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private string gongFaId;
    private int gongFaLevel;
    private List<RectTransform> gongFas;
    /// <summary>
    /// 当点击
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        Debug.Log("testtest");
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
            if (gongFapzt.transform.GetComponent<Button>().interactable&& eventData.position.x>leftX && eventData.position.x < rightX && eventData.position.y > bottomY && eventData.position.y<topY)
            {
                gongFapzt.GetComponent<Image>().sprite = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.gongFaInBattleIcon;
                gongFapzt.Find("Name").GetComponent<TMP_Text>().text = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.gongFaName;
                if (gongFaId.Equals(PlayerManager.instance.playerData.MainGongFaId))
                {
                    PlayerManager.instance.playerData.MainGongFaId = gongFaId;
                    PlayerManager.instance.playerData.InstaillGongFas.Remove(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId);
                    PlayerManager.instance.playerData.InstaillGongFas.Add(gongFaId, gongFaLevel);
                }
                else
                {
                    PlayerManager.instance.playerData.InstaillGongFas.Remove(gongFapzt.GetComponent<InstallStaticGongFaUI>().GongFaId);
                    PlayerManager.instance.playerData.InstaillGongFas.Add(gongFaId, gongFaLevel);
                }
                PropertyManuCtrl.instance.ListInstalledGongFaStaticImage();
                PropertyManuCtrl.instance.ListInstallGongFaBag();
            }
        }
    }
    public void SetGongFaButton(string gongFaId,int gongFalevel, List<RectTransform> gongFas)
    {
        this.gongFaId= gongFaId;
        this.gongFaLevel= gongFalevel;
        this.gongFas= gongFas;
    }

}
