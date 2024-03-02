using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    private int _unlockedLevels;
    [SerializeField] private Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        _unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttons[i].image.color = new Color(84, 84, 84,125);
        }
        for (int i = 0; i < _unlockedLevels; i++)
        {
            buttons[i].interactable = true;
            buttons[i].image.color = new Color(255, 255, 255,255);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
