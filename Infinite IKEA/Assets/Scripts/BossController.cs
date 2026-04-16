using UnityEngine;
using UnityEngine.UIElements;
public class BossController : MonoBehaviour
{
    [SerializeField] private UIDocument _HPbarUIDokument;
    [SerializeField] private Animator animator;


    private TurnManager turnManager;
    int actions; // Example actions for the enemy
    ProgressBar enemyHealthBar;
    ProgressBar playerHealthBar;
    PlayerAnimController playerAnimController;
    void Awake()
    {
        enemyHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("EnemyHp");
        playerHealthBar = _HPbarUIDokument.rootVisualElement.Q<ProgressBar>("PlayerHp");
        playerAnimController = FindFirstObjectByType<PlayerAnimController>();
    }
    public void EnemyTurn()
    {
        Debug.Log("Enemy's turn!");
        // Implement enemy actions here
        actions = Random.Range(1, 4); // Randomly choose an action for the enemy
        switch (actions) // Randomly choose an action for the enemy
        {
            case 1:
                Debug.Log("Enemy attacks!");
                AttackAnimation0();
                playerAnimController.playHurtAnimation();
                playerHealthBar.value -= 10; // Example of dealing damage to the player
                break;
            case 2:
                Debug.Log("Enemy deals large damage");
                AttackAnimation1();
                playerAnimController.playHurtAnimation();
                playerHealthBar.value -= 20; // Example of dealing damage to the player
                break;
            case 3:
                Debug.Log("Enemy Heals");
                enemyheal();
                enemyHealthBar.value += 30; // Example of healing the enemy
                break;
        }
    }
    private void AttackAnimation0()
    {
        animator.SetTrigger("Attack0");
        Debug.Log("Playing enemy attack animation!");
    }
    private void AttackAnimation1()
    {
        animator.SetTrigger("Attack1");
        Debug.Log("Playing enemy attack animation!");
    }
    internal void enemyDeath()
    {
        animator.SetTrigger("Death");
        Debug.Log("Playing enemy death animation!");
    }  
    internal void enemyheal()
    {
        animator.SetTrigger("Heal");
        Debug.Log("Playing enemy heal animation!");
    }
}
