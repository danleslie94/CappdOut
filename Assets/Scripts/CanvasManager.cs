using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(ItemDatabase))]


public class CanvasManager : MonoBehaviour {

    public Canvas GameScreen;
    public Canvas ResearchScreen;
    public Canvas UpgradeScreen;
    public Canvas RecycleBoxScreen;

    public Image background;

    public Text cashText;
    public Text cashTextInResearch;
    public Text cashTextInUpgrades;
    public Text cashTextInRecycleBox;

    public Text BCSearchResult;
    public Text BCSearchTime;
    public GameObject BCResearchCost;
    public GameObject BCValueUpCost;
    public Button BCButton;
    public Slider BCSlider;
    private GameManager gameManager;
    private ItemDatabase database;
    private Inventory inventory;

    public bool isSearchingBC = false;

    public float timeRemaining = 0.0f;

    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        ResearchScreen.enabled = false;
        UpgradeScreen.enabled = false;
        RecycleBoxScreen.enabled = false;
        gameManager = GetComponent<GameManager>();      

    }
    void Update()
    {
        cashText.fontSize = 20;
        cashText.text = "$" + gameManager.GetPlayerCash();
        cashTextInResearch.text = "$" + gameManager.GetPlayerCash();
        cashTextInUpgrades.text = "$" + gameManager.GetPlayerCash();
        cashTextInRecycleBox.text = "$" + gameManager.GetPlayerCash();
        BCResearchCost.GetComponent<Text>().text = "$" + database.items[0].researchCost;
        BCValueUpCost.GetComponent<Text>().text = "$" + database.items[0].valueIncreaseCost;
        

        if (isSearchingBC)
        {
            BCSlider.value = timeRemaining;
            displayBCSearchResult(0);
            timeRemaining -= Time.deltaTime;
            BCSearchTime.text = "Time Remaining     " + timeRemaining.ToString("#.0");
            BCButton.interactable = false;
            if (timeRemaining <= 0)
            {
                isSearchingBC = false;
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
            }
     
        }
        else if(!isSearchingBC)
        {
            BCButton.interactable = true;
            
        }
    }
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
    public void goToResearch()
    {
        UpgradeScreen.enabled = false;
        ResearchScreen.enabled = true;
    }
    public void bottleCapSearch()
    {
        BCSlider.maxValue = database.items[0].searchTime;
        timeRemaining = database.items[0].searchTime;
        isSearchingBC = true;
    }
    public void incBCSearchEff()
    {
        if (gameManager.playerCash >= database.items[0].researchCost)
        {
            gameManager.playerCash = gameManager.playerCash - database.items[0].researchCost;
            database.items[0].searchEfficiency += 2.5f;
            database.items[0].researchCost += 1.75f;
        }
    }
    public void incBCValue()
    {
        if (gameManager.playerCash >= database.items[0].valueIncreaseCost)
        {
            gameManager.playerCash = gameManager.playerCash - database.items[0].valueIncreaseCost;
            database.items[0].valueIncreaseCost += 2.5f;
            database.items[0].value += 0.05f;
        }
    }
    public void researchToMain()
    {
        ResearchScreen.enabled = false;
    }
    public void goToUpgrades()
    {
        UpgradeScreen.enabled = true;
    }
    public void upgradesToMain()
    {
        UpgradeScreen.enabled = false;
    }
    public void goToRecycleBox()
    {
        RecycleBoxScreen.enabled = true;
    }
    public void recycleToMain()
    {
        RecycleBoxScreen.enabled = false;
    }
}
