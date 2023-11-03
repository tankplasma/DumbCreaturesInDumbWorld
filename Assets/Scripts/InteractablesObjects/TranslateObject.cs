using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class TranslateObject : MonoBehaviour, IMovable
{
    [SerializeField]
    float translateMaxAmount = 5f;

    [SerializeField]
    Transform movingPart;

    Vector3 initialHandPosition = Vector3.zero;

    Vector3 initialPos = Vector3.zero;

    private void Start()
    {
        initialPos = movingPart.position;
    }

    public void ProcessMoving(Vector3 position)
    {
        Process(position);
    }

    void Process(Vector3 position)
    {
        Vector3 newPos = CalculFinalPoint(position);

        // Calculez la différence entre la nouvelle position et la position initiale
        Vector3 positionDifference = newPos - initialHandPosition;

        if (initialHandPosition != Vector3.zero)
        {
            // Appliquez cette différence à la position de movingPart
            movingPart.position += positionDifference;
        }

        initialHandPosition = newPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (movingPart)
        {
            Gizmos.DrawLine(transform.position, GetFinalPointTranslateWay());
        }

    }

    Vector3 CalculFinalPoint(Vector3 targetPoint)
    {
        Vector3 finalPoint = GetFinalPointTranslateWay();

        // Trouvez le vecteur du segment AB , celui de translation
        Vector3 translateDir = finalPoint - transform.position;

        // Trouvez le vecteur AP
        Vector3 ap = targetPoint - transform.position;

        // Utilisez la projection vectorielle pour trouver la composante perpendiculaire
        Vector3 perp = Vector3.Project(ap, translateDir);

        // Trouvez le point d'intersection en ajoutant perp à pointA
        Vector3 intersectionPoint = transform.position + perp;
        
        // Calculez le vecteur de déplacement d'intersection par rapport à A et B
        Vector3 displacementFromA = intersectionPoint - initialPos;
        Vector3 displacementFromB = intersectionPoint - finalPoint;

        if (Vector3.Dot(displacementFromA, translateDir) > 0 && Vector3.Dot(displacementFromB, -translateDir) > 0)
        {
            return intersectionPoint;
        }

        // Vérifiez si le point d'intersection dépasse le point A
        else if (Vector3.Dot(displacementFromA, -translateDir) > 0)
        {
            Debug.Log(transform.position);
            return initialPos;
        }

        // Vérifiez si le point d'intersection dépasse le point B
        else if (Vector3.Dot(displacementFromB, translateDir) > 0)
        {
            return finalPoint;
        }

        return finalPoint;
    }

    public Vector3 GetFinalPointTranslateWay()
    {
        Vector3 dir = (movingPart.position - transform.position).normalized;
        return transform.position + (dir * translateMaxAmount);
    }

    public void Select(Vector3 pos)
    {
        //initialPosition = pos;
    }

    public void unselect()
    {
        initialHandPosition = Vector3.zero;
    }
}
