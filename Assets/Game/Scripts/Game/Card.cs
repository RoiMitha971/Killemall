using System.Collections.Generic;
using Killemall.Data;
using UnityEngine;

/// <summary>
/// Card object handle the interaction and visual behaviour of <see cref="T"/>
/// </summary>


public abstract class Card : MonoBehaviour
{
    public bool Active
    {
        get
        {
            return _active;
        }
        protected set
        {
            _active = value;
            gameObject.SetActive(value);
        }
    }
    private bool _active;

    protected Dictionary<DamageType, Color> _typeColors = new Dictionary<DamageType, Color>
    {
        { DamageType.Physical, Color.gray },
        {DamageType.Fire, Color.red },
        {DamageType.Poison, Color.green },
        {DamageType.Magic, Color.blue },
    };

    public abstract void RefreshVisuals();

}
