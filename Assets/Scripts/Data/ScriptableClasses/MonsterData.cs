
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_New_Data", menuName = "ScriptableObjects/Data/Monster")]
public class MonsterData : SerializedScriptableObject
{
    public string MonsterName => _monsterName;
    public DamageTypes Resistance => _resistance;
    public DamageTypes Weakness => _weakness;
    public int Strength => (int)_monsterTiers+1;
    public int Health => _health;

    [SerializeField] private string _monsterName;

    [SerializeField] private DamageTypes _resistance;
    [SerializeField] private DamageTypes _weakness;

    [SerializeField] private MonsterTiers _monsterTiers;

    [SerializeField] private int _health;

    [SerializeField] private int _damage;

    [SerializeField] private int _goldReward;
}
