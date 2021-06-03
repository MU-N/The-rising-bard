using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    /*
     * Dash
     * Double Jump
     * Mind Control
     * Empowered Attack
     * Time Freeze
     * Hyper Attack
     * 
     */
    public Abilities[] abilities;
    public float playerHP = 100;
    public float playerMana = 100;
    public float playerScore = 0;
    public void restoreOrignalData()
    {
        playerHP = 100;
        playerMana = 100;
    }

    public void startFirstLevelData()
    {
        playerHP = 100;
        playerMana = 100;
        foreach (var item in abilities)
        {
            item.abilityGained = false;
        }
    }

    public void startSecondLevelData()
    {
        playerHP = 100;
        playerMana = 100;
        foreach (var item in abilities)
        {
            item.abilityGained = false;
        }
    }

    public void startThirdLevelData()
    {
        playerHP = 100;
        playerMana = 100;
        for (int i = 0; i < 6; i++)
        {
            if (i > 2)
                abilities[i].abilityGained = true;
            else
                abilities[i].abilityGained = false;
        }
    }

}
