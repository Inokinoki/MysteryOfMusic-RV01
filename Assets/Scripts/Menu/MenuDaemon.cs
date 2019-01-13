using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDaemon : MonoBehaviour {

    public Sprite[] operationSprites;
    public Sprite[] aboutSprites;
    public Sprite[] introductionSprites;

    public Sprite mainSprite;

    public Image bar;
    public Sprite mainBar;
    public Sprite confirmationBar;
    public Sprite otherBar;

    public Image main;

    private int lastPage;
    private int page;

    private int lastState;
    private int state;
    private int recoveryState;

    // Automate
    private const int MAIN = 0;
    private const int INTRODUCTION = 1;
    private const int OPERATION = 2;
    private const int ABOUT = 4;
    private const int WAIT_REFRESH = 9;
    private const int WAIT_CONFIRMATION = 10;

    // Use this for initialization
    void Start () {
        lastState = -1;
        state = MAIN;

        lastPage = -1;

        // Init delegate events
        MenuInteraction[] buttons = GetComponentsInChildren<MenuInteraction>();

        for (int i = 0; i < buttons.GetLength(0); i++)
        {
            switch (i)
            {
                case 0:
                    buttons[i].HandleRayCastClick = OnConfirm;
                    break;
                case 2:
                    buttons[i].HandleRayCastClick = OnOperation;
                    break;
                case 4:
                    buttons[i].HandleRayCastClick = OnBeginEasyGame;
                    break;
                case 5:
                    buttons[i].HandleRayCastClick = OnBeginGame;
                    break;
                case 7:
                    buttons[i].HandleRayCastClick = OnAbout;
                    break;
                case 9:
                    buttons[i].HandleRayCastClick = OnBack;
                    break;
                case 11:
                    buttons[i].HandleRayCastClick = OnCancel;
                    break;
            }
            Debug.Log(buttons[i].index);
        }

        Debug.Log(buttons.GetLength(0));
	}
	
	// Update is called once per frame
	void Update () {
        /**
         * Refresh UIs
         */
        if (lastState != state)
        {
            if (bar != null)
            {
                switch (state)
                {
                    case MAIN:
                        main.sprite = mainSprite;
                        bar.sprite = mainBar;
                        break;
                    case WAIT_CONFIRMATION:
                        // mainSprite = ;
                        bar.sprite = confirmationBar;
                        break;
                    default:
                        bar.sprite = otherBar;
                        break;
                }
            }

            lastState = state;
        }

        if (lastPage != page)
        {
            if (main != null)
            {
                switch (state)
                {
                    case MAIN:
                        main.sprite = mainSprite;
                        break;
                    case INTRODUCTION:
                        if (page >= 0 && page < introductionSprites.Length)
                        {
                            main.sprite = introductionSprites[page];
                        }
                        else if (page <0)
                        {
                            page = 0;
                        }
                        else
                        {
                            page = introductionSprites.Length - 1;
                        }
                        break;
                    case OPERATION:
                        if (page >= 0 && page < operationSprites.Length)
                        {
                            main.sprite = operationSprites[page];
                        }
                        else if (page < 0)
                        {
                            page = 0;
                        }
                        else
                        {
                            page = operationSprites.Length - 1;
                        }
                        break;
                    case WAIT_CONFIRMATION:
                        bar.sprite = confirmationBar;
                        break;
                }
            }
            lastPage = page;
        }
	}

    public void OnBack()
    {
        switch (state)
        {
            case MAIN:
                /*if (Application.isEditor)
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                }
                else
                {*/
                    Application.Quit();
                //}
                break;
            default:
                recoveryState = state;
                lastState = state;
                state = WAIT_CONFIRMATION;
                break;
        }
    }

    public void OnIntroduction()
    {
        switch (state)
        {
            case MAIN:
                state = INTRODUCTION;
                lastPage = -1;
                page = 0;
                break;
        }
    }

    public void OnOperation()
    {
        switch (state)
        {
            case MAIN:
                state = OPERATION;
                lastPage = -1;
                page = 0;
                break;
        }
    }

    public void OnBeginEasyGame()
    {
        switch (state)
        {
            case MAIN:
                Configuration.availablePanda = new List<int>(new int[] { 0, 2, 4, 5, 7, 9, 11 });
                SceneManager.LoadScene("Fantasy Environment part2_day");
                break;
            default:
                break;
        }
    }

    public void OnBeginGame()
    {
        switch (state)
        {
            case MAIN:
                Configuration.availablePanda = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
                SceneManager.LoadScene("Fantasy Environment part2_day");
                break;
            default:
                break;
        }
    }

    public void OnAbout()
    {
        // Remove About state
        /*switch (state)
        {
            case MAIN:
                state = INTRODUCTION;
                page = 0;
                break;
        }*/
    }

    public void OnConfirm()
    {
        switch (state)
        {
            case WAIT_CONFIRMATION:
                lastState = state;
                state = MAIN;
                break;
            case INTRODUCTION:
            case OPERATION:
            case ABOUT:
                Debug.Log("Test prev");
                page--;
                break;
        }
    }

    public void OnCancel()
    {
        switch (state)
        {
            case WAIT_CONFIRMATION:
                state = recoveryState;
                lastState = WAIT_CONFIRMATION;
                break;
            case INTRODUCTION:
            case OPERATION:
            case ABOUT:
                Debug.Log("Test next");
                page++;
                break;
        }
    }
}
