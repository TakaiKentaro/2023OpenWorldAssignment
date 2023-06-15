using System.Collections.Generic;
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
    }

    public void ApplyEffect()
    {
        Debug.Log(GetEffectMessage(_effect));
    }

    private string GetEffectMessage(ActionEffect effect)
    {
        switch (effect)
        {
            case ActionEffect.Recovery:
                return "�̗͂��񕜂���";
            case ActionEffect.PowerUp:
                return "�U���͂��オ����";
            case ActionEffect.DefenseUp:
                return "�h��͂��オ����";
            default:
                return "";
        }
    }
}
