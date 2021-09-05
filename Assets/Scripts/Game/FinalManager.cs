using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FinalManager : MonoBehaviour
{
    [SerializeField]
    private Text trueText, falseText, scoreText;
    [SerializeField]
    private GameObject finalImage, backButton, trueFalseImage, scoreImage, timeImage, QACircle;
    private void OnEnable()
    {
        backButton.SetActive(false);
        trueFalseImage.SetActive(false);
        scoreImage.SetActive(false);
        timeImage.SetActive(false);
        QACircle.SetActive(false);
        finalImage.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.OutBack);
        finalImage.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }
    void Start()
    {
        
    }
    public void playAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void placeInfos(int trueCount, int falseCount, int score)
    {
        trueText.text = trueCount.ToString() + " TRUE";
        falseText.text = falseCount.ToString() + " FALSE";
        scoreText.text = score.ToString() + " SCORE";

    }

}
