using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Tool", menuName = "Upgrade Tool")]
public class ScriptableObject_UpgradeTools : ScriptableObject_SkillingItems
{
    public int gatheringMultiplier;
    public float expMultiplier;
    public float timeDecreaseMultiplier;
}
