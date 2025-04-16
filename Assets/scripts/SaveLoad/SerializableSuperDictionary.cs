using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SerializableSuperDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();

    [SerializeField] private List<TValue> listValues = new List<TValue>();
    [SerializeField] private TValue obj;
    /**
     * �������л�֮����load֮��
     */
    public void OnAfterDeserialize()
    {
        this.Clear();
        
    }
    /**
     * �����л�֮ǰ���ڱ��������ʼ֮ǰ��
     */
    public void OnBeforeSerialize()
    {
        keys.Clear();
        listValues.Clear();
    }
}
