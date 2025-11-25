using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    private List<MonsterCard> cards = new List<MonsterCard>();
    [Button("GenerateWave")]
    public void GenerateWave(int str)
    {
        foreach(var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();

        var wave = DatabaseLoader.MonstersDB.RandomMonsterWave(str);
        foreach (var item in wave)
        {
            var monsterCard = MonsterCard.New(transform);
            monsterCard.Activate(item);
            cards.Add(monsterCard);
        }
    }
}
