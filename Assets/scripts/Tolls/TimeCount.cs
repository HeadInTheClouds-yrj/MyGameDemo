using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    private float time;
    public void SetTime(float time)
    {
        this.time = time;
        StartCoroutine(CountTime());
    }
    private IEnumerator CountTime()
    {
        while (true)
        {
            time -= Time.deltaTime;
            yield return null;
            if (time < 0)
            {
                break;
            }
        }

    }
    private void Update()
    {
        
    }
    public bool IsFinished()
    {
        return time < 0;
    }
}
