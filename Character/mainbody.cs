#include<iostream>
#include "Function.h"
#include "Enroll_Login.h"
#include "Write_Read.h"
using namespace std;
int main()
{
	GetTime();
	int choice1 = 0;
	int d = 1;
	int choice2 = 0;
	string r = "0";
	string Id = "0";
	while (d)//��¼����
	{
		cout << "��¼--1��ע��--2���˳�--0" << endl;
		cout << "���������ѡ��" << endl;
		cin >> choice1;
		switch (choice1)
		{
			case 1: 
				r = Login();//��¼�˻�
				d = 0;
				break;
			case 2:	
				Enroll();//ע���˻�
				break;
			case 0://�˳�ϵͳ
				Quit();
				return 0;
				break;
			default:
				cout << "��������������ѡ��" << endl;
				system("pause");
				system("cls");
				break;
		}
	}
	while (1)//���ܽ���
	{
		ShowMenu();
		cout << "���������ѡ��" << endl;
		cin >> choice2;
		switch (choice2)
		{
		case 0://�˳�������ϵͳ
			Quit();
			return 0;
			break;
		case 1://�������Ʒ
			NewGoods(r);
			system("pause");
			system("cls");
			break;
		case 2://չʾ��Ʒ
			ShowGoods();
			system("pause");
			system("cls");
			break;
		case 3://������Ʒ
			FindGoods();
			system("pause");
			system("cls");
			break;
		case 4://��Ʒ����
			InGoods(r);
			system("pause");
			system("cls");
			break;
		case 5://��Ʒ����
			OutGoods(r);
			system("pause");
			system("cls");
			break;
		case 6://�ֿ��̵�
			PanDian(r);
			system("pause");
			system("cls");
			break;
		case 7://��ʾ������¼
			Process();
			system("pause");
			system("cls");
			break;
		default:
			cout << "��������������ѡ��" << endl;
			system("pause");
			system("cls");
			break;
		}
	}
	
	system("pause");
	return 0;
}