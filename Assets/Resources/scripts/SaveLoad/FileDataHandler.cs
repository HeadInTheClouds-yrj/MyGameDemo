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
        //通过链接得到完整路径
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //创建目录以防止写入错误
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //gameData序列化为json格式
            string dataTostory = JsonUtility.ToJson(gameData,true);
            //将json格式的gameData写入文件
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

            Debug.LogError("保存数据时出现错误！" + fullPath + "\n"+ e);
        }
    }
    public GameData Load()
    {
        //通过链接得到完整路径
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedGameData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //从文件中加载序列化之后的数据
                string dataToLoad = "";
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }
                //逆序列化 返回C#类
                loadedGameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {

                Debug.LogError("尝试加载数据时出现错误！" + fullPath + "\n" + e);
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
