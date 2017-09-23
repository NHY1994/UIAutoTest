using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIAutoTest
{
    class Pci
    {
        Form1 f1 = new Form1();
        public void Pic()
        {
            MessageBox.Show(System.IO.Path.GetFullPath("../../window_dump.xml"));

            Process MyProcess = new Process();
            //设定程序名 
            MyProcess.StartInfo.FileName = "cmd.exe";
            //关闭Shell的使用 
            MyProcess.StartInfo.UseShellExecute = false;
            //重定向标准输入 
            MyProcess.StartInfo.RedirectStandardInput = true;
            //重定向标准输出 
            MyProcess.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出 
            MyProcess.StartInfo.RedirectStandardError = true;
            //设置不显示窗口 
            MyProcess.StartInfo.CreateNoWindow = true;
            //执行VER命令 
            MyProcess.Start();
            MyProcess.StandardInput.WriteLine("adb shell uiautomator dump");
            Console.WriteLine("adb shell uiautomator dump");
            MyProcess.StandardInput.WriteLine("adb shell /system/bin/screencap -p /sdcard/screenshot.png");
            Console.WriteLine("adb shell /system/bin/screencap -p /sdcard/screenshot.png");
            MyProcess.StandardInput.WriteLine("adb pull /sdcard/window_dump.xml ../../window_dump.xml");
            Console.WriteLine("adb pull /sdcard/window_dump.xml C:\\Users\\NHY\\Desktop");
            MyProcess.StandardInput.WriteLine("adb pull /sdcard/screenshot.png ../../screenshot.png");
            Console.WriteLine("adb pull /sdcard/screenshot.png C:\\Users\\NHY\\Desktop");
            MyProcess.StandardInput.WriteLine("exit");
            Console.WriteLine("end");
            MyProcess.WaitForExit();
            System.Drawing.Image img = System.Drawing.Image.FromFile("../../screenshot.png");
            System.Drawing.Image bmp = new System.Drawing.Bitmap(img);

            f1.pictureBox1.BackgroundImage = bmp;
            //Image image = pictureBox1.Image;
            //pictureBox1.Image = null;
            //image.Dispose();
            img.Dispose();
        }
    }
}
