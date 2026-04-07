using UnityEngine;

public class mision_colectoin : MonoBehaviour
{
    public GameObject coin;
    public Transform objects;
    public float CoinHigt = 2f;
    void Start()
    {
        Colection();
    }

    void Colection()
    {
        foreach (Transform child in objects)
        {
            if (Random.value > 0.5f) // 50% chance
            {
                Vector3 spawnPos = child.position + Vector3.up * CoinHigt;
                Instantiate(coin, spawnPos, Quaternion.identity);
            }
        }
    }
}
