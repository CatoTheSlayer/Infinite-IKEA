using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class mision_colectoin : MonoBehaviour
{
    public GameObject coin;
    public GameObject hatch;
    public Transform flor;
    public Transform objects;
    public float CoinHigt = 1f;
    public List<Transform> randObj = new List<Transform>();
    public List<Transform> children = new List<Transform>();
    public PlayerController playerController;
    public TextMeshProUGUI QuestUI;
    public TextMeshProUGUI hint;
    public int misoin = 0;
    public Animator Ani;
    public bool pressF = false;


    private int CoinAmount = 0;
    private bool HatchSpawnnig = true;
    void Start()
    {
        switch (misoin)
        {
            case 1:
            Colection();
                break;

            case 2:
                int levelChoser = Random.Range(0, 2);
                if (levelChoser == 0 && SceneManager.GetActiveScene().name == "First Level") { Eskape(); }
                if (levelChoser == 1 && SceneManager.GetActiveScene().name == "SecondLevel") { Eskape(); }
                  break;
        }
    }

    void Eskape()
    {

        while (HatchSpawnnig == true) 
        {
            Collider Fcol = flor.GetComponent<Collider>();
            Collider Hcol = hatch.GetComponent<Collider>();

            float topY = Fcol.bounds.max.y;//Ask: Bestem det højste punkt på colidern på y aksen
            float randXindex = Random.Range(Fcol.bounds.min.x, Fcol.bounds.max.x);
            float randZindex = Random.Range(Fcol.bounds.min.z, Fcol.bounds.max.z);

            Vector3 spawnPos = new Vector3 (randXindex,topY,randZindex);
            //Ask: Laver en ny vektor 3 til vores instantiate

            if (!Physics.CheckSphere(spawnPos + Vector3.up * 1f, 0.5f))
            {
                Instantiate(hatch, spawnPos, Quaternion.identity);
                QuestUI.text = "locate the hatch and escape the ikea";
                HatchSpawnnig = false;
            }
        }
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
                );//Ask: laver en ny vektor 3 til vores instantiate

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

        RaycastHit hit;
        Ray ray = playerController.playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Coin")|| objectHit.CompareTag("flor"))
            {
                hint.text = "press F";
                if (pressF == true && objectHit.CompareTag("Coin"))
                {
                    Destroy(objectHit.gameObject);
                    CoinAmount++;
                    QuestUI.text = "Coleck coins:" + CoinAmount + "/10";
                    pressF = false;
                    if (CoinAmount == 10)
                    {
                        SceneManager.LoadScene("StartMenu");
                    }
                }

                if (pressF == true && objectHit.CompareTag("flor"))
                {
                    Ani.SetTrigger("exit");
                    float tim1 = Time.deltaTime;
                    if (tim1-Time.deltaTime >= 0.3)
                    {
                        SceneManager.LoadScene("StartMenu");
                    }
                }

            }
            if (objectHit.CompareTag("Untagged"))
            {
                hint.text = "";
            }
        }
    }
}
