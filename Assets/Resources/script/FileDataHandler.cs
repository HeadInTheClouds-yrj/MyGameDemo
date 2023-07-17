using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    string dataDirPath = "";
    string dataFileName = "";
    public void Save(GameData gameData)
    {
        //通过链接得到完整路径
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        }
        catch (Exception e)
        {

            Debug.LogError("保存数据时出现错误！" + fullPath + "\n"+ e);
        }
    }
    public void Load()
    {

    }
}
