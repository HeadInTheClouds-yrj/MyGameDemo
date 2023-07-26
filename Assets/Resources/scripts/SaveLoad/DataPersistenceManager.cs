using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("文件名字")]
    [SerializeField]
    private string fileName;
    private FileDataHandler fileDataHandler;
    public static DataPersistenceManager instance { get; private set; }
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceList;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        Debug.Log("加载游戏");
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        //文件层面加载
        this.gameData = fileDataHandler.Load();
        Debug.Log(gameData);
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
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        //通过其他实现了IdataPersitence的脚本可以更新这个gmaeData数据
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.SaveGame(ref gameData);
        }
        Debug.Log(gameData);
        fileDataHandler.Save(gameData);
    }
    public void RemoveData(string fileName)
    {
        fileDataHandler.Remove(fileName);
        DataFileNameManager.Instance.RemoveFileName(fileName);
    }
    // Start is called before the first frame update
    void Start()
    {
        this.dataPersistenceList = FindAllDataPersistenceObjects();
    }
    public void ChangeDataSourceName(string dataFileName="default.data")
    {
        this.fileName = dataFileName;
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath,dataFileName);
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
}
