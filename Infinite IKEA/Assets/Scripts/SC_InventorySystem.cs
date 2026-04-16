using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SC_InventorySystem : MonoBehaviour
{
    public Texture crosshairTexture;
    public PlayerController playerController;
    public SC_PickItem[] availableItems; //List with Prefabs of all the available items
    public SC_PickItem[] foodItems; //List with Prefabs of all the available items
    public SC_PickItem[] weaponItems; //List with Prefabs of all the available items

    //Available items slots
    int[] itemSlots = new int[12];
    bool showInventory = false;
    float windowAnimation = 1;
    float animationTimer = 0;
    Vector2 mousePosition;
    int[] WeaponSlots = new int[4];
    int[] FoodSlots = new int[4];


    //UI Drag & Drop
    public int hoveringOverIndex = -1;
    public int itemIndexToDrag = -1;
    Vector2 dragOffset = Vector2.zero;

    //Item Pick up
    SC_PickItem detectedItem;
    int detectedItemIndex;

    //Coin Pick up
    CoinItem detectedCoin;
    int detectedCoinIndex;

    //Vent interaction
    Vent detectedVent;
    internal bool combatIsStarted = false;
    public int CoinAmount = 0;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = -1;
        }
        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            WeaponSlots[i] = -1;
        }
        for (int i = 0; i < FoodSlots.Length; i++)
        {
            FoodSlots[i] = -1;
        }

    }
    void Start()
    {
        MainManager.Instance.itemSlots = itemSlots; // Link the inventory system's item slots to the main manager's item slots for global access
        MainManager.Instance.availableItems = availableItems; // Link the inventory system's available items to the main manager's available items for global accesss
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interact();
        }
    }

    public void Inventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            showInventory = !showInventory;
            animationTimer = 0;

            if (showInventory)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void Mouseclick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ItemDrag();
        }
    }
    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        //Show/Hide inventory

        if (animationTimer < 1)
        {
            animationTimer += Time.deltaTime;
        }

        if (showInventory)
        {
            windowAnimation = Mathf.Lerp(windowAnimation, 0, animationTimer);
            playerController.canMove = false;
        }
        else
        {
            windowAnimation = Mathf.Lerp(windowAnimation, 1f, animationTimer);
            playerController.canMove = true;
        }
    }
    void FixedUpdate()
    {
        //Detect if the Player is looking at any item
        RaycastHit hit;
        Ray ray = playerController.playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Respawn"))
            {
                if ((detectedItem == null || detectedItem.transform != objectHit) && objectHit.GetComponent<SC_PickItem>() != null)
                {
                    SC_PickItem itemTmp = objectHit.GetComponent<SC_PickItem>();

                    //Check if item is in availableItemsList
                    for (int i = 0; i < availableItems.Length; i++)
                    {
                        if (availableItems[i].itemName == itemTmp.itemName)
                        {
                            detectedItem = itemTmp;
                            detectedItemIndex = i;
                        }
                    }
                }
            }
            else
            {
                detectedItem = null;
            }
        }
        else
        {
            detectedItem = null;
        }

        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Coin"))
            {
                if ((detectedCoin == null || detectedCoin.transform != objectHit) && objectHit.GetComponent<CoinItem>() != null)
                {
                    detectedCoin = objectHit.GetComponent<CoinItem>();
                    Debug.Log("Coin Detected");
                    detectedCoinIndex ++; 
                }
            }
            else
            {
                detectedCoin = null;
            }
        }
        else
        {
            detectedCoin = null;
        }
                
                
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Vent"))
            {
                if ((detectedVent == null || detectedVent.transform != objectHit) && objectHit.GetComponent<Vent>() != null)
                {
                    detectedVent = objectHit.GetComponent<Vent>();
                    Debug.Log("Vent Detected");
                }
            }
            else
            {
                detectedVent = null;
            }
        }
        else
        {
            detectedVent = null;
        }
        if (CoinAmount == 5)
        {
            CoinAmount = 0;
            SceneManager.LoadScene("CombatScene");
            combatIsStarted = true;
        }
    }

            void interact()
            {
                if (detectedItem && detectedItemIndex > -1)
                {

                    //Add the item to inventory
                    int slotToAddTo = -1;
                    for (int i = 0; i < itemSlots.Length; i++)
                    {
                        if (itemSlots[i] == -1)
                        {
                            slotToAddTo = i;
                            break;
                        }
                    }
                    if (slotToAddTo > -1)
                    {
                        itemSlots[slotToAddTo] = detectedItemIndex;
                        detectedItem.PickItem();
                    }
                }
                if (detectedCoin && detectedCoinIndex > -1)
                {
                    CoinAmount++;
                    detectedCoin.PickCoin();
                }
                if (detectedVent)
                {
                    SceneManager.LoadScene("CombatScene");
                    combatIsStarted = true;
                }
            }
            void ItemDrag()
            {
                if (hoveringOverIndex > -1 && itemSlots[hoveringOverIndex] > -1)
                {
                    itemIndexToDrag = hoveringOverIndex;
                }
                else if (itemIndexToDrag > -1)
                {
                    if (hoveringOverIndex < 0)
                    {
                        //Drop the item outside
                        Instantiate(availableItems[itemSlots[itemIndexToDrag]], playerController.playerCamera.transform.position + playerController.playerCamera.transform.forward * 5f, Quaternion.identity);
                        itemSlots[itemIndexToDrag] = -1;
                    }
                    else
                    {
                        //Switch items between the selected slot and the one we are hovering on
                        int itemIndexTmp = itemSlots[itemIndexToDrag];
                        itemSlots[itemIndexToDrag] = itemSlots[hoveringOverIndex];
                        itemSlots[hoveringOverIndex] = itemIndexTmp;

                    }
                    itemIndexToDrag = -1;
                }
            }
            void OnGUI()
            {
                //Inventory UI
                GUI.Label(new Rect(5, 5, 200, 25), "Press 'Tab' to open Inventory");

                //Inventory window
                if (windowAnimation < 1)
                {
                    GUILayout.BeginArea(new Rect(10 - (430 * windowAnimation), Screen.height / 2 - 200, 302, 430), GUI.skin.GetStyle("box"));

                    GUILayout.Label("Inventory", GUILayout.Height(25));

                    GUILayout.BeginVertical();
                    for (int i = 0; i < itemSlots.Length; i += 3)
                    {
                        GUILayout.BeginHorizontal();
                        //Display 3 items in a row
                        for (int a = 0; a < 3; a++)
                        {
                            if (i + a < itemSlots.Length)
                            {
                                if (itemIndexToDrag == i + a || (itemIndexToDrag > -1 && hoveringOverIndex == i + a))
                                {
                                    GUI.enabled = false;
                                }

                                if (itemSlots[i + a] > -1)
                                {
                                    if (availableItems[itemSlots[i + a]].itemPreview)
                                    {
                                        GUILayout.Box(availableItems[itemSlots[i + a]].itemPreview, GUILayout.Width(95), GUILayout.Height(95));
                                    }
                                    else
                                    {
                                        GUILayout.Box(availableItems[itemSlots[i + a]].itemName, GUILayout.Width(95), GUILayout.Height(95));
                                    }
                                }
                                else
                                {
                                    //Empty slot
                                    GUILayout.Box("", GUILayout.Width(95), GUILayout.Height(95));
                                }

                                //Detect if the mouse cursor is hovering over item
                                Rect lastRect = GUILayoutUtility.GetLastRect();
                                Vector2 eventMousePositon = Event.current.mousePosition;
                                if (Event.current.type == EventType.Repaint && lastRect.Contains(eventMousePositon))
                                {
                                    hoveringOverIndex = i + a;
                                    if (itemIndexToDrag < 0)
                                    {
                                        dragOffset = new Vector2(lastRect.x - eventMousePositon.x, lastRect.y - eventMousePositon.y);
                                    }
                                }

                                GUI.enabled = true;
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();

                    if (Event.current.type == EventType.Repaint && !GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                    {
                        hoveringOverIndex = -1;
                    }

                    GUILayout.EndArea();
                }

                //Item dragging
                if (itemIndexToDrag > -1)
                {
                    if (availableItems[itemSlots[itemIndexToDrag]].itemPreview)
                    {
                        GUI.Box(new Rect(mousePosition.x + dragOffset.x, Screen.height - mousePosition.y + dragOffset.y, 95, 95), availableItems[itemSlots[itemIndexToDrag]].itemPreview);
                    }
                    else
                    {
                        GUI.Box(new Rect(mousePosition.x + dragOffset.x, Screen.height - mousePosition.y + dragOffset.y, 95, 95), availableItems[itemSlots[itemIndexToDrag]].itemName);
                    }
                }

                //Display item name when hovering over it
                if (hoveringOverIndex > -1 && itemSlots[hoveringOverIndex] > -1 && itemIndexToDrag < 0)
                {
                    GUI.Box(new Rect(mousePosition.x, Screen.height - mousePosition.y - 30, 100, 25), availableItems[itemSlots[hoveringOverIndex]].itemName);
                }

                if (!showInventory)
                {
                    //Player crosshair
                    GUI.color = detectedItem ? Color.green : Color.white;
                    GUI.DrawTexture(new Rect(Screen.width / 2 - 4, Screen.height / 2 - 4, 8, 8), crosshairTexture);
                    GUI.color = Color.white;

                    //Pick up message
                    if (detectedItem)
                    {
                        GUI.color = new Color(0, 0, 0, 0.84f);
                        GUI.Label(new Rect(Screen.width / 2 - 75 + 1, Screen.height / 2 - 50 + 1, 150, 20), "Press 'F' to pick '" + detectedItem.itemName + "'");
                        GUI.color = Color.green;
                        GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 20), "Press 'F' to pick '" + detectedItem.itemName + "'");
                    }
                }
            }
        }