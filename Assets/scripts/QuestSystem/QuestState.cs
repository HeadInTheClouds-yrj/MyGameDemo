using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    REQUIREMENTS_NOT_MET,//不满足条件，不能执行该任务
    CAN_START,//可以开始接收这个任务
    IN_PROGRESS,//正在进行中
    CAN_FINISH,//可以完成
    FINISHED//已经完成
}
