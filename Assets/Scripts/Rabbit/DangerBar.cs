using UnityEngine;
using UnityEngine.UI;

public class DangerBar : MonoBehaviour
{
    [SerializeField] private RabbitController rabbit;
    [SerializeField] private GameObject barContainer;
    [SerializeField] private Image barImg; 
    
    private void Update()
    {
        barContainer.SetActive(rabbit.AlertPercentage > 0);
        barImg.fillAmount = rabbit.AlertPercentage;
    }
}
