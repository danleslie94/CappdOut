using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public List<List<Item>> inventory;
    public List<Item> BCArray;
    public List<Item> TCArray;
    private ItemDatabase database;
    private GameManager gameManager;
	private CanvasManager canvasManager;
    

    public float boxValue = 0.0f;

	void Start ()
    {
        inventory = new List<List<Item>>();
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
		canvasManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<CanvasManager>();
        
    }

    public void AddToRecycleBox(Item item)
    {
        Debug.Log("Adding to Recycle Box...");
        Debug.Log("Inventory size: " + inventory.Count);
        switch (item.itemID)
        {
            case 1:
               if(database.items[0].stackableSize <= 1)
               {

                    BCArray = new List<Item>();
                    Debug.Log("BC Array size: " + BCArray.Count);
                    BCArray.Add(item);
                    Debug.Log("BC Array size: " + BCArray.Count);
                    inventory.Add(BCArray);
                    if (inventory.Contains(BCArray))
                    {
                        Debug.Log("BC Array in inventory");
                    }
                    break;
               }
               else
               {
                    Debug.Log("BC Array size: " + BCArray.Count);
                    BCArray.Add(item);
                    Debug.Log("BC Array size: " + BCArray.Count);
                    inventory.Add(BCArray);
                    if (inventory.Contains(BCArray))
                    {
                        Debug.Log("BC Array in inventory");
                    }
                    break;
               }
                       
            case 2:
                TCArray = new List<Item>();
                TCArray.Add(item);
                inventory.Add(TCArray);
                if (inventory.Contains(TCArray))
                {
                    Debug.Log("TC Array in inventory");
                }
                break;
            default:
                break;
        }

        Debug.Log("Inventory size: " + inventory.Count);
    }
    public void CalculateTotalCashValueOfItemsInRecycleBox()
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (BCArray.Count >= 0)
            {
                for (int j = BCArray.Count - 1; j >= 0; j--)
                {
                    boxValue = boxValue + BCArray[j].value;
                }
            } 
            if (TCArray.Count >= 0)
            {
                for (int j = TCArray.Count - 1; j >= 0; j--)
                {
                    boxValue = boxValue + TCArray[j].value;
                }
            }
        }

    }
    public float ReturnTotalCashValueOfItemsInRecycleBox()
    {
        return boxValue;
    }
    public void SellAll()
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            for (int j = BCArray.Count - 1; j >= 0; j--)
            {
                gameManager.IncreasePlayerCash(BCArray[j].value);
                BCArray.Remove(BCArray[j]);
            }
            for (int j = TCArray.Count - 1; j >= 0; j--)
            {
                gameManager.IncreasePlayerCash(TCArray[j].value);
                TCArray.Remove(TCArray[j]);
            }   
                //print(inventory[i].itemName);
            inventory.Remove(inventory[i]);
			canvasManager.inventoryFullText.text = "";
        }
        boxValue = 0.0f;
    }

    public void IncreaseBCStackSize()
    {
        if(!database.items[0].firstBoxUpgrade)
        {
            database.items[0].stackableSize = 5;
            database.items[0].firstBoxUpgrade = true;
        }
        else
        {
            database.items[0].stackableSize += 5;
        }
        
    }
}
