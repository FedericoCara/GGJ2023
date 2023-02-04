using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
