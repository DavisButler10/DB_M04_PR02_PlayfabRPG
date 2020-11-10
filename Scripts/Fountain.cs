using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fountain : MonoBehaviourPun
{
    public SpriteRenderer sr;
    public float fountianTime;
    float timer = 0;

    private void Start()
    {
        if(fountianTime == 0)
        {
            sr.sortingOrder = 1600;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (collision.tag == "Player" && timer <= fountianTime)
        {
            player.photonView.RPC("Heal", player.photonPlayer, 1);
            timer += Time.deltaTime;
        }
        else if(timer >= fountianTime)
        {
            sr.sortingOrder = 1600;
        }
    }
}
