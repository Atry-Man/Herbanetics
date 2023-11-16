using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;

public class dataBase : MonoBehaviour
{
    public string database_url = "";
    userDetail detailsScript;
    [SerializeField] TMP_InputField username;
    [SerializeField] TMP_Text score;

  
    void Start()
    {
        detailsScript = new userDetail();
    }


    public void SaveData()
    {
        Debug.Log("DataSent");
        detailsScript.username = username.text;
        detailsScript.score = score.text;
        RestClient.Post(database_url + "/" + "Herbanetic.json", detailsScript);
    }
}
