using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public Animator animator;
    internal void playAttackAnimation()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Playing attack animation!");
    }
    internal void playHealAnimation()
    {
        animator.SetTrigger("Eat");
        Debug.Log("Playing heal animation!");
    }
    internal void playHurtAnimation()
    {
        animator.SetTrigger("isDamaged");
        Debug.Log("Playing damage animation!");
    }
}
