using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject wall;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    private ArrayList alternatives;
    private ArrayList rnd;

    //public TextMesh left;
    //public TextMesh right;

    private bool leftcorrect = false;
    private bool gamefinished = false;

    GameController clearController;


    //---
    public float speed;
    public float WaitTime = 5f;
    public float first;
    public float second;

    private float time;
    private float timeToRestart = 8f;
    private bool NextAnswerAllowed = true;

    private int currentQuestion = 0;
    private int numberOfQuestions = 5;
    private string LeftAnswer = "";

    private bool playerLost = false;
    //---



    // Start is called before the first frame update
    void Start()
    {
        clearController = GameObject.Find("GameController").GetComponent<GameController>();
        clearController.QuizCleared = false;
        nextQuestion();

    }


    void Update()
    {


        time -= Time.deltaTime;

        if (playerLost)
        {
            timeToRestart -= Time.deltaTime;
            if(timeToRestart < 0)
            {
                goBackToStart(false);
            }
        }


        //Debug.Log("Answer: " + GetAnswer() + isLeftCorrect() + " Current " + currentQuestion);
        if (time < 0)
        {
            NextAnswerAllowed = true;
        }
        else
        {
            NextAnswerAllowed = false;
        }

        if (!gameFinished() ) // wall1.GetAnswer().ToString()) )
        {
            leftcorrect = isLeftCorrect();
        }
        else
        {
            goBackToStart(true);
           // print("Win switching Scenes" + currentQuestion);
           // clearController.QuizCleared = true;
           // SceneManager.LoadScene(0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {

        
        if (collisionInfo.collider.tag == "CorrectAnswer" && NextAnswerAllowed)
        {
            NextAnswerAllowed = false;
            
            if (rb.position.x > 0 && !leftcorrect) //right
            {
                Debug.Log("Right");
                currentQuestion++;
                Destroy(wall);
                if (!gameFinished())
                {
                    nextQuestion();
                }
            }
            if (rb.position.x > 0 && leftcorrect) //right
            {
                Debug.Log("Wrong");
                playerLost = true;
            

            }
            if (rb.position.x <= 0 && leftcorrect) //left
            {
                Debug.Log("Correct");
                currentQuestion++;
                
                Destroy(wall);
                if (!gameFinished())
                {
                    nextQuestion();
                }
                

            }
            if (rb.position.x <= 0 && !leftcorrect) //left
            {
                Debug.Log("Wrong");
                playerLost = true;

            }
            
            

        }

    }
    public bool gameFinished()
    {
        if (currentQuestion >= numberOfQuestions)
        {
            return true;

        }
        return false;
    }

    public void goBackToStart(bool GameWon)
    {
        print("Win switching Scenes" + currentQuestion);
        clearController.QuizCleared = GameWon;
        SceneManager.LoadScene(0);
    }

    public void nextQuestion()
    {

        Debug.Log("Make question");
        first = Random.Range(1, 9);
        second = Random.Range(1, 9);
        alternatives = GetAlternatives();
        rnd = GetRandomArrayList();
        wait5Seconds();
        wall = Instantiate(wall);
        wall.transform.position = new Vector3(0, 0, 20);
        Wall test = wall.GetComponent<Wall>();
        test.WaitTime = 4f;
        test.enabled = true;
        test.left.text = alternatives[(int)rnd[0]].ToString();
        test.right.text = alternatives[(int)rnd[1]].ToString();
        Debug.Log("Alternatives: " + alternatives[0] + " " + alternatives[1]);
        Debug.Log("Alternatives: " + alternatives[0] + " " + alternatives[1]);
        LeftAnswer = test.left.text;

        test.Question.text = test.Question.text = GetFirst().ToString() + "X" + GetSecond().ToString();
        // test.left.text = first.ToString();
        //test.right.text = second.ToString();

    }

    public void wait5Seconds()
    {
        time = 5f;
    }

    public void gameLost()
    {
        time = 8f;

        if(time < 0)
        {
            goBackToStart(false);
        }
    }

    public float GetFirst()
    {
        return first;
    }
    public float GetSecond()
    {
        return second;
    }

    public float GetAnswer()
    {
        return first * second;
    }

    public bool isLeftCorrect()
    {
        if (GetAnswer().ToString() == LeftAnswer)
        {
            return true;
        }
        return false;
    }

    public ArrayList GetAlternatives()
    {
        ArrayList returnArray = new ArrayList();


        returnArray.Add(GetAnswer());



        float testnum = 0;
        do
        {
            testnum = (int)Random.Range(0, GetAnswer() * 2);

        } while (testnum == GetAnswer());

        returnArray.Add(testnum);
        return returnArray;
    }

    public ArrayList GetRandomArrayList()
    {
        int test = Random.Range(0, 2);
        ArrayList returnArray = new ArrayList();
        returnArray.Add(test);
        if (test == 0)
        {
            returnArray.Add(1);
        }
        else
        {
            returnArray.Add(0);
        }

        return returnArray;
    }



}
