using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatActions : MonoBehaviour
{
    Vector2 mousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
        public void Mouseclick(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        // Implement attack logic here, using mousePosition for targeting if needed
        //hide the old ui
        //open new ui thing that will show the attack options and then use mousePosition to select the target and attackvor defend type
    }
    void itemUse()
    {
        // Implement item use logic here, using mousePosition for targeting if needed
        //hide the old ui
        //open new ui thing that will show the item options and then use mousePosition to select the target and item type
    }
    void inventory()
    {
        // Implement inventory logic here, using mousePosition for targeting if needed
        //hide the old ui
        //open a new ui where the player can see and interact with their inventory, using mousePosition to select items and manage them
    }   
    void BackButton()
    {
        // Implement back button logic here, using mousePosition for targeting if needed
        //close the current ui and return to the previous one, using mousePosition to select the back button and confirm the action
    }
}
