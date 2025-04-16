using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        if (gameData == null)
        {
            NewGame();
        }
        if (fileDataHandler == null)
        {
            return;
        }
        //文件层面加载
        this.gameData = fileDataHandler.Load();
        //推送数据到需要改变数据的所有脚本(push)
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.LoadGame(gameData);
        }
    }
    public IEnumerator LoadGameData()
    {
        LoadGame();
        yield return null;
    }
    public void SaveGame()
    {
        if (gameData == null)
        {
            NewGame();
        }
        //通过其他实现了IdataPersitence的脚本可以更新这个gmaeData数据
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.SaveGame(gameData);
        }
        if (fileDataHandler == null)
        {
            ChangeDataSourceName();
        }
        fileDataHandler.Save(gameData);
    }
    public GameData GetGameData()
    {
        return fileDataHandler.Load();
    }
    public void RemoveData(string fileName)
    {
        if (fileDataHandler == null)
        {
            fileDataHandler = new FileDataHandler(Application.persistentDataPath, "default.data");
        }
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        //LoadGame();
    }
}
