using System.Collections.Generic;
using UnityEngine;

public class mision_colectoin : MonoBehaviour
{
    public GameObject coin;
    public Transform objects;
    public float CoinHigt = 1f;
    public List<Transform> randObj = new List<Transform>();
    public List<Transform> children = new List<Transform>();
    public PlayerController playerController;
    void Start()
    {
        Colection();
    }

    void Colection()
    {
        foreach (Transform child in objects)
        {
            children.Add(child);//Ask: tilføj alle vores objekter som kan være med i motionen med på en liste
        }

        for (int i = 0; i <= 4 ; i++ )
        {
            int randIndex = Random.Range(0, children.Count);
            randObj.Add(children[randIndex]);//Ask: vælg 5 random objekts og put dem ind i en anden liste
        }

        foreach (Transform obj in randObj)
        {
            Collider col = obj.GetComponent<Collider>();

            if (col != null)//Ask: hvis objet har en colider så vil den sette coinen med coin higt højde
            {
                float topY = col.bounds.max.y;//Ask: Bestem det højste punkt på colidern på y aksen

                Vector3 spawnPos = new Vector3
                (
                obj.position.x,
                topY + CoinHigt,
                obj.position.z
                );//Ask: Lan en ny vektor 3 til vores instantiate

                Instantiate(coin, spawnPos, Quaternion.identity);
            }
            else
            {
                Vector3 spawnPos = obj.position + Vector3.up * (CoinHigt + 2f);//Ask: bare gang coin heigt direkte på
                Instantiate(coin, spawnPos, Quaternion.identity);
            }

        }

        RaycastHit hit;
        Ray ray = playerController.playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Coin") && Input.GetKeyDown(KeyCode.F))
            {
                Destroy(objectHit.gameObject);
            }
        }
    }
}
