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
    public void InstantiateGongFa(Transform parent,GongFa gongFa)
    {
        GameObject gongFaPrfab = gfInfo.gongFaPrefab;
        if (gongFaPrfab != null)
        {
            GameObject obj =GameObject.Instantiate<GameObject>(gongFaPrfab, parent);
            obj.name = gongFa.gfInfo.id;
            obj.GetComponent<GongFaInvokeContro>().InitializeGongFaMessage(gongFa);
        }
    }
    public void RemoveGongFa(string id,Transform parent)
    {
        EventManager.Instance.gongFaEvent.RemoveGongFa(id,parent);
    }
}
