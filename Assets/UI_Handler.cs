using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{

    private static GameObject DS_Image;
    private static Image DS_Loader;
    private static GameObject DS_Key;

    private static GameObject N64_Image;
    private static Image N64_Loader;
    private static GameObject N64_Key;

    private void Start()
    {
        DS_Image = GameObject.Find("Canvas/DS_Loader/DS_image");
        DS_Key = GameObject.Find("Canvas/Help-Q");
        DS_Loader = GameObject.Find("Canvas/DS_Loader").GetComponent<Image>();
        
        N64_Image = GameObject.Find("Canvas/N64_Loader/N64_Image");
        N64_Key = GameObject.Find("Canvas/Help-E");
        N64_Loader = GameObject.Find("Canvas/N64_Loader").GetComponent<Image>();
        
        DS_Image.SetActive(false);
        DS_Loader.gameObject.SetActive(false);
        N64_Image.SetActive(false);
        N64_Loader.gameObject.SetActive(false);
    }

    public static void SetDSView(bool show, float percent)
    {
        DS_Image.SetActive(show);
        DS_Loader.gameObject.SetActive(show);
        DS_Key.SetActive(show);
        
        if (show)
            DS_Loader.fillAmount = percent / 100f;
    }

    public static void SetN64View(bool show, float percent)
    {
        N64_Image.SetActive(show);
        N64_Loader.gameObject.SetActive(show);
        N64_Key.SetActive(show);
        
        if (show)
            N64_Loader.fillAmount = percent / 100f;
        
    }
    
}
