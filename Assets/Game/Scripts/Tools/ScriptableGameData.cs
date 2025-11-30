using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Killemall.Tools;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Killemall.Data
{
    public class ScriptableGameData : SerializedScriptableObject
    {
        public string Id => _id;

        [PropertySpace(SpaceBefore = 4)]
        [TitleGroup("Settings", order: 99)]
        [ReadOnly, LabelWidth(40), LabelText("ID")]
        [SerializeField] private string _id = string.Empty;

        #region Methods
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(_id))
                GenerateId();
        }

        protected virtual void OnEnable()
        {
            if (string.IsNullOrEmpty(_id))
                GenerateId();
        }

        [PropertySpace(SpaceBefore = 4)]
        [TitleGroup("Settings")]
        [InfoBox("\r\n Asset is not included in the database. Use the button below to include it in the game.\r\n", InfoMessageType = InfoMessageType.Warning, VisibleIf = nameof(ShowAddToDatabase), GUIAlwaysEnabled = true)]
        [Button("Generate Unique ID", ButtonSizes.Medium), PropertyTooltip("Generates a new unique Id for this game data. Might break savefiles if the asset is already in use!")]
        private void GenerateId()
        {
            var elements = CheckGameDataIDs();
            for (int i = 0; i < 1000; ++i)
            {
                _id = IdGenerator.GenerateUniqueID();
                if (elements.All(element => element.Id != _id))
                {
                    return;
                }
            }
        }

        [PropertySpace(SpaceBefore = 4)]
        [TitleGroup("Settings")]
        [Button("Check GameData IDs", ButtonSizes.Medium), PropertyTooltip("Check for game datas with duplicate ids. Result is printed in the console.")]
        private void CheckForDuplicates()
        {
            CheckGameDataIDs();
        }

        protected virtual bool IsInDatabase() => false;
        protected virtual bool CanBeAddedToDatabase() => true;

        private bool ShowAddToDatabase => !IsInDatabase() && CanBeAddedToDatabase();

        [PropertySpace(SpaceBefore = 8)]
        [HorizontalGroup("Settings/Split")]
        [ShowIf(nameof(ShowAddToDatabase))]
        [Button("Add To Database", ButtonSizes.Large, Icon = SdfIconType.Plus), GUIColor(0, 1f, 0f)]
        public virtual void AddToDatabase()
        {
            // Override in derived classes if needed
        }

        [PropertySpace(SpaceBefore = 8)]
        [HorizontalGroup("Settings/Split")]
        [ShowIf(nameof(IsInDatabase))]
        [Button("Remove From Database", ButtonSizes.Large, Icon = SdfIconType.Dash), GUIColor(1, 0.6f, 0.4f)]
        public virtual void RemoveFromDatabase()
        {
            // Override in derived classes if needed
        }

        [PropertySpace(SpaceBefore = 8)]
        [HorizontalGroup("Settings/Split")]
        [Button("Delete", ButtonSizes.Large, Icon = SdfIconType.Trash), GUIColor("red")]
        protected void Delete()
        {
            string path = AssetDatabase.GetAssetPath(this);
            if (EditorUtility.DisplayDialog("Delete selected asset?", path + "\r\n\r\nYou cannot undo the delete assets action.", "Delete", "Cancel"))
            {
                RemoveFromDatabase();

                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }

        [MenuItem("Kalank/Data/Check GameData IDs")]
        public static HashSet<ScriptableGameData> CheckGameDataIDs()
        {
            var ids = new Dictionary<string, ScriptableGameData>();
            HashSet<ScriptableGameData> faultyElements = new HashSet<ScriptableGameData>();
            foreach (string guid in AssetDatabase.FindAssets("t:ScriptableGameData"))
            {
                var element = AssetDatabase.LoadAssetAtPath<ScriptableGameData>(AssetDatabase.GUIDToAssetPath(guid));
                if (ids.TryGetValue(element.Id, out var otherElement))
                {
                    faultyElements.Add(element);
                    faultyElements.Add(otherElement);
                }
                else
                {
                    ids.Add(element.Id, element);
                }
            }

            Debug.LogError($"{faultyElements.Count} items with duplicated IDs");
            foreach (ScriptableGameData faultyElement in faultyElements)
            {
                Debug.LogError($"ID {faultyElement.Id} used by : {faultyElement.name}", faultyElement);
            }

            return faultyElements;
        }
#endif // UNITY_EDITOR
        #endregion
    }
}
