using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongFaManager : MonoBehaviour
{
    public static GongFaManager instance;
    private Dictionary<string, GongFa> allGongFaMap;
    private void Awake()
    {
        instance = this;
        allGongFaMap = CreatGongFaMap();
    }
    private void OnEnable()
    {
        EventManager.Instance.gongFaEvent.OnGongFaLevelUP += GongFaLevelUP;
    }

    private void OnDisable()
    {
        EventManager.Instance.gongFaEvent.OnGongFaLevelUP -= GongFaLevelUP;
    }
    public Dictionary<string, GongFa> CreatGongFaMap()
    {
        GongFaInfoSO[] gongFaInfoSOs = Resources.LoadAll<GongFaInfoSO>("AllGongFa");
        Debug.Log(gongFaInfoSOs.Length);
        Dictionary<string,GongFa> gongFaMap = new Dictionary<string,GongFa>();
        foreach (GongFaInfoSO gfInfo in gongFaInfoSOs)
        {
            if (!gongFaMap.ContainsKey(gfInfo.id))
            {
                gongFaMap.Add(gfInfo.id,new GongFa(gfInfo));
            }
        }
        return gongFaMap;
    }
    public GongFa InstantiateGongFa(string id,Data data,Transform parent)
    {
        GongFa gongFa = CloneGongFaById(id,data);
        gongFa.InstantiateGongFa(parent,gongFa);
        return gongFa;
    }
    public void RemoveGongFa(GongFa gongFa,string id,Transform parent)
    {
        gongFa.RemoveGongFa(id, parent);
    }
    private void GongFaLevelUP(GongFa arg1, string arg2, Data arg3, Transform parent)
    {
        RemoveGongFa(arg1, arg2, parent);
        arg1.InstantiateGongFa(parent, arg1);
    }
    public GongFa CloneGongFaById(string id, Data data)
    {
        GongFa gongFa = new GongFa(GetInitGongFaById(id).gfInfo);
        gongFa.gfInfo.currentLearnedRate = data.InstaillGongFa[id];
        return gongFa;
    }
    public GongFa CreateNewGongFa(string id)
    {
        GongFa gongFa = new GongFa(GetInitGongFaById(id).gfInfo);
        return gongFa;
    }
    public GongFa GetInitGongFaById(string gongFaId)
    {
        if (allGongFaMap.ContainsKey(gongFaId))
        {
            return allGongFaMap[gongFaId];
        }
        else
        {
            Debug.LogWarning("根据id找功法：id不存在！！");
            return null;
        }
    }
    private void GongFaLeveUP(string gongFaId,Data data)
    {
        data.InstaillGongFa[gongFaId]++;
    }
}
