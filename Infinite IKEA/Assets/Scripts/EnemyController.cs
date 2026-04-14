using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UIDocument _HPbarUIDokument;
    [SerializeField] private Animator animator;


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
                AttackAnimation();
                playerHealthBar.value -= 10; // Example of dealing damage to the player
                break;
            case 2:
                Debug.Log("Enemy deals medium damage");
                // Implement heal logic here
                AttackAnimation();
                playerHealthBar.value -= 15; // Example of dealing damage to the player
                break;
            case 3:
                Debug.Log("Enemy uses a special ability!");
                // Implement special ability logic here
                AttackAnimation();
                playerHealthBar.value -= 20; // Example of dealing more damage to the player
                break;
        }
    }
    private void AttackAnimation()
    {
        animator.SetTrigger("attackEnemy");
        Debug.Log("Playing enemy attack animation!");
    }
    private void enemyDeath()
    {
        animator.SetTrigger("death");
        Debug.Log("Playing enemy death animation!");
    }  

    

}
