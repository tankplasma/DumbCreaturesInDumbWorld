using UnityEngine;

public class RainbowColor : MonoBehaviour
{
    public float speed = 0.1f;

    public Material material;
    private float hue = 0.0f;

    void Update()
    {
        hue += Time.deltaTime * speed;
        if (hue > 1.0f)
        {
            hue -= 1.0f; 
        }

        Color color = Color.HSVToRGB(hue, 1.0f, 1.0f);
        material.color = color;
    }
}
