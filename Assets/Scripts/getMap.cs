using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getMap : MonoBehaviour
{
    private static int map = 5;

    public static int getMapID()
    {
        return map;
    }
    public static void setMapID(int mp)
    {
        map = mp;
    }
}
