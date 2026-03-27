using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance;
    public int[] itemSlots = new int[12]; // Example inventory slots, can be modified as needed
    public SC_PickItem[] availableItems; //List with Prefabs of all the available items


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
