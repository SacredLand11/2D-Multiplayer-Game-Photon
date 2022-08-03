using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public InputField playerID;
    public Text substantialPlayerID;

    public TextMeshProUGUI[] statistics;
    public Text serverInfo;

    GameObject RandomEntranceButton;
    GameObject BecomeAHostButton;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerIDExist"))
        {
            PlayerPrefs.SetInt("Total_Match", 0);
            PlayerPrefs.SetInt("Wins", 0);
            PlayerPrefs.SetInt("Defeats", 0);
            PlayerPrefs.SetInt("Total_Points", 0);
            firstPanel.SetActive(true);
            WriteTheValues();
        }
        else
        {
            secondPanel.SetActive(true);
            substantialPlayerID.text = PlayerPrefs.GetString("PlayerIDExist");
            WriteTheValues();
        }
    }
    public void SavePlayerID()
    {
        PlayerPrefs.SetString("PlayerIDExist", playerID.text);
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
        substantialPlayerID.text = playerID.text;
        RandomEntranceButton = GameObject.FindWithTag("RandomEntranceButton");
        BecomeAHostButton = GameObject.FindWithTag("BecomeAHostButton");
        RandomEntranceButton.GetComponent<Button>().interactable = true;
        BecomeAHostButton.GetComponent<Button>().interactable = true;
    }
    void WriteTheValues()
    {
        statistics[0].text = PlayerPrefs.GetInt("Total_Match").ToString();
        statistics[1].text = PlayerPrefs.GetInt("Wins").ToString();
        statistics[2].text = PlayerPrefs.GetInt("Defeats").ToString();
        statistics[3].text = PlayerPrefs.GetInt("Total_Points").ToString();
    }
}
