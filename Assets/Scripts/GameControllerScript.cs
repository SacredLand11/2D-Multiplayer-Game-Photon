using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameControllerScript : MonoBehaviour
{
    [Header("Player Health Settings")]
    public Image Player1_Health_Bar;
    float Player1_Health = 100;
    public Image Player2_Health_Bar;
    float Player2_Health = 100;

    PhotonView pw;
    bool isStart;
    int limit;
    float waitingTime;
    public GameObject[] dots;
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        isStart = false;
        limit = 4;
        waitingTime = 15f;
    }
    IEnumerator StartCreateTheAwards()
    {
        int occuringNumber = 0;
        while (true && isStart)
        {
            if(limit == occuringNumber)
            {
                isStart = false;
            }
            yield return new WaitForSeconds(waitingTime);
            int currentvalue = Random.Range(0, 6);
            PhotonNetwork.Instantiate("Award", dots[currentvalue].transform.position, dots[currentvalue].transform.rotation, 0, null);
            occuringNumber++;
        }
    }
    [PunRPC]
    public void StartToCreateAwardBoxes()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            isStart = true;
            StartCoroutine(StartCreateTheAwards());
        }
    }
    [PunRPC]
    public void ImpactHit(int value, float impactPower)
    {
        switch (value)
        {
            case 1:
                if (PhotonNetwork.IsMasterClient)
                {
                    Player1_Health -= impactPower;
                    Player1_Health_Bar.fillAmount = Player1_Health / 100;
                }
                break;
            case 2:
                if (PhotonNetwork.IsMasterClient)
                {
                    Player2_Health -= impactPower;
                    Player2_Health_Bar.fillAmount = Player2_Health / 100;
                }
                break;
        }
    }
    [PunRPC]
    public void IncreaseTheHealth(int PlayerNo)
    {
        switch (PlayerNo)
        {
            case 1:
                Player1_Health += 30;
                if (Player1_Health >= 100)
                {
                    Player1_Health = 100;
                    Player1_Health_Bar.fillAmount = 1;
                }
                else
                {
                    Player1_Health_Bar.fillAmount = Player1_Health / 100;
                }
                break;
            case 2:
                Player2_Health += 30;
                if (Player2_Health >= 100)
                {
                    Player2_Health = 100;
                    Player2_Health_Bar.fillAmount = 1;
                }
                else
                {
                    Player2_Health_Bar.fillAmount = Player2_Health / 100;
                }
                break;
        }
    }
}
