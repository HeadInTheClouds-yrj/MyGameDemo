using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ClipManager
{
    private string[] clipNames;
    public ClipManager()
    {
        ReadConfig();
        LoadClips();
    }
    public void ReadConfig()
    {
        var txtPath = System.IO.Path.Combine(Application.streamingAssetsPath, "AudioProfile.txt");
        FileInfo fileInfo = new FileInfo(txtPath);
        if (fileInfo.Exists)
        {
            StreamReader streamReader = new StreamReader(txtPath);
            string s = streamReader.ReadLine();
            int tmpCount = 0;
            if (int.TryParse(s,out tmpCount))
            {
                clipNames = new string[tmpCount];
                for (int i = 0; i < tmpCount; i++)
                {
                    s = streamReader.ReadLine();
                    clipNames[i] = s.Split(' ')[0];
                }
            }
            streamReader.Close();
        }
    }
    private ClipSingle[] allClipSingles;
    public void LoadClips()
    {
        allClipSingles = new ClipSingle[clipNames.Length];
        for (int i = 0; i < clipNames.Length; i++)
        {
            AudioClip tmpClip = Resources.Load<AudioClip>("Clips/" + clipNames[i]);
            ClipSingle tmpClipSingle = new ClipSingle(tmpClip);
            allClipSingles[i] = tmpClipSingle;
        }
    }
    public ClipSingle GetClipByName(string clipName)
    {
        for (int i = 0; i < clipNames.Length; i++)
        {
            if (clipNames[i].Equals(clipName))
            {
                return allClipSingles[i];
            }
        }
        return null;
    }
}
