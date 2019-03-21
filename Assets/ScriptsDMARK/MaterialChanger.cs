using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material[] material;
    //Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<Renderer>();
        //rend.enabled = true;
        //rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToTrue()
    {
        //Debug.Log("Change to True");
        //gameObject.GetComponent<Renderer>().material.color = Color.blue;
        for(int i = 0; i < gameObject.GetComponentsInChildren<Renderer>().Length; i++)
        {
            gameObject.GetComponentsInChildren<Renderer>()[i].material.color = Color.blue;
        }
        
        
    }
    public void ChangeToFalse()
    {
        //Debug.Log("Change to False");
        for (int i = 0; i < gameObject.GetComponentsInChildren<Renderer>().Length; i++)
        {
            gameObject.GetComponentsInChildren<Renderer>()[i].material.color = Color.red;
        }
    }

    public void ChangeToStandard()
    {
        //Debug.Log("Change to Standard");
        for (int i = 0; i < gameObject.GetComponentsInChildren<Renderer>().Length; i++)
        {
            gameObject.GetComponentsInChildren<Renderer>()[i].material.color = Color.white;
        }
    }

}
