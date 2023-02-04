using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }


    public void DoWin()
    {
        Debug.Log("Ganaste");
        Time.timeScale = 0;
    }
}