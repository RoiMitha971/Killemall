using System.Collections.Generic;
using System.Linq;
using Killemall.Tools;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;


[TypeInfoBox("All Monsters should be listed here.")]
[ScriptableSingletonFolder("Data")]
[CreateAssetMenu(fileName = "MonstersDataList", menuName = "Killemall/Data/Monsters/Monsters List")]
public class MonstersDataList : ScriptableObjectSingleton<MonstersDataList>
{
    public List<MonsterData> Monsters => _monsters;

    [SerializeField] private List<MonsterData> _monsters;

    public List<MonsterData> RandomMonsterWave(int totalStrength)
    {
        List<MonsterData> returnList = new List<MonsterData>();
        var strengths = StrengthRepartition(totalStrength);

        foreach(var str in strengths)
        {

            if(str <= 0)
                continue;

            List<MonsterData> validChoices = _monsters.Where(x=>x.Strength == str).ToList();

            if(!validChoices.IsNullOrEmpty())
                returnList.Add(validChoices[Random.Range(0, validChoices.Count)]);
        }

        return returnList;
    }

    private List<int> StrengthRepartition(int targetStr)
    {
        const int Count = 4;
        const int MinValue = 0;
        const int MaxValue = 7;

        // Clamp pour garantir une solution possible
        int maxTotal = MaxValue * Count;
        targetStr = Mathf.Clamp(targetStr, MinValue, maxTotal);

        List<int> result = new List<int>(Count);


        int remaining = targetStr;

        for (int i = 0; i < Count; i++)
        {
            int slotsLeft = Count - i - 1;

            int minPossible = Mathf.Max(MinValue, remaining - slotsLeft * MaxValue);
            int maxPossible = Mathf.Min(MaxValue, remaining - slotsLeft * MinValue);

            int value = Random.Range(minPossible, maxPossible + 1);
            result.Add(value);

            remaining -= value;
        }

        return result;
    }

    #region Methods
    public bool TryGetMonsterFromId(string id, out MonsterData MonsterData)
    {
        foreach (MonsterData exisitingMonsterData in _monsters)
        {
            if (exisitingMonsterData.Id == id)
            {
                MonsterData = exisitingMonsterData;
                return true;
            }
        }

        Debug.LogException(new System.Exception($"No Monsters with id {id} found"));
        MonsterData = null;
        return false;
    }

    public bool Contains(MonsterData data)
    {
        return _monsters.Any(x => x.Id == data.Id);
    }

    public void Add(MonsterData data)
    {
        if (!Contains(data))
        {
            _monsters.Add(data);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }
    }

    public void Remove(MonsterData data)
    {
        if (Contains(data))
        {
            _monsters.Remove(data);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }
    }

#if UNITY_EDITOR
    [MenuItem("Killemall/Data/Monsters", priority = 51)]
    public static void OpenWindow()
    {
        AssetDatabase.OpenAsset(Instance);
    }
#endif
    #endregion
}
