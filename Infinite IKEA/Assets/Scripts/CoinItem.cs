using UnityEngine;
using UnityEngine.Audio;

public class CoinItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource lyd;
    void Start()
    {
        gameObject.tag = "Coin";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickCoin()
    {
        Destroy(gameObject);
       lyd.Play();
    }
}
