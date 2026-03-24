using System;
using System.IO;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatUI : MonoBehaviour
{
    private UIDocument document;
    private Button AttackButton;
    private Button ItemButton;
    private Button BackButton;
    private ProgressBar progressBar;
    
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
        AttackButton.UnregisterCallback<ClickEvent>(OnAttackButtonClick);
        ItemButton.UnregisterCallback<ClickEvent>(OnItemButtonClick);
    }

    void OnAttackButtonClick(ClickEvent evt)
    {
        Debug.Log("Attack button clicked!");
        // Implement logic to show attack options and handle player input for attacking
        //hides the old ui
        AttackButton.visible = false;
        ItemButton.visible = false;
        BackButton.visible = true;
        progressBar.value -= 10; // Example of reducing HP when attacking
    }
    void OnItemButtonClick(ClickEvent evt)
    {
        Debug.Log("Item button clicked!");
        // Implement logic to show item options and handle player input for using items
        AttackButton.visible = false;
        ItemButton.visible = false;
        BackButton.visible = true;
    }
    void OnBackButtonClick(ClickEvent evt)
    {
        Debug.Log("Back button clicked!");
        // Implement logic to go back to the previous menu or close the current UI
        AttackButton.visible = true;
        ItemButton.visible = true;
        BackButton.visible = false;
    }
}
