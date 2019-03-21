using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicPort : MonoBehaviour
{

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getOutput(bool inputA, bool inputB)
    {
        if(gameObject.name == "NAND")
        {
            return !(inputA && inputB);
        }
        else if(gameObject.name == "AND")
        {
            return (inputA && inputB);
        }
        else if(gameObject.name == "NOR")
        {
            return !(inputA || inputB);
        }
        else
        {
            return (inputA || inputB);
        }
        
    }

    public void setActive( bool value)
    {
        active = value;
    }
    public bool getActive()
    {
        return active;
    }


}
