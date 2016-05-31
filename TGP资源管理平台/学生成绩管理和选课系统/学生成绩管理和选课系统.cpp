// 学生成绩管理和选课系统.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "iostream" //使用cout函数 需要引用iostream库函数
#include "Student.h"

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int x, i = 0;
	bool quit = false;
	cout << "\t\t§§§§§§§§§§§§§§§§§§§§§§§§§§" << endl;
	for (i = 0; i<3; i++)
		cout << "\t\t◎\t\t\t\t\t\t  ◎" << endl;
	cout << "\t\t◎★★★★【  欢迎进入学生成绩管理系统  】★★★★◎" << endl;
	for (i = 0; i<3; i++)
		cout << "\t\t◎\t\t\t\t\t\t  ◎" << endl;
	cout << "\t\t§§§§§§§§§§§§§§§§§§§§§§§§§§\n" << endl;
	Studentmassage Grade;
	cout << "按任意键开始……";
	getch();
	while (!quit)
	{
		system("cls");
		Grade.ShowMenu();
		cin >> x;
		switch (x)
		{
		case 0:quit = true; break;
		case 1:Grade.AddItem(); break;
		case 2:Grade.Display(); break;
		case 3:Grade.Sort(); break;
		case 4:Grade.Find(); break;
		case 5:Grade.RemoveItem(); break;
		case 6:Grade.ModifyItem(); break;
		}
	}
	return 0;
}

