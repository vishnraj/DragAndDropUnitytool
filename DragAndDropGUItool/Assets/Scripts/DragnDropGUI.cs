using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragnDropGUI : MonoBehaviour 
{
    /*************************************************
     This is the Drag and Drop GUI tool. Simply attach this script to a gameobject that you wish to act as a GUI object. Works for 2D and 3D games.
     The GUI Window itself it also draggable. Dragging the GUI requires clicking the GUI Window and moving it to a new location. 
     Dragging an object requires clicking the desired object's GUI button (full click), moving it to a location, and then clicking to drop. 
     (Dragging can only be done via mouse)
     
     Customizing the tool:
     
     -- The tool is composed of 5 Scripts --
     **Note: Each is public and can be edited on the unity editor EXCEPT clicked which is meant to be false unless an object is selected**
     
     - Script 1: This
      - Texture List - Decide on a gameobject that you wish to make Drag and Dropable (DnD). Increase the size by 1. Drag a texture that respresents
        that object into the new slot.
      
      - Prefab List - Select the prefab for the gameobject, increase the size of this list by 1, and drag the prefab into the slot.
      
      - Description List - Increase the size of this list by 1 and enter a name for the object. Keep names short and easy to understand, otherwise 
        they will spill out of the GUI box. Longer descriptions should go in a seperate gui box.
      
      - Condition List - This is a public list to set conditions for certain gameobjects. Ex: If an item costs a certain amount of money or points
        then the condition in the corresponding index would be false unless the condition was satisified. This list is public so it can be accessed by other
        scripts that wish to modify the conditions for a particular object.
     
      - Distance - This is a float that should be the opposite sign of whatever Z value the main camera has. It represents the distance into the screen
        via ray cast.
     
     **Note: The size of each list should be equal after this set up** 
     
     - Scripts 2 and 3: Disabled and Enabled
        Both these scripts are used in conjunction. The Disabled script sets all desired components off and the Enabled script restores them.
        Once an object is selected all desired components are deactivated until it is placed somewhere in the game world. 
        **Note: IMPORTANT - All gameobjects containing desired components are treated the same way. See script for further insight and make modifications
          by following instructions in script**
        **Note: ALWAYS set these to disabled on any gameobject they are attatched to.**
    
     - Scripts 4 and 5: Manipulation_Enabled and Manipulation_Disabled
        Both scripts are used in conjunction. The Manipulation_Enabled script sets all desired components that are required to manipulate
        a game object (see rotation script for an example) on and the Manipulation_Disabled script sets them all to off. Once an object is selected
        it can be manipulated in whatever manner the user wishes based on the components attatched to it until it is set somewhere in the game world.
        **Note: IMPORTANT - All gameobjects containing desired components are treated the same way. See script for further insight and make modifications
          by following instructions in script**
        **Note: ALWAYS set these to disabled on any gameobject they are attatched to.**
     
     ************************************************/

    public List<Texture> texture_list; //Texture List for DnD objects. Modify on editor
    public List<GameObject> prefab_list; //Prefab List for DnD objects. Make sure indexes correspond to Texture list. Modify on editor
    public List<string> description_list; //Description List for DnD objects. Make sure indexes correspond to other lists. Modify on editor
    public List<bool> condition_list; //Condition list for DnD objects. Make sure indexes correspond to other lists. Generally 
                                      //only modify through other scripts.                           
    public float distance; //The distance into the screen. Opposite value of main camera's z value, but can be changed on editor to adjust depth.
    public bool clicked; // DO NOT MODIFY. Public only to be checked by other scripts that need to know when an object is selected on the GUI.

    GameObject clone; //Private gameobject used to reprsent newly intantiated gameobjects.
    Rect windowRect = new Rect(0, 100, 200, 80); //Dimensions for the size of the window and the position. Should not be changed. 
                                                 //Change position by dragging window in the scene instead. 
    public string Title; //The Title for this window. 

    void Start()
    {
        clicked = false; //Intially must be set to false until gameobject is selected
        windowRect.height *= texture_list.Count; //Increases size of GUI box vertically depending on the number of gameobjects added
    }

    void makeWindow(int windowID)
    {
        //Checks to make sure each list is the same size
        if (texture_list.Count == prefab_list.Count && prefab_list.Count == description_list.Count && description_list.Count == condition_list.Count)
        {
            for (int i = 0; i < texture_list.Count; ++i)
            {
                //Adds a new description for each new gameobject
                GUI.Label(new Rect(70, 55 + i * 70, 100, 100), "<size=22> " + description_list[i] + " </size>");
                if (GUI.Button(new Rect(5, 40 + i * 70, 50, 50), new GUIContent(texture_list[i])))
                {
                    //Checks to see if condition has been satisfied and instantiates gameobject
                    if (condition_list[i])
                    {
                        float into = distance;
                        clone = Instantiate(prefab_list[i],
                          Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z)).GetPoint(into),
                          Quaternion.identity) as GameObject;

                        clicked = true; //gameobject is selected
                    }
                }

                //Allows for moving and manipulation once object is selected
                if (clicked)
                {
                    if (clone != null)
                    {
                        //Disables selected gameobject's components until the object is placed somewhere
                        if (clone.GetComponent<Disabled>() != null)
                        {
                            clone.GetComponent<Disabled>().enabled = true;
                        }

                        //Enables selected gameobject's manipulation components for when the object is being dragged
                        if (clone.GetComponent<Manipulation_Enabled>() != null)
                        {
                            clone.GetComponent<Manipulation_Enabled>().enabled = true;
                        }

                        float into = distance;
                        //Moves gameobject around screen via mouse
                        clone.transform.position =
                            Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z)).GetPoint(into);
                    }
                }

                //Sets the game object in the desired location "distance" in front of the camera
                if (clicked && Input.GetMouseButtonDown(0))
                {
                    if (clone != null)
                    {
                        clicked = false;

                        //Enables gameobject fully once placed
                        /**************************************************************/
                        if (clone.GetComponent<Disabled>() != null)
                        {
                            clone.GetComponent<Disabled>().enabled = false;
                        }

                        if (clone.GetComponent<Enabled>() != null)
                        {
                            clone.GetComponent<Enabled>().enabled = true;
                        }
                        /*************************************************************/

                        //Disables all manipulation components
                        /*************************************************************/
                        if (clone.GetComponent<Manipulation_Enabled>() != null)
                        {
                            clone.GetComponent<Manipulation_Enabled>().enabled = false;
                        }

                        if (clone.GetComponent<Manipulation_Disabled>() != null)
                        {
                            clone.GetComponent<Manipulation_Disabled>().enabled = true;
                        }
                        /*************************************************************/

                        clone = null;
                    }
                }
            }
        }

        GUI.DragWindow(); //Makes the GUI Window itself dragable as well
    }

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, makeWindow, "<size=22> " + Title + " </size>"); //Instantiates GUI
    }

        

    void Update()
    {
      
    }
}
