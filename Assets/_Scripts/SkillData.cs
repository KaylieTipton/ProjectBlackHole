using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CurrentSkill
{
    None,
    Woodcutting,
    Mining,
    Foraging,

    Building
};

public class SkillData : MonoBehaviour
{
    //Skill EXP and Level Variables as well as the current skill enum variable
    public CurrentSkill currentSkill;
    public ScriptableObject_SkillingItems currentSkillingItems = null;
    public List<ScriptableObject_SkillingItems> skillingItems;
    private ScriptableObject_CraftingItems currentCraftingItem = null;
    public Inventory inventory;
    public Timer timer;
    public int totalLevel;
    public int totalExp;
    public int woodcutLevel;
    public int woodcutExp;
    public int mineLevel;
    public int mineExp;
    public int forageLevel;
    public int forageExp;

    public int buildLevel;
    public int buildExp;




    // Sets the current skills based on what button is pressed that is linked to the functions
    // Should be placed on the corresponding skill button so it will set the proper ENUM
    public void SetSkillWoodCutting()
    {
        SetCurrentSkill(CurrentSkill.Woodcutting);
        currentSkillingItems = null;
        timer.StopTimer();
    }

    public void SetSkillMining()
    {
        SetCurrentSkill(CurrentSkill.Mining);
        currentSkillingItems = null;
        timer.StopTimer();
    }


    public void SetSkillBuilding()
    {
        SetCurrentSkill(CurrentSkill.Building);
        currentSkillingItems = null;
        timer.StopTimer();
    }

    // Performs the SkillAction based on whatever the current skill is
    // Is located on the item buttons under a particular skill so it knows what item the current item is that the player is gathering
    public void SkillButtonAction(ScriptableObject_SkillingItems _item)
    {
        currentSkillingItems = _item;

        timer.StartTimer(currentSkillingItems.timeTakenToGather, false);
    }

    public void TimeOut()
    {
        bool autoRestart = false;
        if (currentSkill == CurrentSkill.None || currentSkillingItems == null)
            return;

        if (currentSkillingItems.isUnlocked)
        {
            switch (currentSkill)
            {
                case CurrentSkill.Woodcutting:
                    SkillAction(out woodcutLevel, out woodcutExp, woodcutLevel, woodcutExp);
                    autoRestart = true;
                    break;
                case CurrentSkill.Mining:
                    SkillAction(out mineLevel, out mineExp, mineLevel, mineExp);
                    autoRestart = true;
                    break;
                case CurrentSkill.Building:
                    Debug.Log("Build Action?");
                    autoRestart = CraftingAction(out buildLevel, out buildExp, buildLevel, buildExp);
                    break;
            }
            if (autoRestart)
                timer.StartTimer(currentSkillingItems.timeTakenToGather, false);
        }
        else{
            Debug.Log("Item is Locked");
        }

    }

    // Sets the Current Skill in the appropriate SetSkill function above
    public void SetCurrentSkill(CurrentSkill _currentSkill)
    {
        currentSkill = _currentSkill;
    }

    // Performs the skill action of gaining EXP and Levels based on information passed into the function
    // Note for Future: Make an actual Proper level System where the number of exp needed increases per level
    // Located in the TimeOut function in the switch statement under whatever the current skill is
    public void SkillAction(out int skillLevel, out int skillExp, int startSkillLevel, int startSkillExp)
    {
        skillExp = startSkillExp;
        skillLevel = startSkillLevel;
        skillExp = skillExp + currentSkillingItems.exp;
        //Need To Increment Items and the Inventory
        currentSkillingItems.IncrementItem();
        inventory.AddItem(currentSkillingItems);
        LevelUp(skillExp, out skillLevel, skillLevel);


        Debug.Log("Action Completed. Exp Gained. Current EXP: " + skillExp);
    }

