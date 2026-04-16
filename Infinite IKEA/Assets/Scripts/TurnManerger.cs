using System.Collections;
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

        if (playerHealthBar.value <= 0)
        {
            Debug.Log("Player defeated!");
            SceneManager.LoadScene("StartMenu"); // Load defeat screen when the player is defeated
        }
        // Enabel at spilleren kan gøre ting.
    }

    public void PlayerTurnEnd()
    {
        // Called by UI Ends player turn
        isPlayerTurn = false;
        Debug.Log($"Is player turn {isPlayerTurn}");

        StartCoroutine(PlayerTurnEndCoroutine());
    }

    private IEnumerator PlayerTurnEndCoroutine()
    {
        if (enemyHealthBar.value <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Boss").Length > 0)
            {
                Debug.Log("Boss defeated!");
                FindFirstObjectByType<BossController>().enemyDeath();
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("StartMenu");
                yield break;
            }
            else
            {
                Debug.Log("Enemy defeated!");
                enemyController.enemyDeath();
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("StartMenu");
                yield break;
            }
        }

        if (enemyHealthBar.value > 0)
        {
            EnemyTurnStart();
        }
    }

    private void EnemyTurnStart()
    {
        StartCoroutine(EnemyTurnCoroutine());
    }

    private IEnumerator EnemyTurnCoroutine()
    {
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("Enemy turn started");
        enemyController.EnemyTurn();
        PlayerTurnStart();
    }


}
