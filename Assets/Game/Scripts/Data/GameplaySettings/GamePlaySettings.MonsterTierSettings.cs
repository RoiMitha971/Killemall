using System;
using System.Collections.Generic;
using System.Linq;
using Killemall.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Killemall.Data.GameplaySettings
{
    public partial class GameplaySettings : ScriptableObjectSingleton<GameplaySettings>
    {
        [Serializable]
        public class MonsterTierSettings
        {
            [Serializable]
            public class TierSetting
            {
                public MonsterTier Tier => _tier;
                public int Health => _health;
                public int Damage => _damage;


                [SerializeField, HideInInspector] private MonsterTier _tier;

                [TitleGroup("$_tier")]
                [SerializeField] private int _health;

                [SerializeField] private int _damage;


                public TierSetting(MonsterTier tier)
                {
                    _tier = tier;
                }
            }

            [SerializeField] private List<TierSetting> _tierSettings;

            public TierSetting GetSettings(MonsterTier tier)
            {
                return _tierSettings.FirstOrDefault(x => x.Tier == tier);
            }

#if UNITY_EDITOR
            [OnInspectorInit("SetList")]
            public void SetList()
            {
                if (_tierSettings == null || _tierSettings.Count == 0)
                {
                    _tierSettings = new List<TierSetting>();
                    foreach (MonsterTier tier in Enum.GetValues(typeof(MonsterTier)))
                    {
                        _tierSettings.Add(new TierSetting(tier));
                    }
                }
            }
#endif
        }
    }
}


