using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerScript : MonoBehaviour
{
    public GameObject cannonBall;
    public GameObject cannonBallSpawnPoint;
    public ParticleSystem cannonBallFireEffect;
    public AudioSource cannonBallFireAudio;
    float fireDirection;

    [Header("PowerBarSettings")]
    Image powerBar;
    float powerNumber;
    bool isEndofTheBar = false;
    Coroutine powerCoroutine;

    PhotonView pw;
    private void Start()
    {   
        pw = GetComponent<PhotonView>();
        if (pw.IsMine)
        {
            powerBar = GameObject.FindWithTag("PowerBar").GetComponent<Image>();
            if (PhotonNetwork.IsMasterClient)
            {
                //gameObject.tag = "Player1";
                transform.position = GameObject.FindWithTag("CreatingObject1").transform.position;
                fireDirection = 2f;
            }
            else
            {
                //gameObject.tag = "Player2";
                transform.position = GameObject.FindWithTag("CreatingObject2").transform.position;
                transform.rotation = GameObject.FindWithTag("CreatingObject2").transform.rotation;
                fireDirection = -2f;
            }
        }
        InvokeRepeating("StartTheGame", 0, 0.5f);
    }

    private void Update()
    {
        if (pw.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate("ExplosionEffect", cannonBallSpawnPoint.transform.position, cannonBallSpawnPoint.transform.rotation, 0, null);
                cannonBallFireAudio.Play();
                GameObject cannonballObject = PhotonNetwork.Instantiate("Cannonball", cannonBallSpawnPoint.transform.position, cannonBallSpawnPoint.transform.rotation, 0, null);
                cannonballObject.GetComponent<PhotonView>().RPC("TagTransfer", RpcTarget.All, gameObject.tag);
                Rigidbody2D rg = cannonballObject.GetComponent<Rigidbody2D>();
                rg.AddForce(new Vector2(fireDirection, 0f) * powerBar.fillAmount * 10f, ForceMode2D.Impulse);
                StopCoroutine(powerCoroutine);
            }
        }



    }
    public void StartTheGame()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (pw.IsMine)
            {
                powerCoroutine = StartCoroutine(PowerBar());
                CancelInvoke("StartTheGame");
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }
    public void FillAmountMotion()
    {
        powerCoroutine = StartCoroutine(PowerBar());
    }
    IEnumerator PowerBar()
    {
        powerBar.fillAmount = 0;
        isEndofTheBar = false;
        while (true)
        {
            if(powerBar.fillAmount < 1 && !isEndofTheBar)
            {
                powerNumber = 0.01f;
                powerBar.fillAmount += powerNumber;
                yield return new WaitForSeconds(0.001f*Time.deltaTime);
            }
            else
            {
                isEndofTheBar = true;
                powerNumber = 0.01f;
                powerBar.fillAmount -= powerNumber;
                yield return new WaitForSeconds(0.001f*Time.deltaTime);
                if(powerBar.fillAmount == 0)
                {
                    isEndofTheBar = false;
                }
            }
        }
    }
}
