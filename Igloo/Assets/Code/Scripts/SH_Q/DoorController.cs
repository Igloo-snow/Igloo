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
        // �Է� �ʵ��� onEndEdit �̺�Ʈ�� CheckPassword �޼ҵ带 �߰�
        inputField.onEndEdit.AddListener(delegate { CheckPassword(); });

        // �� �� ������Ʈ�� DoorController�� �߰��մϴ�.
        AddDoor("cd A", GameObject.Find("DoorA")); // "A" ��й�ȣ�� DoorA ���� ������Ʈ�� �߰��մϴ�.
        AddDoor("cd B", GameObject.Find("DoorB")); // "B" ��й�ȣ�� DoorB ���� ������Ʈ�� �߰��մϴ�.
        AddDoor("cd C", GameObject.Find("DoorC")); // "C" ��й�ȣ�� DoorC ���� ������Ʈ�� �߰��մϴ�.
    }

    // ��ųʸ��� �� ������Ʈ�� �߰��ϴ� �޼ҵ�
    public void AddDoor(string password, GameObject doorObject)
    {
        if (!doorDictionary.ContainsKey(password))
        {
            doorDictionary.Add(password, doorObject);
        }
    }

    // �Է��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼ҵ�
    public void CheckPassword()
    {
        string inputText = inputField.text;

        if (doorDictionary.ContainsKey(inputText))
        {
            GameObject doorToOpen = doorDictionary[inputText];
            if (doorToOpen != null)
            {
                doorToOpen.SetActive(false);

                // 2�� �Ŀ� MovePlayer �ڷ�ƾ ����
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

        // �Էµ� �ؽ�Ʈ�� ����ϴ�.
        inputField.text = "";
    }

    IEnumerator MovePlayer()
    {
        // 2�ʰ� ���
        yield return new WaitForSeconds(2f);

        // ���⿡ �÷��̾� �̵� ������ �߰��մϴ�.
        // ���� ���, �÷��̾��� ��ġ�� �̵���Ű�ų� �ٸ� ������ ������ �� �ֽ��ϴ�.
        player.transform.position = new Vector3(0, 1, 10);
    }
}