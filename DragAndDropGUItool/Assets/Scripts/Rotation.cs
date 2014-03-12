using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour 
{
    public float time_constant;
    public float angle;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.W))
        {
            float Y = Input.GetAxis("Vertical") * angle;
            Quaternion rotate = Quaternion.Euler(Y, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * time_constant);
        }

        if (Input.GetKey(KeyCode.S))
        {
            float Y = Input.GetAxis("Vertical") * angle;
            Quaternion rotate = Quaternion.Euler(Y, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * time_constant);
        }

        if (Input.GetKey(KeyCode.A))
        {
            float X = -1 * Input.GetAxis("Horizontal") * angle;
            Quaternion rotate = Quaternion.Euler(0, 0, X);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * time_constant);
        }

        if (Input.GetKey(KeyCode.D))
        {
            float X = -1 * Input.GetAxis("Horizontal") * angle;
            Quaternion rotate = Quaternion.Euler(0, 0, X);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * time_constant);
        }
	}
}
