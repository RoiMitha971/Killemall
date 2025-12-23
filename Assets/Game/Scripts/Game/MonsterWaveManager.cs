using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class MonsterWaveManager : MonoBehaviour
{

    [SerializeField] private Transform _cardContainer;

    private List<MonsterCard> _cards;

    [Button("Test")]
    public void DisplayWave()
    {
        if (!_cards.IsNullOrEmpty())
        {
            foreach(var card in _cards)
            {
                card.Deactivate();
            }
        }

        _cards = GenerateWave(3);
        foreach(var monster in _cards)
        {
            monster.transform.SetParent(_cardContainer, false);
        }
    }

    private List<MonsterCard> GenerateWave(int str)
    {
        List<MonsterCard> cards = new List<MonsterCard>();
        var wave = MonstersDataList.Instance.RandomMonsterWave(str);

        foreach (var item in wave)
        {
            var monsterCard = CardFactory.Instance.GetAvailable<MonsterCard>();
            monsterCard.Activate(new Monster(item));
            cards.Add(monsterCard);
        }

        return cards;
    }


}
