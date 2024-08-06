using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongFa
{
    public GongFaInfoSO gfInfo;
    public string gongFaName;
    public string gongFaDisplayName;
    public int gongFaRate;
    public int gongFaMaxLevel;
    public Sprite gongFaInItemIcon;
    public Sprite gongFaInBattleIcon;
    public int gongFaXiuLianSpeek;
    public GongFaTypes gongFaTypes;
    public List<string> gongFaEffectText;
    public GongFa(GongFaInfoSO gfInfo, string gongFaName, string gongFaDisplayName, int gongFaRate, int gongFaMaxLevel, Sprite gongFaInItemIcon, Sprite gongFaInBattleIcon, int gongFaXiuLianSpeek, GongFaTypes gongFaTypes, List<string> gongFaEffectText) : this(gfInfo)
    {
        this.gongFaName = gongFaName;
        this.gongFaDisplayName = gongFaDisplayName;
        this.gongFaRate = gongFaRate;
        this.gongFaMaxLevel = gongFaMaxLevel;
        this.gongFaInItemIcon = gongFaInItemIcon;
        this.gongFaInBattleIcon = gongFaInBattleIcon;
        this.gongFaXiuLianSpeek = gongFaXiuLianSpeek;
        this.gongFaTypes = gongFaTypes;
        this.gongFaEffectText = gongFaEffectText;
    }


    public GongFa(GongFaInfoSO gfInfo)
    {
        this.gfInfo = gfInfo;
    }
    public GongFa() { }
    public void InstantiateGongFa(Transform parent)
    {
        GameObject gongFaPrfab = gfInfo.gongFaPrefab;
        if (gongFaPrfab != null)
        {
            GameObject obj =GameObject.Instantiate<GameObject>(gongFaPrfab, parent);
            obj.name = gfInfo.id;
            obj.GetComponent<GongFaInvokeContro>().InitializeGongFaMessage(this);
        }
    }
}
