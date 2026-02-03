using UnityEngine;

public class HotspotDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    bool IsHover(out string hoveredName)
    {
        hoveredName = "";
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosWorld, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Hotspot"));
        if (hit.collider == null)
        {
            return false;
        }
        
        hoveredName = hit.collider.name;
        return (hit.collider != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHover(out string hoveredName))
        {
            Debug.Log("hovered Name: " + hoveredName);
        }
    }
}
