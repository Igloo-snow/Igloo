using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDialogueParser : MonoBehaviour
{
    public AlgoDialogue[] Parse(string _CSVFileName)
    {
        List<AlgoDialogue> dialogueList = new List<AlgoDialogue>(); //대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); //CSV 파일 가져옴

        string[] data = csvData.text.Split(new char[] { '\n' });

        for(int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });
            //Debug.Log(data[i]);
            AlgoDialogue AlgoDialogue = new AlgoDialogue(); // 대사 리스트 생성

            AlgoDialogue.id = int.Parse(row[0]);
            AlgoDialogue.name = row[1];

            //dialogue.context 배열에 넣기 위한 변환 과정
            List<string> contextList = new List<string>();

            List<AlgoChoiceOption> algochoices = new List<AlgoChoiceOption>();
            AlgoChoiceOption algoChoice = new AlgoChoiceOption();

            do
            {
                contextList.Add(row[2]);
                //Debug.Log(row[2]);

                if (row[6] == "1") // 마지막 대사인지 확인
                {
                    AlgoDialogue.isFinal = true;
                }

                if (int.Parse(row[7]) == 1) // 연관 이벤트 확인
                {
                    AlgoDialogue.nextEvent = true;
                }

                if (row[3] == "1") //선택지 있는지 확인
                {
                    while (true)
                    {
                        //다음 줄로 넘어가기
                        if (++i < data.Length)
                        {
                            row = data[i].Split(new char[] { ',' });
                            //다음 줄 확인하기
                            if (row[0].ToString() != "")
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }

                        //옵션 넣기
                        algoChoice.id = AlgoDialogue.id;
                        algoChoice.option = row[4];
                        //Debug.Log(row[4]);

                        algoChoice.nextId = int.Parse(row[5]);
                        algochoices.Add(algoChoice);
                        algoChoice = new AlgoChoiceOption(); 
                    }
                }
                else
                {
                    if (++i < data.Length)
                    {
                        row = data[i].Split(new char[] { ',' });
                    }
                    else
                    {
                        break;
                    }
                }
            } while (row[0].ToString() == "");

            AlgoDialogue.contexts = contextList.ToArray();
            AlgoDialogue.choices = algochoices.ToArray();

            dialogueList.Add(AlgoDialogue);
        }

        return dialogueList.ToArray();
    }
}
