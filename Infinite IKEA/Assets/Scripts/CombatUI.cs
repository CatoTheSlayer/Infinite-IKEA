using System;
using System.IO;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatUI : MonoBehaviour
{
    public MainManager mainManager; // Reference to the inventory system to access player's inventory and equipped items
    private UIDocument document;
    private Button AttackButton;
    private Button ItemButton;
    private Button BackButton;
    private ProgressBar progressBar;
    private Texture inventoryContents;
    public int[] ItemSlots;
    private SC_PickItem[] availableItems;
    //need to make somthing that gets the inventorys contents and what is equiped
    
    void Awake()
    {
        document = GetComponent<UIDocument>();

        AttackButton = document.rootVisualElement.Q("Attack") as Button;
        AttackButton.RegisterCallback<ClickEvent>(OnAttackButtonClick);

        ItemButton = document.rootVisualElement.Q("Item") as Button;
        ItemButton.RegisterCallback<ClickEvent>(OnItemButtonClick);

        BackButton = document.rootVisualElement.Q("Back") as Button;
        BackButton.RegisterCallback<ClickEvent>(OnBackButtonClick);

        progressBar = document.rootVisualElement.Q<ProgressBar>("HP");
        progressBar.value = 100; // Set initial HP value
    }

    void OnDisable()
    {
<<<<<<< HEAD
            attackButton.UnregisterCallback<ClickEvent>(OnAttackButtonClick);
=======
        AttackButton.UnregisterCallback<ClickEvent>(OnAttackButtonClick);
        ItemButton.UnregisterCallback<ClickEvent>(OnItemButtonClick);
>>>>>>> 06a9264cfe858d9dd3dfa30535e100faf2b0b101
    }

    void OnAttackButtonClick(ClickEvent evt)
    {
        Debug.Log("Attack button clicked!");
        // Implement logic to show attack options and handle player input for attacking
        //hides the old ui
        AttackButton.visible = false;
        ItemButton.visible = false;
        BackButton.visible = true;
        //attactk 1 visble = true
        Debug.Log("items in slot 0: " + mainManager.itemSlots[0]);
        Debug.Log("item name in slot 0: " + mainManager.availableItems[mainManager.itemSlots[0]].itemName);
        progressBar.value -= mainManager.availableItems[mainManager.itemSlots[0]].itemDamage; // Example of calculating damage and updating HP
        Debug.Log("Damage dealt: " + mainManager.availableItems[mainManager.itemSlots[0]].itemDamage);
        Debug.Log("Current HP: " + progressBar.value);
        //show a new ui that shows the weapons that you have equiped and then use mousePosition to select attack type
        //calculate the damage and then update the progress bar to show the new HP value of the target
        //then end the turn and go back to the old ui
    }
    void OnItemButtonClick(ClickEvent evt)
    {
        Debug.Log("Item button clicked!");
        // Implement logic to show item options and handle player input for using items
        AttackButton.visible = false;
        ItemButton.visible = false;
        BackButton.visible = true;
        //show a new ui that shows the items that you have in your hotbar and then use mousePosition to select item type and target
        //do the effect of what ever item is used
        //then end the turn and go back to the old ui
    }
    void OnBackButtonClick(ClickEvent evt)
    {
        Debug.Log("Back button clicked!");
        // Implement logic to go back to the previous menu or close the current UI
        AttackButton.visible = true;
        ItemButton.visible = true;
        BackButton.visible = false;
    }
    void Inventory()
    {
        // Implement inventory logic here, using mousePosition for targeting if needed'
        inventoryContents = mainManager.availableItems[mainManager.itemSlots[0]].itemPreview; // Get the current inventory contents from the MainManager
        //hide the old ui
        //open a new ui where the player can see and interact with their inventory, using mousePosition to select items and manage them
        //if something is changed in the inventory then update the combat ui to show the new inventory and what is equiped
        //go bakc to the first ui
    }
    void Update()
    {
        ItemSlots = mainManager.itemSlots;
        availableItems = mainManager.availableItems;
    }
}
