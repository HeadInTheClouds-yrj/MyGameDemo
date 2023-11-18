using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongFa
{
    public GongFaInfoSO gfInfo;
    public GongFa(GongFaInfoSO gfInfo)
    {
        this.gfInfo = gfInfo;
    }
    public void InstantiateGongFa(Transform parent)
    {
        GameObject gongFaPrfab = gfInfo.gongFaPrefab;
        if (gongFaPrfab != null)
        {
            GameObject obj =GameObject.Instantiate<GameObject>(gongFaPrfab, parent);
            obj.name = gfInfo.id;
            obj.GetComponent<GongFaInvokeContro>().InitializeGongFaMessage(this);
            EventManager.Instance.gongFaEvent.InstallGongFa_Onece();
        }
    }
    public void InstantiateGongFaAgain(Transform parent)
    {
        GameObject gongFaPrfab = gfInfo.gongFaPrefab;
        if (gongFaPrfab != null)
        {
            GameObject obj = GameObject.Instantiate<GameObject>(gongFaPrfab, parent);
            obj.name = gfInfo.id;
            obj.GetComponent<GongFaInvokeContro>().InitializeGongFaMessage(this);
        }
    }
}
