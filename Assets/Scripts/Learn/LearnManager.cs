using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;  

public class LearnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playButton, backButton, fadePanel;
    [SerializeField]
    private GameObject squareRootPrefab;
    [SerializeField]
    private Transform content;  //prefablarý içinde tutacak þey
    [SerializeField]
    private Sprite[] squareRootImages, squareRootOuts;
    [SerializeField]
    private Image answerImage;
    [SerializeField]
    private Text learnTxt;
    
    void Start()
    {
        learnTxt.text = "";
        fadePanel.SetActive(true);
        playButton.GetComponent<RectTransform>().localScale = Vector3.zero;
        backButton.GetComponent<RectTransform>().localScale = Vector3.zero;
        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(firstSettings);
        answerImage.sprite = squareRootOuts[0];
        
    }
    void firstSettings()
    {
        learnTxt.text = "You can click the squre roots and learn their recovery values.";
        
        fadePanel.SetActive(false);
        scaleButtons();
        generateSquareRoots();
    }
    public void goBack()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void play()
    {
        SceneManager.LoadScene("GameScene");
    }
    void scaleButtons()
    {
        playButton.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBack);
        backButton.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBounce);
    }

    void generateSquareRoots()
    {
        for (int i = 0; i < squareRootImages.Length; i++)
        {
            GameObject item = Instantiate(squareRootPrefab, content);
            item.GetComponent<squareRootButtonManager>().buttonNo = i;
            item.transform.GetChild(3).GetComponent<Image>().sprite = squareRootImages[i];
        }
        
    }
    public void showAnswer(int buttonNo)
    {
        answerImage.sprite = squareRootOuts[buttonNo];
    }
}
