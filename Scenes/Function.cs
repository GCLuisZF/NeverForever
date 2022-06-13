#include "Function.h"
#include "Write_Read.h"
#include "Enroll_Login.h"
#include <iostream>
#include <string>
#include <cmath>
using namespace std;

//退出功能实现
void Quit()
{
    cout << "欢迎下一次使用" << endl;
    system("pause");
}

//获取时间功能实现
string GetTime()
{
    time_t timep;
    time(&timep);
    char tmp[256];
    strftime(tmp, sizeof(tmp), "%Y-%m-%d %H:%M:%S", localtime(&timep));
    //cout << tmp << endl;
    return tmp;
}

//展示菜单功能实现
void ShowMenu()
{
    cout << "*********功能良好的进销存管理系统***********" << endl;
    cout << "***************1.新增商品*******************" << endl;
    cout << "***************2.展示商品*******************" << endl;
    cout << "***************3.查找商品*******************" << endl;
    cout << "***************4.商品进货*******************" << endl;
    cout << "***************5.商品销售*******************" << endl;
    cout << "***************6.仓库盘点*******************" << endl;
    cout << "***************7.显示操作记录***************" << endl;
    cout << "***************0.退出进销存系统*************" << endl;
    cout << "*********功能良好的进销存管理系统***********" << endl;
}

//新增商品功能实现
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
        cout << "请输入新商品的编号" << endl;
        cin >> Id;
        if (readGoods(Id) == false)//如果找不到此编号商品，则继续添加
        {
            cout << "请输入新商品的名称" << endl;
            cin >> Name;
            cout << "请输入新商品的单位" << endl;
            cin >> Unit;
            cout << "请输入新商品的价格" << endl;
            cin >> Price;
            while (1)
            {
                cout << "请输入新商品的数量" << endl;
                cin >> quantity;
                if (quantity < 0)
                {
                    cout << "商品数不能为负数，请重试！";
                    system("pause");
                    system("cls");
                }
                else if (zhengshu(quantity) == false)
                {
                    cout << "商品数应为整数，请重试！";
                    system("pause");
                    system("cls");
                }
                else
                {
                    Quantity = to_string(quantity);
                    cout << "添加成功！" << endl;
                    system("pause");
                    system("cls");
                    break;
                }
            }

            break;
        }
        else//否则，说明该编号商品已存在，返回重试
        {
            cout << "该编号商品已存在，请重试" << endl;
            system("pause");
            system("cls");
        }

    }
    writeGoods(Id, Name, Unit, Price, Quantity);//写入商品信息

  //将操作记录储存
    t = GetTime() + "    操作内容：新增商品    商品编号：" + Id 
        +"    商品名称：" + Name
        +"    新增数量："+ Quantity
        +"    操作人："+ r;
    writeTime(t);
}

//展示商品功能实现
void ShowGoods()
{

    map<string, string> dict;//查询字典

    ifstream fin("good.bat", ios::binary);

    char szBuf[25600] = { 0 };
    fin.read(szBuf, sizeof(char) * 25600);

    string tempId;
    string tempName;

    istringstream is(szBuf);
    while (is >> tempId >> tempName)//将所有数据写入字典
    {
        dict.insert(pair<string, string>(tempId, tempName));
    }

    map<string, string>::iterator it;

    it = dict.begin();

    while(it != dict.end())
    {
        cout << "商品编号： " << it->first << "\t" << "商品名称： " << it->second << "\t";
        it++;
        cout << "商品单位： " << it->second << "\t";
        it++;
        cout << "商品价格： " << it->second << "\t";
        it++;
        cout << "商品数量： " << it->second << endl;
        it++;
    }

    fin.close();
    return ;

}

//查找商品功能实现
void FindGoods()
{
    string ID = "0";
    cout << "请输入商品编号" << endl;
    cin >> ID;
    if (readGoods(ID) == false)
    {
        cout << "查无此商品，请重试" << endl;
    }
    else
    {
        printfGoods(ID);//打印商品信息
    }

}

//商品进货
void InGoods(string r)
{
    double a = 0;
    string t = "0";
    string Id = "0";
    cout << "请输入进货商品编号" << endl;
    cin >> Id;
    if (readGoods(Id) == false)
    {
        cout << "查无此商品，请重试" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //打印商品信息
            cout << "请输入进货数量：" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "进货数不能为负数，请重试！";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "进货数应为整数，请重试！";
                system("pause");
                system("cls");
            }
            else
            {
                addQuantity(Id, a);
                cout << "进货成功！" << endl;
                system("pause");
                system("cls");
                //将操作记录储存
                t = GetTime() + "    操作内容：商品进货    商品编号：" + Id
                    + "    商品名称：" + printfIdName(Id)
                    + "    进货数量：" + to_string(a)
                    + "    操作人：" + r;
                writeTime(t);
                break;
                return;

            }

        }
            
    }

}
//进货
void addQuantity(string Id, int a)
{
    int b = 0;
    string Id___ = Id + "___";
    map<string, string> dict;//查询字典

    ifstream fin("good.bat", ios::binary);

    char szBuf[25600] = { 0 };
    fin.read(szBuf, sizeof(char) * 25600);

    string tempId;
    string tempName;

    istringstream is(szBuf);
    while (is >> tempId >> tempName)//将所有数据写入字典
    {
        dict.insert(pair<string, string>(tempId, tempName));
    }

    map<string, string>::iterator it;

    it = dict.find(Id___);

    b = atoi((it->second).c_str()) + a;
    it->second = to_string(b);

    fin.close();


}

