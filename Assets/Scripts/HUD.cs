using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI PlayerHealthTxt;

    public Slider HealthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHealth(int HP, int MaxHP)
    {
        PlayerHealthTxt.text = "Health: " + HP;
        
        float percentage = HP / (float)MaxHP;
        HealthBar.value = percentage;
    }
}
