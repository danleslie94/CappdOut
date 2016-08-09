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
    

    public int inventoryArraySize = 5;
    float boxValue = 0.0f;

	void Start ()
    {
        inventory = new List<List<Item>>();
        BCArray = new List<Item>();
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
		canvasManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<CanvasManager>();
        
    }

    public void AddToRecycleBox(Item item)
    {
        switch (item.itemID)
        {
            case 1:
                BCArray.Add(item);
                inventory.Add(BCArray);
                break;

            default:
                break;
        }
        
    }
    public void CalculateTotalCashValueOfItemsInRecycleBox()
    {
        for (int i = inventory.Count -1; i >= 0; i--)
        {
            //boxValue = boxValue + inventory[i].value;
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
            //gameManager.IncreasePlayerCash(inventory[i].value);
            //print(inventory[i].itemName);
            inventory.Remove(inventory[i]);
			canvasManager.inventoryFullText.text = "";
        }
        boxValue = 0.0f;
    }
}
