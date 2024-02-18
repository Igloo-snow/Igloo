using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DoorController : MonoBehaviour
{
    public Dictionary<string, GameObject> doorDictionary = new Dictionary<string, GameObject>();
    public TMP_InputField inputField;
    public GameObject player;

    void Start()
    {
        inputField.onEndEdit.AddListener(delegate { CheckPassword(); });

        AddDoor("cd A", GameObject.Find("DoorA"));
        AddDoor("cd B", GameObject.Find("DoorB"));
        AddDoor("cd C", GameObject.Find("DoorC"));
    }

    public void AddDoor(string password, GameObject doorObject)
    {
        if (!doorDictionary.ContainsKey(password))
        {
            doorDictionary.Add(password, doorObject);
        }
    }

    public void CheckPassword()
    {
        string inputText = inputField.text;

        if (doorDictionary.ContainsKey(inputText))
        {
            GameObject doorToOpen = doorDictionary[inputText];
            if (doorToOpen != null)
            {
                doorToOpen.SetActive(false);
                StartCoroutine(MovePlayer());
            }
            else
            {
                Debug.LogWarning("The door associated with password " + inputText + " is not found.");
            }
        }
        else
        {
            Debug.Log("Incorrect password!");
        }
        inputField.text = "";
    }

    IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector3(0, 1, 10);
    }
}