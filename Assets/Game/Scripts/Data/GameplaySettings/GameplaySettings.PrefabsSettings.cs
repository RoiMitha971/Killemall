using System;
using Killemall.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Killemall.Data.GameplaySettings
{
    public partial class GameplaySettings : ScriptableObjectSingleton<GameplaySettings>
    {
        [Serializable]
        public class PrefabsSettings
        {
            public GameObject MonsterCardPrefab => _monsterCardPrefab;

            [SerializeField] private GameObject _monsterCardPrefab;
        }
    }

}

