using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class DataFileNameManager : MonoBehaviour
{
    public static DataFileNameManager Instance;
    private List<string> files;
    public void Init()
    {
        files = new List<string>();
        string[] temps = FindAllFiles();
        foreach (string s in temps)
        {
            AddFileName(s);
        }
    }
    public string[] FindAllFiles()
    {
        string path = Application.persistentDataPath;
        string[] fileNames = Directory.GetFiles(path);
        int i = 0;
        foreach (string s in fileNames)
        {
            fileNames[i] = s.Split
                (path + "\\")[1];
            i++;
        }
        return fileNames;
    }
    public void AddFileName(string filesName)
    {
        if (!files.Contains(filesName))
        {
            files.Add(filesName);
        }
    }
    public void RemoveFileName(string fileName)
    {
        if (files.Contains(fileName))
        {
            files.Remove(fileName);
        }
    }
    public List<string> GetAllFileNames()
    {
        return files;
    }
    private void Awake()
    {
        Instance = this;
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
