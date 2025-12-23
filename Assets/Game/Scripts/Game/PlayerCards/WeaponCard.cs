using Killemall.Game;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : PlayerCard<Weapon>
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _damageLabel;
    [PropertySpace]
    [SerializeField] private Image _fullArt;
    [PropertySpace]
    [SerializeField] private Image _damageType;
    [SerializeField] private Image _socketType;
    public override void Activate(Weapon card)
    {
        if (card == null)
            return;
        _context = card;
        RefreshVisuals();
        Active = true;
    }

    public override void RefreshVisuals()
    {
        _nameLabel.text = _context.WeaponName;
        _damageLabel.text = _context.DamageAmount.ToString();
        _fullArt.sprite = _context.Sprite;
        _damageType.color = _typeColors[_context.DamageType];
        _socketType.color = _typeColors[_context.Socket];
    }

    public override void Use()
    {
        Monster target = null;
        _context.DealDamage(target);
    }
}
