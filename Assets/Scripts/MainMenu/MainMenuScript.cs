using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenuScript : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public InputField playerID;
    public Text substantialPlayerID;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerIDExist"))
        {
            firstPanel.SetActive(true);
        }
        else
        {
            secondPanel.SetActive(true);
            substantialPlayerID.text = PlayerPrefs.GetString("PlayerIDExist");
        }
    }
    public void SavePlayerID()
    {
        PlayerPrefs.SetString("PlayerIDExist", playerID.text);
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
        substantialPlayerID.text = playerID.text;
    }
}
