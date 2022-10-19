
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image maxHealthBar;
    [SerializeField] private Image currentHealthbar;

    private void Start()
    {
        maxHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

}
