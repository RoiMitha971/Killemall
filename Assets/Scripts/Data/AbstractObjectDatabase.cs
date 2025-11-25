using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public abstract class AbstractObjectDatabase<T>
{
    protected ReadOnlyCollection<T> Data => _data.AsReadOnly();

    protected List<T> _data;

    protected abstract void LoadData();
}
