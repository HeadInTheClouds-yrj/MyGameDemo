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
    private void Start()
    {
        myself = GetComponentInParent<Humanoid>()?.GetData();
        Debug.Log(myself.Id);
    }
    private void OnEnable()
    {
        EventManager.Instance.gongFaEvent.OnRemoveGongFa += RemoveGongFa;
        EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece += InstallGongFa_Onece;
        EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece += UninstallGongFa_Onece;
    }

    private void OnDisable()
    {
        EventManager.Instance.gongFaEvent.UninstallGongFa_Onece();
        EventManager.Instance.gongFaEvent.OnRemoveGongFa -= RemoveGongFa;
        EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece -= InstallGongFa_Onece;
        EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece -= UninstallGongFa_Onece;
    }

    private void InstallGongFa_Onece()
    {
        switch (myself.InstaillGongFas[gongFa.gfInfo.id])
        {
            case 1:myself.MaxHealth += 10;
                break;
            case 2:myself.MaxHealth += 20;
                break;
            case 3:myself.MaxHealth += 40;
                break;
            case 4:myself.MaxHealth += 80;
                break;
            case 5:myself.MaxHealth += 160;
                break;
        }
    }

    private void UninstallGongFa_Onece()
    {
        switch (myself.InstaillGongFas[gongFa.gfInfo.id])
        {
            case 1:
                myself.MaxHealth -= 10;
                break;
            case 2:
                myself.MaxHealth -= 20;
                break;
            case 3:
                myself.MaxHealth -= 40;
                break;
            case 4:
                myself.MaxHealth -= 80;
                break;
            case 5:
                myself.MaxHealth -= 160;
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
