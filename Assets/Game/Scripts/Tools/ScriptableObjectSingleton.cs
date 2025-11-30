using System.Diagnostics;
using UnityEngine;

namespace Killemall.Tools
{
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (!(Object)_instance)
                {
                    string text = GetAssetPathName();
                    UnityEngine.Debug.LogFormat("Loading {0}'s singleton instance at '{1}'", typeof(T).Name, text);
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    _instance = Resources.Load<T>(text);
                    stopwatch.Stop();

                    if (!(Object)_instance)
                        UnityEngine.Debug.LogWarningFormat("Can't load singleton at {0}. Make sure you've created it with the right name and that it's in a Resources folder", text);
                    else
                        (_instance as ScriptableObjectSingleton<T>).OnInstanceLoaded();

                    if (stopwatch.ElapsedMilliseconds > 500)
                        UnityEngine.Debug.LogWarning("Singleton '" + text + "' took " + stopwatch.ElapsedMilliseconds + " milliseconds to load.");
                }
                return _instance;
            }
        }

        public static bool HasAsset
        {
            get
            {
                if ((bool)(Object)_instance)
                {
                    return true;
                }

                _instance = Resources.Load<T>(GetAssetPathName());
                return (Object)_instance;
            }
        }

        public static string GetAssetPathName()
        {
            object[] customAttributes = typeof(T).GetCustomAttributes(typeof(ScriptableSingletonFolderAttribute), inherit: false);
            if (customAttributes.Length == 0 || !(customAttributes[0] is ScriptableSingletonFolderAttribute scriptableSingletonFolderAttribute))
            {
                return typeof(T).Name;
            }

            return scriptableSingletonFolderAttribute.folder + "/" + typeof(T).Name;
        }

        public virtual void OnInstanceLoaded() { }
    }

    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false)]
    public class ScriptableSingletonFolderAttribute : System.Attribute
    {
        public readonly string folder;

        public ScriptableSingletonFolderAttribute(string folder)
        {
            this.folder = folder;
        }
    }
}
