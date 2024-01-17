using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public Animator animFade;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        BindEvents();
        FadeOut();
    }

    void OnDestroy()
    {
        UnbindEvents();
    }

    void BindEvents() 
    {
        gameManager.onLevelChange.AddListener(FadeIn);
    }

    void UnbindEvents() 
    {
        gameManager.onLevelChange.RemoveListener(FadeIn);
    }

    void FadeIn() 
    {
        animFade.Play("FadeIn");
    }

    void FadeOut()
    {
        animFade.Play("FadeOut");
    }
}
