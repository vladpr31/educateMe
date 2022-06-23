using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class collectablesControl : MonoBehaviour
{
    public AudioClip collector;
    [SerializeField] GameObject obj;
    private moveChar character;
    private void Start()
    {
        character = GetComponent<moveChar>();
    }
    private void OnTriggerEnter2D(Collider2D other) //Function for collider with object and player.
    {
        if(other.tag=="Player")
        {
            if (gControl.getDiceValue() == moveChar.getWaypointIndex())
            {
                AudioSource.PlayClipAtPoint(collector, transform.position, 1f);
                Destroy(gameObject);
                obj.SetActive(true);
            }
            AudioSource.PlayClipAtPoint(collector, transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}
