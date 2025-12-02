using Killemall.Tools;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Killemall.Data.GameplaySettings
{
    [ScriptableSingletonFolder("Data")]
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Killemall/Data/GameplaySettings")]
    public partial class GameplaySettings : ScriptableObjectSingleton<GameplaySettings>
    {
        [TabGroup("Monster Tiers"), HideDuplicateReferenceBox, HideLabel]
        public MonsterTierSettings MonsterTiers;

        [TabGroup("Prefabs"), HideDuplicateReferenceBox, HideLabel]
        public PrefabsSettings Prefabs;
    }
}

