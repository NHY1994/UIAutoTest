using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UIAutoTest
{
    public partial class Form1 : Form
    {
        int name = 0;
        private Point p1, p2;//橡皮筋直线
        private List<drawtype> ls = new List<drawtype>();//橡皮筋直线
        public Bitmap image = null;//橡皮筋直线
        //---------------------------------------------------//
        int x = 0;
        FileStream file, source, source1;
        string buildPath, jarPath, projectPath, gradlePath;
        string path = "../../window_dump.xml";
        int area = 258 * 459,area1;
        int areaX, areaY, areaWhith, areaHight;
        int mouseX, mouseY,phoneX, phoneY;
        int startX, startY, endX, endY;//橡皮筋坐标
        int i=1;
        string[] attributeName = new string[7];
        string[] attributeValue = new string[7];
        ToolStripMenuItem click = new ToolStripMenuItem("点击");
        ToolStripMenuItem Lclick = new ToolStripMenuItem("长按");
        ToolStripMenuItem isExist = new ToolStripMenuItem("是否存在");

        Pen line = new Pen(Color.Red,2);
        public Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
            Pen p = new Pen(Color.Red, 1);
            g = pictureBox1.CreateGraphics();
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            //label1.Text = doc.InnerXml;
            //Regex rg = new Regex("node ");
            //MatchCollection mc = rg.Matches(label1.Text);
            //label2.Text = mc.Count.ToString();
            //String
            XmlNodeList xnl = doc.SelectNodes("//node");
            int x = 0, y = 0, x1 = 0, y1 = 0;
            foreach (XmlNode xn in xnl)
            {
                attributeName[1] = "index";
                attributeName[2] = "text";
                attributeName[3] = "resource-id";
                attributeName[4] = "class";
                attributeName[5] = "package";
                attributeName[6] = "content-desc";
                attributeName[0] = "坐标";
                attributeValue[1] = xn.Attributes["index"].Value;
                attributeValue[2] = xn.Attributes["text"].Value;
                attributeValue[3] = xn.Attributes["resource-id"].Value;
                attributeValue[4] = xn.Attributes["class"].Value;
                attributeValue[5] = xn.Attributes["package"].Value;
                attributeValue[6] = xn.Attributes["content-desc"].Value;
                attributeValue[0] = "X:"+ phoneX.ToString()+"Y:"+ phoneY.ToString();
                label3.Text += xn.Attributes["bounds"].Value + ";";
                Regex regex = new Regex("\\d+\\.?\\d*");
                Match match = regex.Match(xn.Attributes["bounds"].Value);
                x = (int)Math.Ceiling(int.Parse(match.ToString()) / 4.1860465116);
                y = (int)Math.Ceiling(int.Parse(match.NextMatch().ToString()) / 4.1860465116);
                x1 = (int)Math.Ceiling(int.Parse(match.NextMatch().NextMatch().ToString()) / 4.1860465116);
                y1 = (int)Math.Ceiling(int.Parse(match.NextMatch().NextMatch().NextMatch().ToString()) / 4.1860465116);

                area1 = (x1 - x - 2) * (y1 - y - 2);
                
                if (mouseX > x && mouseX < x1 && mouseY > y && mouseY < y1 && area1<=area)
                {
                    label6.Text = x.ToString() + "-" + y.ToString() + ";" + x1.ToString() + "-" + y1.ToString();
                    // pictureBox1.Refresh();
                    //g.DrawRectangle(p, x, y, x1 - x - 2, y1 - y - 2);
                    areaX = x;
                    areaY = y;
                    areaWhith = x1 - x - 2;
                    areaHight = y1 - y - 2;
                    // MessageBox.Show("123");
                    //   label5.Text = x.ToString() + "+" + x1.ToString();
                    listBox1.Items.Clear();
                    click.DropDownItems.Clear();
                    Lclick.DropDownItems.Clear();
                    isExist.DropDownItems.Clear();
                    for (int i = 0; i <= 6; i++)
                    {
                        listBox1.Items.Add(attributeName[i] + "---" + attributeValue[i]);
                        click.DropDownItems.Add(attributeName[i] + "~" + attributeValue[i]);
                        this.contextMenuStrip1.Items.Add(click);
                        Lclick.DropDownItems.Add(attributeName[i] + "~" + attributeValue[i]);
                        this.contextMenuStrip1.Items.Add(Lclick);
                        isExist.DropDownItems.Add(attributeName[i] + "~" + attributeValue[i]);
                        this.contextMenuStrip1.Items.Add(isExist);
                    }
                }
                else
                    area = area1;
            }
            pictureBox1.Refresh();
            g.DrawRectangle(p, areaX, areaY, areaWhith, areaHight);
            label2.Text = areaX.ToString()+"-"+areaY.ToString()+"-"+areaWhith.ToString()+"-"+areaHight.ToString();
        }
        private void Click_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            contextMenuStrip1.Close();
            //button1.Text = e.ClickedItem.Text;
            MessageBox.Show(e.ClickedItem.ToString());
            //throw new NotImplementedException();
            string st = e.ClickedItem.ToString();
            string[] sArray = st.Split('~');
            if (sArray[0].Equals("index"))
            {
                richTextBox1.AppendText("UiObject ub = new UiObject(new UiSelector().index(\"" + int.Parse(sArray[1]) + "\"); " + "\r\n");
                richTextBox1.AppendText("ub.click();");
            }
            else if (sArray[0].Equals("text"))
            {
                richTextBox1.AppendText("UiObject ub = new UiObject(new UiSelector().text(\"" + sArray[1] + "\"); " + "\r\n");
                richTextBox1.AppendText("ub.click();");
            }
            else if (sArray[0].Equals("resource-id"))
            {

            }
            else if (sArray[0].Equals("class"))
            {

            }
            else if (sArray[0].Equals("package"))
            {

            }
            else if (sArray[0].Equals("坐标"))
            {
                richTextBox1.AppendText("device.click(" + phoneX.ToString() + "," + phoneY.ToString()+");" + "\r\n");
            }

            //richTextBox1.AppendText(sArray[0] + "+" + sArray[1] + "\r\n");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Pci pic1 = new Pci();
            pic1.Pic();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Enabled = true;
            System.Drawing.Image img = System.Drawing.Image.FromFile("../../screenshot.png");
            System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
            pictureBox1.Image = null;
            pictureBox1.BackgroundImage = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gradlePath = System.IO.Path.GetFullPath("../../ant/ant/gradle-2.10/bin");
            projectPath = System.IO.Path.GetFullPath("../../ant/ant/MX6-6.0");
            buildPath = System.IO.Path.GetFullPath("../../uiautotest//build.xml");
            jarPath = System.IO.Path.GetFullPath("../../uiautotest//bin//test1.jar");
            MessageBox.Show(projectPath + "\n" + buildPath + "\n" + jarPath);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point poin = pictureBox1.PointToClient(Control.MousePosition);
            mouseX= poin.X;
            mouseY = poin.Y;
            phoneX = (int)Math.Ceiling(mouseX * 4.1860465116);
            phoneY = (int)Math.Ceiling(mouseY * 4.1860465116);
            label1.Text = "X:" + phoneX.ToString() + "Y:" + phoneY.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            click.DropDownItemClicked += Click_DropDownItemClicked;
            Lclick.DropDownItemClicked += Lclick_DropDownItemClicked;
            isExist.DropDownItemClicked += IsExist_DropDownItemClicked;
            //-------------橡皮筋直线 开始-------------------
            this.pictureBox1.Controls.Clear();

            image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);

            Graphics.FromImage(image).Clear(Color.White);//消除底图的黑色

            //pictureBox1.BackgroundImage = (Bitmap)image.Clone();//这句话是关键
            //---------------------橡皮筋直线结束------------
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string str1 = projectPath.Substring(0, 1);
            //string cmdtext = textBox1.Text;
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
            MyProcess.StandardInput.WriteLine("set path=" + gradlePath + "");
            MyProcess.StandardInput.WriteLine("echo 123");
            MyProcess.StandardInput.WriteLine("set path=E:\\UIAutoTest\\UIAutoTest\\UIAutoTest\\ant\\ant\\adb;%path%");
            MyProcess.StandardInput.WriteLine("cd E:\\UIAutoTest\\UIAutoTest\\UIAutoTest\\ant\\ant\\MX6-6.0");
            MyProcess.StandardInput.WriteLine("gradle assembleDebugAndroidTest app:assembleDebug");
            MyProcess.StandardInput.WriteLine("adb install -r E:\\UIAutoTest\\UIAutoTest\\UIAutoTest\\ant\\ant\\MX6-6.0\\app\\build\\outputs\\apk\\app-debug.apk");
            MyProcess.StandardInput.WriteLine("adb install -r E:\\UIAutoTest\\UIAutoTest\\UIAutoTest\\ant\\ant\\MX6-6.0\\app\\build\\outputs\\apk\\app-debug-androidTest-unaligned.apk");
            MyProcess.StandardInput.WriteLine("adb shell am instrument -w -r   -e debug false -e class com.example.nhy.mx6_60.ApplicationTest com.example.nhy.mx6_60.test/android.support.test.runner.AndroidJUnitRunner");
            MyProcess.StandardInput.WriteLine("exit");
            Console.WriteLine("end");
            //从输出流获取命令执行结果， 
            string exepath = Application.StartupPath;
            //把返回的DOS信息读出来 
            String StrInfo = MyProcess.StandardOutput.ReadToEnd();
            //textBox2.Text = StrInfo;
            Console.WriteLine(StrInfo);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists("../../ant/ant/MX6-6.0/app/src/androidTest/java/com/example/nhy/mx6_60/ApplicationTest.java"))
            {
                //source=new FileStream("../../source.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //source1 = new FileStream("../../source1.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file = new FileStream("../../ant/ant/MX6-6.0/app/src/androidTest/java/com/example/nhy/mx6_60/ApplicationTest.java", FileMode.Create, FileAccess.ReadWrite);
                //File.WriteAllText("../../add.java", null);
                string text = File.ReadAllText(@"../../source1.txt");
                string text1 = File.ReadAllText(@"../../source2.txt");
                //MessageBox.Show(text);
                StreamWriter sw = new StreamWriter(file);
                sw.Write(text + "\r\n");
                for (int i = 0; i < richTextBox1.Lines.Length; i++)
                {
                    sw.WriteLine(richTextBox1.Lines[i]);
                }
                sw.Write(text1);
                sw.Flush();
                sw.Close();
                //File.WriteAllText("../../add.java", richTextBox1.Lines.Length.ToString());
                MessageBox.Show(richTextBox1.Lines[0]);
            }
            else
            {
                file = new FileStream("../../add.java", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            p1 = e.Location;
            startX = mouseX;
            startY = mouseY;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"../../loading.gif");
            backgroundWorker1.RunWorkerAsync();
            //Pci pic1 = new Pci();
            //Thread pic = new Thread(pic1.Pic);
            //pic.Start();
            //Task ts = new Task(pic1.Pic);
            //ts.Start();
            //ts.Wait();
           // System.Drawing.Image img = System.Drawing.Image.FromFile("../../screenshot.png");
            //System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
            //pictureBox1.BackgroundImage = bmp;
            //MessageBox.Show(System.IO.Path.GetFullPath("../../window_dump.xml"));

            //Process MyProcess = new Process();
            ////设定程序名 
            //MyProcess.StartInfo.FileName = "cmd.exe";
            ////关闭Shell的使用 
            //MyProcess.StartInfo.UseShellExecute = false;
            ////重定向标准输入 
            //MyProcess.StartInfo.RedirectStandardInput = true;
            ////重定向标准输出 
            //MyProcess.StartInfo.RedirectStandardOutput = true;
            ////重定向错误输出 
            //MyProcess.StartInfo.RedirectStandardError = true;
            ////设置不显示窗口 
            //MyProcess.StartInfo.CreateNoWindow = true;
            ////执行VER命令 
            //MyProcess.Start();
            //MyProcess.StandardInput.WriteLine("adb shell uiautomator dump");
            //Console.WriteLine("adb shell uiautomator dump");
            //MyProcess.StandardInput.WriteLine("adb shell /system/bin/screencap -p /sdcard/screenshot.png");
            //Console.WriteLine("adb shell /system/bin/screencap -p /sdcard/screenshot.png");
            //MyProcess.StandardInput.WriteLine("adb pull /sdcard/window_dump.xml ../../window_dump.xml");
            //Console.WriteLine("adb pull /sdcard/window_dump.xml C:\\Users\\NHY\\Desktop");
            //MyProcess.StandardInput.WriteLine("adb pull /sdcard/screenshot.png ../../screenshot.png");
            //Console.WriteLine("adb pull /sdcard/screenshot.png C:\\Users\\NHY\\Desktop");
            //MyProcess.StandardInput.WriteLine("exit");
            //Console.WriteLine("end");
            //MyProcess.WaitForExit();
            //System.Drawing.Image img = System.Drawing.Image.FromFile("../../screenshot.png");
            //System.Drawing.Image bmp = new System.Drawing.Bitmap(img);

            //pictureBox1.BackgroundImage = bmp;
            ////Image image = pictureBox1.Image;
            ////pictureBox1.Image = null;
            ////image.Dispose();
            //img.Dispose();

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            //pictureBox1.Refresh();
            endX = mouseX;
            endY = mouseY;
            if (Math.Abs(startX -endX)<=1 && Math.Abs(startY - endY)<=1)
            {
                
            }
            else
            {
                MessageBox.Show("开始：" + startX.ToString() + "---" + startY.ToString() + "结束：" + endX.ToString() + "---" + endY.ToString());
            }
            
        }
        private void chonghui()

        {

            Graphics g = pictureBox1.CreateGraphics();

            foreach (drawtype t in ls)

            {

                //画出原有的每一条线

                g.DrawLine(new Pen(Color.Red, 2), t.p1, t.p2);

            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();

            chonghui();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void IsExist_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            contextMenuStrip1.Close();
            //button1.Text = e.ClickedItem.Text;
            
            MessageBox.Show(e.ClickedItem.ToString() + "E");
            string st = e.ClickedItem.ToString();
            string[] sArray = st.Split('~');
            if (sArray[0].Equals("index"))
            {

            }
            else if (sArray[0].Equals("text"))
            {
                richTextBox1.AppendText("UiObject ub = new UiObject(new UiSelector().text(\"" + sArray[1] + "\"); " + "\r\n");
                richTextBox1.AppendText("ub.click();");
            }
            else if (sArray[0].Equals("resource-id"))
            {

            }
            else if (sArray[0].Equals("class"))
            {

            }
            else if (sArray[0].Equals("package"))
            {
                
            }

            richTextBox1.AppendText(sArray[0]+"+"+sArray[1]+"\r\n");
        }

        private void Lclick_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            contextMenuStrip1.Close();
            //button1.Text = e.ClickedItem.Text;
            MessageBox.Show(e.ClickedItem.ToString()+"L");
            richTextBox1.AppendText(e.ClickedItem.ToString() + "\r\n");
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            DoubleBuffered = true;
            label4.Text = i++.ToString();
            label1.ForeColor = Color.Black;
            timer1.Start();
            //Invalidate(true);
            //panel3.Invalidate(true);

            //g.Dispose();
            //g = panel3.CreateGraphics();
            // g.DrawLine(line, 10, 10, mouseX, mouseY);
            //line.Dispose();
            //-------------------------------------------//
            Graphics g = pictureBox1.CreateGraphics();

            //如果鼠标左键一直接下

            if (e.Button == MouseButtons.Left)

            {

                //开始画白线(与背景色一样的线,用来擦除鼠标移动时产生的线)
                pictureBox1.Refresh();
                //g.DrawLine(new Pen(Color.White, 2), p1, p2);

                //获得鼠标在窗体中按下并移动时的坐标点

                p2 = e.Location;

                //在窗体中画出的线

                g.DrawLine(new Pen(Color.Red, 2), p1, p2);

                //使用枚举将所有画过的线重绘(为了防止新画的线的白色会将原有线更改)

                chonghui();
            }

        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            label1.Text = "离线状态";
            label1.ForeColor = Color.Red;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.Red, 1);
            Graphics g = pictureBox1.CreateGraphics();
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            //label1.Text = doc.InnerXml;
            Regex rg = new Regex("node ");
            //MatchCollection mc = rg.Matches(label1.Text);
            //label2.Text =mc.Count.ToString();
            //String
            XmlNodeList xnl = doc.SelectNodes("//node");
            foreach (XmlNode xn in xnl)
            {
                label3.Text += xn.Attributes["bounds"].Value+";";
                Regex regex = new Regex("\\d+\\.?\\d*");
                Match match = regex.Match(xn.Attributes["bounds"].Value);
                int x = (int)Math.Ceiling(int.Parse(match.ToString()) / 4.1860465116);
                int y = (int)Math.Ceiling(int.Parse(match.NextMatch().ToString()) / 4.1860465116);
                int x1 = (int)Math.Ceiling(int.Parse(match.NextMatch().NextMatch().ToString()) / 4.1860465116);
                int y1 = (int)Math.Ceiling(int.Parse(match.NextMatch().NextMatch().NextMatch().ToString()) / 4.1860465116);
                g.DrawRectangle(p,x,y,x1-x-2,y1-y-2);
            }
        }
    }
    //-----------------------橡皮筋直线----------------
    class drawtype

    {

        //定义两个点

        public Point p1, p2;

        //定义构造方法(两个点为参数)

        public drawtype(Point pp, Point pp2)

        {

            p1 = pp;

            p2 = pp2;

        }
        //------------------橡皮筋直线-----------------
    }
}
