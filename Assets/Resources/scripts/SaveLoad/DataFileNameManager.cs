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
        if (files == null)
        {
            files = new List<string>();
        }
        files = FindAllFiles();
    }
    public List<string> FindAllFiles()
    {
        string path = Application.persistentDataPath;
        string[] fileNames = Directory.GetFiles(path);
        List<string> files = new List<string>();
        foreach (string s in fileNames)
        {
            if (!s.EndsWith(".log"))
            {
                
                files.Add(s.Split(path + "\\")[1]);
            }

        }
        return files;
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
