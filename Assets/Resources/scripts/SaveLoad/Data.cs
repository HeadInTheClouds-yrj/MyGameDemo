using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[Serializable]
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
    public List<string> pickupedItemGameObj;
    public string[] installOrderGongFaIds;
    public SerializableDictionary<string,int> itemIds;
    public SerializableDictionary<string, int> instaillGongFas;
    public SerializableDictionary<string, int> learnedGongFas;
    public SerializableDictionary<string, int> learnedSkills;

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
        pickupedItemGameObj = new List<string>();
        installOrderGongFaIds = new string[9];
        itemIds = new SerializableDictionary<string, int>();
        instaillGongFas = new SerializableDictionary<string, int>();
        learnedGongFas = new SerializableDictionary<string, int>();
        learnedSkills = new SerializableDictionary<string, int>();
    }
}
