using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{

    [SerializeField] private GameObject nextStageCanvas;

    // Start is called before the first frame update
    void Start()
    {
        nextStageCanvas.SetActive(false);
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
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
