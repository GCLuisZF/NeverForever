#include "Enroll_Login.h"
#include "Write_Read.h"
#include <fstream>
#include <sstream>
#include <map>
#include <string>
#include <iostream>
using namespace std;

//ע�ắ������
void Enroll()
{
	string userName,passWord;
	while (1)
	{
		cout << "������ע����˺ţ�" << endl;
		cin >> userName;
		if (readData(userName) == "None")
		{
			cout << "����������:" << endl;
			cin >> passWord;
			break;
		}
		else
		{
            cout << "�˺��Ѵ��ڣ�������" << endl;;
		}
	}writeData(userName, passWord);
	system("cls");
	cout << "ע��ɹ�" << endl;
}

//��¼��������
string Login()
{
    string userName, passWord;
    while (1)
    {
        cout << "�������˺ţ�" << endl;
        cin >> userName;
        if (readData(userName) == "None")
        {
            cout << "�˺������������������" << endl;
            system("pause");
            system("cls");
        }
        else
        {
            cout << "����������:" << endl;
            cin >> passWord;
            if (passWord == readData(userName))
            {
                system("cls");
                cout << "��½�ɹ�" << endl;
                return userName;
                break;
            }
            else
            {
                cout << "���������������������" << endl;
                system("pause");
                system("cls");
            }
        }
    }
}