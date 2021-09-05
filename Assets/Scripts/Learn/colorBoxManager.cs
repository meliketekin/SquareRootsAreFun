using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorBoxManager : MonoBehaviour
{
    
    private Image colorBox;
    int random;
    Color color;
    void Start()
    {
        colorBox= GetComponent<Image>();
        changeColor();
    }

    void changeColor()
    {
        random = Random.Range(0, 50);
        if(random<=10)
        {
            color = Color.magenta;
        }
        else if (random <= 20)
        {
            color = Color.yellow;
        }
        else if(random <= 30)
        {
            color = Color.green;
        }
        else if(random <= 40)
        {
            color = Color.red;
        }
        else if(random <= 50)
        {
            color = Color.cyan;
        }
        colorBox.color = color;
    }
    
}
