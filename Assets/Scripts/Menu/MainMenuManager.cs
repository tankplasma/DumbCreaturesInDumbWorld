using Meta.Voice;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    Transform spawnWorldsPoint;

    [SerializeField]
    WorldButton buttonPrefab;

    ScriptableWorlds currentWorld;

    [SerializeField]
    ScrollFieldBar scrollbar;

    public UnityEvent onWorldChoose;

    [SerializeField]
    float DistanceBetweenWorlds, circleR , buttonCircleR;

    private void OnDrawGizmos()
    {
/*        List<Vector3> pos = GenerateCirclePoints(np, circleR, DistanceBetweenWorlds);

        foreach (Vector3 p in pos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + p, 0.1f);
        }*/
    }

    public void DisplayAllWorlds()
    {
        List<ScriptableWorlds> sw = GameManager.Instance.GetAllWorlds();

        List<Vector3> pos = GenerateCirclePoints(sw.Count, circleR, DistanceBetweenWorlds);
        List<Vector3> ButtonPos = GenerateCirclePoints(sw.Count, buttonCircleR, DistanceBetweenWorlds);

        for (int i = 0; i < sw.Count; i++)
        {
            DisplayWorldAtPoint(sw[i], pos[i]);

            WorldButton worldB = Instantiate(buttonPrefab , ButtonPos[i] + spawnWorldsPoint.position , Quaternion.identity);
            worldB.id = sw[i].id;
            worldB.onClicked.AddListener(OnWorldButtonActive);
        }
    }

    void OnWorldButtonActive(int id)
    {
        Debug.Log("go to world with id : " + id);
        onWorldChoose.Invoke();
        scrollbar.ClearField();
        currentWorld = GameManager.Instance.GetWorldByID(id);
        DisplayWorldsLvl(currentWorld);
    }


    void DisplayWorldsLvl(ScriptableWorlds world)
    {
        List<ButtonContent> buttons = new List<ButtonContent>();

        foreach (LevelReference item in world.levels)
        {
            ButtonContent bc = new ButtonContent();
            bc.id = item.id;
            bc.text = item.name;
            bc.image = item.look;
            buttons.Add(bc);
        }

        scrollbar.FillContent(buttons);
        scrollbar.onElementClicked.AddListener(LevelChoosen);
    }

    void LevelChoosen(int lvlID)
    {
        GameManager.Instance.LoadSceneByID(lvlID, currentWorld);
    }

    void DisplayWorldAtPoint(ScriptableWorlds world , Vector3 point)
    {
        if (world.TinyLevelVisual)
            Instantiate(world.TinyLevelVisual, spawnWorldsPoint.position + point, Quaternion.identity);
    }

    public List<Vector3> GenerateCirclePoints(int numberOfPoints, float radius, float angleOffset)
    {
        List<Vector3> points = new List<Vector3>();

        // Start with the direction of the forward vector of the orientation
        Vector3 forwardDirection = transform.forward;

        // Calculate the initial angle based on the forward direction
        float initialAngle = Mathf.Atan2(forwardDirection.z, forwardDirection.x) * Mathf.Rad2Deg;

        // Start with the first point
        float currentAngle = initialAngle;

        for (int i = 0; i < numberOfPoints; i++)
        {
            // Convert angle to radians
            float radians = currentAngle * Mathf.Deg2Rad;

            // Calculate the position of the point on the circle using trigonometry
            float x = radius * Mathf.Cos(radians);
            float z = radius * Mathf.Sin(radians);

            // Add the point to the list
            points.Add(new Vector3(x, 0f, z));

            // Increase the angle for the next point
            currentAngle += angleOffset;
        }

        return points;
    }
}
