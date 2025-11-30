
using Killemall.Data;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_New_Data", menuName = "Killemall/Data/Monsters/Monster")]
public class MonsterData : ScriptableGameData
{
    public string MonsterName => _monsterName;
    public DamageType Resistance => _resistance;
    public DamageType Weakness => _weakness;
    public int Strength => (int)_monsterTiers+1;
    public int Health => _health;

    [SerializeField] private string _monsterName;

    [SerializeField] private DamageType _resistance;
    [SerializeField] private DamageType _weakness;

    [SerializeField] private MonsterTier _monsterTiers;

    [SerializeField] private int _health;

    [SerializeField] private int _damage;

    [SerializeField] private int _goldReward;

#if UNITY_EDITOR
    protected override bool IsInDatabase()
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
