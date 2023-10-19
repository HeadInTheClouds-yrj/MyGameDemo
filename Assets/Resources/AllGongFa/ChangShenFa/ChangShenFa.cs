using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangShenFa : GongFaInvokeContro
{
    private GongFa gongFa;
    private Data myself;
    private List<Data> teams;
    private List<Data> enimies;
    private void OnEnable()
    {
        EventManager.Instance.gongFaEvent.OnRemoveGongFa += RemoveGongFa;
        EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece += InstallGongFa_Onece;
        EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece += UninstallGongFa_Onece;
    }

    private void OnDisable()
    {
        EventManager.Instance.gongFaEvent.UninstallGongFa_Onece(myself,teams,enimies);
        EventManager.Instance.gongFaEvent.OnRemoveGongFa -= RemoveGongFa;
        EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece -= InstallGongFa_Onece;
        EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece -= UninstallGongFa_Onece;
    }

    private void InstallGongFa_Onece(Data arg1, List<Data> arg2, List<Data> arg3)
    {
        switch (arg1.InstaillGongFa[gongFa.gfInfo.id])
        {
            case 0:arg1.MaxHealth += 10;
                break;
            case 1:arg1.MaxHealth += 20;
                break;
            case 2:arg1.MaxHealth += 40;
                break;
            case 3:arg1.MaxHealth += 80;
                break;
            case 4:arg1.MaxHealth += 160;
                break;
        }
        myself = arg1;
        teams = arg2;
        enimies = arg3;
    }

    private void UninstallGongFa_Onece(Data arg1, List<Data> arg2, List<Data> arg3)
    {
        switch (arg1.InstaillGongFa[gongFa.gfInfo.id])
        {
            case 0:
                arg1.MaxHealth -= 10;
                break;
            case 1:
                arg1.MaxHealth -= 20;
                break;
            case 2:
                arg1.MaxHealth -= 40;
                break;
            case 3:
                arg1.MaxHealth -= 80;
                break;
            case 4:
                arg1.MaxHealth -= 160;
                break;
        }
    }
    private void RemoveGongFa(string id,Transform parent)
    {
        if (gongFa.gfInfo.id.Equals(id) && parent == this.GetComponentInParent<Transform>())
        {
            Destroy(this.gameObject);
        }
    }

    protected override void UpdateGongFaMessage(GongFa gongFa)
    {
        this.gongFa = gongFa;
    }
}
