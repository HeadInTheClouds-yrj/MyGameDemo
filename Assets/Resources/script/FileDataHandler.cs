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
        //ͨ�����ӵõ�����·��
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        }
        catch (Exception e)
        {

            Debug.LogError("��������ʱ���ִ���" + fullPath + "\n"+ e);
        }
    }
    public void Load()
    {

    }
}
