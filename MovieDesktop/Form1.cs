using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

/**
 * 动态桌面
 * 作者：democyann
 * url:https://democyann.moe/about/
 * github:https://github.com/democyann
 * */
namespace MovieDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern long SetParent(long hWndChild, long hWndNewParent);
        [DllImport("user32.dll")]
        public static extern long SetWindowPos(long hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);
        [DllImport("user32.dll")]
        public static extern long SetLayeredWindowAttributes(long hwnd, long crKey, byte bAlpha, long dwFlags);
        [DllImport("user32.dll")]
        public static extern long GetWindowLong(long hwnd, long nIndex);
        [DllImport("user32.dll")]
        public static extern long SetWindowLong(long hwnd, long nIndex, long dwNewLong);
        [DllImport("user32.dll")]
        public static extern long FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern long FindWindowEx(long hWnd1, long hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        public static extern long SetCapture(long hwnd);
        [DllImport("user32.dll")]
        public static extern long PostMessage(long hWnd, uint Msg, long wParam, long lParam);
        [DllImport("user32.dll")]
        public static extern long SendMessage(long hwnd, long wMsg, long wParam, long lParam);

        private const long WS_EX_LAYERED = 0x80000;
        private const long GWL_EXSTYLE = -20;
        private const long LWA_ALPHA = 0x2;
        private const long LWA_COLORKEY = 0x1;
        private const long WS_EX_TRANSPARENT = 0x20;


        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;
        public const uint WM_RBUTTONDOWN = 0x0204;
        public const uint WM_RBUTTONUP = 0x0205;
        public const uint WM_MOUSEMOVE = 0x0200;

        public const long WM_SETCURSOR = 0x0020;
        public const long WM_PARENTNOTIFY = 0x0210;



        public long h1, h2, h3, rba;
        public int typ = -1;

        private void Form1_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.uiMode = "None";
            axWindowsMediaPlayer1.enableContextMenu = false;
            axWindowsMediaPlayer1.settings.autoStart = false;
            //AxWindowsMediaPlayer1.settings.playCount = -1;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.settings.volume = 100;
            Size s = Screen.PrimaryScreen.Bounds.Size;
            //s.Width += 10;
            this.Size = s;
            axWindowsMediaPlayer1.Size = s;
            axWindowsMediaPlayer1.URL = "F:\\Programtempfile\\1.wpl";
            axWindowsMediaPlayer1.Ctlcontrols.play();
            h1 = 0;
            int cont = 0;
            while (h2 == 0)
            {
                if (cont >= 100)
                {
                    MessageBox.Show("未找到桌面！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    h2 = 1;
                    Application.Exit();
                }
                cont++;
                h1 = FindWindowEx(0, h1, "WorkerW", null); //获得第一个WorkerW类的窗口
                h2 = FindWindowEx(h1, 0, "SHELLDLL_DefView", null);
                //Debug.WriteLine(h1 & " " & h2)
                if (h2 == 0)
                    continue;
                h3 = FindWindowEx(h2, 0, null, "folderview");
            }
            axWindowsMediaPlayer1.Ctlcontrols.play();
            SetParent(h2, this.Handle.ToInt64());

            long rtn;
            rtn = GetWindowLong(h2, GWL_EXSTYLE);
            rba = rtn;
            //rtn = rtn | WS_EX_LAYERED | WS_EX_TRANSPARENT;
            rtn = rtn | WS_EX_LAYERED;
            SetWindowLong(h2, GWL_EXSTYLE, rtn);
            SetLayeredWindowAttributes(h2, 0, 10, LWA_COLORKEY);
            //SetCapture(h2);
            //SetWindowPos(h1, 1, 30, 30, s.Width, s.Height, 0);
            SetParent(this.Handle.ToInt64(), h1);

            SetWindowPos(this.Handle.ToInt64(), 1, 0, 0, 0, 0, 0);
            //this.FormBorderStyle = FormBorderStyle.None;
        }

        private void 下一桌面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.next();
        }

        private void voiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (voiceToolStripMenuItem.Text == "静音")
            {
                axWindowsMediaPlayer1.settings.mute = true;
                voiceToolStripMenuItem.Text = "恢复音量";
            }
            else
            {
                voiceToolStripMenuItem.Text = "静音";
                axWindowsMediaPlayer1.settings.mute = false;
            }
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            SetLayeredWindowAttributes(h2, 0, 255, LWA_COLORKEY);
            SetWindowLong(h2, GWL_EXSTYLE, rba);
            SetParent(h2, h1);
            开始ToolStripMenuItem.Enabled = true;
            停止ToolStripMenuItem.Enabled = false;
            下一桌面ToolStripMenuItem.Enabled = false;
            上一桌面ToolStripMenuItem.Enabled = false;
            暂停ToolStripMenuItem.Enabled = false;
            this.Hide();
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            axWindowsMediaPlayer1.Ctlcontrols.play();
            SetParent(h2, this.Handle.ToInt64());
            long rtn;
            rtn = GetWindowLong(h2, GWL_EXSTYLE);
            //rba = rtn;
            rtn = rtn | WS_EX_LAYERED;
            SetWindowLong(h2, GWL_EXSTYLE, rtn);
            SetLayeredWindowAttributes(h2, 0, 10, LWA_COLORKEY);
            //SetParent(Me.Handle, h1);
            //SetWindowPos(this.Handle.ToInt64(), 1, 0, 0, 0, 0, 0);
            停止ToolStripMenuItem.Enabled = true;
            开始ToolStripMenuItem.Enabled = false;
            下一桌面ToolStripMenuItem.Enabled = true;
            上一桌面ToolStripMenuItem.Enabled = true;
            暂停ToolStripMenuItem.Enabled = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLayeredWindowAttributes(h2, 0, 255, LWA_COLORKEY);
            SetWindowLong(h2, GWL_EXSTYLE, rba);
            SetParent(h2, h1);
            this.Close();
        }

        private void 上一桌面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.previous();
        }

        /**
         * 获取按键抬起动作，透传至底层窗体
         * */
        private void axWindowsMediaPlayer1_MouseUpEvent(object sender, AxWMPLib._WMPOCXEvents_MouseUpEvent e)
        {
            if (e.nButton == 1)
            {
                PostMessage(h2, WM_LBUTTONUP, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                typ = 1;
                //PostMessage(h2, WM_LBUTTONUP, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_PARENTNOTIFY, WM_LBUTTONUP, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_SETCURSOR, h3, (WM_LBUTTONUP<<16) |0x0001 );
            }
            else
            {
                PostMessage(h2, WM_RBUTTONUP, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                typ = 2;
                //PostMessage(h2, WM_RBUTTONUP, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_PARENTNOTIFY, WM_RBUTTONUP, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_SETCURSOR, h3, (WM_RBUTTONUP<<16) |0x0001);
            }
        }

        /**
         * 获取按键按下动作，透传至底层窗体
         * */
        private void axWindowsMediaPlayer1_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            //Debug.WriteLine(e.fX.ToString());
            if (e.nButton == 1)
            {
                PostMessage(h2, WM_LBUTTONDOWN, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                typ = 3;
                //PostMessage(h2, WM_LBUTTONDOWN, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_PARENTNOTIFY, WM_LBUTTONDOWN, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_SETCURSOR, h3, (WM_LBUTTONDOWN<<16) |0x0001);
            }
            else
            {
                PostMessage(h2, WM_RBUTTONDOWN, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                typ = 4;
                //PostMessage(h2, WM_RBUTTONDOWN, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_PARENTNOTIFY, WM_RBUTTONDOWN, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
                //SendMessage(h2, WM_SETCURSOR, h3, (WM_RBUTTONDOWN<<16) |0x0001);
            }

        }

        /**
         * 获取鼠标移动动作 
         * 设想是通过组合抬起和按下动作来实现图标拖动
         * 但目前失败了
         * */
        private void axWindowsMediaPlayer1_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {

            //Debug.WriteLine(e.nButton.ToString());
            //Trace.Write(e.nButton.ToString());
            //if (e.nButton != 0)
            //{
            //    switch (typ)
            //    {
            //        case 1:
            //            PostMessage(h3, WM_LBUTTONUP, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //            break;
            //        case 2:
            //            PostMessage(h3, WM_RBUTTONUP, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //            break;
            //        case 3:
            //            PostMessage(h3, WM_LBUTTONDOWN, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //            break;
            //        case 4:
            //            PostMessage(h3, WM_RBUTTONDOWN, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //            break;
            //    }
            //    typ = -1;

            //    if (e.nButton == 1)
            //    {
            //        // PostMessage(h2, WM_MOUSEMOVE, 1, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //        PostMessage(h3, WM_MOUSEMOVE, 1, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //    }
            //    else if (e.nButton == 2)
            //    {
            //        //PostMessage(h2, WM_MOUSEMOVE, 2, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //        PostMessage(h3, WM_MOUSEMOVE, 2, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //    }
            //    else
            //    {
            //        //PostMessage(h2, WM_MOUSEMOVE, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //        PostMessage(h3, WM_MOUSEMOVE, 0, (e.fX & 0xFFFF) + (e.fY & 0xFFFF) * 0x10000);
            //    }
            //}
        }

        private void 暂停ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (暂停ToolStripMenuItem.Text == "暂停")
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                暂停ToolStripMenuItem.Text = "继续";
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                暂停ToolStripMenuItem.Text = "暂停";
            }
        }

    }
}
