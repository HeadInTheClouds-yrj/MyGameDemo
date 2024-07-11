using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiJianJue : GongFaInvokeContro
{
    private GongFa liJianJue;
    private Data myself;
    private BattleState state = BattleState.End;
    private void OnEnable()
    {
        EventManager.Instance.gongFaEvent.OnAddGongFa += AddGongFaActualEffect_InstallOnece;
        EventManager.Instance.gongFaEvent.OnRemoveGongFa += RemoveGongFaActualEffect_UnInstallOnece;
        //EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece += InstallGongFa_Onece;
        //EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece += UninstallGongFa_Onece;
    }

    private void RemoveGongFaActualEffect_UnInstallOnece(string arg1, Transform arg2)
    {
        if (liJianJue.gfInfo.id.Equals(arg1) && arg2.GetComponent<Humanoid>().GetData() == this.GetComponentInParent<Humanoid>().GetData())
        {

            UninstallGongFa_Onece();
            Destroy(this.gameObject);
        }
    }

    private void AddGongFaActualEffect_InstallOnece(string arg1, Transform arg2)
    {
        if (liJianJue.gfInfo.id.Equals(arg1) && arg2.GetComponent<Humanoid>().GetData() == this.GetComponentInParent<Humanoid>().GetData())
        {
            InstallGongFa_Onece();
        }
    }

    private void OnDisable()
    {
        EventManager.Instance.gongFaEvent.OnAddGongFa -= AddGongFaActualEffect_InstallOnece;
        EventManager.Instance.gongFaEvent.OnRemoveGongFa -= RemoveGongFaActualEffect_UnInstallOnece;
        //EventManager.Instance.gongFaEvent.OnInstallGongFa_Onece -= InstallGongFa_Onece;
        //EventManager.Instance.gongFaEvent.OnUninstallGongFa_Onece -= UninstallGongFa_Onece;
    }
    public override void InstallGongFa_Onece()
    {
        Debug.Log("功法开始装备");
    }

    public override void UninstallGongFa_Onece()
    {
        Debug.Log("功法开始卸载");
    }

    protected override void UpdateGongFaMessage(GongFa gongFa)
    {
        liJianJue = gongFa;
    }

    private void upBattleState(BattleState state)
    {
        this.state = state;
    }
    // Start is called before the first frame update
    void Awake()
    {
        myself = GetComponentInParent<Humanoid>()?.GetData();

    }

    // Update is called once per frame
    void HandleUpdate()
    {
        
    }
    private void OnBattleStart()
    {
        Debug.Log("111111");
    }
}
