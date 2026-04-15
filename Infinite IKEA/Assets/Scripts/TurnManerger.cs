using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class TurnManager : MonoBehaviour
{

    // Liste af spiller karakterer
    private List<GameObject> playerUnits = new List<GameObject>();
    [SerializeField] 
    private UIDocument _HPbarUIDokument;


    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private EnemyController enemyController;

    public int turnCounter = 1;

    public bool isPlayerTurn = true;
    [SerializeField]
    private TextMeshProUGUI turnCounterDisplay;

    ProgressBar enemyHealthBar;
    ProgressBar playerHealthBar;
    void Awake()
    {
        playerUnits.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        enemyHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("EnemyHp");
        playerHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("PlayerHp");
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None; // Unlock the cursor for UI interaction
        UnityEngine.Cursor.visible = true; // Make the cursor visible

    }

    private void PlayerTurnStart()
    {
        isPlayerTurn = true;
        turnCounter++;
        turnCounterDisplay.text = $"Turn: {turnCounter}";

        Debug.Log($"Is player turn {isPlayerTurn}");

        if (playerHealthBar.value <= 0)
        {
            Debug.Log("Player defeated!");
        }
        // Enabel at spilleren kan gøre ting, og reset stats som movement og actions
    }

    public void PlayerTurnEnd()
    {
        // Called by UI Ends player turn
        isPlayerTurn = false;
        Debug.Log($"Is player turn {isPlayerTurn}");

        if (enemyHealthBar.value <= 0)
        {
            Debug.Log("Enemy defeated!");
            enemyController.enemyDeath();
            //SceneManager.LoadScene("StartMenu"); // Load victory screen when the enemy is defeated
            // Implement logic for when the enemy is defeated, such as ending the combat or transitioning to a victory screen
            return; // Exit the method if the enemy is defeated
        }
        //PlayerTurnStart(); // temp
        if (enemyHealthBar.value > 0) // Only start the enemy's turn if they are still alive
        {
            EnemyTurnStart();
        }
    }

    private void EnemyTurnStart()
    {
        // Disable at spilleren kan gøre ting, og flytte på enemies, angribe med enemies og tager ande actions.
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(3f); // Wait for 1 second before starting the enemy's turn
        Debug.Log("Enemy turn started");
        enemyController.EnemyTurn();

        PlayerTurnStart(); // temp
    }


}
