using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectSelector : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        if (Input.GetKeyDown(KeyCode.Keypad0)) // Particule / charged shot
        {
            int effect = PlayerPrefs.GetInt("Effect0");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect0", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            int effect = PlayerPrefs.GetInt("Effect1");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect1", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            int effect = PlayerPrefs.GetInt("Effect2");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect2", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            int effect = PlayerPrefs.GetInt("Effect3");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect3", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            int effect = PlayerPrefs.GetInt("Effect4");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect4", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            int effect = PlayerPrefs.GetInt("Effect5");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect5", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            int effect = PlayerPrefs.GetInt("Effect6");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect6", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            int effect = PlayerPrefs.GetInt("Effect7");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect7", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            int effect = PlayerPrefs.GetInt("Effect8");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect8", effect);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            int effect = PlayerPrefs.GetInt("Effect9");
            effect = (effect + 1) % 2;
            PlayerPrefs.SetInt("Effect9", effect);
        }
    }
}
