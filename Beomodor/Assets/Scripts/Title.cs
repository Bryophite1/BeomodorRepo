using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    
    public GameObject HTP_page;
    public GameObject cred_page;
    public bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d")) {
	    transform.Translate(Vector3.right * 5);
	}
	if (Input.GetKeyDown("a")) {
	    transform.Translate(Vector3.right * -5);
	}
	if (Input.GetKeyDown(KeyCode.Space)) {
	    if (transform.position.x == -7.5)
            {
                print("Start");
            }
	    if (transform.position.x == -2.5)
            {
	    	isActive = !isActive;
	    	HTP_page.SetActive(isActive);
            }
	    if (transform.position.x == 7.5)
            {
                Application.Quit();
            }
	    if (transform.position.x == 2.5)
            {
	    	isActive = !isActive;
	    	cred_page.SetActive(isActive);
            }
		
	}

    }
}



//WIP boundary code
//if (Select.transform.position < Vector3(-8, -2, 0)) {
	    //Select.transform.position = new Vector3(7.5, -2, 0);
	//}
//if (Select.transform.position > Vector3(8, -2, 0)) {
	    //Select.transform.position = new Vector3(-7.5, -2, 0);
	//}