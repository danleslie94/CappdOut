using UnityEngine;
using System.Collections;

public class BottleCap : MonoBehaviour {

    public float bCSearchEff = 10.0f;
    public float bCResearchCost = 5.0f;
    public float bottleCapValue = 0.25f;
    public float bottleCapValueIncreaseCost = 10.0f;
    public float bottleCapSearchTime = 2.0f;
    private CanvasManager canvasManager;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        canvasManager = GetComponent<CanvasManager>();
    }
    
    public void bottleCapSearch()
    {
        canvasManager.timeRemaining = bottleCapSearchTime;
        canvasManager.isSearchingBC = true;               
    }
 
}
