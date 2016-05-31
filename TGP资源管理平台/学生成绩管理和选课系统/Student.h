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
	char id[20];
	int m_Cnum, //C++课程得分  
		m_Mnum, //汇编课程得分  
		m_Enum, //嵌入式操作系统课程得分  
		m_Sum;	//总分      
	Student * Next;


	/**
	*	接收控制台输入参数
	*/
	void Input(){

		//cin操作符 
		cout << "\t\t请输入学生的姓名：";  cin >> name;
		cout << "\t\t请输入学生的学号：";  cin >> id;
		cout << "\t\t请输入C++课程的成绩：";  cin >> m_Cnum;
		cout << "\t\t请输入汇编课程的成绩：";  cin >> m_Mnum;
		cout << "\t\t请输入嵌入式操作系统课程的成绩：";  cin >> m_Enum;
		m_Sum = m_Cnum + m_Mnum + m_Sum;
	}


};

