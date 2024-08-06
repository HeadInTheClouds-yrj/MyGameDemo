using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallStaticGongFaUI : MonoBehaviour
{
    private string gongFaId;
    private int gongFaLevel;
    private int inStaticGongFaIndex;

    public string GongFaId { get => gongFaId; set => gongFaId = value; }
    public int GongFaLevel { get => gongFaLevel; set => gongFaLevel = value; }
    public int InStaticGongFaIndex { get => inStaticGongFaIndex; set => inStaticGongFaIndex = value; }
}
