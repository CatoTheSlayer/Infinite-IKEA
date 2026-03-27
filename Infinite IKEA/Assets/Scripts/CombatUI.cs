using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatUI : MonoBehaviour
{
    private UIDocument document;
    private Button attackButton;
    
    void Awake()
    {
        document = GetComponent<UIDocument>();

        attackButton = document.rootVisualElement.Q("Attack") as Button;
        attackButton.RegisterCallback<ClickEvent>(OnAttackButtonClick);
    }

    void OnDisable()
    {
            attackButton.UnregisterCallback<ClickEvent>(OnAttackButtonClick);
    }

    void OnAttackButtonClick(ClickEvent evt)
    {
        Debug.Log("Attack button clicked!");
        // Implement logic to show attack options and handle player input for attacking
    }
}
