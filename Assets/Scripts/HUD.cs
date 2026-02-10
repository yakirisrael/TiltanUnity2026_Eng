using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI PlayerHealthTxt;

    public Slider HealthBar;

    public Image soulsImage;

    public int soulsNum = 3;
    int soulSize = 200;
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

    public void UpdateSoulsWidth()
    {
        RectTransform tm = soulsImage.rectTransform;
        //tm.sizeDelta = new Vector2(100, tm.sizeDelta.y);
        tm.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tm.sizeDelta.x + (soulSize * soulsNum));
    }
}
