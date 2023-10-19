using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongFaEvent
{
    public event Action<string,Transform> OnAddGongFa;
    public void AddGongFa(string gongFaId,Transform parent)
    {
        if (OnAddGongFa != null)
        {
            OnAddGongFa(gongFaId,parent);
        }
    }
    public event Action<string, Transform> OnRemoveGongFa;
    public void RemoveGongFa(string gongFaId, Transform parent)
    {
        if (OnRemoveGongFa != null)
        {
            OnRemoveGongFa(gongFaId, parent);
        }
    }
    public event Action<GongFa, string,Data,Transform> OnGongFaLevelUP;
    public void GongFaLevelUP(GongFa gongFa,string id,Data data, Transform parent)
    {
        if (OnGongFaLevelUP != null)
        {
            OnGongFaLevelUP(gongFa,id,data,parent);
        }
    }
    public event Action<Data,List<Data>,List<Data>> OnInstallGongFa_Onece;
    public void InstallGongFa_Onece(Data myselfData,List<Data> teamDatas,List<Data> enimyDatas)
    {
        if (OnInstallGongFa_Onece != null)
        {
            OnInstallGongFa_Onece(myselfData,teamDatas,enimyDatas);
        }
    }
    public event Action<Data, List<Data>, List<Data>> OnUsedGongFa_Keep;
    public void UsedGongFa_Keep(Data myselfData, List<Data> teamDatas, List<Data> enimyDatas)
    {
        if (OnUsedGongFa_Keep != null)
        {
            OnUsedGongFa_Keep(myselfData, teamDatas, enimyDatas);
        }
    }
    public event Action<Data, List<Data>, List<Data>> OnBeforeEnterBattel_Onece;
    public void BeforeEnterBattel_Onece(Data myselfData, List<Data> teamDatas, List<Data> enimyDatas)
    {
        if (OnBeforeEnterBattel_Onece != null)
        {
            OnBeforeEnterBattel_Onece(myselfData, teamDatas, enimyDatas);
        }
    }
    public event Action<Data, List<Data>, List<Data>> OnStartEnterBattel_Onece;
    public void StartEnterBattel_Onece(Data myselfData, List<Data> teamDatas, List<Data> enimyDatas)
    {
        if (OnStartEnterBattel_Onece != null)
        {
            OnStartEnterBattel_Onece(myselfData, teamDatas, enimyDatas);
        }
    }
    public event Action<Data, List<Data>, List<Data>> OnBattelIsOngoing_Keep;
    public void BattelIsOngoing_Keep(Data myselfData, List<Data> teamDatas, List<Data> enimyDatas)
    {
        if (OnBattelIsOngoing_Keep != null)
        {
            OnBattelIsOngoing_Keep(myselfData, teamDatas, enimyDatas);
        }
    }
    public event Action<Data, List<Data>, List<Data>> OnBeforeBattelOver_Onece;
    public void BeforeBattelOver_Onece(Data myselfData, List<Data> teamDatas, List<Data> enimyDatas)
    {
        if (OnBeforeBattelOver_Onece != null)
        {
            OnBeforeBattelOver_Onece(myselfData, teamDatas, enimyDatas);
        }
    }
    public event Action<Data, List<Data>, List<Data>> OnAfterBattelOver_Onece;
    public void AfterBattelOver_Onece(Data myselfData, List<Data> teamDatas, List<Data> enimyDatas)
    {
        if (OnAfterBattelOver_Onece != null)
        {
            OnAfterBattelOver_Onece(myselfData, teamDatas, enimyDatas);
        }
    }
    public event Action<Data, List<Data>, List<Data>> OnUninstallGongFa_Onece;
    public void UninstallGongFa_Onece(Data myselfData, List<Data> teamDatas, List<Data> enimyDatas)
    {
        if (OnUninstallGongFa_Onece != null)
        {
            OnUninstallGongFa_Onece(myselfData, teamDatas, enimyDatas);
        }
    }

}
