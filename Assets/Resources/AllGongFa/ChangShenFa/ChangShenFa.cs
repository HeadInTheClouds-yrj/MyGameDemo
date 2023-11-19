using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangShenFa : GongFaInvokeContro
{
    private GongFa gongFa;
    private Data myself;
    private Dictionary<string,Data> teams;
    private Dictionary<string,Data> enimies;
    private void Awake()
    {
        myself = GetComponentInParent<Humanoid>()?.GetData();
        teams = GetComponentInParent<Humanoid>()?.GetTeams();
        enimies = GetComponentInParent<Humanoid>()?.GetEnimies();
    }
    private void OnEnable()
    {
        EventManager.Instance.gongFaEvent.OnAddGongFa += AddGongFaActualEffect_InstallOnece;
        EventManager.Instance.gongFaEvent.OnRemoveGongFa += RemoveGongFaActualEffect_UnInstallOnece;
        //EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece += InstallGongFa_Onece;
        //EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece += UninstallGongFa_Onece;
    }

    private void OnDisable()
    {
        EventManager.Instance.gongFaEvent.OnAddGongFa -= AddGongFaActualEffect_InstallOnece;
        EventManager.Instance.gongFaEvent.OnRemoveGongFa -= RemoveGongFaActualEffect_UnInstallOnece;
        //EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece -= InstallGongFa_Onece;
        //EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece -= UninstallGongFa_Onece;
    }
    private void Start()
    {
    }
    public override void InstallGongFa_Onece()
    {
        switch (myself.instaillGongFas[gongFa.gfInfo.id])
        {
            case 1:myself.maxHealth += 10;
                break;
            case 2:myself.maxHealth += 20;
                break;
            case 3:myself.maxHealth += 40;
                break;
            case 4:myself.maxHealth += 80;
                break;
            case 5:myself.maxHealth += 160;
                break;
        }
    }

    public override void UninstallGongFa_Onece()
    {
        switch (myself.instaillGongFas[gongFa.gfInfo.id])
        {
            case 1:
                myself.maxHealth -= 10;
                break;
            case 2:
                myself.maxHealth -= 20;
                break;
            case 3:
                myself.maxHealth -= 40;
                break;
            case 4:
                myself.maxHealth -= 80;
                break;
            case 5:
                myself.maxHealth -= 160;
                break;
        }
    }
    private void AddGongFaActualEffect_InstallOnece(string id,Transform parent)
    {
        if (gongFa.gfInfo.id.Equals(id) && parent.GetComponent<Humanoid>().GetData() == this.GetComponentInParent<Humanoid>().GetData())
        {
            InstallGongFa_Onece();
        }
    }
    private void RemoveGongFaActualEffect_UnInstallOnece(string id,Transform parent)
    {
        if (gongFa.gfInfo.id.Equals(id) && parent.GetComponent<Humanoid>().GetData() == this.GetComponentInParent<Humanoid>().GetData())
        {
            UninstallGongFa_Onece();
            Destroy(this.gameObject);
        }
    }

    protected override void UpdateGongFaMessage(GongFa gongFa)
    {
        this.gongFa = gongFa;
    }
}
