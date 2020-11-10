using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    [Header("Players")]
    public string playerPrefabPath;
    public string cryptDoorPath;
    public Transform cryptDoorLocation;
    public Transform[] spawnPoints;
    public float respawnTime;
    private int playersInGame;
    public PlayerController[] players;
    public GameObject crpytDoor;
    public GameObject Chest1;
    public GameObject Chest2;
    public GameObject Chest3;

    // instance
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    // keep track of how many players have joined the game so we can 
    // spawn player characters when everyone's in
    [PunRPC]
    void ImInGame()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
            SpawnPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
        if (PhotonNetwork.IsMasterClient)
        {
            crpytDoor = PhotonNetwork.Instantiate(cryptDoorPath, cryptDoorLocation.position, Quaternion.identity);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            Chest1 = PhotonNetwork.Instantiate("Chest", new Vector3(-4f, 13f, 0), Quaternion.identity);
            Chest2 = PhotonNetwork.Instantiate("Chest", new Vector3(8f, 7f, 0), Quaternion.identity);
            Chest3 = PhotonNetwork.Instantiate("Chest", new Vector3(9f, -7f, 0), Quaternion.identity);
        }

    }

    void SpawnPlayer()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabPath, spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, Quaternion.identity);

        // initialize the player
        playerObj.GetComponent<PhotonView>().RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

}
