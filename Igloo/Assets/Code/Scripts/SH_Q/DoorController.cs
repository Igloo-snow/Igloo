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
        // 입력 필드의 onEndEdit 이벤트에 CheckPassword 메소드를 추가
        inputField.onEndEdit.AddListener(delegate { CheckPassword(); });

        // 각 문 오브젝트를 DoorController에 추가합니다.
        AddDoor("cd A", GameObject.Find("DoorA")); // "A" 비밀번호로 DoorA 게임 오브젝트를 추가합니다.
        AddDoor("cd B", GameObject.Find("DoorB")); // "B" 비밀번호로 DoorB 게임 오브젝트를 추가합니다.
        AddDoor("cd C", GameObject.Find("DoorC")); // "C" 비밀번호로 DoorC 게임 오브젝트를 추가합니다.
    }

    // 딕셔너리에 문 오브젝트를 추가하는 메소드
    public void AddDoor(string password, GameObject doorObject)
    {
        if (!doorDictionary.ContainsKey(password))
        {
            doorDictionary.Add(password, doorObject);
        }
    }

    // 입력이 완료되었을 때 호출되는 메소드
    public void CheckPassword()
    {
        string inputText = inputField.text;

        if (doorDictionary.ContainsKey(inputText))
        {
            GameObject doorToOpen = doorDictionary[inputText];
            if (doorToOpen != null)
            {
                doorToOpen.SetActive(false);

                // 2초 후에 MovePlayer 코루틴 실행
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

        // 입력된 텍스트를 지웁니다.
        inputField.text = "";
    }

    IEnumerator MovePlayer()
    {
        // 2초간 대기
        yield return new WaitForSeconds(2f);

        // 여기에 플레이어 이동 로직을 추가합니다.
        // 예를 들어, 플레이어의 위치를 이동시키거나 다른 동작을 수행할 수 있습니다.
        player.transform.position = new Vector3(0, 1, 10);
    }
}