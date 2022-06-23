using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minuspress : MonoBehaviour
{
    public void minusclick()
    {

        Calculate vertemp = GameObject.Find("Calculate").GetComponent<Calculate>();
        vertemp.funcminus();
    }
}
