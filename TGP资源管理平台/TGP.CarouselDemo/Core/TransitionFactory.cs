using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Transitionals;

namespace TGP.CarouselDemo
{
    /// <summary>
    /// 过渡动画处理工厂类
    /// </summary>
    internal class TransitionFactory
    {
        private static ObservableCollection<Type> transitionTypes = new ObservableCollection<Type>();
        private static TransitionFactory instance = null;
        private static Object lockObj = new Object();
        private TransitionFactory()
        {
            this.LoadTransitions(Assembly.GetAssembly(typeof(Transition)));
        }

        /// <summary>
        /// 加载动画
        /// </summary>
        /// <param name="assembly"></param>
        private void LoadTransitions(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                // 已存在
                if (transitionTypes.Contains(type)) { continue; }
                // 非抽象类
                if ((typeof(Transition).IsAssignableFrom(type)) && (!type.IsAbstract))
                {
                    transitionTypes.Add(type);
                }
            }
        }

        /// <summary>
        /// 获取过渡动画处理工厂类<see cref="TransitionFactory"/>实例
        /// </summary>
        public static TransitionFactory Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null) instance = new TransitionFactory();
                    return instance;
                }
            }
        }

        /// <summary>
        /// 获取过渡动画总数
        /// </summary>
        /// <returns></returns>
        public Int32 GetTransitionCount()
        {
            return transitionTypes.Count;
        }

        /// <summary>
        /// 获取随机动画
        /// </summary>
        /// <returns></returns>
        public Transition GetRandomTransition()
        {
            //图片显示动画 随机动画
            int m_nIndex = 0;
            m_nIndex = new Random().Next(0, transitionTypes.Count);
            //获取当前动画
            Type transitionType = transitionTypes[m_nIndex];
            return (Transition)Activator.CreateInstance(transitionType);
        }

        /// <summary>
        /// 获取指定动画，当未获取到时返回默认动画
        /// </summary>
        /// <param name="t">动画类型</param>
        /// <returns></returns>
        public Transition GetAppointTransition(Type t)
        {
            return (Transition)Activator.CreateInstance(transitionTypes.FirstOrDefault((o) => o == t));
        }
    }
}
