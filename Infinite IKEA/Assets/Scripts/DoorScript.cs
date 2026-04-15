using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DoorScript : MonoBehaviour
{
    public string DoorExit;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("SecondLevel");
        }
    }
}
