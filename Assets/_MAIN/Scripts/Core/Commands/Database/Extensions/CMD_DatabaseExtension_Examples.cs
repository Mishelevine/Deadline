using COMMANDS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class CMD_DatabaseExtension_Examples : CMD_DatabaseExtension
    {
        new public static void Extend(CommandDatabase database)
        {
            //���������� ������� ��� ����������
            database.AddCommand("print", new Action(PrintDefaultMessage));
            database.AddCommand("print_1p", new Action<string>(PrintUserMessage));
            database.AddCommand("print_mp", new Action<string[]>(PrintLines));

            //���������� ������ ��������� ��� ����������
            database.AddCommand("lambda", new Action(() => { Debug.Log("Printing a default message to log from lamda"); }));
            database.AddCommand("lambda_1p", new Action<string>((arg) => { Debug.Log($"User Lambda Message: '{arg}'"); }));
            database.AddCommand("lambda_mp", new Action<string[]>((args) => { Debug.Log(string.Join(", ", args)); }));

            //���������� ��������� ��� ����������
            database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
            database.AddCommand("process_1p", new Func<string, IEnumerator>(LineProcess));
            database.AddCommand("process_mp", new Func<string[], IEnumerator>(ArrayProcess));

            //����
            database.AddCommand("moveCharDemo", new Func<string, IEnumerator>(MoveCharacter));
        }

        private static void PrintDefaultMessage()
        {
            Debug.Log("Printing a default message to log");
        }

        private static void PrintUserMessage(string message)
        {
            Debug.Log($"User Message: '{message}'");
        }

        private static void PrintLines(string[] lines)
        {
            int i = 0;
            foreach (string line in lines)
            {
                Debug.Log($"{i++}. '{line}'");
            }
        }

        private static IEnumerator SimpleProcess()
        {
            for (int i = 1; i <= 5; i++)
            {
                Debug.Log($"Process is Running... {i}");
                yield return new WaitForSeconds(1);
            }
        }

        private static IEnumerator LineProcess(string data)
        {
            if (int.TryParse(data, out int number))
            {
                for (int i = 1; i <= number; i++)
                {
                    Debug.Log($"Process is Running... {i}");
                    yield return new WaitForSeconds(1);
                }
            }
        }

        private static IEnumerator ArrayProcess(string[] data)
        {
            foreach (string line in data)
            {
                Debug.Log($"Process Message: '{line}'");
                yield return new WaitForSeconds(0.5f);
            }
        }

        private static IEnumerator MoveCharacter(string direction)
        {
            bool left = direction.ToLower() == "left";

            Transform character = GameObject.Find("Mifim").transform;
            float moveSpeed = 5;

            float targetX = left ? -8 : 8;

            float currentX = character.position.x;

            while (Mathf.Abs(targetX - currentX) > 0.1f)
            {
                currentX = Mathf.MoveTowards(currentX, targetX, moveSpeed * Time.deltaTime);
                character.position = new Vector3(currentX, character.position.y, character.position.z);
                yield return null;
            }
        }
    }
}