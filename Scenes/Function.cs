#include "Function.h"
#include "Write_Read.h"
#include "Enroll_Login.h"
#include <iostream>
#include <string>
#include <cmath>
using namespace std;

//�˳�����ʵ��
void Quit()
{
    cout << "��ӭ��һ��ʹ��" << endl;
    system("pause");
}

//��ȡʱ�书��ʵ��
string GetTime()
{
    time_t timep;
    time(&timep);
    char tmp[256];
    strftime(tmp, sizeof(tmp), "%Y-%m-%d %H:%M:%S", localtime(&timep));
    //cout << tmp << endl;
    return tmp;
}

//չʾ�˵�����ʵ��
void ShowMenu()
{
    cout << "*********�������õĽ��������ϵͳ***********" << endl;
    cout << "***************1.������Ʒ*******************" << endl;
    cout << "***************2.չʾ��Ʒ*******************" << endl;
    cout << "***************3.������Ʒ*******************" << endl;
    cout << "***************4.��Ʒ����*******************" << endl;
    cout << "***************5.��Ʒ����*******************" << endl;
    cout << "***************6.�ֿ��̵�*******************" << endl;
    cout << "***************7.��ʾ������¼***************" << endl;
    cout << "***************0.�˳�������ϵͳ*************" << endl;
    cout << "*********�������õĽ��������ϵͳ***********" << endl;
}

//������Ʒ����ʵ��
void NewGoods(string r)
{
    string t;
    string Id;
    string Name;
    string Unit;
    string Price;
    double quantity;
    string Quantity;
    while (1)
    {
        cout << "����������Ʒ�ı��" << endl;
        cin >> Id;
        if (readGoods(Id) == false)//����Ҳ����˱����Ʒ����������
        {
            cout << "����������Ʒ������" << endl;
            cin >> Name;
            cout << "����������Ʒ�ĵ�λ" << endl;
            cin >> Unit;
            cout << "����������Ʒ�ļ۸�" << endl;
            cin >> Price;
            while (1)
            {
                cout << "����������Ʒ������" << endl;
                cin >> quantity;
                if (quantity < 0)
                {
                    cout << "��Ʒ������Ϊ�����������ԣ�";
                    system("pause");
                    system("cls");
                }
                else if (zhengshu(quantity) == false)
                {
                    cout << "��Ʒ��ӦΪ�����������ԣ�";
                    system("pause");
                    system("cls");
                }
                else
                {
                    Quantity = to_string(quantity);
                    cout << "��ӳɹ���" << endl;
                    system("pause");
                    system("cls");
                    break;
                }
            }

            break;
        }
        else//����˵���ñ����Ʒ�Ѵ��ڣ���������
        {
            cout << "�ñ����Ʒ�Ѵ��ڣ�������" << endl;
            system("pause");
            system("cls");
        }

    }
    writeGoods(Id, Name, Unit, Price, Quantity);//д����Ʒ��Ϣ

  //��������¼����
    t = GetTime() + "    �������ݣ�������Ʒ    ��Ʒ��ţ�" + Id 
        +"    ��Ʒ���ƣ�" + Name
        +"    ����������"+ Quantity
        +"    �����ˣ�"+ r;
    writeTime(t);
}

//չʾ��Ʒ����ʵ��
void ShowGoods()
{

    map<string, string> dict;//��ѯ�ֵ�

    ifstream fin("good.bat", ios::binary);

    char szBuf[25600] = { 0 };
    fin.read(szBuf, sizeof(char) * 25600);

    string tempId;
    string tempName;

    istringstream is(szBuf);
    while (is >> tempId >> tempName)//����������д���ֵ�
    {
        dict.insert(pair<string, string>(tempId, tempName));
    }

    map<string, string>::iterator it;

    it = dict.begin();

    while(it != dict.end())
    {
        cout << "��Ʒ��ţ� " << it->first << "\t" << "��Ʒ���ƣ� " << it->second << "\t";
        it++;
        cout << "��Ʒ��λ�� " << it->second << "\t";
        it++;
        cout << "��Ʒ�۸� " << it->second << "\t";
        it++;
        cout << "��Ʒ������ " << it->second << endl;
        it++;
    }

    fin.close();
    return ;

}

//������Ʒ����ʵ��
void FindGoods()
{
    string ID = "0";
    cout << "��������Ʒ���" << endl;
    cin >> ID;
    if (readGoods(ID) == false)
    {
        cout << "���޴���Ʒ��������" << endl;
    }
    else
    {
        printfGoods(ID);//��ӡ��Ʒ��Ϣ
    }

}

//��Ʒ����
void InGoods(string r)
{
    double a = 0;
    string t = "0";
    string Id = "0";
    cout << "�����������Ʒ���" << endl;
    cin >> Id;
    if (readGoods(Id) == false)
    {
        cout << "���޴���Ʒ��������" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //��ӡ��Ʒ��Ϣ
            cout << "���������������" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "����������Ϊ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "������ӦΪ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else
            {
                addQuantity(Id, a);
                cout << "�����ɹ���" << endl;
                system("pause");
                system("cls");
                //��������¼����
                t = GetTime() + "    �������ݣ���Ʒ����    ��Ʒ��ţ�" + Id
                    + "    ��Ʒ���ƣ�" + printfIdName(Id)
                    + "    ����������" + to_string(a)
                    + "    �����ˣ�" + r;
                writeTime(t);
                break;
                return;

            }

        }
            
    }

}
//����
void addQuantity(string Id, int a)
{
    int b = 0;
    string Id___ = Id + "___";
    map<string, string> dict;//��ѯ�ֵ�

    ifstream fin("good.bat", ios::binary);

    char szBuf[25600] = { 0 };
    fin.read(szBuf, sizeof(char) * 25600);

    string tempId;
    string tempName;

    istringstream is(szBuf);
    while (is >> tempId >> tempName)//����������д���ֵ�
    {
        dict.insert(pair<string, string>(tempId, tempName));
    }

    map<string, string>::iterator it;

    it = dict.find(Id___);

    b = atoi((it->second).c_str()) + a;
    it->second = to_string(b);

    fin.close();


}

