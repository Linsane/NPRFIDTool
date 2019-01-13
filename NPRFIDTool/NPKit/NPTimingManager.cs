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
        public Timer ioStoreTimer; // 出入库读取时间上限

        public ElapsedEventHandler readPortTimesUpHandler;
        public ElapsedEventHandler scanCycleStartHandler;
        public ElapsedEventHandler analyzeCycleStartHandler;
        public ElapsedEventHandler ioStoreTimeOutHandler;

        // 单位统一为秒(s)
        public NPTimingManager(int readPortTime, int scanCycleTime, int analyzeCycleTime)
        {
            readPortTimer = new Timer(readPortTime * 1000);
            readPortTimer.AutoReset = false;
            readPortTimer.Elapsed += (src,e) =>
            {
                readPortTimesUpHandler(src, e);
            };

            scanCycleTimer = new Timer(scanCycleTime * 1000);
            scanCycleTimer.AutoReset = true;
            scanCycleTimer.Elapsed += (src, e) =>
            {
                readPortTimer.Enabled = true;
                scanCycleStartHandler(src, e);
            };

            analyzeCycleTimer = new Timer(analyzeCycleTime * 1000);
            analyzeCycleTimer.AutoReset = true;
            analyzeCycleTimer.Elapsed += (src, e) =>
            {
                analyzeCycleStartHandler(src, e);
            };

            ioStoreTimer = new Timer(10 * 60 * 1000);
            ioStoreTimer.AutoReset = false;
            ioStoreTimer.Elapsed += (src, e) =>
            {
                ioStoreTimeOutHandler(src, e);
            };
        }

        public void startCycles()
        {
            scanCycleTimer.Enabled = true;
            analyzeCycleTimer.Enabled = true;
        }

        public void stopCycles()
        {
            scanCycleTimer.Enabled = false;
            analyzeCycleTimer.Enabled = false;
            readPortTimer.Enabled = false;
        }

        public void startIOStoreTimer()
        {
            ioStoreTimer.Stop();
            ioStoreTimer.Enabled = true;
        }

    }
}
