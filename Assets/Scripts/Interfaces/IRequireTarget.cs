using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public interface IRequireTarget<T>
{
    public abstract Task<List<T>> TargetSelectionTask();
}
