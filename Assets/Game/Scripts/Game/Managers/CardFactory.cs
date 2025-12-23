using System;
using System.Collections.Generic;
using System.Linq;
using Killemall.Data.GameplaySettings;
using Sirenix.Utilities;
using UnityEngine;

public class CardFactory
{
    #region Properties
    public static CardFactory Instance
    {
        get
        {
            if(_instance == null)
                _instance = new CardFactory();

            _instance.CleanList();
            return _instance;
        }
    }
    #endregion
    #region Fields
    private static CardFactory _instance;

    private List<Card> _allCards;

    private Dictionary<Type, GameObject> _prefabByType;
    #endregion

    #region Constructor
    public CardFactory()
    {
        _allCards = new List<Card>();
        _prefabByType = new Dictionary<Type, GameObject>()
        {
            { typeof(MonsterCard), GameplaySettings.Instance.Prefabs.MonsterCardPrefab },
            { typeof(WeaponCard), null }, //TODO
        };
    }
    #endregion

    #region Methods
    private void CleanList()
    {
        _allCards ??= new List<Card>();

        //Remove any null entry in the list
        _allCards = _allCards.Where(x => x != null).ToList();
    }

    public T GetAvailable<T>() where T : Card
    {

        Card card = null;
        if(_allCards.TryGetFirst(x=>!x.Active && x is T, out card))
        {
            return (T)card;
        }
        card = CreateNew<T>();
        _allCards.Add(card);
        return (T)card;
    }

    private T CreateNew<T>() where T : Card
    {
        if (!_prefabByType.TryGetValue(typeof(T), out GameObject prefab))
            return null;

        GameObject go = GameObject.Instantiate(prefab);

        if(!go.TryGetComponent(out T cardComponent))
        {
            GameObject.Destroy(go);
            return null;
        }
        return cardComponent;
    }
    #endregion
}
