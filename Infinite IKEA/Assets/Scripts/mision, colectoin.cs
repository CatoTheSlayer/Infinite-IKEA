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
            System.Random rand = new System.Random();
            int randomNumber = rand.Next(0,2);
            if (SceneManager.GetActiveScene().name == "First Level" && randomNumber == 0) { Eskape();}
            if (SceneManager.GetActiveScene().name == "SecondLevel" && randomNumber == 1) { Eskape();}
                break;
        }
    }

    public void Eskape()
    {

        while (HatchSpawnnig == true) 
        {
            Collider Fcol = flor.GetComponent<Collider>();
            Collider Hcol = hatch.GetComponent<Collider>();

            float topY = Fcol.bounds.max.y;//Ask: Bestem det h�jste punkt p� colidern p� y aksen
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

    public void Colection()
    {
        QuestUI.text = "Colekt all furniture with a coin above it";
        foreach (Transform child in objects)
        {
            children.Add(child);//Ask: tilf�j alle vores objekter som kan v�re med i motionen med p� en liste
        }

        for (int i = 0; i <= 4; i++)
        {
            int randIndex = Random.Range(0, children.Count);
            randObj.Add(children[randIndex]);//Ask: v�lg 5 random objekts og put dem ind i en anden liste
        }

        foreach (Transform obj in randObj)
        {
            Collider col = obj.GetComponent<Collider>();

            if (col != null)//Ask: hvis objet har en colider s� vil den sette coinen med coin higt h�jde
            {
                float topY = col.bounds.max.y;//Ask: Bestem det h�jste punkt p� colidern p� y aksen

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
                Vector3 spawnPos = obj.position + Vector3.up * (CoinHigt + 2f);//Ask: bare gang coin heigt direkte p�
                Instantiate(coin, spawnPos, Quaternion.identity);
            }

        }


    }

}
