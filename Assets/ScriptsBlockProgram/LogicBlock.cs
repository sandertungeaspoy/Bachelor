using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBlock : MonoBehaviour
{
    public VRTK.VRTK_InteractableObject linkedObject;
    public TextMesh text;
    private int sum;
    // Start is called before the first frame update
    void Start()
    {
        
        
        sum = getOutputStart();
        text.text = getOutput().ToString();
        colorBlock();
    }

    // Update is called once per frame
    void Update()
    {
        //updateNumber();
        text.text = getOutput().ToString();
    }

    protected virtual void OnEnable()
    {
        Debug.Log(1);
        linkedObject = (linkedObject == null ? GetComponent<VRTK.VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {

            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }


    }

    protected virtual void OnDisable()
    {
        /*
        if (linkedObject != null && value)
        {
            value = !value;
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }
        */
    }

    protected virtual void InteractableObjectUsed(object sender, VRTK.InteractableObjectEventArgs e)
    {
        Debug.Log(1);

    }

    protected virtual void InteractableObjectUnused(object sender, VRTK.InteractableObjectEventArgs e)
    {
        //CurrentSum = onClick();
        //sum++;
        updateNumber();
        Debug.Log("Shiit");
    }

    public int getOutput()
    {
        if (gameObject.name == "Block1")
        {
            //Debug.Log(1);
            //sum = 0;
            return sum;
        }
        else if (gameObject.name == "Block2")
        {
            //Debug.Log(2);
            return sum;
        }
        else if (gameObject.name == "Block3")
        {
           // Debug.Log(3);
            return sum;
        }
        else if (gameObject.name == "Block4")
        {
           // Debug.Log(4);
            return sum;
        }
        else
        {
            //Debug.Log(5);
            return sum;
        }
    }

    public void colorBlock()
    {
        if (gameObject.name == "Block1")
        {
            //Debug.Log(1);
            text.text = "Blue";
        }
        else if (gameObject.name == "Block2")
        {
            //Debug.Log(2);
            text.text = "Red" ;
        }
    }

    public bool isColorBlock()
    {
        if (gameObject.name == "Block1")
        {
            //Debug.Log(1);
            return true;
        }
        else if (gameObject.name == "Block2")
        {
            //Debug.Log(2);
            return true;
        }
        else
        {
            return false;
        }
    }


    public int getOutputStart()
    {
        if (gameObject.name == "Block1")
        {
            //Debug.Log(1);
            sum = 0;
            return sum;
        }
        else if (gameObject.name == "Block2")
        {
            //Debug.Log(2);
            
            sum = 0;
            return sum;
        }
        else if (gameObject.name == "Block3")
        {
            // Debug.Log(3);
            sum = 0;
            return sum;
        }
        else if (gameObject.name == "Block4")
        {
            // Debug.Log(4);
            sum = 0;
            return sum;
        }
        else
        {
            //Debug.Log(5);
            sum = 0;
            return sum;
        }
    }

    public void updateNumber()
    {
        Debug.Log("Sei fisk");
        sum++;
        if(sum == 10)
        {
            sum = 0;
        }
    }

}
