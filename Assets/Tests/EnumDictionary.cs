using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class EnumDictionary<TEnum, TValue>
    where TEnum : System.Enum
    where TValue : class
{
    [System.Serializable]
    private class ValueItem
    {
        [SerializeReference]
        public TValue value;
    }

    [SerializeField]
    Dictionary<TEnum, ValueItem> _enumValueDictionary;

    [SerializeReference]
    TValue _defaultValue;

    public TValue this[TEnum key]
    {
        get
        {
            if (_enumValueDictionary.ContainsKey(key)) return _enumValueDictionary[key].value;
            return _defaultValue;
        }
        set
        {
            _enumValueDictionary[key] = new ValueItem() {value = value};
        }
    }
}