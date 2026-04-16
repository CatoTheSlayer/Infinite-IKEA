using UnityEngine;

public class Vent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    internal void VentAni()
    {
        animator.SetTrigger("exit");
    }
}
