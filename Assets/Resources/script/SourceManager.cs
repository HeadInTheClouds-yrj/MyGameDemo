using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceManager
{

    private GameObject ower;
    private List<AudioSource> allAudioSources;
    public SourceManager(GameObject ower)
    {
        this.ower = ower;
        Initial();
    }

    private void Initial()
    {
        allAudioSources = new List<AudioSource>();
        for (int i = 0; i < 3; i++)
        {
            AudioSource source = ower.AddComponent<AudioSource>();
            allAudioSources.Add(source);
        }
    }
    public AudioSource GetFreeSource()
    {
        for (int i = 0; i < allAudioSources.Count; i++)
        {
            if (!allAudioSources[i].isPlaying)
            {
                return allAudioSources[i];
            }
        }
        AudioSource freeSource = ower.AddComponent<AudioSource>();
        allAudioSources.Add (freeSource);
        return freeSource;
    }
    List<AudioSource> tmpList = new List<AudioSource>();
    public void CloseFreeSource()
    {
        float count = 0;
        for (int i = 0; i < allAudioSources.Count; i++)
        {
            if (!allAudioSources[i].isPlaying)
            {
                count++;
                if (count > 3)
                {
                    tmpList.Add(allAudioSources[i]);
                }
            }
        }
        Free(tmpList);
    }
    private void Free(List<AudioSource> tmpList)
    {
        foreach (AudioSource list in tmpList)
        {
            allAudioSources.Remove(list);
            UnityEngine.Object.Destroy(list);
        }
        tmpList.Clear();
        tmpList = null;
    }
}
