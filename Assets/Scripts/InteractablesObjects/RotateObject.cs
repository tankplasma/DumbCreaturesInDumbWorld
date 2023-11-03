using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public enum RotationType
{
    DiffRotation,
    lookRotation,
    inverseRotation
}

public class RotateObject : MonoBehaviour , IMovable
{
    [SerializeField]
    RotationType rotationType;

    [SerializeField]
    Transform rotateObject;

    Quaternion LastRotation;


    [SerializeField]
    bool inverseRotation;

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

    private void Start()
    {
        LastRotation = Quaternion.identity;
    }

    void ProcessRotationWithDiff(Vector3 handPos)
    {
        if(LastRotation != Quaternion.identity)
            rotateObject.rotation *= Quaternion.Inverse(GetRotationDiff(handPos));
        
        LastRotation = GetNewRotation(handPos);
        ApplyConstraintRotation();
    }

    void SimpleInverseRotation(Vector3 handPos)
    {
        Vector3 direction = rotateObject.position - handPos;
        Quaternion rotation = Quaternion.LookRotation(direction);

        rotateObject.rotation = rotation;

        ApplyConstraintRotation();
    }

    void SimpleRotate(Vector3 handPos)
    {
        rotateObject.rotation = GetNewRotation(handPos);
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

        if (XConstaints < xMin)
        {

            XConstaints = xMin;
        }
        if (XConstaints > xMax)
        {
            XConstaints = xMax;
        }

        if (YConstaints < yMin)
        {
            YConstaints = yMin;
        }

        if (YConstaints > yMax)
        {
            YConstaints = yMax;
        }

        if (ZConstaints < zMin)
        {
            ZConstaints = zMin;
        }

        if (ZConstaints > zMax)
        {
            ZConstaints = zMax;
        }

        rotateObject.rotation = Quaternion.Euler(XConstaints, YConstaints, ZConstaints);
    }

    Quaternion GetNewRotation(Vector3 handPos)
    {
        Vector3 direction = handPos - rotateObject.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        return rotation;
    }

    Quaternion GetRotationDiff(Vector3 position)
    {
        Quaternion newRotation =  GetNewRotation(position);

        Quaternion diff = LastRotation * Quaternion.Inverse(newRotation);

        return diff;
    }

    public void ProcessMoving(Vector3 position)
    {
        switch (rotationType)
        {
            case RotationType.DiffRotation:
                ProcessRotationWithDiff(position);
                break;
            case RotationType.lookRotation:
                SimpleRotate(position);
                break;
            case RotationType.inverseRotation:
                SimpleInverseRotation(position);
                break;
            default:
                break;
        }   
    }

    public void Select(Vector3 pos)
    {
        
    }

    public void unselect()
    {
        LastRotation = Quaternion.identity;
    }
}
