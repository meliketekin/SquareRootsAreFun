using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class menuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject aboutPanel;
    bool isOpen;
    
    void Start()
    {
        aboutPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        isOpen = false;
    }
    public void aboutUsButton()
    {
        if(!isOpen)
        {
            aboutPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            isOpen = true;
        }
        else
        {
            aboutPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            isOpen = false;
        }
        
    } 
    public void exitButton()
    {
        Application.Quit();
    }
    public void playButton()
    {
        SceneManager.LoadScene("LearnScene");
    }
}
