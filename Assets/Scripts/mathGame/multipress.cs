using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipress : MonoBehaviour
{
    public void multiclick()
    {

        Calculate vertemp = GameObject.Find("Calculate").GetComponent<Calculate>();
        vertemp.funcmulti();
    }
}
