using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UIDocument _HPbarUIDokument;
    [SerializeField] private Animator animator;
    public AudioSource hit_sound;


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
                // Implement attack logic here
                AttackAnimation();
                playerAnimController.playHurtAnimation();
                playerHealthBar.value -= 10; // Example of dealing damage to the player
                hit_sound.Play();
                break;
            case 2:
                Debug.Log("Enemy deals medium damage");
                // Implement heal logic here
                AttackAnimation();
                playerAnimController.playHurtAnimation();
                playerHealthBar.value -= 15; // Example of dealing damage to the player
                hit_sound.Play();
                break;
            case 3:
                Debug.Log("Enemy uses a special ability!");
                // Implement special ability logic here
                AttackAnimation();
                playerAnimController.playHurtAnimation();
                playerHealthBar.value -= 20; // Example of dealing more damage to the player
                hit_sound.Play();
                break;
        }
    }
    private void AttackAnimation()
    {
        animator.SetTrigger("attackEnemy");
        Debug.Log("Playing enemy attack animation!");
    }
    internal void enemyDeath()
    {
        animator.SetTrigger("death");
        Debug.Log("Playing enemy death animation!");
    }  
    internal void enemyHurt()
    {
        animator.SetTrigger("Hurt");
        Debug.Log("Playing enemy hurt animation!");
    }

    

}
