using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

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
    private bool enemyisAlive = true;

    [SerializeField]
    private TextMeshProUGUI turnCounterDisplay;

    ProgressBar enemyHealthBar;
    ProgressBar playerHealthBar;
    void Awake()
    {
        playerUnits.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        enemyHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("EnemyHp");
        playerHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("PlayerHp");
    }

    private void EnableDeathScreen()
    {
        int counter = 0;
        foreach (GameObject player in playerUnits)
        {
            /*if (!player.GetComponent<Unit>().isActiveAndEnabled)// somehow check if player is alive
            {
                counter++;
            }*/
        }

        if (counter == playerUnits.Count)
        {
            //sdeathScreen.SetActive(true);
        }
    }

    private void PlayerTurnStart()
    {
        EnableDeathScreen();
        isPlayerTurn = true;
        turnCounter++;
        turnCounterDisplay.text = $"Turn: {turnCounter}";

        Debug.Log($"Is player turn {isPlayerTurn}");

        if (playerHealthBar.value <= 0)
        {
            EnableDeathScreen();
            Debug.Log("Player defeated!");
        }
        if (!enemyisAlive)
        {
            Debug.Log("Enemy defeated!");
            return; // Exit the method if the enemy is defeated
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
            enemyisAlive = false;
            // Implement logic for when the enemy is defeated, such as ending the combat or transitioning to a victory screen
            return; // Exit the method if the enemy is defeated
        }
        //PlayerTurnStart(); // temp
        if (enemyisAlive)
        {
            EnemyTurnStart();
        }
        else
        {
            PlayerTurnStart(); // If the enemy is already defeated, start the player's turn immediately
        }
        
    }

    private void EnemyTurnStart()
    {
        // Disable at spilleren kan gøre ting, og flytte på enemies, angribe med enemies og tager ande actions.
        Debug.Log("Enemy turn started");
        enemyController.EnemyTurn();

        PlayerTurnStart(); // temp
    }


}
