using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dividepress : MonoBehaviour
{
    public void divideclick()
    {

        Calculate vertemp = GameObject.Find("Calculate").GetComponent<Calculate>();
        vertemp.funcdivide();
    }
}
