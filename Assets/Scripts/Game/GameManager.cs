using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] questionImages;
    [SerializeField]
    private Sprite[] answerImages;
    [SerializeField]
    private Image purpleAnswer, blueAnswer, greyAnswer;
    [SerializeField]
    private Image yellowAnswer, pinkAnswer, greenAnswer;
    [SerializeField]
    private Image upQImage, bottomQImage;
    [SerializeField]
    private Transform QACircle;
    [SerializeField]
    private Transform leftbar, rightbar;
    [SerializeField]
    private Text trueCount, falseCount, scoreText, timeText;
    [SerializeField]
    private GameObject trueIcon, falseIcon, trueFalseObject, bonusImage;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip startClip, circleRotateClip, closeBarClip;
    public int trueCounter, falseCounter;
    int whichQuestion;
    bool isUp;
    string buttonName;
    bool isCircleRotate;
    TimeManager timeManager;
    int howmanyFalse, bonusCounter;
    bool isTrue, isClick;
    public int totalScore;

    Vector3 leftbar1st = new Vector3(-211f, 67f, 0f);
    Vector3 leftbar2nd = new Vector3(-140, 67f, 0f);
    Vector3 leftbar3rd = new Vector3(-98, 67f, 0f);

    Vector3 rightbar1st = new Vector3(218f, 67f, 0f);
    Vector3 rightbar2nd = new Vector3(147, 67f, 0f);
    Vector3 rightbar3rd = new Vector3(88, 67f, 0f);
    void Start()
    {
        audioSource.PlayOneShot(startClip);
        isClick = true;
        bonusImage.GetComponent<RectTransform>().localScale = Vector3.zero;
        totalScore = 0;
        bonusCounter = 0;
        trueIcon.SetActive(false);
        falseIcon.SetActive(false);
        timeManager = Object.FindObjectOfType<TimeManager>();
        timeManager.StartCoroutine("startTime");
        trueCounter = 0;
        falseCounter = 0;
        isUp = true;
        isCircleRotate = true;
        replaceAnswersAndQuestions();
    }
    public void goBack()
    {
        SceneManager.LoadScene("LearnScene");
    }
    void replaceAnswersAndQuestions()
    {
        whichQuestion = Random.Range(0, answerImages.Length - 3);
        int random = Random.Range(0, 100);
        if(isUp)
        {
            if(random<=33)
            {
                purpleAnswer.sprite = answerImages[whichQuestion];
                blueAnswer.sprite = answerImages[whichQuestion + 1];
                greyAnswer.sprite = answerImages[whichQuestion + 2];
            }
            else if (random <= 66)
            {
                purpleAnswer.sprite = answerImages[whichQuestion+1];
                blueAnswer.sprite = answerImages[whichQuestion];
                greyAnswer.sprite = answerImages[whichQuestion + 2];
            }
            else
            {
                purpleAnswer.sprite = answerImages[whichQuestion + 1];
                blueAnswer.sprite = answerImages[whichQuestion+2];
                greyAnswer.sprite = answerImages[whichQuestion];
            }
        }
        else
        {
            if (random <= 33)
            {
                yellowAnswer.sprite = answerImages[whichQuestion];
                pinkAnswer.sprite = answerImages[whichQuestion+1];
                greenAnswer.sprite = answerImages[whichQuestion + 2];
                
            }
            else if (random <= 66)
            {
                yellowAnswer.sprite = answerImages[whichQuestion + 1];
                pinkAnswer.sprite = answerImages[whichQuestion];
                greenAnswer.sprite = answerImages[whichQuestion + 2];
            }
            else
            {
                yellowAnswer.sprite = answerImages[whichQuestion + 1];
                pinkAnswer.sprite = answerImages[whichQuestion + 2];
                greenAnswer.sprite = answerImages[whichQuestion];
            }
        }

        if(isUp)
        {
            upQImage.sprite = questionImages[whichQuestion];
        }
        else
        {
            bottomQImage.sprite = questionImages[whichQuestion];
        }
        isUp = !isUp;
    }
    public void buttonClick()
    {
        buttonName = UnityEngine.EventSystems.EventSystem.current.
            currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;
        if(isClick)
        {
            isClick = false;
            checkResult();
        }
       
    }
    void checkResult()
    {
        if(buttonName==answerImages[whichQuestion].name)
        {
            trueCounter++;
            isTrue = true;
            
            bonusCounter++;
            if(bonusCounter>=5 && bonusCounter<10) {
                bonusImage.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(closeBonus);
                totalScore += 30;
            }
            else
            {
                totalScore += 20;
            }
            if(bonusCounter>=10)
            {
                bonusCounter = 0;
            }
            rotateCircle();
        }
        else
        {
            falseCounter++;
            howmanyFalse++;
            isTrue = false;
            totalScore -= 5;
            bonusCounter = 0;
            closeBars(howmanyFalse);
            
        }
       
        trueOrFalse(isTrue);
        trueCount.text = trueCounter.ToString();
        falseCount.text = falseCounter.ToString();
        if(totalScore<=0)
        {
            totalScore = 0;
            scoreText.text = totalScore.ToString();
        }
        else
        {
            scoreText.text = totalScore.ToString();
        }
        
            
        
        
        
       
    }
    void rotateCircle()
    {
        if(isCircleRotate)
        {
            isCircleRotate = false;
            howmanyFalse = 0;
            leftbar.DOLocalMove(leftbar1st, 0.2f);
            rightbar.DOLocalMove(rightbar1st, 0.2f);
            replaceAnswersAndQuestions();
            audioSource.PlayOneShot(circleRotateClip);
            QACircle.DORotate(QACircle.rotation.eulerAngles+new Vector3(0,0,180f), 0.5f).OnComplete(rotateCircleTrue);
        }
    }
    void rotateCircleTrue()
    {
        isClick = true;
        isCircleRotate = true;
    }
    void closeBars(int falseNo)
    {
        if(falseNo==1)
        {
            isClick = true;
            leftbar.DOLocalMove(leftbar2nd, 0.2f);
            rightbar.DOLocalMove(rightbar2nd, 0.2f);
        }
        else if(falseNo==2)
        {
            isClick = false;
            leftbar.DOLocalMove(leftbar3rd, 0.2f);
            rightbar.DOLocalMove(rightbar3rd, 0.2f).OnComplete(waitForClose);
        }
        audioSource.PlayOneShot(closeBarClip);
    }
    void waitForClose()
    {
        isCircleRotate = true;
        Invoke("rotateCircle", 0.6f);

    }
    void trueOrFalse(bool isTrue)
    {
        trueFalseObject.GetComponent<CanvasGroup>().alpha = 0;
        if(isTrue)
        {
            trueIcon.SetActive(true);
            falseIcon.SetActive(false);
        }
        else
        {
            trueIcon.SetActive(false);
            falseIcon.SetActive(true);
        }
        trueFalseObject.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(closeTrueFalse);
    }
    void closeTrueFalse()
    {
        trueFalseObject.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
    }
    void closeBonus()
    {
        bonusImage.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack);
    }
    
    

}
