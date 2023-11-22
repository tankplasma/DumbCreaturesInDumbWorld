using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Digicode))]
public class DigicodeEditor : Editor
{
    float totalSize = 500f;

    [SerializeField]
    List<int> codeTemplate = new List<int>();

    public void AddNewButton(int i)
    {
        codeTemplate.Add(i);
    }

    public void RemoveButton(int i)
    {
        codeTemplate.Remove(i);
    }

    public int GetButtonOrder(int i)
    {
        int count = 1;

        foreach (int j in codeTemplate)
        {
            if (j == i)
            {
                return count;
            }
            count++;
        }

        return -1;
    }

    public void ClearButtons()
    {
        codeTemplate.Clear();
    }

    public void UpdateButtonsOrder(Digicode digi)
    {
        digi.SetNewOrder(codeTemplate);
        Debug.Log("new order : " );
        foreach (var item in codeTemplate)
        {
            Debug.Log(item);
        }
    }

    public override void OnInspectorGUI()
    {
        Digicode gridButtons = (Digicode)target;
        float size = totalSize / gridButtons.digicodeSize;

        int Count = 0;

        GUILayout.BeginVertical(); // Commence un groupe vertical
        GUILayout.FlexibleSpace(); // Ajoute un espace flexible en haut

        for (int i = 0; i < gridButtons.digicodeSize; i++)
        {
            GUILayout.BeginHorizontal(); // Commence un groupe horizontal
            GUILayout.FlexibleSpace();  // Ajoute un espace flexible à gauche
            for (int j = 0; j < gridButtons.digicodeSize; j++)
            {
                int order = GetButtonOrder(Count);

                string newtext = order == -1 ? "" : order.ToString();

                if (GUILayout.Button(newtext.ToString(), GUILayout.Width(size), GUILayout.Height(size)))
                {
                    if (newtext == "")
                        AddNewButton(Count);
                    else
                        RemoveButton(Count);

                    UpdateButtonsOrder(gridButtons);
                }
                Count++;
            }

            GUILayout.FlexibleSpace(); // Ajoute un espace flexible à droite
            GUILayout.EndHorizontal(); // Termine le groupe horizontal
        }


        GUILayout.EndHorizontal(); // Termine le groupe horizontal

        if (GUILayout.Button("Reset"))
        {
            ClearButtons();
            UpdateButtonsOrder(gridButtons);
        }
        GUILayout.FlexibleSpace(); // Ajoute un espace flexible à droite

        DrawDefaultInspector();
    }
}
