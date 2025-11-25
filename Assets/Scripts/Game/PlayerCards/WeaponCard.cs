using System.Threading;
using TMPro;
using UnityEngine;

public class WeaponCard : Card<Weapon>
{
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _damageLabel;
    [SerializeField] private TextMeshProUGUI _socketLabel;
    public override void Activate(Weapon weapon)
    {
        if (weapon == null)
        {
            return;
        }

        _context = weapon;

    }
}
