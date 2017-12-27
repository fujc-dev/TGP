
	

	项目结构(持续增加中...)：
		MIS.ClientUI							//界面部分(程序界面，提示信息(弹出提示，右下角Tips提示等)
		MIS.YTIM								//即时通信部分(涉及到界面相关以及业务逻辑都会封装到此类库中，http://yanshi.sucaihuo.com/jquery/6/669/demo/)
		MIS.Foundation.Framework		//基础框架
		MIS.DB									//数据库处理部分(如果条件可行，会做一个Nhibernate、EF、ADO.NET等多库切换的封装处理)
		MIS.AutoUpdate						//更新程序，与服务器连接，并下载最新的DLL文件


			MIS.Foundation.Framework侵入式框架：
				顶部模块加载
				左侧导航加载
				Main内容部分加载方式
				
				核心：
					1、动态加载插件（采用OSGI.NET，源码参见https://github.com/FreezeSoul/OSGi.NET） 
						注：OSGi(Open Service Gateway Initiative)技术是Java动态化模块化系统的一系列规范。
					2、DLL版本控制
					3、即时通信Socket封装处理(采用SuperSocket框架来实现即时通信)
					4、LocalMessagePusher(在框架类封装一个消息队列，用于生产者和消费者之间的通信的场景，暂时未定技术)
					5、日志(Log4Net) 
					6、AOP权限，日志，运行记录等(准备使用企业库)


	
