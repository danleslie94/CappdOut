using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	static GameManager _instance = null;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    public float efficiencyCheckMin = 1.0f;
	public float efficiencyCheckMax = 101.0f;
	public float playerCash = 5.0f;

    public float GetPlayerCash()
    {
        return playerCash;
    }
    public void IncreasePlayerCash(float cashIncrease)
    {
        playerCash = playerCash + cashIncrease;
    }

    void Start()
    {
        playerCash = 5.0f;
    }

	
	


	
	
	



}
