using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NPRFIDTool.NPKit
{
    class NPTimingManager
    {
        public Timer readPortTimer; // 读盘点端口的时长
        public Timer scanCycleTimer; // 定时扫描的周期
        public Timer analyzeCycleTimer; // 盘点结果周期

        public ElapsedEventHandler readPortTimesUpHandler;
        public ElapsedEventHandler scanCycleStartHandler;
        public ElapsedEventHandler analyzeCycleStartHandler;

        // 单位统一为秒(s)
        public NPTimingManager(int readPortTime, int scanCycleTime, int analyzeCycleTime)
        {
            readPortTimer = new Timer(readPortTime * 1000);
            readPortTimer.AutoReset = false;
            readPortTimer.Elapsed += (src,ee) =>
            {
                readPortTimesUpHandler(src, ee);
            };

            scanCycleTimer = new Timer(scanCycleTime * 60 * 1000);
            scanCycleTimer.AutoReset = true;
            scanCycleTimer.Elapsed += scanCycleStartHandler;

            analyzeCycleTimer = new Timer(analyzeCycleTime * 60 * 1000);
            analyzeCycleTimer.AutoReset = true;
            analyzeCycleTimer.Elapsed += analyzeCycleStartHandler;
        }

        public void startCycles()
        {
            readPortTimer.Enabled = true;
        }
    }
}
