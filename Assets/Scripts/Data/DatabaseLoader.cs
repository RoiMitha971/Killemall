using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatabaseLoader
{
    public static DatabaseLoader Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new DatabaseLoader();
            }
            return _instance;
        }
    }

    public static PrefabsDatabase PrefabsDB => Instance._prefabsDatabase;
    public static MonstersDatabase MonstersDB => Instance._monstersDatabase;

    private static DatabaseLoader _instance;

    private PrefabsDatabase _prefabsDatabase;
    private MonstersDatabase _monstersDatabase;

    
    private DatabaseLoader()
    {
        _prefabsDatabase = Resources.Load<PrefabsDatabase>("PrefabsDatabase");
        _monstersDatabase = new MonstersDatabase();
    }
}
