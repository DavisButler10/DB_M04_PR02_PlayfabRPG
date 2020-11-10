using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptPortal : MonoBehaviour
{
    public GameObject portalActive;
    public GameObject portalClosed;
    public bool portalOpen = false;
    public GameObject endText;

    public static CryptPortal instance;
    void Start()
    {
        instance = this;
        endText.SetActive(false);
        portalActive.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ActivatePortal()
    {
        portalActive.SetActive(true);
        portalOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && portalOpen)
        {
            collision.gameObject.transform.position = new Vector3(30f, 30f, 0f);
            //endText.SetActive(true);
            Leaderboard.instance.OnEndGame();
        }
    }
}
