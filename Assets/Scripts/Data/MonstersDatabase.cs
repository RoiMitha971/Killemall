using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;

public class MonstersDatabase : AbstractObjectDatabase<MonsterData>
{

    public MonstersDatabase()
    {
        LoadData();
    }

    protected override void LoadData()
    {
        _data = Resources.LoadAll<MonsterData>("Monsters").ToList();
    }

    public List<MonsterData> RandomMonsterWave(int totalStrength)
    {
        List<MonsterData> returnList = new List<MonsterData>();
        var strengths = StrengthRepartition(totalStrength);

        foreach(var str in strengths)
        {

            if(str <= 0)
                continue;

            List<MonsterData> validChoices = _data.Where(x=>x.Strength == str).ToList();

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

}
