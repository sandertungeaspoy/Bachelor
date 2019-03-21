using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallQuiz : MonoBehaviour
{
    private Vector3 Destination;
    public Vector3 stopPosition;
    public float speed;
    public float WaitTime = 5f;
   
    public TextMesh left;
    public TextMesh right;
    public TextMesh QuestionText;
    private ArrayList alternatives;
    private ArrayList rnd;

    private int currentQuestion = 0;
    List<Question> questions;


    // Start is called before the first frame update
    void Start()
    {
        questions = new List<Question>();
        //questions.Add(new Question("Jeg heter hk", true));
        speed = 1;
        stopPosition = new Vector3(0, 0, -5);

        left.text = "True";
        right.text = "False";
        Destination = transform.position;
        


    }

    // Update is called once per frame
    void Update()
    {

        
        //QuestionText.text = questions[currentQuestion].getQuestion();
        transform.position = Vector3.Lerp(transform.position, Destination, Time.deltaTime * speed);

        WaitTime -= Time.deltaTime;

        if (WaitTime <= 0)
        {
            MoveWall();
        }
    }

    void MoveWall()
    {
        Destination = stopPosition;
    }

    public void DestroyWall()
    {
        Destroy(gameObject);
    }

    

    

    

   

    

}
