using UnityEngine;

public class destructible : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;

    public GameObject destroyedTarget;

    public bool destroyed;

    // Start is called before the first frame update
    void Start()
    {
        destroyed = false;
        position = transform.position;
        rotation.Set(0, 0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( destroyed == false)
        {
            Instantiate(destroyedTarget, position, rotation);
            destroyed = true;
        }
    }

}
