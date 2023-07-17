using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("��������ļ���")]
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
        //�ļ��������
        this.gameData = fileDataHandler.Load();
        if (gameData == null)
        {
            NewGame();
        }
        //�������ݵ���Ҫ�ı����ݵ����нű�(push)
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.LoadGame(gameData);
        }
    }
    public void SaveGame()
    {
        //ͨ������ʵ����IdataPersitence�Ľű����Ը������gmaeData����
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
