using UnityEngine;
using System.Collections;

public class Disabled : MonoBehaviour 
{
    /**********************************************************************************************************
    First read DragnDropGUI script. This script is used specifically to turn off desired gameobject components. 
    
    --How to modify--
    **Note: Everything is done in update**
     
    1.) Decide on a feature that you do not wish to be active while the object is being dragged.
    2.) Make sure that there is an if statement that checks whether the component that manages this feature is not
        null since this script is called for ALL DnD gameobjects it is attatched to.
    3.) Select the component that manages this feature (for example gravity is managed by rigidbody, or 
        in another scenario projectiles may be managed by one's own script) and set component or the bool
        attatched to the component to correct state.
    ***********************************************************************************************************/

    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }

        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        if (gameObject.GetComponent<SphereCollider>() != null)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (gameObject.GetComponent<CapsuleCollider>() != null)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
	}
}
