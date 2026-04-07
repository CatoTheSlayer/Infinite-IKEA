using UnityEngine;
using UnityEngine.UIElements;
public class AttackUI : MonoBehaviour
{
    private MainManager mainManager;
    private Texture inventoryContents;
    public int[] ItemSlots;
    private SC_PickItem[] availableItems;
    private ProgressBar progressBar;
    private SC_InventorySystem inventorySystem;
    [SerializeField] private UIDocument _HPbarUIDokument;


    void Awake()
    {          
        progressBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("EnemyHp");
    }

        //Due: Metode for attack knap 1
    internal void AttackButton1()
    {
        Debug.Log("items in slot 0: " + MainManager.Instance.itemSlots[0]);
        Debug.Log("item name in slot 0: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemName);
        progressBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemDamage; // Example of calculating damage and updating HP
        Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[0]].itemDamage);
        Debug.Log("Current HP: " + progressBar.value);
    }
    
    //Due: Metode for attack knap 2
    internal void AttackButton2()
    {
        Debug.Log("items in slot 1: " + MainManager.Instance.itemSlots[1]);
        Debug.Log("item name in slot 1: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemName);
        progressBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemDamage; // Example of calculating damage and updating HP
        Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[1]].itemDamage);
        Debug.Log("Current HP: " + progressBar.value);
    }
    
    //Due: Metode for attack knap 3
    internal void AttackButton3()
    {
        Debug.Log("items in slot 2: " + MainManager.Instance.itemSlots[2]);
        Debug.Log("item name in slot 2: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemName);
        progressBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemDamage; // Example of calculating damage and updating HP
        Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[2]].itemDamage);
        Debug.Log("Current HP: " + progressBar.value);  
    }
    
    //Due: Metode for attack knap 4
    internal void AttackButton4()
    {
        Debug.Log("items in slot 3: " + MainManager.Instance.itemSlots[3]);
        Debug.Log("item name in slot 3: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemName);
        progressBar.value -= MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemDamage; // Example of calculating damage and updating HP
        Debug.Log("Damage dealt: " + MainManager.Instance.availableItems[MainManager.Instance.itemSlots[3]].itemDamage);
        Debug.Log("Current HP: " + progressBar.value);
    }



    void FixedUpdate()
    {
        ItemSlots = MainManager.Instance.itemSlots;
        availableItems = MainManager.Instance.availableItems;
    }
}
