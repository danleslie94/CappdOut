using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blink : MonoBehaviour {


    private bool blink = false;
    public bool startBlink = false;
    private int count = 0;
    public int blinkSpeed = 5;
    public Button button;
	
	void Start ()
    {
        button = GetComponent<Button>();
	}
	
	
	void Update ()
    {
        if (startBlink)
        {
            if (count == blinkSpeed)
            {
                blink = true;
                count = 0;
            }
            else
            {
                blink = false;
            }
            count++;
        }

        
        
	}
    void OnGUI()
    {
        if (blink)
        {
            Debug.Log("Button Blue");
            button.GetComponent<Button>().image.color = Color.cyan;
        }
        else
        {
            Debug.Log("Button White");
            button.GetComponent<Button>().image.color = Color.white;
        }
    }
    public void StartBlink()
    {
        startBlink = true;
    }
    public void StopBlink()
    {
        startBlink = false;
    }
}
