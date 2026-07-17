using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataBaseCore<TData> : ScriptableObject where TData : DefinitionBase
{
    [SerializeField] private List<TData> dataList = new List<TData>();

    private Dictionary<string, TData> dataLookup;

    public IReadOnlyList<TData> DataList => dataList;
    public int Count => dataList.Count;

    public void Initialize()
    {
        dataLookup = new Dictionary<string, TData>();

        foreach (TData data in dataList)
        {
            if (data == null)
            {
                Debug.LogError($"{name} 데이터베이스에 null 데이터가 들어 있습니다.", this);
                continue;
            }

            if (!dataLookup.TryAdd(data.DataId, data))
            {
                Debug.LogError($"중복된 유닛 ID입니다: {data.DataId}", data);
                continue;
            }
        }
    }

    public bool TryGetData(string dataId, out TData data)
    {
        EnsureInitialized();
        return dataLookup.TryGetValue(dataId, out data);
    }

    public TData GetData(string dataId)
    {
        EnsureInitialized();
        if (dataLookup.TryGetValue(dataId, out TData data))
        {
            return data;
        }
        Debug.LogError($"{typeof(TData).Name} 데이터를 찾을 수 없습니다: {dataId}", this);
        return null;
    }

    public bool Contains(string dataId)
    {
        EnsureInitialized();
        return dataLookup.ContainsKey(dataId);
    }

    public int GetCount()
    {
        return dataList.Count;
    }

    private void EnsureInitialized()
    {
        if (dataLookup == null)
        {
            Initialize();
        }
    }
}