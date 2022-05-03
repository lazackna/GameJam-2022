using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{

    [SerializeField] private GameObject gameoverCanvas;

    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        gameoverCanvas.SetActive(false);
        audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // do respawn work.
            StartCoroutine(RespawnCoroutine());
        }
    }

    IEnumerator RespawnCoroutine()
    {
        //show gameover screen.
        gameoverCanvas.SetActive(true);
        if(audioManager != null)
            audioManager.Play("death");
        yield return new WaitForSeconds(4);
        //Reload the entire scene.
        SceneManager.LoadScene(0);
    }
    
    
}
