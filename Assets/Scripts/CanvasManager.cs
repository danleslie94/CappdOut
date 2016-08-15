using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(ItemDatabase))]


public class CanvasManager : MonoBehaviour {

    public Canvas GameScreen;
    public Canvas ResearchScreen;
    public Canvas UpgradeScreen;
    public Canvas RecycleBoxScreen;
	public Canvas UnlockScreen;

    public GameObject RecycleBoxButtonObj;
    public GameObject UpgradesButtonObj;
    public GameObject ResearchButtonObj;
    public GameObject UnlocksButtonObj;
    public GameObject ShowButtonsButtonObj;
    public Image background;

    public Text cashText;
    public Text cashTextInResearch;
    public Text cashTextInUpgrades;
    public Text cashTextInRecycleBox;
	public Text inventoryFullText;

    public Text BCSearchResult;
    public Text BCSearchTime;
    public GameObject BCSearchEffResearchCost;
    public GameObject BCValueUpCost;
    public Button BCButton;
    public Slider BCSlider;
	bool isSearchingBC = false;
	float BCtimeRemaining = 0.0f;

	public Text TCSearchResult;
	public Text TCSearchTime;
	public GameObject TCSearchEffResearchCost;
	public GameObject TCValueUpCost;
	public GameObject TCUnlockCost;
	public GameObject TCButtonObj;
	public Button TCButton;
    public Button TCUnlockButton;
	public Slider TCSlider;
	bool isSearchingTC = false;
	float TCtimeRemaining = 0.0f;

    private GameManager gameManager;
    private ItemDatabase database;
    private Inventory inventory;

    public Text RecycleBoxTotal;
    public Text RecycleBoxSpace;

