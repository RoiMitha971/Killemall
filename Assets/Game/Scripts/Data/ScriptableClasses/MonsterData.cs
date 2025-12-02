using Killemall.Data.GameplaySettings;
using Killemall.Data;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_New_Data", menuName = "Killemall/Data/Monsters/Monster")]
public class MonsterData : ScriptableGameData
{

    public Sprite Sprite => _sprite;
    public string MonsterName => _monsterName;
    public DamageType Resistance => _resistance;
    public DamageType Weakness => _weakness;
    public int Strength => (int)_monsterTier;
    public int Health => _health;
    public int Damage => _damage;


    [TitleGroup("Monster")]
    [HorizontalGroup("Monster/Split", width: 111)]
    [PreviewField(Height = 100, Alignment = ObjectFieldAlignment.Left), HideLabel]
    [SerializeField] Sprite _sprite;

    [HorizontalGroup("Monster/Split")]
    [VerticalGroup("Monster/Split/Right")]
    [LabelText("Monster Name"), LabelWidth(120)]
    [SerializeField] private string _monsterName;

    [PropertySpace(8)]
    
    [HorizontalGroup("Monster/Split")]
    [VerticalGroup("Monster/Split/Right")]
    [LabelText("Resistance"), LabelWidth(120)]
    [SerializeField] private DamageType _resistance;

    [HorizontalGroup("Monster/Split")]
    [VerticalGroup("Monster/Split/Right")]
    [LabelText("Weakness"), LabelWidth(120)]
    [SerializeField] private DamageType _weakness;

    [PropertySpace(8)]

    [HorizontalGroup("Monster/Split")]
    [VerticalGroup("Monster/Split/Right")]
    [OnValueChanged("RefreshTierSettings")]
    [LabelText("Tier"), LabelWidth(120)]
    [SerializeField] private MonsterTier _monsterTier = MonsterTier.Tier1;

    [TitleGroup("Statistics")]
    [OnValueChanged("RefreshTierSettings")]
    [SerializeField] private bool _overrideTierSettings;

    [EnableIf("_overrideTierSettings")]
    [SerializeField] private int _health;

    [EnableIf("_overrideTierSettings")]
    [SerializeField] private int _damage;


#if UNITY_EDITOR

    [OnInspectorInit("RefreshTierSettings")]
    private void RefreshTierSettings()
    {
        if (!_overrideTierSettings)
        {
            var settings = GameplaySettings.Instance.MonsterTiers.GetSettings(_monsterTier);
            _health = settings.Health;
            _damage = settings.Damage;
        }
    }

    public override bool IsInDatabase()
    {
        return MonstersDataList.Instance.Contains(this);
    }

    public override void AddToDatabase()
    {
        MonstersDataList.Instance.Add(this);

        EditorUtility.SetDirty(MonstersDataList.Instance);
        AssetDatabase.SaveAssets();
    }

    public override void RemoveFromDatabase()
    {
        MonstersDataList.Instance.Remove(this);

        EditorUtility.SetDirty(MonstersDataList.Instance);
        AssetDatabase.SaveAssets();
    }
#endif
}
