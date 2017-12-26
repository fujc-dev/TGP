
	
	项目结构：
		MIS.ClientUI							//主界面
		MIS.YTIM								//即时通信部分(涉及到界面相关以及业务逻辑都会封装到此类库中)
		MIS.Foundation.Framework		//基础框架
		MIS.DB									//数据库处理部分(如果条件可行，会做一个Nhibernate、EF、ADO.NET等多库切换的封装处理)


			MIS.Foundation.Framework侵入式框架：
				顶部模块加载
				左侧导航加载
				Main内容部分
				提示信息(弹出提示，右下角Tips提示等)
				核心：

