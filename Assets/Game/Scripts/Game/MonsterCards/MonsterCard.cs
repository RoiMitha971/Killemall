using Killemall.Data.GameplaySettings;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class MonsterCard : Card<MonsterData>
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _healthLabel;

    [SerializeField] private Image _resistIcon;
    [SerializeField] private Image _weakIcon;

    [Header("Fields")]
    [SerializeField] private SpriteLibraryAsset _library;


    public override void Activate(MonsterData monster)
    {
        if(monster == null)
        {
            return;
        }

        _context = monster;
        _nameLabel.text = _context.MonsterName;
        _healthLabel.text = _context.Health.ToString();
        
        _resistIcon.sprite = _library.GetSprite("DamageTypes", _context.Resistance.ToString());
        _weakIcon.sprite = _library.GetSprite("DamageTypes", _context.Weakness.ToString());

        Active = true;
    }

    [Button("Deactivate")]
    public void Deactivate()
    {
        Active = false;
    }

    public static MonsterCard New(Transform parent)
    {
        var go = Instantiate(original:GameplaySettings.Instance.Prefabs.MonsterCardPrefab, parent:parent);
        if(go.TryGetComponent(out MonsterCard card))
        {
            card.Active = false;
            return card;
        }
        else
        {
            Destroy(go);
            return null;
        }
    }
}
