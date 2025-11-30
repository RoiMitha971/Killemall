using UnityEditor;
using UnityEngine;

namespace Killemall.Data
{
    [CreateAssetMenu(fileName = "Weapon_New_Data", menuName = "Killemall/Data/Weapons/Weapon")]
    public class WeaponData : ScriptableGameData
    {
        public string WeaponName => _weaponName;
        public DamageType DamageType => _damageType;
        public int Damage => _damageAmount;
        public DamageType Socket => _socket;

        [SerializeField] private string _weaponName;

        [SerializeField] private DamageType _damageType;
        [SerializeField] private int _damageAmount;

        [SerializeField] DamageType _socket;

#if UNITY_EDITOR
        protected override bool IsInDatabase()
        {
            return WeaponsDataList.Instance.Contains(this);
        }

        public override void AddToDatabase()
        {
            WeaponsDataList.Instance.Add(this);

            EditorUtility.SetDirty(WeaponsDataList.Instance);
            AssetDatabase.SaveAssets();
        }

        public override void RemoveFromDatabase()
        {
            WeaponsDataList.Instance.Remove(this);

            EditorUtility.SetDirty(WeaponsDataList.Instance);
            AssetDatabase.SaveAssets();
        }
#endif
    }

}
