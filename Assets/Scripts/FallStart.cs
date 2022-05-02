using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class FallStart : MonoBehaviour
{
    [SerializeField]private GameObject enemyWall;

    [SerializeField] private GameObject fallColliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            // turn on player movement.
            Destroy(fallColliders);
            Destroy(enemyWall);
            Destroy(this);
        }
    }
}
