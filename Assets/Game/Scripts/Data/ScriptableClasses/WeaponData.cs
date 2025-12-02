using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Killemall.Data
{
    [CreateAssetMenu(fileName = "Weapon_New_Data", menuName = "Killemall/Data/Weapons/Weapon")]
    public class WeaponData : ScriptableGameData
    {
        #region Properties
        public Sprite Sprite => _sprite;
        public string WeaponName => _weaponName;
        public DamageType DamageType => _damageType;
        public int Damage => _damageAmount;
        public DamageType Socket => _socket;
        #endregion

        #region Fields
        [TitleGroup("Weapon")]
        [HorizontalGroup("Weapon/Split", width: 111)]
        [PreviewField(Height = 100, Alignment = ObjectFieldAlignment.Left), HideLabel]
        [SerializeField] private Sprite _sprite;

        [HorizontalGroup("Weapon/Split")]
        [VerticalGroup("Weapon/Split/Right")]
        [LabelText("Weapon Name"), LabelWidth(120)]
        [SerializeField] private string _weaponName;

        [PropertySpace(8)]

        [HorizontalGroup("Weapon/Split")]
        [VerticalGroup("Weapon/Split/Right")]
        [LabelText("Damage Type"), LabelWidth(120)]
        [SerializeField] private DamageType _damageType;

        [HorizontalGroup("Weapon/Split")]
        [VerticalGroup("Weapon/Split/Right")]
        [LabelText("Damage"), LabelWidth(120)]
        [SerializeField] private int _damageAmount;

        [PropertySpace(8)]

        [HorizontalGroup("Weapon/Split")]
        [VerticalGroup("Weapon/Split/Right")]
        [LabelText("Socket"), LabelWidth(120)]
        [SerializeField] DamageType _socket;
        #endregion
        #region Methods

#if UNITY_EDITOR
        public override bool IsInDatabase()
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
        #endregion
    }

}
