using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

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
    public void Presentation(GameObject Menu, Image[] presentation)
    {
        Image[] Images =Menu.GetComponentsInChildren<Image>();
        foreach (var image in Images)
        {
            image.DOColor(new Color(1,1,1,0),1f);
        }
    }
}
