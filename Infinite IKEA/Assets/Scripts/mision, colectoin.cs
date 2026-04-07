using System.Collections.Generic;
using UnityEngine;

public class mision_colectoin : MonoBehaviour
{
    public GameObject coin;
    public Transform objects;
    public float CoinHigt = 1f;
    public List<Transform> randObj = new List<Transform>();
    public List<Transform> children = new List<Transform>();
    void Start()
    {
        Colection();
    }

    void Colection()
    {
        foreach (Transform child in objects)
        {
            children.Add(child);
        }

        for (int i = 0; i <= 4 ; i++ )
        {
            int randIndex = Random.Range(0, children.Count);
            randObj.Add(children[randIndex]);
        }

        foreach (Transform obj in randObj)
        {
            Collider col = obj.GetComponent<Collider>();

            if (col != null)
            {
                float topY = col.bounds.max.y;

                Vector3 spawnPos = new Vector3
                (
                obj.position.x,
                topY + CoinHigt,
                obj.position.z
                );

                Instantiate(coin, spawnPos, Quaternion.identity);
            }
            else
            {
                Vector3 spawnPos = obj.position + Vector3.up * (CoinHigt + 2f);
                Instantiate(coin, spawnPos, Quaternion.identity);
            }
        }

    }
}
