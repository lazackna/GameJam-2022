using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{

    [SerializeField] private GameObject nextStageCanvas;

    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        nextStageCanvas.SetActive(false);
        audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
            StartCoroutine(NextStageCoroutine());
    }

    private IEnumerator NextStageCoroutine()
    {
        nextStageCanvas.SetActive(true);
        if(audioManager != null)
            audioManager.Play("win");
        yield return new WaitForSeconds(3);
        Debug.Log("Next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
