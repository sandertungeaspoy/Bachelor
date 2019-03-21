using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetDestroyed : MonoBehaviour
{
    private float lifetime = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void DestroyRemains(GameObject gameobject2)
    {
        Destroy(gameobject2);
    }
}
