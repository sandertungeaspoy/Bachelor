using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeypadLock : MonoBehaviour
{
    public Text textPad;
    public string password = "1337";
    public Animator GateAnimator;

    // Start is called before the first frame update
    void Start()
    {
        textPad = textPad.GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Max 14 numbers

    public void AddNumber()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "1";
        }
        
    }
    public void AddNumber2()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "2";
        }

    }
    public void AddNumber3()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "3";
        }

    }
    public void AddNumber4()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "4";
        }

    }
    public void AddNumber5()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "5";
        }

    }
    public void AddNumber6()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "6";
        }

    }
    public void AddNumber7()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "7";
        }

    }
    public void AddNumber8()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "8";
        }

    }
    public void AddNumber9()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "9";
        }

    }
    public void AddNumber0()
    {
        if (textPad.text.Length < 14 && CheckClear())
        {
            textPad.text += "0";
        }

    }
    public void Clear()
    {
        if (CheckClear())
        {
            textPad.text = "";
        }
    }

    public void Enter()
    {
        if (CheckClear())
        {
            if (textPad.text == password)
            {
                textPad.text = "Correct";
                StartCoroutine("ClearPad");
                GateAnimator.Play("OpenGate");
            }
            else
            {
                textPad.text = "Wrong";
                StartCoroutine("ClearPad");
            }
        }

    }

    IEnumerator ClearPad()
    {
        yield return new WaitForSeconds(2f);
        if (textPad.text == "Correct")
        {
            textPad.text = "Open";
        }
        else
        {
            textPad.text = "";
        }
    }

    private bool CheckClear()
    {
        return textPad.text == "Wrong" || textPad.text == "Correct" || textPad.text == "Open" ? false : true;
    }
}
