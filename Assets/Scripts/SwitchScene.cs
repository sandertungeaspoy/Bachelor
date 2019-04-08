using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public VRTK.VRTK_InteractableObject linkedObject;
    //public TeleportPoint portal;

    // Start is called before the first frame update
    void Start()
    {
        //portal = gameObject.GetComponent<TeleportPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected virtual void OnEnable()
    {

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
        Debug.Log("Switching Scene");

    }

    protected virtual void InteractableObjectUnused(object sender, VRTK.InteractableObjectEventArgs e)
    {
        switch (this.GetComponent<TeleportPoint>().title)
        {
            case "MathGame":
                SceneManager.LoadScene(1);
                break;
            case "PhysicsGame":
                SceneManager.LoadScene(2);
                break;
            case "DmarkGame":
                SceneManager.LoadScene(5);
                break;
            case "DataQuiz":
                SceneManager.LoadScene(4);
                break;
            case "PhysicsQuiz":
                SceneManager.LoadScene(6);
                break;
            case "BinaryGame":
                SceneManager.LoadScene(7);
                break;
            case "BlockProg":
                SceneManager.LoadScene(8);
                break;
            case "GameHub":
                SceneManager.LoadScene(12);
                break;
            case "EscapeRoom":
                SceneManager.LoadScene(13);
                break;
            default:
                Debug.Log("No scene found");
                break;
        }
           

        

        
    }

    public void onClick()
    {

        

    }
}
