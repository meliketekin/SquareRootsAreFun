using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class squareRootButtonManager : MonoBehaviour
{
    [SerializeField]
    private Image squareRootImage;
    public int buttonNo;
    LearnManager learnManager;
    private void Awake()
    {
        learnManager = Object.FindObjectOfType<LearnManager>();
    }
    public void buttonClick()
    {
        learnManager.showAnswer(buttonNo);
    }
}
