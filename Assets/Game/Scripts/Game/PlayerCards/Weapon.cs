using UnityEngine;
using Killemall.Data;

namespace Killemall.Game
{
    public class Weapon
    {
        public string WeaponName => _weaponName;
        public int DamageAmount => _damageAmount;
        public DamageType DamageType => _damageType;
        public DamageType Socket => _socket;

        private string _weaponName;
        private int _damageAmount;
        private DamageType _damageType;

        private DamageType _socket;

        public Weapon(WeaponData weaponData)
        {
            _weaponName = weaponData.WeaponName;
            _damageAmount = weaponData.Damage;
            _damageType = weaponData.DamageType;
            _socket = weaponData.Socket;
        }

    }
}

