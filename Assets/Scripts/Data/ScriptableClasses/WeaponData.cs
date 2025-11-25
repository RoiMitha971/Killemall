using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon_New_Data", menuName = "ScriptableObjects/Data/Weapon")]
public class WeaponData : SerializedScriptableObject
{
    public string WeaponName => _weaponName;
    public DamageTypes DamageType => _damageType;
    public int Damage => _damageAmount;
    public DamageTypes Socket => _socket;

    [SerializeField] private string _weaponName;

    [SerializeField] private DamageTypes _damageType;
    [SerializeField] private int _damageAmount;

    [SerializeField] DamageTypes _socket;
}
