using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckBlocks : MonoBehaviour
{

    private LogicBlock [] blocks;
    private LogicBlock[] map;
    private bool allBlocksPlaced = false;
    private bool outPutActive = false;
    private float firstBlockHeight = 4;
    private int[] correctAnswer = new int[5];
    private int a;
    public TextMesh text;
    public Transform parent;


    private int[] randomAssignment;
    private Color[] colors ;
    public GameObject outPutBlock;
    public GameObject outPutBlockExample;
    public double wrongMargin = 0.001;
    // Start is called before the first frame update
    void Start()
    {
        blocks = FindObjectsOfType<LogicBlock>();
        map = new LogicBlock[blocks.Length];
        colors = new Color[10];
        colors[0] = Color.blue;
        colors[1] = Color.red;
        colors[2] = Color.black;
        colors[3] = Color.cyan;
        colors[4] = Color.green;
        colors[5] = Color.grey;
        colors[6] = Color.white;
        colors[7] = Color.yellow;
        colors[8] = new Color(1, 0.5f, 0);
        colors[9] = Color.magenta;
        randomAssignment = GetrandomAssignment();
        correctAnswer[0] = randomAssignment[0];
        correctAnswer[1] = randomAssignment[1];
        correctAnswer[2] = randomAssignment[2];
        correctAnswer[3] = randomAssignment[3];
        correctAnswer[4] = randomAssignment[4];

        CreateExample();
        Debug.Log(correctAnswer[0] + " " + correctAnswer[1] + " " + correctAnswer[2] + " " + correctAnswer[3] + " " + correctAnswer[4]);
    }

    // Update is called once per frame
    void Update()
    {
      checkAllPorts();
    }

    /// <summary>
    /// Check the position of the blocks and adds the to the blocks array
    /// </summary>
    public void checkAllPorts()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].transform.position.y >= firstBlockHeight -0.1 && blocks[i].transform.position.y <= firstBlockHeight + 0.1) 
            {
                map[0] = blocks[i];
                
                //blocks[i].GetComponent<VRTK.VRTK_InteractableObject>().enabled = false;
                //VRTK.VRTK_InteractableObject.InteractionType.None
                
            }
            if ((blocks[i].transform.position.y >= (firstBlockHeight-0.5) - wrongMargin && blocks[i].transform.position.y <= (firstBlockHeight-0.5) + wrongMargin)) 
            {
                map[1] = blocks[i];              
            }
            if ((blocks[i].transform.position.y >= (firstBlockHeight - 1) - wrongMargin && blocks[i].transform.position.y <= (firstBlockHeight - 1) + wrongMargin))
            {
                map[2] = blocks[i];
            }
            if ((blocks[i].transform.position.y >= (firstBlockHeight - 1.5) - wrongMargin && blocks[i].transform.position.y <= (firstBlockHeight - 1.5) + wrongMargin))
            {
                map[3] = blocks[i];
            }
            if ((blocks[i].transform.position.y >= (firstBlockHeight - 2) - wrongMargin && blocks[i].transform.position.y <= (firstBlockHeight - 2) + wrongMargin))
            {
                map[4] = blocks[i];
                
                a = i;
                
                allBlocksPlaced = true;

                if (!outPutActive)
                {
                    RunBlockScript();
                    outPutActive = true;
                }
                
                if (checkIfWon())
                {
                    WinText();
                }
                

            }
            else
            {
                if(i == a)
                {
                    if(allBlocksPlaced == true)
                    {
                        allBlocksPlaced = false;
                        outPutActive = false;
                        text.text = "Create the pattern shown to the right";
                        DeleteBlocks();
                    }
                    
                }

            }

        }

    }

    /// <summary>
    /// Creates the blocks according to the array
    /// </summary>
    public void RunBlockScript()
    {
        /*
        bool error = false;
        

        if (map[0].isColorBlock() || map[1].isColorBlock() || map[2].isColorBlock()) {
            error = true;
        }
        */

        if(map[2].getOutput() == 0)
        {
            text.text = "Cannot divide by 0 at Line 3";
            return;
        }
        else
        {
            text.text = "Create the pattern shown to the right";
        }
            for (int i = 1; i <= map[0].getOutput(); i++)
        {
            for(int j = 1; j <= map[1].getOutput(); j++)
            {
                outPutBlock = Instantiate(outPutBlock);
                //Debug.Log(map[3].getOutput() + " <  " +colors.Length  );
                if (i % map[2].getOutput() == 0)
                {
                    if(map[3].getOutput() < colors.Length)
                    {
                        outPutBlock.GetComponent<Renderer>().material.color = colors[map[3].getOutput()];
                    }
                    else
                    {
                       // error = true;
                    }
                    
                }
                else
                {
                    

                    if (map[4].getOutput() < colors.Length)
                    {
                        outPutBlock.GetComponent<Renderer>().material.color = colors[map[4].getOutput()];
                    }
                    else
                    {
                      //  error = true;
                    }
                }
                outPutBlock.transform.position = new Vector3(i-2, j+1, 2);
            }
        }

            /*
        if (error)
        {
            Debug.Log("Du gjorde en feil");
            DeleteBlocks();
            text.text = "Error in code, Cannot convert color to float";
        }
        */

        outPutActive = true;
    }

    /// <summary>
    /// Deletes the boxes
    /// </summary>
    public void DeleteBlocks()
    {
        Debug.Log("Deleting objects");
        VisualBox[] blocks = FindObjectsOfType<VisualBox>();

        for(int i = 0; i < blocks.Length; i++)
        {
            blocks[i].destroyObject(parent);
            
        }
    }

    /// <summary>
    /// Creates and example for the player
    /// </summary>
    public void CreateExample()
    {
        
        for (int i = 1; i <= randomAssignment[0]; i++)
        {
            for (int j = 1; j <= randomAssignment[1]; j++)
            {
                outPutBlockExample = Instantiate(outPutBlockExample);
                
                if (i % randomAssignment[2] == 0)
                {
                    outPutBlockExample.GetComponent<Renderer>().material.color = colors[randomAssignment[3]];
                }
                else
                {
                    outPutBlockExample.GetComponent<Renderer>().material.color = colors[randomAssignment[4]];
                }
                outPutBlockExample.transform.position = new Vector3(6 , j +1, -i + 2);
            }
        }
    }
    /// <summary>
    /// Checks if the player has won
    /// </summary>
    /// <returns></returns>
    public bool checkIfWon()
    {
        int[] checkarray = new int[correctAnswer.Length];
        bool correct = false;
        for (int i = 0; i < map.Length; i++)
        {          
            if(correctAnswer[i] == map[i].getOutput())
            {
                correct = true;
            }
            else
            {
                return false;
            }
     
        }      
        return correct;
    }

    public int[] GetrandomAssignment()
    {
        int[] returnArray = new int[5];
        /*
        for(int i = 0; i < returnArray.Length; i++)
        {
            if(i < 2)
            {
                returnArray[i] = Random.Range(3, 10);
            }
            else
            {
                returnArray[i] = Random.Range(0, 10);
            }
            
        }
        */
        returnArray[0] = Random.Range(3, 10);
        returnArray[1] = Random.Range(2, 10);
        returnArray[2] = Random.Range(2, returnArray[0]);
        returnArray[3] = Random.Range(0, 10);
        returnArray[4] = Random.Range(0, 10);


        return returnArray;
    }

    public bool getAllPlaced()
    {
        return allBlocksPlaced;
    }

    public void WinText()
    {
        text.text = "Congrats, you won!";
        StartCoroutine("ReturnToMain");
    }

    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(10);
    }
}
