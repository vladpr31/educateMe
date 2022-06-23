using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class confettiWin : MonoBehaviour
{
    [SerializeField] private Transform pConfetti;
    [SerializeField] private Color[] colorArr;
    private List<Confetti> confettiList;
    private float spawnTimer;
    private const float maxSpawnTimer=0.033f;

    private void Start()
    {
        confettiList = new List<Confetti>();
        Debug.Log("1");
    }
    private void Update()
    {
        Debug.Log("2");
        foreach (Confetti confetti in new List<Confetti> (confettiList))
        {
            confetti.Update();
            /*
            if (confetti.Update())
            {
                confettiList.Remove(confetti);
            }
            */
        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer += maxSpawnTimer;
            int spawnAmount = Random.Range(1, 4);
            for(int i=0;i < spawnAmount;i++)
            {
                confettiSpawner();
            }
        }
    }

    private void confettiSpawner()
    {
        Debug.Log("3");
        float width = transform.GetComponent<RectTransform>().rect.width;
        float height = transform.GetComponent<RectTransform>().rect.height;
        Vector2 anchoredPosition = new Vector2(Random.Range(-width/2f,width/2f),height/2);
        //Color color = colorArr[Random.Range(0, colorArr.Length)];
        Confetti confetti = new Confetti(pConfetti,transform,anchoredPosition,-height/2f);
        confettiList.Add(confetti);
    }

    private class Confetti
    {
        private Transform transform;
        private RectTransform rectTransform;
        private Vector2 anchoredPosition;
        private Vector3 euler; //Rotation of Confetti
        private float eulerSpeed;
        private Vector2 moveAmount;
        private float minY;
        public Confetti(Transform prefab,Transform container,Vector2 anchoredPosition, float minY)
        {
            Debug.Log("4");
            this.anchoredPosition = anchoredPosition;
            transform = Instantiate(prefab, container);
            rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta*=Random.Range(0.1f, 0.4f);
            euler = new Vector3(0, 0, Random.Range(0f, 360f));
            eulerSpeed = Random.Range(100f, 200f); //Rotation
            eulerSpeed = Random.Range(0f, 2f) == 0 ? 1f:-1f ; //Clockwise and counter clockwise Rotation.
            transform.localEulerAngles = euler;
            moveAmount = new Vector2(0, Random.Range(-200f, -50f)); //Controls the speed of falling to be random.
            //transform.GetComponent<Image>().color = color;
        }
        public void Update()
        {
            Debug.Log("5");
            Vector2 moveAmount = new Vector2(0, -70f); //speed of the confetti
            anchoredPosition += moveAmount * Time.deltaTime;
            rectTransform.anchoredPosition= anchoredPosition;
            euler.z+=eulerSpeed * Time.deltaTime;
            transform.localEulerAngles = euler;
            /*
            if(anchoredPosition.y<minY)
            {
                Destroy(transform.gameObject);
                return true;
            }
            else
            {
                return false;
            }
            */
        }
    }


}
