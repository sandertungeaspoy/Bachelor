using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Animator wallAnimator;

    // Start is called before the first frame update
    void Start()
    {
        wallAnimator = GameObject.Find("InfoWall").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WallAnim()
    {
        wallAnimator.Play("Play");
    }
}
