using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Alter : MonoBehaviour
{
    //public Text text;
    public GameObject textObject;
    GameObject[] souls;
    float lerpDuration = 1f;
    public int numSouls = 0;
    public GameObject bloodyAlter;
    int count = 0;
    void Start()
    {
        textObject.GetComponent<MeshRenderer>().sortingOrder = 1000;
        
    }

    private void Update()
    {
        souls = GameObject.FindGameObjectsWithTag("Soul");
        foreach(GameObject soul in souls)
        {
            StartCoroutine("LerpTheSoul", soul);
        }
        if (numSouls > 10)
        {
            numSouls = 10;
        }
        textObject.GetComponent<TextMeshPro>().text = numSouls + "/10";

        foreach (GameObject soul in souls)
        {
            if (!soul.gameObject.activeSelf)
            {
                PhotonNetwork.Destroy(soul);
            }
        }
        if(numSouls == 10)
        {
            CryptPortal.instance.ActivatePortal();
            bloodyAlter.SetActive(true);
            textObject.SetActive(false);
            if (count == 0)
            {
                ChatBox.instance.LogPrivate("Scary Monster Guy", "Thank you for the feast, you may pass!");
                count++;
            }
        }
    }

    IEnumerator LerpTheSoul(GameObject soul)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            soul.transform.position = Vector3.Lerp(soul.transform.position, transform.position + new Vector3(0.5f, 0.5f, 0), timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        soul.transform.position = transform.position + new Vector3(0.5f, 0.5f, 0);
        if (soul.gameObject.activeSelf)
        {
            numSouls++;
        }
        soul.SetActive(false);
    }
}
