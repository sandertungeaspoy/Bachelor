using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBox : MonoBehaviour
{
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyObject()
    {
        //gameObject.SetActive(false);
        rend.enabled = false;
    }
}
