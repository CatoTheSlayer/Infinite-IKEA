using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CombatUI1 : MonoBehaviour
{
    //Due: Opsætting af de forskellige UI Dokumenter
    [SerializeField] private UIDocument _MenuUIDokument;
    [SerializeField] private UIDocument _AttackMenuUIDokument;
    [SerializeField] private UIDocument _FoodMenuUIDokument;
    [SerializeField] private UIDocument _HPbarUIDokument;
   
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

    //Ask: import af lyd
    public AudioSource eat_sound;
    public AudioSource hit_sound;

    //Due: Opsætning af knapperne for Inventory Menu
    private Button _BackToMenuFromInventory;
    //ian:
    private MainManager mainManager;
    private Texture inventoryContents;
    public int[] ItemSlots;
    private SC_PickItem[] availableItems;
    private ProgressBar enemyHealthBar;
    private ProgressBar playerHealthBar;
    private SC_InventorySystem inventorySystem;
    private AttackUI attackUI;
    private TurnManager turnManager;
    private PlayerAnimController playerAnimController;
    private EnemyController enemyController;

    //Due: Opsætning af de forskellige knapper og UI Dokuments
    private void Awake()
    {
       //Due: Sætter så man kan se MenuUI dokumentet og resten kan ikke ses
        _MenuUIDokument.rootVisualElement.style.display = DisplayStyle.Flex;

        _AttackMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _FoodMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;

        //Due: Sætter knapperne op, så scripted ved hvilke knapper der er hvad og hvad der sker når man trykker på den
        _AttackMenuButton = _MenuUIDokument.rootVisualElement.Q("AttackMenuButton") as Button;
        _AttackMenuButton.RegisterCallback<ClickEvent>(OnAttackMenuClick);
      

        _FoodMenuButton = _MenuUIDokument.rootVisualElement.Q("FoodMenuButton") as Button;
        _FoodMenuButton.RegisterCallback<ClickEvent>(OnFoodMenuButtonClick);

        _InventoryMenuButton = _MenuUIDokument.rootVisualElement.Q("InventoryMenuButton") as Button;
        _InventoryMenuButton.RegisterCallback<ClickEvent>(OnInventoryMenuClick);

        enemyHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("EnemyHp");
        enemyHealthBar.value = 100; // Set initial HP value

        playerHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("PlayerHp");
        playerHealthBar.value = 100; // Set initial HP value
        turnManager = FindFirstObjectByType<TurnManager>();
        playerAnimController = FindFirstObjectByType<PlayerAnimController>();
        enemyController = FindFirstObjectByType<EnemyController>();
        
       
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

        //Due: Opsætning af knapperne der bruges i denne menu
        _FoodButton1 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton1") as Button; 
        _FoodButton1.RegisterCallback<ClickEvent>(FoodButton1);

        _FoodButton2 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton2") as Button; 
        _FoodButton2.RegisterCallback<ClickEvent>(FoodButton2);
        _FoodButton3 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton3") as Button; 
        _FoodButton3.RegisterCallback<ClickEvent>(FoodButton3);
        _FoodButton4 = _FoodMenuUIDokument.rootVisualElement.Q("FoodButton4") as Button; 
        _FoodButton4.RegisterCallback<ClickEvent>(FoodButton4);

        _BackToMenuFromFood = _FoodMenuUIDokument.rootVisualElement.Q("BackToMenuFromFood") as Button;
        _BackToMenuFromFood.RegisterCallback<ClickEvent>(BlackToCombatMenu);
    }

    //Due: Metode som styre UI til Inventory Menu
    private void OnInventoryMenuClick(ClickEvent evt)
    {
        Debug.Log("Du trykkede på Inventory Menu Knappen");

        //Due: Sætter UI dokumenter til man ikke kan se dem
        _MenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _AttackMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;
        _FoodMenuUIDokument.rootVisualElement.style.display = DisplayStyle.None;

        //Due: Opsætning af knapperne der bruges i denne menu
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

    }

    //Due: Metode for attack knap 1
    private void AttackButton1(ClickEvent evt)
    {
        //attackUI.AttackButton1();
        if(MainManager.Instance.itemSlots[0] == -1)
        {
            Debug.Log("No item in slot 0");
            return;
        }
        else
        {
            Debug.Log("items in slot 0: " + MainManager.Instance.itemSlots[0]);
            Debug.Log("item name in slot 0: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemName);
            enemyHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemDamage; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemDamage);
            Debug.Log("Current HP: " + enemyHealthBar.value);
            playerAnimController.playAttackAnimation();
            if (GameObject.FindGameObjectsWithTag("Boss").Length > 0)
            {
                
            }
            else
            {
                enemyController.enemyHurt();
            }
            hit_sound.Play();
            turnManager.PlayerTurnEnd();
        }
    }
    
    //Due: Metode for attack knap 2
    private void AttackButton2(ClickEvent evt)
    {
        if(MainManager.Instance.itemSlots[1] == -1)
        {
            Debug.Log("No item in slot 1");
            return;
        }
        else
        {
            Debug.Log("items in slot 1: " + MainManager.Instance.itemSlots[1]);
            Debug.Log("item name in slot 1: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemName);
            enemyHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemDamage; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemDamage);
            Debug.Log("Current HP: " + enemyHealthBar.value);
            playerAnimController.playAttackAnimation();
            if (GameObject.FindGameObjectsWithTag("Boss").Length > 0)
            {
                
            }
            else
            {
                enemyController.enemyHurt();
            }
            hit_sound.Play();
            turnManager.PlayerTurnEnd();
        }
    }
    
    //Due: Metode for attack knap 3
    private void AttackButton3(ClickEvent evt)
    {
        if(MainManager.Instance.itemSlots[2] == -1)
        {
            Debug.Log("No item in slot 2");
            return;
        }
        else
        {
            Debug.Log("items in slot 2: " + MainManager.Instance.itemSlots[2]);
            Debug.Log("item name in slot 2: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemName);
            enemyHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemDamage; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemDamage);
            Debug.Log("Current HP: " + enemyHealthBar.value);
            playerAnimController.playAttackAnimation();
            if (GameObject.FindGameObjectsWithTag("Boss").Length > 0)
            {
                
            }
            else
            {
                enemyController.enemyHurt();
            }
            hit_sound.Play();
            turnManager.PlayerTurnEnd();
        }

    }
    
    //Due: Metode for attack knap 4
    private void AttackButton4(ClickEvent evt)
    {
        if(MainManager.Instance.itemSlots[3] == -1)
        {
            Debug.Log("No item in slot 3");
            return;
        }
        else
        {
            Debug.Log("items in slot 3: " + MainManager.Instance.itemSlots[3]);
            Debug.Log("item name in slot 3: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemName);
            enemyHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemDamage; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemDamage);
            Debug.Log("Current HP: " + enemyHealthBar.value);
            playerAnimController.playAttackAnimation();
            if (GameObject.FindGameObjectsWithTag("Boss").Length > 0)
            {
                
            }
            else
            {
                enemyController.enemyHurt();
            }
            hit_sound.Play();
            turnManager.PlayerTurnEnd();
        }
    }

    private void FoodButton1(ClickEvent evt)
    {
        //attackUI.AttackButton1();
        if(MainManager.Instance.itemSlots[0] == -1)
        {
            Debug.Log("No item in slot 0");
            return;
        }
        else
        {
            Debug.Log("items in slot 0: " + MainManager.Instance.itemSlots[0]);
            Debug.Log("item name in slot 0: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemName);
            playerHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemHeal; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemDamage);
            Debug.Log("Current HP: " + playerHealthBar.value);
            playerAnimController.playHealAnimation();
            eat_sound.Play();
            turnManager.PlayerTurnEnd();
        }
    }

    private void FoodButton2(ClickEvent evt)
    {
        //attackUI.AttackButton1();
        if(MainManager.Instance.itemSlots[1] == -1)
        {
            Debug.Log("No item in slot 1");
            return;
        }
        else
        {
            Debug.Log("items in slot 1: " + MainManager.Instance.itemSlots[1]);
            Debug.Log("item name in slot 1: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemName);
            playerHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemHeal; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemDamage);
            Debug.Log("Current HP: " + playerHealthBar.value);
            playerAnimController.playHealAnimation();
            eat_sound.Play();
            turnManager.PlayerTurnEnd();  
        }
    }
    private void FoodButton3(ClickEvent evt)
    {
        //attackUI.AttackButton1();
        if(MainManager.Instance.itemSlots[2] == -1)
        {
            Debug.Log("No item in slot 2");
            return;
        }
        else
        {
            Debug.Log("items in slot 2: " + MainManager.Instance.itemSlots[2]);
            Debug.Log("item name in slot 2: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemName);
            playerHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemHeal; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemDamage);
            Debug.Log("Current HP: " + playerHealthBar.value);
            playerAnimController.playHealAnimation();
            eat_sound.Play();
            turnManager.PlayerTurnEnd();
        }
    }
    private void FoodButton4(ClickEvent evt)
    {
        //attackUI.AttackButton1();
        if(MainManager.Instance.itemSlots[3] == -1)
        {
            Debug.Log("No item in slot 3");
            return;
        }
        else
        {
            Debug.Log("items in slot 3: " + MainManager.Instance.itemSlots[3]);
            Debug.Log("item name in slot 3: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemName);
            playerHealthBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemHeal; // Example of calculating damage and updating HP
            Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemDamage);
            Debug.Log("Current HP: " + playerHealthBar.value);
            playerAnimController.playHealAnimation();
            eat_sound.Play();
            turnManager.PlayerTurnEnd();
        }
    }

    private void OnDisable()
    {
        _BackToMenuFromInventory.UnregisterCallback<ClickEvent>(BlackToCombatMenu);
        _BackToMenuFromAttack.UnregisterCallback<ClickEvent>(BlackToCombatMenu);
    }
    void FixedUpdate()
    {
        ItemSlots = MainManager.Instance.itemSlots;
        availableItems = MainManager.Instance.availableItems;
    }
}
