using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TGP.CarouselDemo
{
    /// <summary>
    /// 定时器处理
    /// </summary>
    public sealed class CarouselTimer
    {
        private Timer mTimer = null;
        private static Dictionary<String, CarouselTimer> container = new Dictionary<string, CarouselTimer>();
        private static String CurrentTimerID = null;
        private static Object lockObj = new Object();


        /// <summary>
        /// 构造函数，创建<see cref="CarouselTimer"/>类实例
        /// </summary>
        /// <param name="interval">调用 callback 的时间间隔（以毫秒为单位）</param>
        /// <param name="callback">一个 TimerCallback 委托，表示要执行的方法。</param>
        /// <param name="obj">一个包含回调方法要使用的信息的对象</param>
        private CarouselTimer(Int32 interval, TimerCallback callback, Object obj)
        {
            mTimer = new Timer(callback, obj, 500, interval);
        }

        /// <summary>
        /// 启动一个定时器，并返回一个定时器编号
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="interval"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String Start<T>(Int32 interval, TimerCallback callback, T obj)
        {
            lock (lockObj)
            {
                CarouselTimer carousel = new CarouselTimer(interval, callback, obj);
                String timerID = Guid.NewGuid().ToString("N").ToUpper();
                CurrentTimerID = timerID;
                container.Add(timerID, carousel);
                return timerID;
            }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public static void Stop()
        {
            if (String.IsNullOrWhiteSpace(CurrentTimerID)) throw new ArgumentNullException("当前未开启任何定时器");
            Stop(CurrentTimerID);
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        /// <param name="timerID">定时器编号</param>
        public static void Stop(String timerID)
        {
            CarouselTimer tiemr = null;
            if (container.TryGetValue(timerID, out tiemr))
            {
                tiemr.mTimer.Dispose();
                tiemr = null;
            }
        }
    }
}
