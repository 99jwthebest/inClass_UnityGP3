using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum BlackboardType
{
    Int,
    Float,
    Bool,
    Vector3,
    GameObject
}


[Serializable]
public class  BlackboardEntry
{
    [SerializeField] string keyName;
    [SerializeField] object value;
    [SerializeField] string runtimeValue;
    [SerializeField] BlackboardType type;

    public string GetKeyName() { return keyName; }

    public BlackboardEntry(string keyName, BlackboardType type)
    {
        this.keyName = keyName;
        this.value = null;
        this.runtimeValue = "null";
        this.type = type;
    }

    // if val is null, clears the data.
    public bool SetVal<T>(T val)
    {
        if(val == null)
        {
            value = null;
            runtimeValue = "null";
            return true;
        }

        if(val.GetType() != typeDictionary[type])
        {
            Debug.LogError($"trying to set blackboard data: {keyName}, with type {val.GetType()}, should be: {typeDictionary[type]}");
            return false;
        }

        value = val;
        runtimeValue = val.ToString();
        return true;
    }

    public bool GetVal<T>(out T val)
    {
        val = default;
        if(value == null) return false;

        if(val.GetType() != typeDictionary[type])
        {
            return false;
        }

        return false;
    }

    static Dictionary<BlackboardType, System.Type> typeDictionary = new Dictionary<BlackboardType, Type>
    {
        {BlackboardType.Float, typeof(float) },
        {BlackboardType.Int, typeof(int) },
        {BlackboardType.Bool, typeof(bool) },
        {BlackboardType.Vector3, typeof(Vector3) },
        {BlackboardType.GameObject, typeof(GameObject) },
    };

};


[CreateAssetMenu(menuName = "BehaviorTree/Blackboard")]
public class Blackboard : ScriptableObject
{
    [SerializeField]
    List<BlackboardEntry> blackboardData;

    public delegate void OnBlackboardValueChanged(BlackboardEntry entry);
    public event OnBlackboardValueChanged onBlackboardValueChanged;

    bool SetBlackboardData<T>(string keyName, T val)
    {
        foreach(var entry in blackboardData)
        {
            if(entry.GetKeyName() == keyName)
            {
                onBlackboardValueChanged?.Invoke(entry);
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    bool GetBlackboardData<T>(string keyName, out T val)
    {
        foreach(var entry in blackboardData)
        {
            if(entry.GetKeyName() == keyName)
            {
                return entry.GetVal(out val);
            }
        }
        val = default;
        return false;
    }
}
