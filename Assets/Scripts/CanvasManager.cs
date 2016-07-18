using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(BottleCap))]


public class CanvasManager : MonoBehaviour {

    public Canvas GameScreen;
    public Canvas ResearchScreen;
    public Canvas UpgradeScreen;

    public Image background;

    public Text cashText;
    public Text cashTextInResearch;
    public Text cashTextInUpgrades;

    public Text BCSearchResult;
    public Text BCSearchTime;
    public GameObject BCResearchCost;
    public GameObject BCValueUpCost;
    public Button BCButton;
    private BottleCap bottleCap;
    private GameManager gameManager;
    public bool isSearchingBC = false;

    public float timeRemaining = 0.0f;

    void Start()
    {
        ResearchScreen.enabled = false;
        UpgradeScreen.enabled = false;
        gameManager = GetComponent<GameManager>();
        bottleCap = GetComponent<BottleCap>();

    }
    void Update()
    {
        cashText.fontSize = 20;
        cashText.text = "$" + gameManager.GetPlayerCash();
        cashTextInResearch.text = "$" + gameManager.GetPlayerCash();
        cashTextInUpgrades.text = "$" + gameManager.GetPlayerCash();

        BCResearchCost.GetComponent<Text>().text = "$" + bottleCap.bCResearchCost;
        BCValueUpCost.GetComponent<Text>().text = "$" + bottleCap.bottleCapValueIncreaseCost;
        

        if (isSearchingBC)
        {





            displayBCSearchResult(0);
            timeRemaining -= Time.deltaTime;
            BCSearchTime.text = timeRemaining.ToString("#.0");
            BCButton.interactable = false;
            if (timeRemaining <= 0)
            {
                isSearchingBC = false;
                float rando = Random.Range(gameManager.efficiencyCheckMin, gameManager.efficiencyCheckMax);
                if (rando <= bottleCap.bCSearchEff)
                {
                    gameManager.IncreasePlayerCash(bottleCap.bottleCapValue);
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
    public void incBCSearchEff()
    {
        if (gameManager.playerCash >= bottleCap.bCResearchCost)
        {
            gameManager.playerCash = gameManager.playerCash - bottleCap.bCResearchCost;
            bottleCap.bCSearchEff += 2.5f;
            bottleCap.bCResearchCost += 1.75f;
        }
    }
    public void incBCValue()
    {
        if (gameManager.playerCash >= bottleCap.bottleCapValueIncreaseCost)
        {
            gameManager.playerCash = gameManager.playerCash - bottleCap.bottleCapValueIncreaseCost;
            bottleCap.bottleCapValueIncreaseCost += 2.5f;
            bottleCap.bottleCapValue += 0.05f;
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
}
