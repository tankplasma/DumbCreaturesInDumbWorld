using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class DisplayDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI display;

    [Serializable]
    class Dialogue
    {
        [TextArea] public string dialogue;
        public GameObject[] Entity;
    }

    [SerializeField] Dialogue[] dialogue;

    bool isStop = false;
    int currentIndexDialogue = 0;

    void Start() 
    {
        BeginDialogue();
    }

    public void BeginDialogue()
    {
        isStop = true;
        if (dialogue.Length < 0)
        {
            SetEntity(0, true);
            display.text = dialogue[0].dialogue;
        }
    }

    public void NextDialogue()
    {
        if (currentIndexDialogue < dialogue.Length - 1 && !isStop)
        {
            SetEntity(currentIndexDialogue, false);
            currentIndexDialogue++;
            display.text = dialogue[currentIndexDialogue].dialogue;
            SetEntity(currentIndexDialogue, true);
        }
        else
        {
            StopDialogue();
        }
    }

    public void StopDialogue()
    {
        isStop = true;
        Enumerable.Range(0, dialogue.Length - 1).ToList().ForEach(i => SetEntity(i, false));
        gameObject.SetActive(false);
    }

    void SetEntity(int index, bool state)
    {
        if (dialogue[index].Entity.Length > 0)
        {
            Array.ForEach(dialogue[index].Entity, go => go.SetActive(state));
        }
    }
}