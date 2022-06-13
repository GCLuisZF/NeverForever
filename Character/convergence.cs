#include "Enroll_Login.h"
#include "Write_Read.h"
#include <fstream>
#include <sstream>
#include <map>
#include <string>
#include <iostream>
using namespace std;

//×¢²áº¯Êý¶¨Òå
void Enroll()
{
	string userName,passWord;
	while (1)
	{
		cout << "ÇëÊäÈë×¢²áµÄÕËºÅ£º" << endl;
		cin >> userName;
		if (readData(userName) == "None")
		{
			cout << "ÇëÊäÈëÃÜÂë:" << endl;
			cin >> passWord;
			break;
		}
		else
		{
            cout << "ÕËºÅÒÑ´æÔÚ£¬ÇëÖØÊÔ" << endl;;
		}
	}writeData(userName, passWord);
	system("cls");
	cout << "×¢²á³É¹¦" << endl;
}

//µÇÂ¼º¯Êý¶¨Òå
string Login()
{
    string userName, passWord;
    while (1)
    {
        cout << "ÇëÊäÈëÕËºÅ£º" << endl;
        cin >> userName;
        if (readData(userName) == "None")
        {
            cout << "ÕËºÅÊäÈë´íÎó£¬ÇëÖØÐÂÊäÈë" << endl;
            system("pause");
            system("cls");
        }
        else
        {
            cout << "ÇëÊäÈëÃÜÂë:" << endl;
            cin >> passWord;
            if (passWord == readData(userName))
            {
                system("cls");
                cout << "µÇÂ½³É¹¦" << endl;
                return userName;
                break;
            }
            else
            {
                cout << "ÃÜÂëÊäÈë´íÎó£¬ÇëÖØÐÂÊäÈë" << endl;
                system("pause");
                system("cls");
            }
        }
    }
}