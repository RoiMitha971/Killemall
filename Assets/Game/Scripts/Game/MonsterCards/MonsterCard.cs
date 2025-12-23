using System.Collections.Generic;
using System.Threading;
using Killemall.Data.GameplaySettings;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class MonsterCard : Card
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _healthLabel;
    [PropertySpace]
    [SerializeField] private Image _fullArt;
    [PropertySpace]
    [SerializeField] private Image _resistIcon;
    [SerializeField] private Image _weakIcon;

    private Monster _monster; 
    public void Activate(Monster monster)
    {
        if(monster == null)
        {
            return;
        }

        _monster = monster;
        RefreshVisuals();
        Active = true;
    }

    public override void RefreshVisuals()
    {
        _nameLabel.text = _monster.MonsterName;
        _healthLabel.text = _monster.Health.ToString();

        _fullArt.sprite = _monster.Sprite;

        _resistIcon.color = _typeColors[_monster.Resistance];
        _weakIcon.color = _typeColors[_monster.Weakness];
    }

    public void Deactivate()
    {
        Active = false;
    }
}
