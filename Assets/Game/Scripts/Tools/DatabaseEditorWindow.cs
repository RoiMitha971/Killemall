using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Killemall.Tools;
using Killemall.Data;

namespace Killemall.Editor
{
    public class DatabaseEditorWindow : OdinMenuEditorWindow
    {
        private class Paths
        {
            public const string MonstersList = "Assets/Game/Resources/Data/MonstersDataList.asset";
            public const string Monster = "Assets/Game/ScriptableObjects/Monsters";

            public const string WeaponsList = "Assets/Game/Resources/Data/WeaponsDataList.asset";
            public const string Weapon = "Assets/Game/ScriptableObjects/Weapons";

        }

        private static OdinMenuStyle menuStyle => new OdinMenuStyle()
        {
            Height = 30,
            Offset = 20.00f,
            IndentAmount = 15.00f,
            IconSize = 26.00f,
            IconOffset = -5.00f,
            NotSelectedIconAlpha = 0.85f,
            IconPadding = 0.00f,
            TriangleSize = 16.00f,
            TrianglePadding = 0.00f,
            AlignTriangleLeft = true,
            Borders = true,
            BorderPadding = 0.00f,
            BorderAlpha = 0.32f,
            SelectedColorDarkSkin = new Color(0.243f, 0.373f, 0.588f, 1.000f),
            SelectedColorLightSkin = new Color(0.243f, 0.490f, 0.900f, 1.000f)
        };

        [MenuItem("Killemall/Data/Database", priority = 0)]
        private static void OpenWindow()
        {
            GetWindow<DatabaseEditorWindow>("Database").Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.DefaultMenuStyle = menuStyle;
            tree.Config.DrawSearchToolbar = true;

            tree.DrawSearchToolbar();
            //tree.AddAssetAtPath("Gameplay Settings", Paths.GameplaySettings, typeof(GameplaySettings)).AddIcon(EditorIcons.SettingsCog);

            tree.AddAssetAtPath("Monsters", Paths.MonstersList, typeof(MonstersDataList));
            tree.AddAllAssetsAtPath("Monsters", Paths.Monster, typeof(MonsterData), includeSubDirectories: true).ForEach(TrimName);

            tree.AddAssetAtPath("Weapons", Paths.WeaponsList, typeof(WeaponsDataList));
            tree.AddAllAssetsAtPath("Weapons", Paths.Weapon, typeof(WeaponData), includeSubDirectories: true).ForEach(TrimName);

            tree.EnumerateTree().SortMenuItemsByName();
            return tree;
        }

        private void TrimName(OdinMenuItem item)
        {
            item.Name = item.Name.Replace("_Data", "");
            item.Name = item.Name.Replace("Monster_", "");
            item.Name = item.Name.Replace("Weapon_", "");

            if (Regex.IsMatch(item.Name, @"^\d{2}_"))
            {
                item.Name = Regex.Replace(item.Name, @"^\d{2}_", "");
            }
        }

        protected override void OnBeginDrawEditors()
        {
            if (MenuTree == null)
                return;

            var selected = MenuTree.Selection.FirstOrDefault();
            if (selected == null)
                return;

            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                SirenixEditorGUI.Title(" " + selected.Name, "", TextAlignment.Left, false);

                SirenixEditorGUI.VerticalLineSeparator(2);

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Ping", "Find and open in inspector window")))
                {
                    Object asset = selected.Value as Object;
                    if (asset)
                        AssetDatabase.OpenAsset(asset);
                }

                SirenixEditorGUI.VerticalLineSeparator();

                CreateNewAssetFromMenuItem(selected);

                SirenixEditorGUI.VerticalLineSeparator();

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Delete", "Remove asset from project")))
                {
                    Object asset = selected.Value as Object;
                    if (asset)
                    {
                        string path = AssetDatabase.GetAssetPath(asset);
                        if (EditorUtility.DisplayDialog("Delete selected asset?", path + "\r\n\r\nYou cannot undo the delete assets action.", "Delete", "Cancel"))
                        {
                            AssetDatabase.DeleteAsset(path);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private void CreateNewAssetFromMenuItem(OdinMenuItem menuItem)
        {
            if (menuItem.Value is MonsterData)
            {
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("New", "Create a new asset")))
                {
                    ScriptableObjectUtility.ShowDialog<MonsterData>(Paths.Monster, obj =>
                    {
                        base.TrySelectMenuItemWithObject(obj);
                    });
                }
            }
        }
    }
}
