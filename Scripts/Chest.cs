using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Chest : MonoBehaviourPun
{
    public GameObject chestOpen;
    public GameObject chestClosed;
    public SpriteRenderer itemSlot;
    int frames = 0;
    int weaponNum = 0;
    int timeNum = 0;
    int randomTimes = 0;
    float timer = 0;
    bool timerOn = false;
    int cost = 50;
    PlayerController player;
    Weapons playerW;
    int count = 0;

    public static Chest instance;

    public bool rolling = false;
    public bool rolled = false;
    void Start()
    {
        instance = this;
        chestClosed.SetActive(true);
        itemSlot.enabled = false;
        chestOpen.SetActive(false);
        randomTimes = Random.Range(8, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (rolling)
        {
            switch (weaponNum)
            {
                case 0:
                    itemSlot.sprite = playerW.woodenSword;
                    itemSlot.transform.localPosition = new Vector3(0.62f, 0.75f, 0f);
                    break;
                case 1:
                    itemSlot.sprite = playerW.bronzeSword;
                    itemSlot.transform.localPosition = new Vector3(0.62f, 0.75f, 0f);
                    break;
                case 2:
                    itemSlot.sprite = playerW.silverSword;
                    itemSlot.transform.localPosition = new Vector3(0.62f, 0.75f, 0f);
                    break;
                case 3:
                    itemSlot.sprite = playerW.goldenSword;
                    itemSlot.transform.localPosition = new Vector3(0.62f, 0.75f, 0f);
                    break;
                case 4:
                    itemSlot.sprite = playerW.diamondSword;
                    itemSlot.transform.localPosition = new Vector3(0.62f, 0.75f, 0f);
                    break;
                case 5:
                    itemSlot.sprite = playerW.woodenShield;
                    itemSlot.transform.localPosition = new Vector3(0.16f, 0.84f, 0f);
                    break;
                case 6:
                    itemSlot.sprite = playerW.bronzeShield;
                    itemSlot.transform.localPosition = new Vector3(0.16f, 0.84f, 0f);
                    break;
                case 7:
                    itemSlot.sprite = playerW.silverShield;
                    itemSlot.transform.localPosition = new Vector3(0.16f, 0.84f, 0f);
                    break;
                case 8:
                    itemSlot.sprite = playerW.goldenShield;
                    itemSlot.transform.localPosition = new Vector3(0.16f, 0.84f, 0f);
                    break;
                case 9:
                    itemSlot.sprite = playerW.diamondShield;
                    itemSlot.transform.localPosition = new Vector3(0.16f, 0.84f, 0f);
                    break;
            }

            if (timeNum == randomTimes)
            {
                rolling = false;

            }
            else
            {
                if (frames % 30 == 0)
                {
                    weaponNum = Random.Range(0, 10);
                    timeNum++;
                }
            }
            frames++;
        }
        if (!rolling && rolled && Input.GetKey(KeyCode.E))
        {

            if (weaponNum < 5)
            {
                //playerW.weaponSprite.sprite = itemSlot.sprite;
                playerW.photonView.RPC("UpdateSword", RpcTarget.All, weaponNum);
            }
            else
            {
                //playerW.shieldSprite.sprite = itemSlot.sprite;
                playerW.photonView.RPC("UpdateShield", RpcTarget.All, weaponNum);
            }
            itemSlot.enabled = false;
            timerOn = false;
            StartCoroutine("Reset", 2);
        }
        if (timerOn)
        {
            timer++;
        }
        if(timer >= 500)
        {
            timerOn = false;
            StartCoroutine("Reset", 2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && count == 0)
        {
            ChatBox.instance.LogPrivate("Chest", "Pay " + cost + " gold to roll!");
            count++;
        }
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && !rolling &&!rolled && PlayerController.me.gold >= cost)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            playerW = collision.gameObject.GetComponent<Weapons>();

            player.photonView.RPC("TakeGold", player.photonPlayer, cost);
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);
            itemSlot.enabled = true;
            rolling = true;
            rolled = true;
            timerOn = true;
            timer = 0;
        }
    }

    IEnumerator Reset(int time)
    {
        yield return new WaitForSeconds(time);
        rolled = false;
        rolling = false;
        chestClosed.SetActive(true);
        chestOpen.SetActive(false);
        frames = 0;
        weaponNum = 0;
        timeNum = 0;
        randomTimes = Random.Range(8, 10);
        itemSlot.enabled = false;
        timerOn = false;
        timer = 0;
    }
}
