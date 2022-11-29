using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEMTYPE
{
    RESOURCE,
    AXE,
    PICKAXE,
    BUILDING
}


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ScriptableObject_SkillingItems : ScriptableObject
{
    public string nodeName;
    public string resourceName;
    [TextArea]
    public string itemDescription;
    public int ID;
    public bool isUnlocked;
    public int lvlReq;
    public int exp;
    public float timeTakenToGather;
    public int quanity;
    public Sprite menuIcon;
    public Sprite invIcon;

    public ITEMTYPE itemType;
    public CURRENTSKILL skillType;

    public bool UnlockItem()
    {
        return isUnlocked = true;
    }

    public void IncrementItem(int _amount)
    {
        quanity += _amount;
    }
}
