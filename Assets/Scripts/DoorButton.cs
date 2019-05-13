using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorButton : MonoBehaviour
{
    Transform parent;
    public Transform pointA;
    public Transform pointB;

    Vector3 offset;

    public UnityEvent HitA;
    public UnityEvent HitB;

    public UnityEvent ReleasedA;
    public UnityEvent ReleasedB;

    int state = 0;
    int prevState = 0;
    public bool doorOpen = false;

    public Animator animator;

    private bool animTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Wall_Stone_WoodenDoor").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            transform.position = ClosestPointOnLine(parent.position) - offset;
        }
        else
        {
            transform.position = ClosestPointOnLine(transform.position);
        }

        if (transform.position == pointA.position)
        {
            state = 1;
        }
        else if (transform.position == pointB.position)
        {
            state = 2;
        }
        else
        {
            state = 0;
        }

        if (state == 1 && prevState == 0)
        {
            HitA.Invoke();
        }
        else if (state == 2 && prevState == 0)
        {
            HitB.Invoke();

        }
        else if (state == 0 && prevState == 1)
        {
            ReleasedA.Invoke();
        }
        else if (state == 0 && prevState == 2)
        {
            ReleasedB.Invoke();
        }

        prevState = state;
    }
  

    public void OpenDoor()
    {
        
        if (doorOpen == false && animTrigger)
        {
            print("Click");
            animator.Play("Play");
            doorOpen = true;
        }
        animTrigger = true;
    }

    //referance https://github.com/LochieWestfallGames/VR-Tutorial/blob/master/Episode%205/VR%20Tutorial/Assets/Scripts/SlidingDrawer.cs
    Vector3 ClosestPointOnLine(Vector3 point)
    {
        Vector3 va = pointA.position + offset;
        Vector3 vb = pointB.position + offset;

        Vector3 vVector1 = point - va;

        Vector3 vVector2 = (vb - va).normalized;

        float t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return va;

        if (t >= Vector3.Distance(va, vb))
            return vb;

        Vector3 vVector3 = vVector2 * t;

        Vector3 vClosestPoint = va + vVector3;

        return vClosestPoint;
    }
}

