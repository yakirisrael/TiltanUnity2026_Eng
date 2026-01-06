using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI PlayerHealthTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealth(100);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHealth(int HP)
    {
        PlayerHealthTxt.text = "Health: " + HP;
    }
}
