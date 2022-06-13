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
	while (d)//登录界面
	{
		cout << "登录--1，注册--2，退出--0" << endl;
		cout << "请输入你的选择" << endl;
		cin >> choice1;
		switch (choice1)
		{
			case 1: 
				r = Login();//登录账户
				d = 0;
				break;
			case 2:	
				Enroll();//注册账户
				break;
			case 0://退出系统
				Quit();
				return 0;
				break;
			default:
				cout << "输入有误，请重新选择！" << endl;
				system("pause");
				system("cls");
				break;
		}
	}
	while (1)//功能界面
	{
		ShowMenu();
		cout << "请输入你的选择" << endl;
		cin >> choice2;
		switch (choice2)
		{
		case 0://退出进销存系统
			Quit();
			return 0;
			break;
		case 1://添加新商品
			NewGoods(r);
			system("pause");
			system("cls");
			break;
		case 2://展示商品
			ShowGoods();
			system("pause");
			system("cls");
			break;
		case 3://查找商品
			FindGoods();
			system("pause");
			system("cls");
			break;
		case 4://商品进货
			InGoods(r);
			system("pause");
			system("cls");
			break;
		case 5://商品销售
			OutGoods(r);
			system("pause");
			system("cls");
			break;
		case 6://仓库盘点
			PanDian(r);
			system("pause");
			system("cls");
			break;
		case 7://显示操作记录
			Process();
			system("pause");
			system("cls");
			break;
		default:
			cout << "输入有误，请重新选择！" << endl;
			system("pause");
			system("cls");
			break;
		}
	}
	
	system("pause");
	return 0;
}