using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAllPorts : MonoBehaviour
{

    GameController clearController;

    private LogicPort[] ports;
    private bool StartBeamA = true;
    private bool StartBeamB = false;

    public TextMesh Question;
    public TextMesh Score;

    private bool StartBeamC = true;
    private bool StartBeamD = false;

    private bool[] map = { false, false, false };
    //private float[] testMap = { 2, 2, 2 };
   // private float[] color = { 2, 2, 2 };


    private int a = 99;
    private int b = 99;
    private int c = 99;



    private float amountOfCombos = 3f;
    private float currentScore = 0;
    private float currentCombos = 0;
    private bool Connector1 = false;
    private bool Connector2 = false;
    private bool Connector3 = false;


    
    private List<string[]> usedPorts2 = new List<string[]>();

    private MaterialChanger[] materials;

    

    // Start is called before the first frame update
    void Start()
    {
        Question.text = "Find " + amountOfCombos  +" different combinations that gives Blue (True) out!";
        ports = FindObjectsOfType<LogicPort>();
        
        materials = FindObjectsOfType<MaterialChanger>();
        Debug.Log(ports[0].gameObject.name  + "navn" );
        checker();

    }

    // Update is called once per frame
    void Update()
    {
        checkAllPorts();
        GameWon();
        
        Score.text = currentCombos + "/" + amountOfCombos + " Score:" + currentScore;
        
    }

    public void checkAllPorts()
    {
       for (int i = 0; i < ports.Length; i++)
        {
            if (ports[i].transform.position.x == -3 && ports[i].transform.position.y == 3) // first snapZone
            {
                //2
                Connector1 = true;
                a = i;
                
                map[0] = ports[i].getOutput(StartBeamA, StartBeamB);
                
                

                if (map[0] == true )
                {
                    
                    materials[0].ChangeToTrue();


                }
                else if (map[0] == false )
                {
                    
                 
                    materials[0].ChangeToFalse();


                }
                
                

            }
            else
            {
                if (i == a)
                {
                    Connector1 = false;
                    materials[2].ChangeToStandard();
                    materials[0].ChangeToStandard();
                }
                

            }

            
            if (ports[i].transform.position.x == -3 && ports[i].transform.position.y == 1) // second snapZone
            {
                Connector2 = true;
                
                b = i;
                
                map[1] = ports[i].getOutput(StartBeamC, StartBeamD);
                
                if (map[1] == true)
                {               
                    materials[1].ChangeToTrue();  
                }
                else if (map[1] == false )
                {
                                      
                    materials[1].ChangeToFalse();
                    
                }
     
            }
            else
            {
                
                if (i == b)
                {
                    Connector2 = false;
                    materials[1].ChangeToStandard();
                    materials[2].ChangeToStandard();
                }

            }
            
            if (ports[i].transform.position.x == 1 && ports[i].transform.position.y == 2) // third snapZone
            {
                Connector3 = true;
                c = i;
                
                map[2] = ports[i].getOutput(map[0], map[1]);
                
                if (map [2] == true )
                {
                    materials[2].ChangeToTrue();
                    if (AllPlaced())
                    {
                        CheckIfValid2();
                    }
                    

                    
                }
                else if(map[2] == false)
                {
                    materials[2].ChangeToFalse();
                }
            }
            else
            {
                if (i == c)
                {
                    Connector3 = false;
                    materials[2].ChangeToStandard();
                }

            }

        }

    }

    /// <summary>
    /// Sets the materials array in correct order
    /// </summary>
    public void checker()
    {
        MaterialChanger temp1 = materials[0];
        MaterialChanger temp2 = materials[0];
        MaterialChanger temp3 = materials[0];
        
        for (int i = 0; i < materials.Length; i++)
        {
            if(materials[i].gameObject.name == "GateConnector1")
            {
                
                temp1 = materials[i];
            }
            if (materials[i].gameObject.name == "GateConnector2")
            {
                
                temp2 = materials[i];
            }
            if (materials[i].gameObject.name == "GateConnector3")
            {
                
                temp3 = materials[i];
            }
        }
        materials[0] = temp1;
        materials[1] = temp2;
        materials[2] = temp3;

        /*
        LogicPort temp4 = ports[0];
        LogicPort temp5 = ports[0];
        LogicPort temp6 = ports[0];
        LogicPort temp7 = ports[0];
        LogicPort temp8 = ports[0];

        bool test = false;
        for (int i = 0; i < ports.Length; i++)
        {
            if (ports[i].gameObject.name == "NAND")
            {

                temp4 = ports[i];
            }
            if (ports[i].gameObject.name == "AND" && !test)
            {
                test = true;
                temp5 = ports[i];
            }
            if (ports[i].gameObject.name == "NOR")
            {

                temp6 = ports[i];
            }
            if (ports[i].gameObject.name == "OR")
            {

                temp7 = ports[i];
            }
            if (ports[i].gameObject.name == "AND")
            {

                temp8 = ports[i];
            }
        }
        ports[0] = temp4;
        ports[1] = temp5;
        ports[2] = temp6;
        ports[3] = temp7;
        ports[4] = temp8;
        */


    }

    /// <summary>
    /// Check if all ports are placed
    /// </summary>
    /// <returns></returns>
    public bool AllPlaced()
    {
        if(Connector1 && Connector2 && Connector3)
        {
            return true;
        }
        return false;
    }

    

    /// <summary>
    /// Checks if the solution is not already done before
    /// </summary>
    /// <returns></returns>
    public bool CheckIfValid2()
    {

        for (int i = 0; i < usedPorts2.Count; i++)
        {

            if (usedPorts2[i][0] == ports[a].gameObject.name && usedPorts2[i][1] == ports[b].gameObject.name && usedPorts2[i][2] == ports[c].gameObject.name)
            {
                return false;
            }

        }
        currentCombos++;
        currentScore += 100;
        //CheckForDuplicates(new LogicPort[] { ports[a], ports[b], ports[c] });
        usedPorts2.Add(new string[] { ports[a].gameObject.name, ports[b].gameObject.name, ports[c].gameObject.name });
        usedPorts2.Add(new string[] { ports[b].gameObject.name, ports[a].gameObject.name, ports[c].gameObject.name });
        return true;
    }

    

    


    /// <summary>
    /// Checks if the game is won
    /// </summary>
    /// <returns></returns>
    public bool GameWon()
    {
        if(currentCombos == amountOfCombos)
        {
            Question.text = "Congratulations You Won!";
            StartCoroutine("ReturnToMain");
            return true;
        }
        
        return false;
    }
    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(10f);
        clearController = GameObject.Find("GameController").GetComponent<GameController>();
        if (clearController.fromHub)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(10);
        }
    }
}
