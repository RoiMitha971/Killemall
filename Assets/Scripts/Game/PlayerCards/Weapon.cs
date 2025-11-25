using UnityEngine;

public class Weapon
{
    public string WeaponName => _weaponName;
    public int DamageAmount => _damageAmount;
    public DamageTypes DamageType => _damageType;
    public DamageTypes Socket => _socket;

    private string _weaponName;
    private int _damageAmount;
    private DamageTypes _damageType;

    private DamageTypes _socket;

    public Weapon(WeaponData weaponData)
    {
        _weaponName = weaponData.WeaponName;
        _damageAmount = weaponData.Damage;
        _damageType = weaponData.DamageType;
        _socket = weaponData.Socket;
    }

}
