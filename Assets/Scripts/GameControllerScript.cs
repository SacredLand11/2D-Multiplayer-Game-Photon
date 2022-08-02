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
    private void Start()
    {
        pw = GetComponent<PhotonView>();
    }
    [PunRPC]
    public void ImpactHit(int value, float impactPower)
    {
        switch (value)
        {
            case 1:
                Player1_Health -= impactPower;
                Player1_Health_Bar.fillAmount = Player1_Health / 100;
                break;
            case 2:
                Player2_Health -= impactPower;
                Player2_Health_Bar.fillAmount = Player2_Health / 100;
                break;
        }
    }
}
