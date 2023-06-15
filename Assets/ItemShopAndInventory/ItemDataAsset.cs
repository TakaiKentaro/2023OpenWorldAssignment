using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemDataAsset")]
public class ItemDataAsset : ScriptableObject
{
    [SerializeField]
    private List<ItemData> _itemDataList = new List<ItemData>();

    public List<ItemData> ItemDataList { get => _itemDataList; }
}

[System.Serializable]
public class ItemData
{
    [SerializeField, Header("�A�C�e���̖��O")]
    private string _itemName = "None";

    [SerializeField, Header("�A�C�e���̉摜"), Tooltip("�����Ă��ǂ�")]
    private Sprite _sprite;

    [SerializeField, Header("�A�C�e���̌���")]
    private ActionEffect _effect = ActionEffect.Recovery;

    [SerializeField, Header("�A�C�e���̒l�i")]
    private int _price = 0;

    public string Name { get => _itemName; }
    public Sprite Sprite { get => _sprite; }
    public ActionEffect Effect { get => _effect; }
    public int Price { get => _price; }

    public enum ActionEffect
    {
        Recovery, 
        PowerUp,
        DefenseUp,
        SpeedUp,
        None,
    }

    public ActionEffect ApplyEffect()
    {
        return GetEffectMessage( _effect);
    }

    private ActionEffect GetEffectMessage(ActionEffect effect)
    {
        switch (effect)
        {
            case ActionEffect.Recovery:
                return ActionEffect.Recovery;
            case ActionEffect.PowerUp:
                return ActionEffect.PowerUp;
            case ActionEffect.DefenseUp:
                return ActionEffect.DefenseUp;
            case ActionEffect.SpeedUp:
                return ActionEffect.SpeedUp;
        }

        return ActionEffect.None;
    }
}
