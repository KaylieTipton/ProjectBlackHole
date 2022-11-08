using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Item", menuName = "Crafting Item")]
public class ScriptableObject_CraftingItems : ScriptableObject_SkillingItems
{
    [SerializeField]
    public List<Ingredients> recipe;    
}

[System.Serializable]
public class Ingredients
{
    public ScriptableObject_SkillingItems ingredientItem;
    public int ingredientAmount;
}
