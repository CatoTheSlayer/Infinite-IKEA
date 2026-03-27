using UnityEngine;
using UnityEngine.UIElements;

public class CombatUI1 : MonoBehaviour
{
    //Due: Opsætting af de forskellige UI Dokumenter
    [SerializeField] private UIDocument _MenuUIDokument;
    [SerializeField] private UIDocument _AttackMenuUIDokument;
    [SerializeField] private UIDocument _FoodMenuUIDokument;
    [SerializeField] private UIDocument _InventoryMenuUIDokument;
   
   //Due: Opsætting af de forskellige knapper for CombatMenu
    private Button _AttackMenuButton;
    private Button _FoodMenuButton;
    private Button _InventoryMenuButton;

    //Due: OPsætning af knapperne for Attack menu
    private Button _AttackButton1;
    private Button _AttackButton2;
    private Button _AttackButton3;
    private Button _AttackButton4;
    private Button _BackToMenuFromAttack;

    //Due: opsætning af knapperne for Food menu
    private Button _FoodButton1;
    private Button _FoodButton2;
    private Button _FoodButton3;
    private Button _FoodButton4;
    private Button _BackToMenuFromFood;

    //Due: Opsætning af knapperne for Inventory Menu
    private Button _BackToMenuFromInventory;

    //Due: Opsætning af de forskellige knapper og UI Dokuments
    private void Awake()
    {
       //Due: Sætter så man kan se MenuUI dokumentet og resten kan ikke ses
        _MenuUIDokument.rootVisualElement.style.display = DisplayStyle.Flex;

        _AttackMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _FoodMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _InventoryMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;

        //Due: Sætter knapperne op, så scripted ved hvilke knapper der er hvad og hvad der sker når man trykker på den
        _AttackMenuButton = _MenuUIDokument.rootVisualElement.Q("AttackMenuButton") as Button;
        _AttackMenuButton.RegisterCallback<ClickEvent>(OnAttackMenuClick);
      

        _FoodMenuButton = _MenuUIDokument.rootVisualElement.Q("FoodMenuButton") as Button;
        _FoodMenuButton.RegisterCallback<ClickEvent>(OnFoodMenuButtonClick);

        _InventoryMenuButton = _MenuUIDokument.rootVisualElement.Q("InventoryMenuButton") as Button;
        _InventoryMenuButton.RegisterCallback<ClickEvent>(OnInventoryMenuClick);



    }

    //Due: Metode som styre UI til attack menu
    private void OnAttackMenuClick(ClickEvent evt)
    {
        Debug.Log("Du trykkede på Attack Menu knappen");
        
        //Due: Sætter UI dokument til man kan se det
         _AttackMenuUIDokument.rootVisualElement.style.display = DisplayStyle.Flex;
        
        //Due: Sætter UI dokumenter til man ikke kan se dem
        _MenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _FoodMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _InventoryMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;

        //Due: Opsætning af knapperne der bruges i denne menu
        _AttackButton1 = _AttackMenuUIDokument.rootVisualElement.Q("AttackButton1") as Button;
        _AttackButton1.RegisterCallback<ClickEvent>(AttackButton1);

        _AttackButton2 = _AttackMenuUIDokument.rootVisualElement.Q("AttackButton2") as Button;
        _AttackButton2.RegisterCallback<ClickEvent>(AttackButton2);

        _AttackButton3 = _AttackMenuUIDokument.rootVisualElement.Q("AttackButton3") as Button;
        _AttackButton3.RegisterCallback<ClickEvent>(AttackButton3);

        _AttackButton4 = _AttackMenuUIDokument.rootVisualElement.Q("AttackButton4") as Button;
        _AttackButton4.RegisterCallback<ClickEvent>(AttackButton4);

        _BackToMenuFromAttack = _AttackMenuUIDokument.rootVisualElement.Q("BackToMenuButton") as Button;
        _BackToMenuFromAttack.RegisterCallback<ClickEvent>(BlackToCombatMenu);
    }

    //Due: Metode som styre UI for Food menu
    private void OnFoodMenuButtonClick(ClickEvent evt)
    {
        Debug.Log("Du trykkede på FoodMenu knappen");
        _FoodMenuUIDokument.rootVisualElement.style.display = DisplayStyle.Flex;

        //Due: sætter UI dokumenter til man ikke kan se dem
        _MenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _AttackMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _InventoryMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;

        //Due: Opsætning af knapperne der bruges i denne menu
        _FoodButton1 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton1") as Button; 
        _FoodButton2 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton2") as Button; 
        _FoodButton3 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton3") as Button; 
        _FoodButton4 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton4") as Button; 

        _BackToMenuFromFood = _FoodMenuUIDokument.rootVisualElement.Q("BackToMenuFromFood") as Button;
        _BackToMenuFromFood.RegisterCallback<ClickEvent>(BlackToCombatMenu);
    }

    //Due: Metode som styre UI til Inventory Menu
    private void OnInventoryMenuClick(ClickEvent evt)
    {
        Debug.Log("Du trykkede på Inventory Menu Knappen");
        _InventoryMenuUIDokument.rootVisualElement.style.display = DisplayStyle.Flex;

        //Due: Sætter UI dokumenter til man ikke kan se dem
        _MenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _AttackMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _FoodMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;

        //Due: Opsætning af knapperne der bruges i denne menu
        _BackToMenuFromInventory = _InventoryMenuUIDokument.rootVisualElement.Q("") as Button;
        _BackToMenuFromInventory.RegisterCallback<ClickEvent>(BlackToCombatMenu);
    }

    //Due: Metode som tager spilleren tilbage til Combatmenuen
    private void BlackToCombatMenu(ClickEvent evt)
    {
        Debug.Log("Du hhat trykkede på knappen tilbage til combat menu");
        
        //Due: Hvilke UIDokumenter man kan se og hvilke man ikke kan se
         _MenuUIDokument.rootVisualElement.style.display = DisplayStyle.Flex;

        _AttackMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _FoodMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _InventoryMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;

    }

    //Due: Metode for attack knap 1
    private void AttackButton1(ClickEvent evt)
    {
        
    }
    
    //Due: Metode for attack knap 2
    private void AttackButton2(ClickEvent evt)
    {
        
    }
    
    //Due: Metode for attack knap 3
    private void AttackButton3(ClickEvent evt)
    {
        
    }
    
    //Due: Metode for attack knap 4
    private void AttackButton4(ClickEvent evt)
    {
        
    }


    private void OnDisable()
    {
        _BackToMenuFromInventory.UnregisterCallback<ClickEvent>(BlackToCombatMenu);
        _BackToMenuFromAttack.UnregisterCallback<ClickEvent>(BlackToCombatMenu);
    }
}
