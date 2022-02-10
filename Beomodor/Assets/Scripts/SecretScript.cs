using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretScript : MonoBehaviour
{
    public int temp;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        temp = Random.Range(0, 100);
        if (temp == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
