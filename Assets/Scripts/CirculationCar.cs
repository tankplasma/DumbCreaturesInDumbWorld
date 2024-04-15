using Unity.VisualScripting;
using UnityEngine;

public class CirculationCar : MonoBehaviour
{
    public GameObject[] car;
    public Light[] lights;
    GameObject currentCar;
    public Transform startPoint;
    public Transform endPoint;
    public Transform stopPoint;

    public float moveSpeed = 5f;
    public float acceleration = 2f;
    public float maxSpeed = 10f;
    Rigidbody rb;
    bool carCanMove = true;
    bool carIsSpawn = false;

    void Start()
    {
        Invoke("SpawnCar", Random.Range(0f, 5f));
    }

    void SpawnCar()
    {
        currentCar = Instantiate(car[Random.Range(0, car.Length)], startPoint.position, startPoint.rotation);
        currentCar.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        rb = currentCar.GetComponent<Rigidbody>();
        carIsSpawn = true;
    }

    [ContextMenu("EnabledCar")]
    void test1()
    {
        EnabledCirculation(true);
    }

    [ContextMenu("DisabledCar")]
    void test2()
    {
        EnabledCirculation(false);
    }

    public void EnabledCirculation(bool state)
    {
        if (state)
        {
            carCanMove = true;
            foreach (Light lightComp in lights)
            {
                lightComp.color = Color.green;
            }
        }
        else
        {
            carCanMove = false;
            foreach (Light lightComp in lights)
            {
                lightComp.color = Color.red;
            }
        }
    }

    void Update()
    {
        if (carIsSpawn)
        {
            if (carCanMove)
            {
                Vector3 direction = (endPoint.position - currentCar.transform.position).normalized;
                rb.MovePosition(currentCar.transform.position + direction * Time.deltaTime * maxSpeed);
            }
            else
            {
                if (IsNotToLate())
                {
                    Vector3 direction = (stopPoint.position - currentCar.transform.position).normalized;
                    float distanceToStopPoint = Vector3.Distance(currentCar.transform.position, stopPoint.position);
                    float decelerationFactor = Mathf.Clamp01(distanceToStopPoint);

                    float currentSpeed = maxSpeed * decelerationFactor;
                    rb.MovePosition(currentCar.transform.position + direction * Time.deltaTime * currentSpeed);
                }
                else
                {
                    Vector3 direction = (endPoint.position - currentCar.transform.position).normalized;
                    rb.MovePosition(currentCar.transform.position + direction * Time.deltaTime * maxSpeed);
                }
            }

            if (currentCar.transform.position.y < -5)
            {
                Destroy(currentCar);
                carIsSpawn = false;
                Invoke("SpawnCar", Random.Range(0f, 2f));
            }
        }
    }

    bool IsNotToLate()
    {
        float dist1 = Vector3.Distance(stopPoint.position, endPoint.position);
        float dist2 = Vector3.Distance(currentCar.transform.position, endPoint.position);
        return dist2 > dist1;
    }
}
