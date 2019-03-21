using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class MenuFix : MonoBehaviour
{
    // https://answers.unity.com/questions/1437486/vrtk-ui-doesnt-work-after-load-scene.html
    public GameObject EventSystem;
    public VRTK_UIPointer PointerController;

    private VRTK_VRInputModule[] inputModule;

    void Start()
    {
        StartCoroutine(LateStart(1));
        print("ran start");
    }

    void Update()
    {

        if (inputModule != null)
        {
            print("input not null");
            if (inputModule.Length > 0)
            {
                
                inputModule[0].enabled = true;
                if (inputModule[0].pointers.Count == 0)
                    inputModule[0].pointers.Add(PointerController);
                print("enable input");
            }
            else
                inputModule = EventSystem.GetComponents<VRTK_VRInputModule>();

        }
    }

    IEnumerator LateStart(float waitTime)
    {
        print("Waiting");
        yield return new WaitForSeconds(waitTime);
        EventSystem.SetActive(true);
        EventSystem.GetComponent<EventSystem>().enabled = false;
        inputModule = EventSystem.GetComponents<VRTK_VRInputModule>();
    }
}