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
    public event Action<string,Transform> OnGongFaLevelUP;
    public void GongFaLevelUP(GongFa gongFa,string id,Transform parent)
    {
        if (OnGongFaLevelUP != null)
        {
            OnGongFaLevelUP(id,parent);
        }
    }
    public event Action OnInstallGongFa_Onece;
    public void InstallGongFa_Onece()
    {
        if (OnInstallGongFa_Onece != null)
        {
            OnInstallGongFa_Onece();
        }
    }
    public event Action OnUsedGongFa_Keep;
    public void UsedGongFa_Keep()
    {
        if (OnUsedGongFa_Keep != null)
        {
            OnUsedGongFa_Keep();
        }
    }
    public event Action OnBeforeEnterBattel_Onece;
    public void BeforeEnterBattel_Onece()
    {
        if (OnBeforeEnterBattel_Onece != null)
        {
            OnBeforeEnterBattel_Onece();
        }
    }
    public event Action OnStartEnterBattel_Onece;
    public void StartEnterBattel_Onece()
    {
        if (OnStartEnterBattel_Onece != null)
        {
            OnStartEnterBattel_Onece();
        }
    }
    public event Action OnBattelIsOngoing_Keep;
    public void BattelIsOngoing_Keep()
    {
        if (OnBattelIsOngoing_Keep != null)
        {
            OnBattelIsOngoing_Keep();
        }
    }
    public event Action OnBeforeBattelOver_Onece;
    public void BeforeBattelOver_Onece()
    {
        if (OnBeforeBattelOver_Onece != null)
        {
            OnBeforeBattelOver_Onece();
        }
    }
    public event Action OnAfterBattelOver_Onece;
    public void AfterBattelOver_Onece()
    {
        if (OnAfterBattelOver_Onece != null)
        {
            OnAfterBattelOver_Onece();
        }
    }
    public event Action OnUninstallGongFa_Onece;
    public void UninstallGongFa_Onece()
    {
        if (OnUninstallGongFa_Onece != null)
        {
            OnUninstallGongFa_Onece();
        }
    }

}
