using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("�ļ�����")]
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
        Debug.Log("������Ϸ");
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        //�ļ��������
        this.gameData = fileDataHandler.Load();
        Debug.Log(gameData);
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
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        //ͨ������ʵ����IdataPersitence�Ľű����Ը������gmaeData����
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
