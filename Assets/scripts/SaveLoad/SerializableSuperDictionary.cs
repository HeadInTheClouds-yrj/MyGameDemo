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
     * 在逆序列化之后（在load之后）
     */
    public void OnAfterDeserialize()
    {
        this.Clear();
        
    }
    /**
     * 在序列化之前（在保存操作开始之前）
     */
    public void OnBeforeSerialize()
    {
        keys.Clear();
        listValues.Clear();
    }
}
