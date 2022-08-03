using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AwardScript : MonoBehaviour
{
    PhotonView pw;

    private void Start()
    {
        pw = GetComponent<PhotonView>();
        StartCoroutine(vanished());
    }
    IEnumerator vanished()
    {
        yield return new WaitForSeconds(10);
        if (pw.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
