using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CannonballScript : MonoBehaviour
{
    float impactPower;
    int whoIsThePlayer;

    GameObject gameController;
    GameObject player;
    PhotonView pw;
    AudioSource cannonBallDestroyAudio;
    private void Start()
    {
        impactPower = 20;
        gameController = GameObject.FindWithTag("GameController");
        pw = GetComponent<PhotonView>();
        cannonBallDestroyAudio = GetComponent<AudioSource>();
    }
    [PunRPC]
    public void TagTransfer(string arrivaltag)
    {
        player = GameObject.FindWithTag(arrivaltag); 
        if(arrivaltag == "Player1")
        {
            whoIsThePlayer = 1;
        }
        else
        {
            whoIsThePlayer = 2;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OtherBoxes"))
        {
            collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, impactPower);
            player.GetComponent<PlayerScript>().FillAmountMotion();
            if (pw.IsMine)
            {
                PhotonNetwork.Instantiate("SmokePuffCollisionEffect", transform.position, transform.rotation, 0, null);
                cannonBallDestroyAudio.Play();
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player1Boxes") || collision.gameObject.CompareTag("Player1"))
        {
            gameController.GetComponent<PhotonView>().RPC("ImpactHit", RpcTarget.All, 1, impactPower);
            player.GetComponent<PlayerScript>().FillAmountMotion();
            if (pw.IsMine)
            {
                PhotonNetwork.Instantiate("SmokePuffCollisionEffect", transform.position, transform.rotation, 0, null);
                cannonBallDestroyAudio.Play();
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player2Boxes") || collision.gameObject.CompareTag("Player2"))
        {
            gameController.GetComponent<PhotonView>().RPC("ImpactHit", RpcTarget.All, 2, impactPower);
            player.GetComponent<PlayerScript>().FillAmountMotion();
            if (pw.IsMine)
            {
                PhotonNetwork.Instantiate("SmokePuffCollisionEffect", transform.position, transform.rotation, 0, null);
                cannonBallDestroyAudio.Play();
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.GetComponent<PlayerScript>().FillAmountMotion();
            PhotonNetwork.Instantiate("SmokePuffCollisionEffect", transform.position, transform.rotation, 0, null);
            cannonBallDestroyAudio.Play();
            if (pw.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Wood"))
        {
            player.GetComponent<PlayerScript>().FillAmountMotion();
            PhotonNetwork.Instantiate("SmokePuffCollisionEffect", transform.position, transform.rotation, 0, null);
            cannonBallDestroyAudio.Play();
            if (pw.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Award"))
        {
            gameController.GetComponent<PhotonView>().RPC("IncreaseTheHealth", RpcTarget.All, whoIsThePlayer);
            PhotonNetwork.Destroy(collision.transform.gameObject);
            player.GetComponent<PlayerScript>().FillAmountMotion();
            PhotonNetwork.Instantiate("SmokePuffCollisionEffect", transform.position, transform.rotation, 0, null);
            cannonBallDestroyAudio.Play();
            if (pw.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);

            }
        }
    }
}
