using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 shootingDirection;
    public float shootingForce;
    public float upwardForce;
    private float lifetime = 5;
    public GameObject target1;
    private Vector3 test5;
    private float minimumForce = 500;
    

    void Start()
    {
        //test5 = new Vector3(0, upwardForce, shootingForce).normalized;

        if(shootingForce < minimumForce)
        {
            shootingForce = minimumForce;
        }

        GetComponent<Rigidbody>().AddForce(shootingDirection * shootingForce);
        //GetComponent<Rigidbody>().AddForce(test5);

    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        
        if (collisionInfo.collider.tag == "Target")
        {
            Debug.Log("hit");
            Destroy(collisionInfo.collider.gameObject);

            Destroy(gameObject);
            //GameObject newTarg = Instantiate(target1);

            //target target = newTarg.GetComponent<target>();
            //target.CreateNewTarget();
            //FindObjectOfType<PlayerCannon>().CreateNewTarget();
            FindObjectOfType<PlayerCannon>().HitEvent();
            //GetComponent<player>().hittarget = true;
        }

    }


}
