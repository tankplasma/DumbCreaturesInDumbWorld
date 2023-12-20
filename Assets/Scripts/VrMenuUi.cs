using System;
using Oculus.Interaction;
using UnityEngine;

public class VrMenuUi : MonoBehaviour
{
    [Serializable]
    public class Main
    {
        public GameObject menu;
        public InteractableUnityEventWrapper buttonPlay;
        public InteractableUnityEventWrapper buttonHowTP;
        public InteractableUnityEventWrapper buttonOptions;
        public InteractableUnityEventWrapper buttonQuit;
    }

    public Main main;

    [Serializable]
    public class Options
    {
        public GameObject menu;
        public InteractableUnityEventWrapper buttonBackTM;
        public InteractableUnityEventWrapper buttonResetSave;
        public InteractableUnityEventWrapper buttonSoundFX;
        public InteractableUnityEventWrapper buttonMusique;
    }

    public Options options;

    [Serializable]
    public class ChapterSelection
    {
        public GameObject menu;
        public InteractableUnityEventWrapper buttonBackTM;
        public InteractableUnityEventWrapper buttonChapTuto;
        public InteractableUnityEventWrapper buttonChap1;
        public InteractableUnityEventWrapper buttonChap2;
    }

    public ChapterSelection chapterSelection;

    [Serializable]
    public class LevelsSelection
    {
        public GameObject menu;
        public InteractableUnityEventWrapper buttonBackTC;
        public InteractableUnityEventWrapper buttonBackTM;
        public InteractableUnityEventWrapper buttonLevels1;
        public InteractableUnityEventWrapper buttonLevels2;
        public InteractableUnityEventWrapper buttonLevels3;
        public InteractableUnityEventWrapper buttonLevels4;
        public InteractableUnityEventWrapper buttonLevels5;
    }

    public LevelsSelection levelsSelection;

    GameObject currentUI;

    void Start()
    {
        BindButtons();
    }

    void BindButtons()
    {
        BindMainButtons();
        BindOptionsButtons();
        BindChapterSelectionButtons();
        BindlevelsSelectionButtons();
    }

    void BindMainButtons()
    {
        main.menu.SetActive(true);
        currentUI = main.menu;
        main.buttonPlay.WhenUnselect.AddListener(() => ActiveUI(chapterSelection.menu));
        //main.buttonHowTP.WhenUnselect.AddListener(() => CurrentUI = );
        main.buttonOptions.WhenUnselect.AddListener(() => ActiveUI(options.menu));
        main.buttonQuit.WhenUnselect.AddListener(() => Application.Quit());
    }

    void BindOptionsButtons()
    {
        options.menu.SetActive(false);
        options.buttonBackTM.WhenUnselect.AddListener(() => ActiveUI(main.menu));
        //options.buttonResetSave.WhenUnselect.AddListener(() => );
        //options.buttonMusique.WhenUnselect.AddListener(() => );
        //options.buttonSoundFX.WhenUnselect.AddListener(() => );
    }

    void BindChapterSelectionButtons()
    {
        chapterSelection.menu.SetActive(false);
        chapterSelection.buttonBackTM.WhenUnselect.AddListener(() => ActiveUI(main.menu));
        chapterSelection.buttonChapTuto.WhenUnselect.AddListener(() => ActiveUI(levelsSelection.menu));
    }

    void BindlevelsSelectionButtons()
    {
        levelsSelection.menu.SetActive(false);
        levelsSelection.buttonBackTM.WhenUnselect.AddListener(() => ActiveUI(main.menu));
        levelsSelection.buttonBackTC.WhenUnselect.AddListener(() => ActiveUI(chapterSelection.menu));
    }

    void ActiveUI(GameObject go)
    {
        currentUI.SetActive(false);
        currentUI = go;
        currentUI.SetActive(true);
    }
}
