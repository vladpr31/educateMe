using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberManager : MonoBehaviour
{
    [SerializeField] Transform[] number;
    private static Queue<int> Ids = new Queue<int>();
    private bool locked = true;
    public static float field = -10f;
    private int index = 0;

    private void Start()
    {
        field = -10f;
        createIds();
        if(!locked)
        {
            StartCoroutine(numberSpawner());
        }
    }

    public IEnumerator numberSpawner()
    {
        while (Ids.Count > 0)
        {
            index = Ids.Dequeue();
            number[index].transform.position = new Vector2(field, -5f);
            field += 2f;
            yield return new WaitForSeconds(0);
        }
        field = -7f;
    }
    void createIds()
    {
        int count = 0;
        while (count != 11)
        {
            int newID = Random.Range(0, 11);
            if (Ids.Contains(newID) != true)
            {
                Ids.Enqueue(newID);
                count += 1;
            }
        }

        locked = false;
    }
}
