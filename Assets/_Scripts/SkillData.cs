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

    Building,

    AXE
};

public class SkillData : MonoBehaviour
{
    public static SkillData instance { get; private set;}
    //Skill EXP and Level Variables as well as the current skill enum variable
    public CurrentSkill currentSkill;
    public ScriptableObject_SkillingItems currentSkillingItems = null;
    public List<ScriptableObject_SkillingItems> skillingItems;
    private ScriptableObject_CraftingItems currentCraftingItem = null;
    public UIStateMachine stateMachine;
    
    [HideInInspector]
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


    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else{
            Destroy(this.gameObject);
            return;
        }
    }

    public void Start()
    {
        inventory = SkillData.instance.inventory;
    }


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
                Debug.Log("WoodCutt");
                    SkillAction(out woodcutLevel, out woodcutExp, woodcutLevel, woodcutExp,
                    ((ScriptableObject_UpgradeTools)stateMachine.uIWoodcuttingState.axe).gatheringMultiplier,
                    ((ScriptableObject_UpgradeTools)stateMachine.uIWoodcuttingState.axe).expMultiplier);
                    autoRestart = false;
                    timer.StartTimer(currentSkillingItems.timeTakenToGather * ((ScriptableObject_UpgradeTools)stateMachine.uIWoodcuttingState.axe).timeDecreaseMultiplier, false);
                    break;
                case CurrentSkill.Mining:
                    SkillAction(out mineLevel, out mineExp, mineLevel, mineExp, 1, 1);
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
    public void SkillAction(out int skillLevel, out int skillExp, int startSkillLevel, int startSkillExp,
                            int gatherMultiplier, float expMultiplier)
    {
        skillExp = startSkillExp;
        skillLevel = startSkillLevel;
        skillExp = (int)(skillExp + currentSkillingItems.exp * expMultiplier);
        //Need To Increment Items and the Inventory
        currentSkillingItems.IncrementItem(gatherMultiplier);
        inventory.AddItem(currentSkillingItems);
        LevelUp(skillExp, out skillLevel, skillLevel);


        //Debug.Log("Action Completed. Exp Gained. Current EXP: " + skillExp);
    }

    public bool CraftingAction(out int skillLevel, out int skillExp, int startSkillLevel, int startSkillExp)
    {
        skillExp = startSkillExp;
        skillLevel = startSkillLevel;
        
        List<int> index = new List<int>();

        if(currentSkillingItems is ScriptableObject_CraftingItems)
        {
            currentCraftingItem = (ScriptableObject_CraftingItems)currentSkillingItems;

            for(int i = 0; i < currentCraftingItem.recipe.Count; i++)
            {
                for(int j = 0; j < inventory.inventoryList.Count; j++)
                {
                    Debug.Log("J: " + j);
                    Debug.Log("Inventory: " + inventory.inventoryList[j].skillingItem + " Recipe: " + currentCraftingItem.recipe[i].ingredientItem);
                    Debug.Log("Inventory: " + inventory.inventoryList[j].skillingItem.ID + " Recipe: " + currentCraftingItem.recipe[i].ingredientItem.ID);
                    if(inventory.inventoryList[j].skillingItem.ID == currentCraftingItem.recipe[i].ingredientItem.ID)
                    {
                        Debug.Log("Inventory 2: " + inventory.inventoryList[j].stackSize + " Recipe 2:  " + currentCraftingItem.recipe[i].ingredientAmount);
                        if(inventory.inventoryList[j].stackSize >= currentCraftingItem.recipe[i].ingredientAmount)
                        {
                            index.Add(j);
                            Debug.Log("Help: " + inventory.inventoryList[j].skillingItem);
                           // Debug.Log("Index: " + index[j]);
                        }
                    }
                }
            }

            if(index.Count == currentCraftingItem.recipe.Count)
            {
                for(int i = 0; i < index.Count; i++)
                {
                    Debug.Log("Index: " + index[i]);
                    inventory.RemoveItem(inventory.inventoryList[index[i]].skillingItem, currentCraftingItem.recipe[i].ingredientAmount);
                    
                    Debug.Log("HAve all ingredients");
                    Debug.Log("Inventory: " + inventory.inventoryList[index[i]].skillingItem + " Recipe: " + currentCraftingItem.recipe[i].ingredientItem);
                    
                }
            }
            else
                return false;
            
        }
        currentSkillingItems.IncrementItem(1);
        inventory.AddItem(currentSkillingItems);
        skillExp = (int)(skillExp + currentSkillingItems.exp);
        Debug.Log("c" + currentCraftingItem.exp);
        Debug.Log("s" +currentSkillingItems.exp);
        Debug.Log("s" + currentSkillingItems);
        Debug.Log("c" + currentCraftingItem);
        LevelUp(skillExp, out skillLevel, skillLevel);
        return true;
    }


      
    

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