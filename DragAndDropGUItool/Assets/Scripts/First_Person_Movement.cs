using UnityEngine;
using System.Collections;

public class First_Person_Movement : MonoBehaviour 
{
    DragnDropGUI dndTool;
	// Use this for initialization
	void Start () 
    {
        dndTool = gameObject.GetComponent<DragnDropGUI>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(100f * Vector3.forward *Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(100f * Vector3.back * Time.deltaTime);
        }
	}
}
