using UnityEngine;
using System.Collections;

public class Manipulation_Disabled : MonoBehaviour 
{
   /******************************************************************************************************************************************************
   First read DragnDropGUI script. This script is used specifically to turn off components that are used to manipulate the object while it is being dragged. 
    
   --How to modify--
   **Note: Everything is done in update**
     
   1.) Decide on a feature that you want to be able to modify while the gameobject is being dragged. Example: in certain puzzle games objects must be rotated
       a specific way to that they can be placed in a location. Then write a script for this behavior.
   2.) Make sure that there is an if statement that checks whether the component that manages this feature is not
       null since this script is called for ALL DnD gameobjects it is attatched to.
   3.) Select the component that manages this feature set component or the bool attatched to the component to it's opposite state from Manipulation Enabled.
   *******************************************************************************************************************************************************/

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (gameObject.GetComponent<Rotation>() != null)
        {
            gameObject.GetComponent<Rotation>().enabled = false;
        }
	}
}
