using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    Transform target , rotateObject;

    public bool ContraintX;
    public float xMin { get { return m_XMin; } set { m_XMin = value; } }
    [SerializeField]
    float m_XMin = -45f;

    public float xMax { get { return m_XMax; } set { m_XMax = value; } }
    [SerializeField]
    float m_XMax = 45f;

    public bool ContraintY;
    public float yMin { get { return m_YMin; } set { m_YMin = value; } }
    [SerializeField]
    float m_YMin = 45f;

    public float yMax { get { return m_YMax; } set { m_YMax = value; } }
    [SerializeField]
    float m_YMax = 45f;

    public bool ContraintZ;
    public float zMin { get { return m_ZMin; } set { m_ZMin = value; } }
    [SerializeField]
    float m_ZMin = 45f;

    public float zMax { get { return m_ZMax; } set { m_ZMax = value; } }
    [SerializeField]
    float m_ZMax = 45f;

    private void Update()
    {
        RotateObject(target.position);
    }

    void RotateObject(Vector3 handPos)
    {
        Vector3 direction = handPos - rotateObject.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        rotateObject.rotation = rotation;

        ApplyConstraintRotation();
    }

    void ApplyConstraintRotation()
    {
        float XConstaints = rotateObject.eulerAngles.x;
        float YConstaints = rotateObject.eulerAngles.y;
        float ZConstaints = rotateObject.eulerAngles.z;


        if (XConstaints > 180)
        {
            XConstaints -= 360;
        }
        if (YConstaints > 180)
        {
            YConstaints -= 360;
        }
        if (ZConstaints > 180)
        {
            ZConstaints -= 360;
        }

        Debug.Log("Current angle : " + XConstaints);


        if (XConstaints < xMin)
        {
            
            XConstaints = xMin;
            Debug.Log("this constraint = " + XConstaints);
        }
        if (XConstaints > xMax)
        {
            Debug.Log("new angle = " + XConstaints);
            XConstaints = xMax;
        }

        if (YConstaints < yMin)
        {
            YConstaints = 360 + yMin;
        }

        if (YConstaints > yMax)
        {
            YConstaints = yMax;
        }

        if (ZConstaints < zMin)
        {
            ZConstaints = 360 + zMin;
        }

        if (ZConstaints > zMax)
        {
            ZConstaints = zMax;
        }

        Debug.Log("Constarints angles : "+XConstaints);
        

        rotateObject.rotation = Quaternion.Euler(XConstaints , YConstaints , ZConstaints);
    }
}

