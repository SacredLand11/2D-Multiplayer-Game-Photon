using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class OtherBoxesScript : MonoBehaviour
{
    float health = 100;
    public GameObject healthbarCanvas;
    public Image healthbar;

    GameObject gameController;
    PhotonView pw;
    AudioSource otherBoxDestroyAudio;
    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        pw = GetComponent<PhotonView>();
        otherBoxDestroyAudio = GetComponent<AudioSource>();
    }
    [PunRPC]
    public void TakeDamage(float DamagePower)
    {
        if (pw.IsMine)
        {
            health -= DamagePower;
            healthbar.fillAmount = health / 100;
            if (health <= 0)
            {
                //gameController.GetComponent<GameControllerScript>().Audio_And_Effect_Creator(2, gameObject);
                PhotonNetwork.Instantiate("BoxCollisionEffect2", transform.position, transform.rotation, 0, null);
                otherBoxDestroyAudio.Play();
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                StartCoroutine(ExistTheCanvas());
            }
        }
    }

    IEnumerator ExistTheCanvas()
    {
        if (!healthbarCanvas.activeInHierarchy)
        {
            healthbarCanvas.SetActive(true);
            yield return new WaitForSeconds(2);
            healthbarCanvas.SetActive(false);
        }
    }
}
