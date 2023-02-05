using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private GameObject victoryPanel;

    private void Awake()
    {
        _instance = this;
    }


    public void DoWin()
    {
        Debug.Log("Ganaste");
        Time.timeScale = 0;
        victoryPanel.SetActive(true);
    }
}