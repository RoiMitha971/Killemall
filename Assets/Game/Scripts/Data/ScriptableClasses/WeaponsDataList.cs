using System.Collections.Generic;
using System.Linq;
using Killemall.Data;
using Killemall.Tools;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;


namespace Killemall.Data
{
    [TypeInfoBox("All Weapons should be listed here.")]
    [ScriptableSingletonFolder("Data")]
    [CreateAssetMenu(fileName = "WeaponsDataList", menuName = "Killemall/Data/Weapons/Weapons List")]
    public class WeaponsDataList : ScriptableObjectSingleton<WeaponsDataList>
    {
        public List<WeaponData> Weapons => _weapons;

        [SerializeField] private List<WeaponData> _weapons;

        #region Methods
        public bool TryGetWeaponFromId(string id, out WeaponData WeaponData)
        {
            foreach (WeaponData exisitingWeaponData in _weapons)
            {
                if (exisitingWeaponData.Id == id)
                {
                    WeaponData = exisitingWeaponData;
                    return true;
                }
            }

            Debug.LogException(new System.Exception($"No Weapons with id {id} found"));
            WeaponData = null;
            return false;
        }

        public bool Contains(WeaponData data)
        {
            return _weapons.Any(x => x.Id == data.Id);
        }

        public void Add(WeaponData data)
        {
            if (!Contains(data))
            {
                _weapons.Add(data);
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
#endif
            }
        }

        public void Remove(WeaponData data)
        {
            if (Contains(data))
            {
                _weapons.Remove(data);
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
#endif
            }
        }

#if UNITY_EDITOR
        [MenuItem("Killemall/Data/Weapons", priority = 51)]
        public static void OpenWindow()
        {
            AssetDatabase.OpenAsset(Instance);
        }
#endif
        #endregion
    }
}

