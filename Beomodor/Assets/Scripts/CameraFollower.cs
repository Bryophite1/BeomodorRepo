using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject followTarget;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, followTarget.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, followTarget.transform.position.x, interpolation);

        this.transform.position = position;
    }
}
