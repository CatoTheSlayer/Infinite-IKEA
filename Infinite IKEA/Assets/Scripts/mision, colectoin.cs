using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class mision_colectoin : MonoBehaviour
{
    public GameObject coin;
    public Transform objects;
    public float CoinHigt = 1f;
    public List<Transform> randObj = new List<Transform>();
    public List<Transform> children = new List<Transform>();
    public PlayerController playerController;
    public TextMeshProUGUI QuestUI;
    public TextMeshProUGUI hint;
    public int misoin = 1;

    private int CoinAmount = 0;
    private bool pressF = false;

    void Start()
    {
        switch (misoin)
        {
            case 1:
            Colection();
                break;

            case 2:
                Eskape();
                break;
        }
    }

    void Eskape()
    {

    }


    void Colection()
    {
        QuestUI.text = "Colekt all furniture with a coin above it";
        foreach (Transform child in objects)
        {
            children.Add(child);//Ask: tilføj alle vores objekter som kan være med i motionen med på en liste
        }

        for (int i = 0; i <= 4; i++)
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


    }
    public void pickup(InputAction.CallbackContext context)
    {
        pressF = true;
    }

    void Update()
    {
        if (misoin == 1)
        {
            RaycastHit hit;
            Ray ray = playerController.playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Transform objectHit = hit.transform;

                if (objectHit.CompareTag("Coin"))
                {
                    hint.text = "press F";
                    if (pressF == true)
                    {
                        Destroy(objectHit.gameObject);
                        CoinAmount++;
                        QuestUI.text = "Coleck coins:" + CoinAmount + "/10";
                        pressF = false;
                    }
                }
                if (objectHit.CompareTag("Untagged"))
                {
                    hint.text = "";
                }
            }
        }
    }
}
