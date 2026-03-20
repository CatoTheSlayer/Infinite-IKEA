using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] players;
    private int currentPlayerIndex = 0;

    void Start()
    {
        StartTurn();
    }

    void StartTurn()
    {
        // Activate current player
        players[currentPlayerIndex].SetActive(true);
    }

    public void EndTurn()
    {
        // Deactivate current player
        players[currentPlayerIndex].SetActive(false);
        
        // Move to next player
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        
        // Start next turn
        StartTurn();
    }
}