using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Survey : MonoBehaviour
{
    [SerializeField] InputField feedback1;

    string URL = "https://docs.google.com/forms/d/e/1FAIpQLSdIXRLFVn8DXfmQTLgdVk7q4ZcYUy8thJYaxTANRwpGEEe8IQ/viewform?usp=sf_link";

    
    public void Send()
    {
        StartCoroutine(Post(feedback1.text));
    }

    IEnumerator Post(string s1)
    {
        WWWForm form = new WWWForm();
        form.AddField("", s1);




        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        
        yield return www.SendWebRequest();

    }
}
