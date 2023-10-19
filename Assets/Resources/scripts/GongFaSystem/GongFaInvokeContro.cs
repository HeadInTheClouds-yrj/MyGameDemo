using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GongFaInvokeContro : MonoBehaviour
{
    private GongFa gongFa;
    public void InitializeGongFaMessage(GongFa gongFa)
    {
        this.gongFa = gongFa;
        UpdateGongFaMessage(gongFa);
    }
    protected abstract void UpdateGongFaMessage(GongFa gongFa);
}