	int inventoryMaxSize = 3;
    bool inventoryFull = false;
    bool buttonsShowing = false;
    bool isInRecycleBox = false;

    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        ResearchScreen.enabled = false;
        UpgradeScreen.enabled = false;
        RecycleBoxScreen.enabled = false;
		UnlockScreen.enabled = false;
        gameManager = GetComponent<GameManager>();
		TCButtonObj.SetActive(false);
		TCButton.interactable = false;

    }
    //========================================================================
    void Update()
    {
		//Text Displays
        cashText.fontSize = 20;
        cashText.text = "$" + gameManager.GetPlayerCash().ToString("#.00");
        cashTextInResearch.text = "$" + gameManager.GetPlayerCash().ToString("#.00");
        cashTextInUpgrades.text = "$" + gameManager.GetPlayerCash().ToString("#.00");
        cashTextInRecycleBox.text = "$" + gameManager.GetPlayerCash().ToString("#.00");
        BCSearchEffResearchCost.GetComponent<Text>().text = "$" + database.items[0].researchCost;
        BCValueUpCost.GetComponent<Text>().text = "$" + database.items[0].valueIncreaseCost;

        //Button Display control
        if (buttonsShowing)
        {
            RecycleBoxButtonObj.SetActive(true);
            UnlocksButtonObj.SetActive(true);
            ResearchButtonObj.SetActive(true);
            UpgradesButtonObj.SetActive(true);
        }
        else
        {
            RecycleBoxButtonObj.SetActive(false);
            UnlocksButtonObj.SetActive(false);
            ResearchButtonObj.SetActive(false);
            UpgradesButtonObj.SetActive(false);
        }

		//Blink control 
        if (inventory.inventory.Count > 0)
        {
            if (buttonsShowing)
            {
                RecycleBoxButtonObj.GetComponent<Blink>().StartBlink();
                ShowButtonsButtonObj.GetComponent<Blink>().StopBlink();
            }
            else
            {
                ShowButtonsButtonObj.GetComponent<Blink>().StartBlink();
            }
            
        }
        else if(inventory.inventory.Count <= 0 )
        {
            if (buttonsShowing)
            {
                RecycleBoxButtonObj.GetComponent<Blink>().StopBlink();
            }
            else
            {
                ShowButtonsButtonObj.GetComponent<Blink>().StopBlink();
            }
        }
        //In Recycle Box Check
        if (isInRecycleBox)
        {
            RecycleBoxTotal.text = "$ " + inventory.ReturnTotalCashValueOfItemsInRecycleBox();
            RecycleBoxSpace.text = inventory.inventory.Count + "/" + inventoryMaxSize;
        }
        else
        {

        }
        //Inventory Size Check
		if(inventory.inventory.Count >= inventoryMaxSize)
		{
			inventoryFull = true;
		}
		else
		{
			inventoryFull = false;
		}
		if(inventoryFull)
		{
			inventoryFullText.text = "Recycle Box Full! Go Empty it!";
			BCButton.interactable = false;
			TCButton.interactable = false;
		}
		else
		{
			inventoryFullText.text = "";
			BCButton.interactable = true;
			TCButton.interactable = true;
		}


		//Bottle Cap searching
        if (isSearchingBC && !inventoryFull)
        {
			BCSlider.value = BCtimeRemaining;
			displayBCSearchResult(0);
			BCtimeRemaining -= Time.deltaTime;
			BCSearchTime.text = "Time Remaining     " + BCtimeRemaining.ToString("#.0");
			BCButton.interactable = false;
			if (BCtimeRemaining <= 0)
			{
				float rando = Random.Range(gameManager.efficiencyCheckMin, gameManager.efficiencyCheckMax);
				if (rando <= database.items[0].searchEfficiency)
				{
					inventory.AddToRecycleBox(database.items[0]);
					displayBCSearchResult(1);
				}
				else
				{
					displayBCSearchResult(-1);
				}
				isSearchingBC = false;
			}
		
		}
		else if(!isSearchingBC && !inventoryFull)
		{
			BCButton.interactable = true;			
		}
		
		//Tin Can searching
		if(isSearchingTC && !inventoryFull)
		{
			TCSlider.value = TCtimeRemaining;
			displayTCSearchResult(0);
			TCtimeRemaining -= Time.deltaTime;
			TCSearchTime.text = "Time Remaining      " + TCtimeRemaining.ToString("#.0");
			TCButton.interactable = false;
			if(TCtimeRemaining <= 0)
			{
				isSearchingTC = false;
				float rando = Random.Range(gameManager.efficiencyCheckMin, gameManager.efficiencyCheckMax);
				if(rando <= database.items[1].searchEfficiency)
				{
					inventory.AddToRecycleBox(database.items[1]);
					displayTCSearchResult(1);
				}
				else
				{
					displayTCSearchResult(-1);
				}
			}
		}
		else if(!isSearchingTC && !inventoryFull)
		{
			TCButton.interactable = true;
		}

    }
	//===============================================================================
	//Display search result for bottle cap
    public void displayBCSearchResult(int flag)
    {
        if (flag > 0)
        {
            BCSearchResult.text = "Success! Found a Bottlecap!";
        }
        else if (flag < 0)
        {

            BCSearchResult.text = "Nothing Found! Keep Searching!";
        }
        else
        {
            BCSearchResult.text = "";

        }

    }
	//Bottle Cap search
    public void bottleCapSearch()
    {
        BCSlider.maxValue = database.items[0].searchTime;
		BCtimeRemaining = database.items[0].searchTime;
        isSearchingBC = true;
    }
	//Increase bottle cap search efficiency
    public void incBCSearchEff()
    {
        if (gameManager.playerCash >= database.items[0].researchCost)
        {
            gameManager.playerCash = gameManager.playerCash - database.items[0].researchCost;
            database.items[0].searchEfficiency += 2.5f;
            database.items[0].researchCost += 1.75f;
        }
    }
	//Increase the bottle cap value and the cost to increase the value
    public void incBCValue()
    {
        if (gameManager.playerCash >= database.items[0].valueIncreaseCost)
        {
            gameManager.playerCash = gameManager.playerCash - database.items[0].valueIncreaseCost;
            database.items[0].valueIncreaseCost += 2.5f;
            database.items[0].value += 0.01f;
        }
    }
	//================================================================================

	//================================================================================
	//Display search result for bottle cap
	public void displayTCSearchResult(int flag)
	{
		if (flag > 0)
		{
			TCSearchResult.text = "Success! Found a Tin Can!";
		}
		else if (flag < 0)
		{
			
			TCSearchResult.text = "Nothing Found! Keep Searching!";
		}
		else
		{
			TCSearchResult.text = "";
			
		}
		
	}
	//Tin Can search
	public void tinCanSearch()
	{
		TCSlider.maxValue = database.items[1].searchTime;
		TCtimeRemaining = database.items[1].searchTime;
		isSearchingTC = true;
	}
	public void incTCSearchEff()
	{
		if (gameManager.playerCash >= database.items[1].researchCost)
		{
			gameManager.playerCash = gameManager.playerCash - database.items[1].researchCost;
			database.items[1].searchEfficiency += 2.5f;
			database.items[1].researchCost += 2.50f;
		}
	}
	//Increase tin can value
	public void incTCValue()
	{
		if(gameManager.playerCash >= database.items[1].valueIncreaseCost)
		{
			gameManager.playerCash = gameManager.playerCash - database.items[1].valueIncreaseCost;
			database.items[1].valueIncreaseCost += 5.0f;
			database.items[1].value += 0.5f;
		}
	}
    //================================================================================
    public void displayButtons()
    {
        if (buttonsShowing)
        {
            buttonsShowing = false;
        }
        else
        {
            buttonsShowing = true;
        }
    }

	//================================================================================
	//Go to research screen from main screen
	public void goToResearch()
	{
		ResearchScreen.enabled = true;
	}
	//Go to main screen from research screen
    public void researchToMain()
    {
        ResearchScreen.enabled = false;
    }
	//Go to upgrades screen from main screen
    public void goToUpgrades()
    {
        UpgradeScreen.enabled = true;
    }
	//Go to main screen from main screen
    public void upgradesToMain()
    {
        UpgradeScreen.enabled = false;
    }
	//Go to recycle box screen from main screen
    public void goToRecycleBox()
    {
        RecycleBoxScreen.enabled = true;
        isInRecycleBox = true;
        inventory.CalculateTotalCashValueOfItemsInRecycleBox();
    }
	//Go to main screen from recycle box screen
    public void recycleToMain()
    {
        RecycleBoxScreen.enabled = false;
        isInRecycleBox = false;
        inventory.boxValue = 0.0f;
    }
	//Go to unlocks screen from main screen
	public void goToUnlocks()
	{
		UnlockScreen.enabled = true;
	}
	//Go to main screen from unlocks screen
	public void unlocksToMain()
	{
		UnlockScreen.enabled = false;
	}
	//================================================================================


	//================================================================================
	//Unlocks Tin Can Button
	public void unlockTC()
	{
        if (gameManager.GetPlayerCash() >= database.items[1].unlockCost)
        {
            TCButtonObj.SetActive(true);
            TCButton.interactable = true;
            gameManager.playerCash = gameManager.playerCash - database.items[1].unlockCost;

        }
		
	}
	//================================================================================
}
