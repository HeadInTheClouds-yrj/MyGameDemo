using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    private float time;
    public void SetTime(float time)
    {
        this.time = time;
    }
    private void Update()
    {
        time -= Time.deltaTime;
    }
    public bool IsFinished()
    {
        return time < 0;
    }
}
