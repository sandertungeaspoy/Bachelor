using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BinaryPlayer : MonoBehaviour
{

    private float sum = 0;
    public TextMesh SumText;
    public TextMesh question;
    public TextMesh timerText;
    //public VRTK.VRTK_DestinationMarker hit2;
    private bool testround = true;
    private float correctAnswer;
    private float prevAnswer = 0; // 0 So that the first question will not be 0 in Binary.
    private float NumberOfCorrectAnswers = 0;
    private float testRoundReq = 2;
    private float timeLimit = 40;
    private float score;
    private bool gameOver = false;
    GameController clearController;

    // Start is called before the first frame update
    void Start()
    {
        generateNewQuestion();
        
    }

    // Update is called once per frame
    void Update()
    {

        //  VRTK.VRTK_ob

        checkIfTestRound();

        if (!testround)
        {
            if(timeLimit > 0)
            {
                timeLimit -= Time.deltaTime;
            }
            else
            {
                gameOver = true;
                timerText.text = "TIME IS UP!";
                question.text = "You got " + score + " points!\nHint: Illusory wall ahead";
                StartCoroutine("ReturnToMain");
            }
            
            timerText.text = timeLimit.ToString("0") + " sec(s) Left" + " SCORE: " + score;
        }


        sum = currentValue();

        

        SumText.text = sum.ToString();

        checkIfCorrect();

    }

    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(5f);
        clearController = GameObject.Find("GameController").GetComponent<GameController>();
        if (clearController.fromHub)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(9);
        }
    }

    void checkIfCorrect()
    {
        if (sum == correctAnswer && !gameOver)
        {
            score += 10;
            NumberOfCorrectAnswers++;
            generateNewQuestion();
            
        }
        
    }

    void generateNewQuestion()
    {

        do
        {
            correctAnswer = Random.Range(0, 16);
        } while (correctAnswer == prevAnswer);


        prevAnswer = correctAnswer;
        if (testround)
        {
            question.text = "What is " + correctAnswer + " in Binary? " + NumberOfCorrectAnswers + "/" + testRoundReq;
        }
        else
        {
            question.text = "What is " + correctAnswer + " in Binary?";
        }
        


    }

    float currentValue()
    {
        BinaryBox[] objects = FindObjectsOfType<BinaryBox>();
        float sum = 0;
        for(int i = 0; i < objects.Length; i++)
        {
            if (objects[i].getValue())
            {
                sum += objects[i].getCurrentSum();
            }
        }
        return sum;
    }

    void checkIfTestRound()
    {
        if ( testround && (NumberOfCorrectAnswers == testRoundReq))
        {
            score = 0;
            testround = false;
            NumberOfCorrectAnswers = 0;
        }
    }



}
