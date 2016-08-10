using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum Colors
{
    CYAN,
    GREEN,
    BLUE,
    MAGENTA
};
public class Blink : MonoBehaviour {

    public Colors thisBlinkColor;
    private Color blinkColor;
    private Color defaultColor;
    public bool startBlink = false;
    public float blinkTime = 2.5f;
    public Button button;
	
	void Start ()
    {
        button = GetComponent<Button>();
        defaultColor = Color.white;

        if (thisBlinkColor == Colors.CYAN)
        {
            blinkColor = Color.cyan;
        }
        else if (thisBlinkColor == Colors.BLUE)
        {
            blinkColor = Color.blue;
        }
        else if (thisBlinkColor == Colors.GREEN)
        {
            blinkColor = Color.green;
        }
        else if (thisBlinkColor == Colors.MAGENTA)
        {
            blinkColor = Color.magenta;
        }
	}
	
	
	void Update ()
    {
        if (startBlink)
        {
            if (button.GetComponent<Button>().image.color == blinkColor)
            {
                StartCoroutine(BlinkForXSeconds(blinkTime, defaultColor));
            }
            else if (button.GetComponent<Button>().image.color == defaultColor)
            {
                StartCoroutine(BlinkForXSeconds(blinkTime, blinkColor));
            }
        }
        else
        {
            button.GetComponent<Button>().image.color = defaultColor;
        }
     

    }
    IEnumerator BlinkForXSeconds(float blinkTime, Color colorChange)
    {
        yield return new WaitForSeconds(blinkTime);
        button.GetComponent<Button>().image.color = colorChange;
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
  