/*******************************************************************
*	Copyright(c) 2000-2013 Company Microsoft All rights reserved.
*
*	文件名称:
*	简要描述:
*
*	当前版本:2.0
*	作者:
*	日期:
*	说明:
*
*	取代版本:1.0
*	作者:
*	日期:
*	说明:
******************************************************************/

#pragma once
#include <iostream>
#include <fstream>  
#include <string.h>  
#include <conio.h>//用getch();  

using namespace std;


class Student
{
public:

#pragma region 构造函数与析构函数
	Student();
	~Student();
#pragma endregion

	// 成员变量
	char name[20];
	char Id[20];
	int Cnum, //C++课程得分  
		Mnum, //汇编课程得分  
		Enum, //嵌入式操作系统课程得分  
		sum;	//总分      
	Student * Next;


	/**
	*	接收控制台输入参数
	*/
	void Input(){

		//cin操作符 
		cout << "\t\t请输入学生的姓名：";  cin >> name;
		cout << "\t\t请输入学生的学号：";  cin >> Id;
		cout << "\t\t请输入C++课程的成绩：";  cin >> Cnum;
		cout << "\t\t请输入汇编课程的成绩：";  cin >> Mnum;
		cout << "\t\t请输入嵌入式操作系统课程的成绩：";  cin >> Enum;
		sum = Cnum + Mnum + Enum;
	}

	void ReadFile(istream & in){
		in >> name >> Id >> Cnum >> Mnum >> Enum >> sum;
	}

	void Show()
	{
		cout << "姓名:" << name << endl
			<< "学号:" << Id << endl
			<< "C++:" << Cnum << endl
			<< "汇编:" << Mnum << endl
			<< "嵌入式操作系统：" << Enum << endl
			<< "总成绩:" << sum << endl << endl << endl;
	}

};

