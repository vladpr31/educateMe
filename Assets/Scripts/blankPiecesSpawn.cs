using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class blankPiecesSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform[] blankPiece;
    private static float posX = -10f;
    private static float posY = 0.5f;
    private static Queue<int> Ids = new Queue<int>();
    private int i = 0;
    private bool locked = true;
    private void Start()
    {
        posX = -10f;
        posY = 0.5f;
        createIds();
        if (!locked) { StartCoroutine(blankSpanwer()); }
        //StartCoroutine(blankSpanwer());
    }
 
    private IEnumerator blankSpanwer()
    {
        while (Ids.Count > 0)
        {
            blankPiece[i].transform.position = new Vector2(posX + (2f * Ids.Dequeue()), posY);
            yield return new WaitForSeconds(0);
            i += 1;
        }
    }
    void createIds()
    {
        int count = 0;
        while (count != 11)
        {
            int newID = Random.Range(0, 11);
            if(Ids.Contains(newID)!=true)
            {
                Ids.Enqueue(newID);
                count += 1;
            }
        }

        locked = false;
    }

}
