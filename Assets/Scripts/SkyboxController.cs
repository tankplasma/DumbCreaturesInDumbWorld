using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material skybox;
    public Color color;

    void Update()
    {
        skybox.SetColor("_Tint", color);
    }
}
