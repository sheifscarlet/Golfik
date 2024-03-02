using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void ToMainMenu()
    {
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("UnlockedLevels",1);
    }
}
