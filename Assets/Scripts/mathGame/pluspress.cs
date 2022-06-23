using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pluspress : MonoBehaviour
{
    public void plusclick()
    {
        
        Calculate vertemp = GameObject.Find("Calculate").GetComponent<Calculate>();
        vertemp.funcplus();
    }

}
