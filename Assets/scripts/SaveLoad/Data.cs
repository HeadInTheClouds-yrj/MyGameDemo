using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class Data
{
    public string id;
    public string name;
    public long lingShi;
    public int maxAge;
    public int currentAge;
    public int scenceIndex;
    public bool survival;
    public float maxLingQi;
    public float currentLingQi;
    public float regenerateLingQi;
    public float maxHealth;
    public float curenttHealth;
    public float moveSpeed;
    public int killEnimiesCont;
    public int maxGongFaInstall;
    public Vector3 currentPosition;
    public List<string> pickupedItemGameObj;
    public string[] installOrderGongFaIds;
    public string[] installOrderSkillIds;
    public SerializableDictionary<string,int> itemIds;
    public SerializableDictionary<string, int> instaillGongFas;
    public SerializableDictionary<string, int> installSkills;
    public SerializableDictionary<string, int> learnedGongFas;
    public SerializableDictionary<string, int> learnedSkills;
    public List<QuestData> questDatas;
    public int[] skillKey;

    public Data()
    {
        id = "";
        name = "";
        lingShi = 0;
        maxAge = 100;
        currentAge = 18;
        scenceIndex = 1;
        survival = true;
        maxLingQi = 1000;
        currentLingQi = 1000;
        regenerateLingQi = 1f;
        maxHealth = 100;
        curenttHealth = 100;
        moveSpeed = 2;
        killEnimiesCont = 0;
        maxGongFaInstall = 5;
        currentPosition = new Vector3(0, 0, 0);
        pickupedItemGameObj = new List<string>();
        installOrderGongFaIds = new string[9];
        installOrderSkillIds = new string[10];
        for (int i = 0; i < installOrderGongFaIds.Length; i++)
        {
            installOrderGongFaIds[i] = "empty";
        }
        for (int i = 0; i < installOrderSkillIds.Length; i++)
        {
            installOrderSkillIds[i] = "empty";
        }
        itemIds = new SerializableDictionary<string, int>();
        instaillGongFas = new SerializableDictionary<string, int>();
        installSkills = new SerializableDictionary<string, int>();
        learnedGongFas = new SerializableDictionary<string, int>();
        learnedSkills = new SerializableDictionary<string, int>();
        questDatas = new List<QuestData>();
        skillKey = new int[] { (int)KeyCode.Q, (int)KeyCode.E, (int)KeyCode.R, (int)KeyCode.T, (int)KeyCode.F, (int)KeyCode.Mouse0, (int)KeyCode.Mouse1, (int)KeyCode.Mouse2, (int)KeyCode.Alpha1, (int)KeyCode.Alpha2 };
    }
}
