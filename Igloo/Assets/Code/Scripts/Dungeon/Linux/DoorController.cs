using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorController : MonoBehaviour
{
    public Dictionary<string, GameObject> doorDictionary = new Dictionary<string, GameObject>();
    public TMP_InputField inputField;
    public TextMeshProUGUI outputText;
    public GameObject player;
    private Animator animator;
    private Vector3 previousPosition;

    public Dictionary<string, string> textDictionary = new Dictionary<string, string>();

    public GameObject DoorA;
    public GameObject DoorB;
    public GameObject DoorC;
    public GameObject DoorD;
    public GameObject DoorE;
    public GameObject DoorF;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;
    public GameObject copynoonsong;


    private bool isTyping;
    private Queue<string> textQueue = new Queue<string>();

    void Start()
    {
        copynoonsong.SetActive(false);

        previousPosition = player.transform.position;
        animator = GetComponent<Animator>();

        inputField.onEndEdit.AddListener(delegate { CheckPassword(); });

        AddDoor("cd A", DoorA);
        AddDoor("cd B", DoorB);
        AddDoor("cd C", DoorC);

        AddDoor("mv noonsong D", DoorD);
        AddDoor("mv noonsong E", DoorE);
        AddDoor("mv noonsong F", DoorF);

        AddDoor("cd next", Door1);
        AddDoor("mv noonsong next", Door2);
        AddDoor("cp noonsong next", Door3);


        textDictionary.Add("start", "리눅스 던전에 어서 와!몸이 갑자기 움직이지 않아서 놀랐지? 여기선 다른 방식으로 움직일 수 있어. cd A를 쳐서 A 방으로 들어가보자!");

        textDictionary.Add("cd A", "잘했어! cd next로 다음 방으로 넘어가보자!");
        textDictionary.Add("cd B", "이런, 막혀있네. cd .. 으로 다시 뒤로 돌아가보자.");
        textDictionary.Add("cd C", "이런, 막혀있네. cd .. 으로 다시 뒤로 돌아가보자.");

        textDictionary.Add("mv noonsong E", "잘했어! mv noonsong next로 다음 방으로 넘어가보자!");
        textDictionary.Add("mv noonsong D", "이런, 막혀있네. cd .. 으로 다시 뒤로 돌아가보자.");
        textDictionary.Add("mv noonsong F", "이런, 막혀있네. cd .. 으로 다시 뒤로 돌아가보자.");

        textDictionary.Add("cd next", "이번에는 mv noonsong 명령어로 이동해보자. 눈송이라는 파일을 지정해 이동시키는 명령어야!");
        textDictionary.Add("mv noonsong next", "이번에는 cp noonsong next로 이동해보자. 뒤에 이동하고 싶은 방 이름을 넣으면 돼! 이건 파일을 해당 위치로 복사하는 명령어야.");
        textDictionary.Add("cp noonsong next", "오, 뒤를 돌아봐! 복사 명령어를 사용했더니 눈송이가 한 명 더 있잖아! rm noonsong으로 삭제하자.");
        textDictionary.Add("rm noonsong", "그런데 이 방은... 위험해 보여. 여기에 교재가 있을 것 같지 않아.. cd back으로 뒤의 방으로 이동하자.");
        textDictionary.Add("cd back", "저 방에서 이상한 게 나올 것 같아... 방을 아예 삭제해버리자! rmdir room으로 삭제할 수 있어.");
        textDictionary.Add("rmdir room", "여기에 교재가 없나봐... 그럼 교재를 만들자! cat 리눅스교재 명령어로 만들 수 있어!");
        textDictionary.Add("cat 리눅스교재", "잘했어! 이제 shutdown 명령어로 이 던전에서 나가면 돼. 즐거운 학교 생활이 되길 바라!");
    }

    public void AddDoor(string password, GameObject doorObject)
    {
        if (!doorDictionary.ContainsKey(password))
        {
            doorDictionary.Add(password, doorObject);
        }
    }

    private Coroutine typingCoroutine;

    public void CheckPassword()
    {
        string inputText = inputField.text;

        if (textDictionary.ContainsKey(inputText))
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(textDictionary[inputText]));
        }

        if (inputText == "rm noonsong")
        {
            copynoonsong.SetActive(false);
        }

        if (inputText == "cd .." || inputText == "cd back")
        {
            player.transform.position = previousPosition;
            inputField.text = "";
            return;
        }

        if (inputText == "shutdown")
        {
            FindAnyObjectByType<SceneMgr>().StartLoadScene(4);
        }



        if (doorDictionary.ContainsKey(inputText))
        {
            GameObject doorToOpen = doorDictionary[inputText];
            if (doorToOpen != null)
            {
                Animator doorAnimator = doorToOpen.GetComponent<Animator>();
                doorAnimator.SetTrigger("open");

                if (inputText == "cd A")
                {
                    StartCoroutine(MovePlayer1());
                }
                else if (inputText == "cd B")
                {
                    StartCoroutine(MovePlayer2());
                }
                else if (inputText == "cd C")
                {
                    StartCoroutine(MovePlayer3());
                }
                else if (inputText == "cd next")
                {
                    StartCoroutine(MovePlayer2());
                }
                else if (inputText == "mv noonsong D")
                {
                    StartCoroutine(MovePlayer1());
                }
                else if (inputText == "mv noonsong E")
                {
                    StartCoroutine(MovePlayer2());
                }
                else if (inputText == "mv noonsong F")
                {
                    StartCoroutine(MovePlayer3());
                }
                else if (inputText == "mv noonsong next")
                {
                    StartCoroutine(MovePlayer4());
                }
                else if (inputText == "cp noonsong next")
                {
                    StartCoroutine(MovePlayer5());
                }
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

    IEnumerator TypeText(string text)
    {
        outputText.text = "";
        foreach (char letter in text)
        {
            outputText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }


    IEnumerator MovePlayer1()
    {
        previousPosition = player.transform.position;
        yield return new WaitForSeconds(2f);
        Vector3 currentPosition = player.transform.position;
        Vector3 targetPosition = currentPosition + new Vector3(10, 0, -20);
        player.transform.position = targetPosition;
    }

    IEnumerator MovePlayer2()
    {
        previousPosition = player.transform.position;
        yield return new WaitForSeconds(2f);
        Vector3 currentPosition = player.transform.position;
        Vector3 targetPosition = currentPosition + new Vector3(0, 0, -20);
        player.transform.position = targetPosition;
    }

    IEnumerator MovePlayer3()
    {
        previousPosition = player.transform.position;
        yield return new WaitForSeconds(2f);
        Vector3 currentPosition = player.transform.position;
        Vector3 targetPosition = currentPosition + new Vector3(-10, 0, -20);
        player.transform.position = targetPosition;
    }

    IEnumerator MovePlayer4()
    {
        previousPosition = player.transform.position;
        yield return new WaitForSeconds(2f);
        Vector3 currentPosition = player.transform.position;
        Vector3 targetPosition = currentPosition + new Vector3(0, 0, -16);
        player.transform.position = targetPosition;
    }

    IEnumerator MovePlayer5()
    {
        previousPosition = player.transform.position;
        yield return new WaitForSeconds(2f);
        Vector3 currentPosition = player.transform.position;
        Vector3 targetPosition = currentPosition + new Vector3(0, 0, -20);
        player.transform.position = targetPosition;
        yield return new WaitForSeconds(2f);
        copynoonsong.SetActive(true);
    }
}