//商品销售
void OutGoods(string r)
{
    double a = 0;
    string t = "0";
    string Id = "0";
    cout << "请输入销售商品编号" << endl;
    cin >> Id;
    if (readGoods(Id) == false)
    {
        cout << "查无此商品，请重试" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //打印商品信息
            cout << "请输入销售数量：" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "销售数不能为负数，请重试！";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "销售数应为整数，请重试！";
                system("pause");
                system("cls");
            }
            else
            {
                while (1)
                {
                    if (DeQuantity(Id,a)==false)
                    {
                        cout << "库存不足，操作失败！";
                        system("pause");
                        system("cls");
                        break;
                    }
                    else
                    {
                        cout << "销售成功！" << endl;
                        system("pause");
                        system("cls");
                        //将操作记录储存
                        t = GetTime() + "    操作内容：商品销售    商品编号：" + Id
                        + "    商品名称：" + printfIdName(Id)
                        + "    销售数量：" + to_string(a)
                        + "    操作人：" + r;
                        writeTime(t);   
                        break;
                        return;

                    }
                }
               
                
             
            }

        }

    }

}
//销售
bool DeQuantity(string Id, int a)
{
    int b = 0;
    string Id___ = Id + "___";
    map<string, string> dict;//查询字典

    ifstream fin("good.bat", ios::binary);

    char szBuf[25600] = { 0 };
    fin.read(szBuf, sizeof(char) * 25600);

    string tempId;
    string tempName;

    istringstream is(szBuf);
    while (is >> tempId >> tempName)//将所有数据写入字典
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

//盘点
void PanDian(string r)
{
    string Id;
    int choice;
    cout << "请输入商品编号" << endl;
    cin >> Id;
    if (readGoods(Id) == false)
    {
        cout << "查无此商品，请重试" << endl;
    }
    else
    {
        cout << "请选择对应平库操作： " << endl;
        cout << "1--入库    2--出库" << endl;
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
                    cout << "输入有误，请重新选择！" << endl;
                    break;
            }
        }
   


    }

}
//红字入库函数
void InStore(string Id, string r)
{
    double a = 0;
    string t = "0";
    if (readGoods(Id) == false)
    {
        cout << "查无此商品，请重试" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //打印商品信息
            cout << "请输入入库数量：" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "入库数不能为负数，请重试！";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "入库数应为整数，请重试！";
                system("pause");
                system("cls");
            }
            else
            {
                addQuantity(Id, a);
                cout << "入货成功！" << endl;
                system("pause");
                system("cls");
                //将操作记录储存
                t = GetTime() + "    操作内容：红字入库    商品编号：" + Id
                    + "    商品名称：" + printfIdName(Id)
                    + "    进货数量：" + to_string(a)
                    + "    操作人：" + r;
                writeTime(t);
                break;
                return;

            }

        }

    }

}
//红字出库函数
void OutStore(string Id,string r)
{
    double a = 0;
    string t = "0";
    if (readGoods(Id) == false)
    {
        cout << "查无此商品，请重试" << endl;
    }
    else
    {
        while (1)
        {
            system("cls");
            printfGoods(Id); //打印商品信息
            cout << "请输入出库数量：" << endl;
            cin >> a;
            if (a < 0)
            {
                cout << "出库数不能为负数，请重试！";
                system("pause");
                system("cls");
            }
            else if (zhengshu(a) == false)
            {
                cout << "出库数应为整数，请重试！";
                system("pause");
                system("cls");
            }
            else
            {
                while (1)
                {
                    if (DeQuantity(Id, a) == false)
                    {
                        cout << "库存不足，操作失败！";
                        system("pause");
                        system("cls");
                        break;
                    }
                    else
                    {
                        cout << "出库成功！" << endl;
                        system("pause");
                        system("cls");
                        //将操作记录储存
                        t = GetTime() + "    操作内容：红字出口    商品编号：" + Id
                            + "    商品名称：" + printfIdName(Id)
                            + "    销售数量：" + to_string(a)
                            + "    操作人：" + r;
                        writeTime(t);
                        break;
                        return;

                    }
                }



            }

        }

    }

}


//显示操作记录功能实现
void Process()
{
    readTime();
}

//判断数量是否为整数
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