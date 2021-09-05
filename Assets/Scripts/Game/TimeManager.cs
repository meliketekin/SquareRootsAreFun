using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    int timer = 90;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private GameObject finalPanel;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip finishClip;
    FinalManager finalManager;
    GameManager gameManager;
    private void Awake()
    {
        
        gameManager = Object.FindObjectOfType<GameManager>();
    }

    public IEnumerator startTime()
    {
        for(int i = timer; i>=0; i--)
        {
           
            if(i<10)
            {
                timeText.text = "0" + i.ToString();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                 timeText.text = i.ToString();
                timeText.color = Color.red;
                 yield return new WaitForSeconds(1f);
            }
            if(i==1)
            {
                audioSource.PlayOneShot(finishClip);
            }
            if(i==0)
            {
                
                finalPanel.SetActive(true);
                
                if(finalPanel!=null)
                {
                    finalManager = Object.FindObjectOfType<FinalManager>();
                    
                    finalManager.placeInfos(gameManager.trueCounter, gameManager.falseCounter, gameManager.totalScore);
                }
                
            }
        }
    }
}
