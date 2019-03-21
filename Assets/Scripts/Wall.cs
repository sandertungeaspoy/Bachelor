using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Vector3 Destination;
    public Vector3 stopPosition;
    public float speed;
    public float WaitTime = 5f;
    public float first;
    public float second;
    public TextMesh left;
    public TextMesh right;
    public TextMesh Question;
    private ArrayList alternatives;
    private ArrayList rnd;

    


    // Start is called before the first frame update
    void Start()
    {
        
       // first = Random.Range(1, 9);
       // second = Random.Range(1, 9);
        Destination = transform.position;
       // alternatives = GetAlternatives();
       // rnd = GetRandomArrayList();

        
    }

    // Update is called once per frame
    void Update()
    {

       // left.text = alternatives[(int)rnd[0]].ToString(); 
       // right.text = alternatives[(int)rnd[1]].ToString();
      //  Question.text = GetFirst().ToString() + "X" + GetSecond().ToString();
        transform.position = Vector3.Lerp(transform.position, Destination, Time.deltaTime * speed);

        WaitTime -= Time.deltaTime;

        if( WaitTime<= 0)
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
        if (GetAnswer().ToString() == left.text)
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

   

    

    
