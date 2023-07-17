using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("保存加载文件名")]
    [SerializeField]
    public string dataFileName;
    private FileDataHandler fileDataHandler;
    public static DataPersistenceManager instance { get; private set; }
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceList;
    private void Awake()
    {
        instance= this;
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //文件层面加载
        this.gameData = fileDataHandler.Load();
        if (gameData == null)
        {
            NewGame();
        }
        //推送数据到需要改变数据的所有脚本(push)
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.LoadGame(gameData);
        }
    }
    public void SaveGame()
    {
        //通过其他实现了IdataPersitence的脚本可以更新这个gmaeData数据
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.SaveGame(ref gameData);
        }
        fileDataHandler.Save(gameData);
    }
    // Start is called before the first frame update
    void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath,dataFileName);
        this.dataPersistenceList = FindAllDataPersistenceObjects();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> allDataPersistenceObj = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(allDataPersistenceObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
