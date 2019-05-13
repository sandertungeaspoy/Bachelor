using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerQuiz : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject wall;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    public TextMesh hint;

    private bool playerLost = false;

    private bool leftcorrect = false;
    // private bool gamefinished = false;

    private float time;
    private bool NextAnswerAllowed = true;
    private float timeToRestart = 8f;

    List<int> randomSequence;

    GameController clearController;
    List<Question> questions;
    private int currentQuestion = 0;
    private int numberOfQuestions;
    private bool newQuestion = false;
    Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        clearController = GameObject.Find("GameController").GetComponent<GameController>();
        questions = new List<Question>();
        clearController.QuizCleared = false;
        
        questions.Add(new Question("A do while loop runs atleast once", true));
        questions.Add(new Question("!False", true));
        questions.Add(new Question("This code will run: String number = 1;", false));
        questions.Add(new Question("This code will run. if (2+3 == 5 or 5= 4){}", true));
        questions.Add(new Question("This code will run if (2+3 == 5 and 5=4{}", false));
        Debug.Log(questions.Count + " lengde");
        numberOfQuestions = questions.Count;

        randomSequence = GetRandomSequence(0,numberOfQuestions);
        Debug.Log(randomSequence[0] + " " + randomSequence[1] + " " + randomSequence[2] + " " + randomSequence[3] + " " + randomSequence[4]);
        /*
        wall = Instantiate(wall);
        
        WallQuiz test = wall.GetComponent<WallQuiz>();
        test.QuestionText.text = questions[currentQuestion].getQuestion();

        */
        nextQuestion();
    }


    void Update()
    {
        time -= Time.deltaTime;

        if (playerLost)
        {
            timeToRestart -= Time.deltaTime;
            if (timeToRestart < 0)
            {
                goBackToStart(false);
            }
        }

        if (time < 0)
        {
            NextAnswerAllowed = true;
        }
        else
        {
            NextAnswerAllowed = false;
        }
       
        //Debug.Log(questions.Count + " lengde");
        if (!gameFinished()  ) // wall1.GetAnswer().ToString()) )
        {
            leftcorrect = questions[randomSequence[currentQuestion]].getAnswer();
            
        }
        else
        {
            goBackToStart(true);
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

        Debug.Log("hi");
        
        if (collisionInfo.collider.tag == "CorrectAnswer" && NextAnswerAllowed) // Hit the wall
        {
            NextAnswerAllowed = false;
            //gamefinished = true;
            if (rb.position.x > 0 && !leftcorrect) //right
            {
                currentQuestion++;
                Debug.Log("Right");
                Destroy(wall);
                if (!gameFinished())
                {
                    nextQuestion();
                }
                
                //FindObjectOfType<Wall>().DestroyWall();
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
                //wall.isKinematic = false;
                Destroy(wall);
                if (!gameFinished())
                {
                    nextQuestion();
                }

                //FindObjectOfType<Wall>().DestroyWall();
            }
            if (rb.position.x <= 0 && !leftcorrect) //left
            {
                Debug.Log("Wrong");
                playerLost = true;
                //wall.isKinematic = false;
                // Destroy(wall);

            }
            


        }
    }

    public void goBackToStart(bool GameWon)
    {
        print("Win switching Scenes" + currentQuestion);
        if (GameWon)
        {
            hint.text = "Hint: The wall left of this game is not solid";
        }
        clearController.QuizCleared = GameWon;
        StartCoroutine("ReturnToMain");
    }

    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(10f);
        clearController = GameObject.Find("GameController").GetComponent<GameController>();
        if (clearController.fromHub)
        {
            SceneManager.LoadScene(12);
        }
        else
        {
            SceneManager.LoadScene(9);
        }
    }

    public bool gameFinished()
    {
        if(currentQuestion >= numberOfQuestions)
        {
            return true;

        }
        return false;
    }

    public void nextQuestion()
    {

        wait5Seconds();
        wall = Instantiate(wall);
        wall.transform.position = new Vector3(0, 0, 20);
        WallQuiz test = wall.GetComponent<WallQuiz>();
        test.WaitTime = 10f;
        test.enabled = true;
        test.QuestionText.text = questions[randomSequence[currentQuestion]].getQuestion();
        //test.QuestionText.text = questions[currentQuestion].getQuestion();

    }

    public List<int> GetRandomSequence( int start, int end)
    {
        Debug.Log("olala");
        List<int> returnArray = new List<int>();     
        for(int i = start; i < end; i++)
        {     
            int number = 0;
            do
            {
                number = Random.Range(start, end);
                
            } while (!notInList(returnArray, number));
            returnArray.Add(number);
        }
        return returnArray;
    }

    public bool notInList(List<int> list, int number)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(number == list[i])
            {
                return false;
            }
        }
        return true;
    }

    public void wait5Seconds()
    {
        time = 5f;
    }

    class Question
    {
        private string question;
        private bool answer;

        public Question(string question, bool answer)
        {
            this.question = question;
            this.answer = answer;
        }
        public string getQuestion()
        {
            return question;
        }
        public bool getAnswer()
        {
            return answer;
        }


    }



}

