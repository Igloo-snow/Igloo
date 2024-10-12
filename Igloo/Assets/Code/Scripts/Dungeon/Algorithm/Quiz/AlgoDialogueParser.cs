using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDialogueParser : MonoBehaviour
{
    public AlgoDialogue[] Parse(string _CSVFileName)
    {
        List<AlgoDialogue> dialogueList = new List<AlgoDialogue>(); //��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); //CSV ���� ������

        string[] data = csvData.text.Split(new char[] { '\n' });

        for(int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });
            //Debug.Log(data[i]);
            AlgoDialogue AlgoDialogue = new AlgoDialogue(); // ��� ����Ʈ ����

            AlgoDialogue.id = int.Parse(row[0]);
            AlgoDialogue.name = row[1];

            //dialogue.context �迭�� �ֱ� ���� ��ȯ ����
            List<string> contextList = new List<string>();

            List<AlgoChoiceOption> algochoices = new List<AlgoChoiceOption>();
            AlgoChoiceOption algoChoice = new AlgoChoiceOption();

            do
            {
                contextList.Add(row[2]);
                //Debug.Log(row[2]);

                if (row[6] == "1") // ������ ������� Ȯ��
                {
                    AlgoDialogue.isFinal = true;
                }

                if (int.Parse(row[7]) == 1) // ���� �̺�Ʈ Ȯ��
                {
                    AlgoDialogue.nextEvent = true;
                }

                if (row[3] == "1") //������ �ִ��� Ȯ��
                {
                    while (true)
                    {
                        //���� �ٷ� �Ѿ��
                        if (++i < data.Length)
                        {
                            row = data[i].Split(new char[] { ',' });
                            //���� �� Ȯ���ϱ�
                            if (row[0].ToString() != "")
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }

                        //�ɼ� �ֱ�
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
