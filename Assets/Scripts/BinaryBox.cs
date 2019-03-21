using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryBox : MonoBehaviour
{
    public VRTK.VRTK_InteractableObject linkedObject;
    public TextMesh text;
    public bool value = false; // 0 start
    private float CurrentSum;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnEnable()
    {

        linkedObject = (linkedObject == null ? GetComponent<VRTK.VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null )
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
        CurrentSum = onClick();
        Debug.Log(1);
    }


    public float onClick()
    {
        value = !value;
        float returnV = 0;
        if (value)
        {
            Debug.Log("1");
            text.text = "1";
            if (this.transform.position.x == 1)
            {
                Debug.Log("1");
                returnV = 1;
            }
            else if (this.transform.position.x == -1)
            {
                Debug.Log("2");
                returnV = 2;
            }
            else if (this.transform.position.x == -3)
            {
                Debug.Log("4");
                returnV = 4;
            }
            else
            {
                Debug.Log("8");
                returnV = 8;
            }
            
        }
        else
        {
            Debug.Log("0");
            text.text = "0";
            if (this.transform.position.x == 1)
            {
                returnV = -1;
            }
            else if (this.transform.position.x == -1)
            {
                returnV = -2;
            }
            else if (this.transform.position.x == -3)
            {
                returnV = -4;
            }
            else
            {
                returnV = -8;
            }
        }

        return returnV;
    }


    public float getCurrentSum()
    {
        return CurrentSum;
    }

    public bool getValue()
    {
        return value;
    }

}
