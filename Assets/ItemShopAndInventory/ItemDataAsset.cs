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
    [SerializeField, Header("アイテムの名前")]
    private string _itemName = "None";

    [SerializeField, Header("アイテムの画像"), Tooltip("無くても良い")]
    private Sprite _sprite;

    [SerializeField, Header("アイテムの効果")]
    private ActionEffect _effect = ActionEffect.Recovery;

    [SerializeField, Header("アイテムの値段")]
    private int _price = 0;

    public string Name { get => _itemName; }
    public Sprite Sprite { get => _sprite; }
    public ActionEffect Effect { get => _effect; }
    public int Price { get => _price; }

    public enum ActionEffect
    {
        Recovery, // 回復
        PowerUp, // 攻撃力上昇
        DefenseUp, // 防御力上昇
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
                return "体力が回復した";
            case ActionEffect.PowerUp:
                return "攻撃力が上がった";
            case ActionEffect.DefenseUp:
                return "防御力が上がった";
            default:
                return "";
        }
    }
}
