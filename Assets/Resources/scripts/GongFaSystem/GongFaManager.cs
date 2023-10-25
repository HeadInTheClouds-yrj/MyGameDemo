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
        GongFaInvokeContro[] gongFaInvokeContros = parent.GetComponentsInChildren<GongFaInvokeContro>();
        if (gongFaInvokeContros.Length>=5)
        {
            Debug.LogWarning("װ��������������װ��������");
        }
        else
        {
            foreach (Transform t in transforms)
            {
                if (t.name.Equals(id))
                {
                    return;
                }
            }
            GongFa gongFa = GetInitGongFaById(id);
            Data data = parent.GetComponent<Humanoid>().GetData();
            data.InstaillGongFas.Add(id, data.LearnedGongFas[id]);
            gongFa.InstantiateGongFa(parent);
        }
    }
    public void RemoveGongFa(string id,Transform parent)
    {
        EventManager.Instance.gongFaEvent.RemoveGongFa(id, parent);
        Data data = parent.GetComponent<Humanoid>().GetData();
        if (data.InstaillGongFas.ContainsKey(id))
        {
            data.InstaillGongFas.Remove(id);
        }
    }
    private void GongFaLevelUP(string id, Transform parent)
    {
        Data data = parent.GetComponent<Humanoid>().GetData();
        if (data.LearnedGongFas.ContainsKey(id))
        {
            if (data.LearnedGongFas[id] < GetInitGongFaById(id).gfInfo.gongFaMaxLevel)
            {
                data.LearnedGongFas[id]++;
                if (data.InstaillGongFas.ContainsKey(id))
                {
                    data.InstaillGongFas[id]++;
                }
            }
            else
            {
                data.LearnedGongFas[id] = GetInitGongFaById(id).gfInfo.gongFaMaxLevel;
                if (data.InstaillGongFas.ContainsKey(id))
                {
                    data.InstaillGongFas[id] = GetInitGongFaById(id).gfInfo.gongFaMaxLevel;
                }
            }
            
        }
        else
        {
            return;
        }
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name.Equals(id)&& data != null && data.InstaillGongFas.ContainsKey(id))
            {
                EventManager.Instance.gongFaEvent.RemoveGongFa(id, parent);
                GongFa gongFa = GetInitGongFaById(id);
                gongFa.InstantiateGongFa(parent);
                return;
            }
        }
    }
    public GongFa GetInitGongFaById(string gongFaId)
    {
        if (allGongFaMap.ContainsKey(gongFaId))
        {
            return allGongFaMap[gongFaId];
        }
        else
        {
            Debug.LogWarning("����id�ҹ�����id("+gongFaId+")�����ڣ���");
            return null;
        }
    }
    public int GetGongFaCurrentLevelById(Data data,string gongFaId)
    {
        if (data.LearnedGongFas.ContainsKey(gongFaId))
        {
            return data.LearnedGongFas[gongFaId];
        }
        else
        {
            return 0;
        }
    }
}
