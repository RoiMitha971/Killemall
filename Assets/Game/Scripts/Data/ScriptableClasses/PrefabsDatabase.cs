using Killemall.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

[TypeInfoBox("All Prefabs should be listed here.")]
[ScriptableSingletonFolder("Data")]
[CreateAssetMenu(fileName = "PrefabsDatabase", menuName = "Killemall/Data/PrefabsDatabase")]
public class PrefabsDatabase : ScriptableObjectSingleton<PrefabsDatabase>
{
    public GameObject MonsterCardPrefab => _monsterCardPrefab;

    [SerializeField] private GameObject _monsterCardPrefab;
}
