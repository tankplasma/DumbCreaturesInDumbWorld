using UnityEngine;

public class RotateMenu : MonoBehaviour
{
    float rotationSpeed;
    bool canStart = false;

    void Start()
    {
        rotationSpeed = Random.Range(5, 15);
        Invoke("Play", Random.Range(0, 10));
    }

    void Play()
    {
        canStart = true;
    }

    void Update()
    {
        if (canStart) transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
