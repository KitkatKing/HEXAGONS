using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameItem[] inventoryArray = new GameItem[45];

    public int currentItem;

    public InventoryManager()
    {

    }

    //takes an arguement for a text file or whatever and makes an inventory based off it
    //used on loading world
    public bool callFromStorage()
    {
        return true;
    }

    //turns the inventory to a text file to be saved
    //used on world exit
    public bool callToStorage()
    {
        return true;
    }

}
