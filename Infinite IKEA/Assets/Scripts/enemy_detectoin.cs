using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy_detectoin : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("CombatScene");
            gameObject.SetActive(false);
        }
    }
}
