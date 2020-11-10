using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviourPun
{
    public string DoorName;
    public int cost;
    public bool lerpDoor;
    private float lerpDuration = 5f;
    public GameObject Door;
    public GameObject DoorUnlocker;
    int count = 0;
    
    Vector3 startValue = new Vector3(0f, -1f, 0f);
    Vector3 endValue = new Vector3(1f, -1f, 0f);

    private void Start()
    {
        //Door = GameManager.instance.crpytDoor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && lerpDoor && count == 0)
        {
            ChatBox.instance.LogPrivate(DoorName, "Pay " + cost + " gold to enter!");
            count++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Door = GameObject.FindGameObjectWithTag("CryptDoor");

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Door.GetComponent<PhotonView>().TransferOwnership(player.id);
        if (collision.tag == "Player" && Input.GetKey(KeyCode.E) && PlayerController.me.gold >= cost && lerpDoor)
        {
            player.photonView.RPC("TakeGold", player.photonPlayer, cost);
            if (lerpDoor)
            {
                StartCoroutine("LerpTheDoor");
            }
            lerpDoor = false;
            DoorUnlocker.GetComponent<UnlockDoor>().enabled = false;
        }
    }

    [PunRPC]
    IEnumerator LerpTheDoor()
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            Door.transform.position = Vector3.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Door.transform.position = endValue;
    }
}


