using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager Instance;
    public GameObject playerPrefab;
    public bool isHomeowner = false;
    void Start()
    {
        Instance = this;

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            GameManager.Instance.isHomeowner = true; //确定是否房主
        }

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (isHomeowner)
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(5f, 3f, 0f), Quaternion.identity, 0);
            }
            else
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(15f, 3f, 0f), Quaternion.identity, 0);
            }
        }


    }

    #region Photon Callbacks


    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }


    #endregion


    #region Public Methods


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    #endregion
}
