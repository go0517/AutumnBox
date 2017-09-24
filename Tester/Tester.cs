﻿using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AutumnBox.Basic.Functions.Event;

namespace Tester
{
    public class Tester:IOutReceiver
    {
        public DeviceSimpleInfo defaultInfo = new DeviceSimpleInfo() { Id = "xxxxx", Status = DeviceStatus.RUNNING };
        public void Run() {
            _Run();
            Pause();
        }
        public void _Run() {
            RunningManager rm = RunningManager.Create(defaultInfo,new TestFunction());
            rm.FuncEvents.OutReceiver = this;
            rm.FuncEvents.Finished += (s, e) => { Print(e.OutputData.ToString()); };
            rm.FuncStart();
        }
        public void Print(string message) {
            Console.WriteLine(message);
        }
        public void Pause() {
            Console.ReadKey();
        }

        public void OutReceived(object sender, DataReceivedEventArgs e)
        {
            Print("RealTime out  : " + e.Data);   
        }

        public void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            Print("RealTime error : " + e.Data);
        }

        public void FuncFinished(object sender, FinishEventArgs e)
        {
            Print(e.Result.IsHandled.ToString());
        }
    }
}