//��Ʒ����
void OutGoods(string r)
{
    double a = 0;
    string t = "0";
    string Id = "0";
    cout << "������������Ʒ���" << endl;
    cin >> Id;
    if (readGoods(Id) == false)
    {
        cout << "���޴���Ʒ��������" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //��ӡ��Ʒ��Ϣ
            cout << "����������������" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "����������Ϊ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "������ӦΪ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else
            {
                while (1)
                {
                    if (DeQuantity(Id,a)==false)
                    {
                        cout << "��治�㣬����ʧ�ܣ�";
                        system("pause");
                        system("cls");
                        break;
                    }
                    else
                    {
                        cout << "���۳ɹ���" << endl;
                        system("pause");
                        system("cls");
                        //��������¼����
                        t = GetTime() + "    �������ݣ���Ʒ����    ��Ʒ��ţ�" + Id
                        + "    ��Ʒ���ƣ�" + printfIdName(Id)
                        + "    ����������" + to_string(a)
                        + "    �����ˣ�" + r;
                        writeTime(t);   
                        break;
                        return;

                    }
                }
               
                
             
            }

        }

    }

}
//����
bool DeQuantity(string Id, int a)
{
    int b = 0;
    string Id___ = Id + "___";
    map<string, string> dict;//��ѯ�ֵ�

    ifstream fin("good.bat", ios::binary);

    char szBuf[25600] = { 0 };
    fin.read(szBuf, sizeof(char) * 25600);

    string tempId;
    string tempName;

    istringstream is(szBuf);
    while (is >> tempId >> tempName)//����������д���ֵ�
    {
        dict.insert(pair<string, string>(tempId, tempName));
    }

    map<string, string>::iterator it;

    it = dict.find(Id___);

    b = atoi((it->second).c_str()) - a;
    if (b < 0)
    {
        return false;
    }
    else
    {
        it->second = to_string(b);
        fin.close();
        return true;
    }
    
}

//�̵�
void PanDian(string r)
{
    string Id;
    int choice;
    cout << "��������Ʒ���" << endl;
    cin >> Id;
    if (readGoods(Id) == false)
    {
        cout << "���޴���Ʒ��������" << endl;
    }
    else
    {
        cout << "��ѡ���Ӧƽ������� " << endl;
        cout << "1--���    2--����" << endl;
        cin >> choice;
        while (1)
        {
            switch (choice)
            {
                case 1:
                    InStore(Id, r);
                    return;
                    break;
                case 2:
                    OutStore(Id, r);
                    return;
                    break;
                default:
                    cout << "��������������ѡ��" << endl;
                    break;
            }
        }
   


    }

}
//������⺯��
void InStore(string Id, string r)
{
    double a = 0;
    string t = "0";
    if (readGoods(Id) == false)
    {
        cout << "���޴���Ʒ��������" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //��ӡ��Ʒ��Ϣ
            cout << "���������������" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "���������Ϊ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "�����ӦΪ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else
            {
                addQuantity(Id, a);
                cout << "����ɹ���" << endl;
                system("pause");
                system("cls");
                //��������¼����
                t = GetTime() + "    �������ݣ��������    ��Ʒ��ţ�" + Id
                    + "    ��Ʒ���ƣ�" + printfIdName(Id)
                    + "    ����������" + to_string(a)
                    + "    �����ˣ�" + r;
                writeTime(t);
                break;
                return;

            }

        }

    }

}
//���ֳ��⺯��
void OutStore(string Id,string r)
{
    double a = 0;
    string t = "0";
    if (readGoods(Id) == false)
    {
        cout << "���޴���Ʒ��������" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //��ӡ��Ʒ��Ϣ
            cout << "���������������" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "����������Ϊ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "������ӦΪ�����������ԣ�";
                system("pause");
                system("cls");
            }
            else
            {
                while (1)
                {
                    if (DeQuantity(Id, a) == false)
                    {
                        cout << "��治�㣬����ʧ�ܣ�";
                        system("pause");
                        system("cls");
                        break;
                    }
                    else
                    {
                        cout << "����ɹ���" << endl;
                        system("pause");
                        system("cls");
                        //��������¼����
                        t = GetTime() + "    �������ݣ����ֳ���    ��Ʒ��ţ�" + Id
                            + "    ��Ʒ���ƣ�" + printfIdName(Id)
                            + "    ����������" + to_string(a)
                            + "    �����ˣ�" + r;
                        writeTime(t);
                        break;
                        return;

                    }
                }



            }

        }

    }

}


//��ʾ������¼����ʵ��
void Process()
{
    readTime();
}

//�ж������Ƿ�Ϊ����
bool zhengshu(double n)
{
        if (abs(round(n) - n) < 0.000000000000001) {
            //cout << "is an integer" << endl;
            return true;
        }
        else {
            //cout << "not is an integer" << endl;
            return false;
    }
    return 0;
}