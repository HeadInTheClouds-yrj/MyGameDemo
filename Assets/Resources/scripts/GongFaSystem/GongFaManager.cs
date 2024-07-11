using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongFaManager : MonoBehaviour
{
    public static GongFaManager instance;
    private Dictionary<string,GongFa> allGongFaMap;
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
    private Dictionary<string, GongFa> CreatGongFaMap()
    {
        GongFaInfoSO[] gongFaInfoSOs = Resources.LoadAll<GongFaInfoSO>("AllGongFa");
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
    public void InstantiateGongFa(string id,Transform parent)
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();
        Data data = parent.GetComponent<Humanoid>().GetData();
        GongFaInvokeContro[] gongFaInvokeContros = parent.GetComponentsInChildren<GongFaInvokeContro>();
        if (gongFaInvokeContros.Length>= data.maxGongFaInstall)
        {
            Debug.LogWarning("装备功法超过最大可装备数量！");
        }
        else
        {
            GongFa gongFa = GetInitGongFaById(id);
            gongFa.InstantiateGongFa(parent);
        }
    }
    public void RemoveGongFa(string id,Transform parent)
    {
        EventManager.Instance.gongFaEvent.RemoveGongFa(id, parent);
        Data data = parent.GetComponent<Humanoid>().GetData();
        if (data.instaillGongFas.ContainsKey(id))
        {
            data.instaillGongFas.Remove(id);
        }
    }
    private void GongFaLevelUP(string id, Transform parent)
    {
        bool flag = false;
        Data data = parent.GetComponent<Humanoid>().GetData();
        GongFaInvokeContro[] transforms = parent.GetComponentsInChildren<GongFaInvokeContro>();
        foreach (GongFaInvokeContro t in transforms)
        {
            if (t.name.Equals(id) && data != null && data.instaillGongFas.ContainsKey(id))
            {
                EventManager.Instance.gongFaEvent.RemoveGongFa(id, parent);
                GongFa gongFa = GetInitGongFaById(id);
                gongFa.InstantiateGongFa(parent);
                flag = true;
                break;
            }
        }

        if (data.learnedGongFas.ContainsKey(id))
        {
            if (data.learnedGongFas[id] < GetInitGongFaById(id).gfInfo.gongFaMaxLevel)
            {
                data.learnedGongFas[id]++;
                if (data.instaillGongFas.ContainsKey(id))
                {
                    data.instaillGongFas[id]++;
                }
            }
            else
            {
                data.learnedGongFas[id] = GetInitGongFaById(id).gfInfo.gongFaMaxLevel;
                if (data.instaillGongFas.ContainsKey(id))
                {
                    data.instaillGongFas[id] = GetInitGongFaById(id).gfInfo.gongFaMaxLevel;
                }
            }
            
        }
        else
        {
            return;
        }

        if (flag)
        {
            EventManager.Instance.gongFaEvent.AddGongFa(id, parent);
        }
    }
    public GongFa GetInitGongFaById(string gongFaId)
    {
        if (gongFaId!=null && allGongFaMap.ContainsKey(gongFaId))
        {
            return allGongFaMap[gongFaId];
        }
        else
        {
            Debug.LogWarning("根据id找功法：id("+gongFaId+")不存在！！");
            return null;
        }
    }
    public int GetGongFaCurrentLevelById(Data data,string gongFaId)
    {
        if (data.learnedGongFas.ContainsKey(gongFaId))
        {
            return data.learnedGongFas[gongFaId];
        }
        else
        {
            return 0;
        }
    }
}
