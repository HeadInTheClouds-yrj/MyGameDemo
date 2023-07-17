using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
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
        //���մ��ļ��м��������л�֮���json����
        if (gameData == null)
        {
            NewGame();
        }
        //�������ݵ���Ҫ�ı����ݵ����нű�(push)
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.LoadGame(gameData);
            Debug.Log(gameData.PlayerPosition);
        }
    }
    public void SaveGame()
    {
        //ͨ������ʵ����IdataPersitence�Ľű����Ը������gmaeData����
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence?.SaveGame(ref gameData);
            Debug.Log(gameData.PlayerPosition);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        LoadGame();
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
