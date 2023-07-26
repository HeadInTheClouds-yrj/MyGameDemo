using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Playables;

public class FileDataHandler
{
    string dataDirPath = "";
    string dataFileName = "";
    public FileDataHandler(string dataDirPath,string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public void Save(GameData gameData)
    {
        //ͨ�����ӵõ�����·��
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //����Ŀ¼�Է�ֹд�����
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //gameData���л�Ϊjson��ʽ
            string dataTostory = JsonUtility.ToJson(gameData,true);
            //��json��ʽ��gameDataд���ļ�
            using(FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(dataTostory);
                }
            }
        }
        catch (Exception e)
        {

            Debug.LogError("��������ʱ���ִ���" + fullPath + "\n"+ e);
        }
    }
    public GameData Load()
    {
        //ͨ�����ӵõ�����·��
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedGameData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //���ļ��м������л�֮�������
                string dataToLoad = "";
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }
                //�����л� ����C#��
                loadedGameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {

                Debug.LogError("���Լ�������ʱ���ִ���" + fullPath + "\n" + e);
            }
        }
        return loadedGameData;
    }
    public void Remove(string dataFileName)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Directory.Delete(fullPath, true);
    }
}
