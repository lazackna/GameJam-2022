using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFirstLevel()
    {
        LoadScene(1);
    }

    public static void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