    public bool CraftingAction(out int skillLevel, out int skillExp, int startSkillLevel, int startSkillExp)
    {
        skillExp = startSkillExp;
        skillLevel = startSkillLevel;
        
        
        Debug.Log("BUild ACTION!");
        if(currentSkillingItems is ScriptableObject_CraftingItems)
        {
            currentCraftingItem = (ScriptableObject_CraftingItems)currentSkillingItems;
            for(int i = 0; i < inventory.inventoryList.Count; i++)
            {
                Debug.Log("I " + i);
                for(int j = 0; j < currentCraftingItem.recipe.Count; j++)
                {
                    
                    if(!(inventory.inventoryList[i].stackSize >= currentCraftingItem.recipe[j].ingredientAmount))
                    {
                        Debug.Log("Ingredients have. Removed. Item Crafted");
                        return false;
                        //inventory.RemoveItem(inventory.inventoryList[i].skillingItem, currentCraftingItem.recipe[j].ingredientAmount);
                        
                    }

                }
                for(int j = 0; j < currentCraftingItem.recipe.Count; j++)
                {
                    
                    if(inventory.inventoryList[i].stackSize >= currentCraftingItem.recipe[j].ingredientAmount)
                    {
                        Debug.Log("Ingredients have. Removed. Item Crafted");
                        inventory.RemoveItem(inventory.inventoryList[i].skillingItem, currentCraftingItem.recipe[j].ingredientAmount);
                        
                    }
                    
                }
            }
        }
        else{
            return false;
        }

        inventory.AddItem(currentSkillingItems);
        currentSkillingItems.IncrementItem();
        skillExp = skillExp + currentSkillingItems.exp;
        LevelUp(skillExp, out skillLevel, skillLevel);

        return true;
    }



/*    public void CraftingAction(out int skillLevel, out int skillExp, int startSkillLevel, int startSkillExp)
    {
        skillExp = startSkillExp;
        skillLevel = startSkillLevel;
        skillExp = skillExp + currentSkillingItems.exp;
        Debug.Log("BUild ACTION!");
        if(currentSkillingItems is ScriptableObject_CraftingItems)
        {
            Debug.Log("Starting the Action. First If Statement");
            currentCraftingItem = (ScriptableObject_CraftingItems)currentSkillingItems;
            for(int i = 0; i < inventory.inventoryList.Count; i++)
            {
                Debug.Log("I " + i);
                if(inventory.inventoryList[i].skillingItem == currentCraftingItem.recipe[0].ingredientItem)
                {
                    Debug.Log("Inventory item " + inventory.inventoryList[i].skillingItem + " , Ingredient Item  " +  currentCraftingItem.recipe[0].ingredientItem);
                    //This for loop is screaming
                    for(int j = 0; j < currentCraftingItem.recipe[j].ingredientAmount; j++)
                    {
                        Debug.Log("Second For Loop");
                        if(inventory.inventoryList[i].skillingItem.quanity == currentCraftingItem.recipe[j].ingredientAmount)
                        {
                            Debug.Log("Removing " + currentCraftingItem.recipe[j].ingredientItem + " , Current Amount " + inventory.inventoryList[i].skillingItem.quanity);
                            inventory.RemoveItem(currentCraftingItem.recipe[j].ingredientItem);
                        }
                    }
                }
                
            }
        }
    }*/

    // Level ups the player and unlocks any items that need to be unlocked
    // Called in the SkillAction function and checks if the player has leveled up and unlocked a new item
    public void LevelUp(int skillExp, out int skillLevel, int startSkillLevel)
    {
        skillLevel = startSkillLevel;
        if (skillExp >= 100 * skillLevel)
        {
            skillLevel++;
            Debug.Log("Level Up" + skillLevel);
            
            // Unlock Item based on checking if any items in the list meet their unlock reqs and running the UnlockItem function in the SO
            for(int i = 0; i < skillingItems.Count; i++)
            {
                if(skillLevel >= skillingItems[i].lvlReq && skillingItems[i].skillType == currentSkill)
                {   
                    skillingItems[i].UnlockItem();
                    Debug.Log("Item Unlocked");
                }
            }
        }
    }


    

}
