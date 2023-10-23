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
        foreach (Transform t in transforms)
        {
            if (t.name.Equals(id))
            {
                return;
            }
        }
        GongFa gongFa = GetInitGongFaById(id);
        gongFa.InstantiateGongFa(parent);
    }
    public void RemoveGongFa(string id,Transform parent)
    {
        EventManager.Instance.gongFaEvent.RemoveGongFa(id, parent);
    }
    private void GongFaLevelUP(string id, Transform parent)
    {
        Data data = parent.GetComponent<Humanoid>().GetData();
        if (data.LearnedGongFa.ContainsKey(id))
        {
            data.LearnedGongFa[id]++;
            data.InstaillGongFa[id]++;
        }
        else
        {
            return;
        }
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name.Equals(id)&& data != null && data.InstaillGongFa.ContainsKey(id))
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
            Debug.LogWarning("根据id找功法：id("+gongFaId+")不存在！！");
            return null;
        }
    }
}
