using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UIDocument _HPbarUIDokument;

    private TurnManager turnManager;
    int actions; // Example actions for the enemy
    ProgressBar enemyHealthBar;
    ProgressBar playerHealthBar;
    void Awake()
    {
        enemyHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("EnemyHp");
        playerHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("PlayerHp");
    }
    public void EnemyTurn()
    {
        Debug.Log("Enemy's turn!");
        // Implement enemy actions here
        actions = Random.Range(1, 3); // Randomly choose an action for the enemy
        switch (actions) // Randomly choose an action for the enemy
        {
            case 1:
                Debug.Log("Enemy attacks!");
                // Implement attack logic here
                playerHealthBar.value -= 10; // Example of dealing damage to the player
                break;
            case 2:
                Debug.Log("Enemy heals!");
                // Implement heal logic here
                enemyHealthBar.value += 20; // Example of healing the enemy
                break;
            case 3:
                Debug.Log("Enemy uses a special ability!");
                // Implement special ability logic here
                playerHealthBar.value -= 20; // Example of dealing more damage to the player
                break;
        }
    }
}
