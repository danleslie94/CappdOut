using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public List<Item> inventory = new List<Item>();
    private ItemDatabase database;
    private GameManager gameManager;
	private CanvasManager canvasManager;


	void Start ()
    {
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
		canvasManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<CanvasManager>();
    }

    public void AddToRecycleBox(Item item)
    {
        inventory.Add(item);
    }

    public void SellAll()
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            gameManager.IncreasePlayerCash(inventory[i].value);
            print(inventory[i].itemName);
            inventory.Remove(inventory[i]);
			canvasManager.inventoryFullText.text = "";
        }

    }
}
