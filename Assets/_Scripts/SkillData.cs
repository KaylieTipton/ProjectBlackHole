using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// Enum For the Current Skill. Has a Reference to All Skill Types
public enum CURRENTSKILL
{
    NONE,
    WOODCUTTING,
    MINING,
    FORAGING,

    BUILDING,
};

public class SkillData : MonoBehaviour
{
    // Singleton of the SkillData class
    public static SkillData instance { get; private set;}
    // Skill EXP and Level Variables as well as the current skill enum variable
    public CURRENTSKILL CURRENTSKILL;
    public ScriptableObject_SkillingItems currentSkillingItem = null;
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
        SetCurrentSkill(CURRENTSKILL.WOODCUTTING);
        currentSkillingItem = null;
        timer.StopTimer();
    }

    public void SetSkillMining()
    {
        SetCurrentSkill(CURRENTSKILL.MINING);
        currentSkillingItem = null;
        timer.StopTimer();
    }


    public void SetSkillBuilding()
    {
        SetCurrentSkill(CURRENTSKILL.BUILDING);
        currentSkillingItem = null;
        timer.StopTimer();
    }

    // Performs the SkillAction based on whatever the current skill is
    // Is located on the item buttons under a particular skill so it knows what item the current item is that the player is gathering
    public void SkillButtonAction(ScriptableObject_SkillingItems _item)
    {
        currentSkillingItem = _item;

        timer.StartTimer(currentSkillingItem.timeTakenToGather, false);
    }

    // Performs the Actions based on whatever skill is selected and Runs the timer.
    // For Gathering Skills it runs until the user selects a new skill or item
    // For Crafting it runs until the user has no more items
    public void TimeOut()
    {
        bool autoRestart = false;
        if (CURRENTSKILL == CURRENTSKILL.NONE || currentSkillingItem == null)
            return;

        if (currentSkillingItem.isUnlocked)
        {
            switch (CURRENTSKILL)
            {
                case CURRENTSKILL.WOODCUTTING:
                    SkillAction(out woodcutLevel, out woodcutExp, woodcutLevel, woodcutExp,
                    ((ScriptableObject_UpgradeTools)stateMachine.uIWoodcuttingState.axe).gatheringMultiplier,
                    ((ScriptableObject_UpgradeTools)stateMachine.uIWoodcuttingState.axe).expMultiplier);

                    autoRestart = false;
                    timer.StartTimer(currentSkillingItem.timeTakenToGather * ((ScriptableObject_UpgradeTools)stateMachine.uIWoodcuttingState.axe).timeDecreaseMultiplier, false);
                    break;

                case CURRENTSKILL.MINING:
                    SkillAction(out mineLevel, out mineExp, mineLevel, mineExp, 1, 1);
                    autoRestart = false;
                    timer.StartTimer(currentSkillingItem.timeTakenToGather, false);
                    break;

                case CURRENTSKILL.BUILDING:
                    autoRestart = CraftingAction(out buildLevel, out buildExp, buildLevel, buildExp);
                    break;
            }
            if (autoRestart)
                timer.StartTimer(currentSkillingItem.timeTakenToGather, false);
        }
        else{
            Debug.Log("Item is Locked");
        }

    }

    // Sets the Current Skill in the appropriate SetSkill function above
    public void SetCurrentSkill(CURRENTSKILL _CURRENTSKILL)
    {
        CURRENTSKILL = _CURRENTSKILL;
    }

    // Performs the skill action of gaining EXP and Levels based on information passed into the function
    // Note for Future: Make an actual Proper level System where the number of exp needed increases per level
    // Located in the TimeOut function in the switch statement under whatever the current skill is
    // Takes in a Gather Multipler and an EXP Multiplier from the users tools
    public void SkillAction(out int skillLevel, out int skillExp, int startSkillLevel, int startSkillExp,
                            int gatherMultiplier, float expMultiplier)
    {
        skillExp = startSkillExp;
        skillLevel = startSkillLevel;
        skillExp = (int)(skillExp + currentSkillingItem.exp * expMultiplier);
        //currentSkillingItem.IncrementItem(gatherMultiplier);
        inventory.AddItem(currentSkillingItem, gatherMultiplier);
        LevelUp(skillExp, out skillLevel, skillLevel);

    }

    // Performs the skill action of gaining EXP and Levels based on information passed into the function
    // Looks through the users inventory and compares the items to the items needed for the crafted item
    // If the item is found and the user has more then the amount needed then the index is saved
    // If the index list meets the same amount as the recipe list then it removes the items from the users inventory
    // and gives the user the crafted item
    public bool CraftingAction(out int skillLevel, out int skillExp, int startSkillLevel, int startSkillExp)
    {
        skillExp = startSkillExp;
        skillLevel = startSkillLevel;
        
        List<int> index = new List<int>();

        if(currentSkillingItem is ScriptableObject_CraftingItems)
        {
            currentCraftingItem = (ScriptableObject_CraftingItems)currentSkillingItem;

            for(int i = 0; i < currentCraftingItem.recipe.Count; i++)
            {
                for(int j = 0; j < inventory.inventoryList.Count; j++)
                {
                    if(inventory.inventoryList[j].skillingItem.ID == currentCraftingItem.recipe[i].ingredientItem.ID)
                    {
                        if(inventory.inventoryList[j].stackSize >= currentCraftingItem.recipe[i].ingredientAmount)
                        {
                            index.Add(j);
                        }
                    }
                }
            }

            if(index.Count == currentCraftingItem.recipe.Count)
            {
                for(int i = 0; i < index.Count; i++)
                {
                    inventory.RemoveItem(inventory.inventoryList[index[i]].skillingItem, currentCraftingItem.recipe[i].ingredientAmount);                   
                }
            }
            else
                return false;
            
        }
        //currentSkillingItem.IncrementItem(1);
        inventory.AddItem(currentSkillingItem);
        skillExp = (int)(skillExp + currentSkillingItem.exp);
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
            
            // Unlock Item based on checking if any items in the list meet their unlock reqs and running the UnlockItem function in the SO
            for(int i = 0; i < skillingItems.Count; i++)
            {
                if(skillLevel >= skillingItems[i].lvlReq && skillingItems[i].skillType == CURRENTSKILL)
                {   
                    skillingItems[i].UnlockItem();
                }
            }
        }
    }
}