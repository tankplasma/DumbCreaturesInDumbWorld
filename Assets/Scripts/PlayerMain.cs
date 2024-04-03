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
        if(gameManager)
            gameManager.onLevelChange.AddListener(FadeIn);
    }

    void UnbindEvents() 
    {
        if(gameManager)
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
