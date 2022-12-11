using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace TimeofLock
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class LockScreen : Window
    {
        public LockScreen()
        {
            InitializeComponent(); 
        }

        public static bool on_or_off = true;
        public bool u_drive = false;
        public bool a_kill = false;


        ////////////////////////////////////////////////////////////////////实例化一个计时器/////////////////////////////////////////////////////////////////////////////

        DispatcherTimer clock = new DispatcherTimer();

        ////////////////////////////////////////////////////////////////////////键盘钩子///////////////////////////////////////////////////////////////////////////////

        #region
        /// 声明回调函数委托  
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        /// 委托实例  
 
        HookProc KeyboardHookProcedure;

        /// 键盘钩子句柄  
        static int hKeyboardHook = 0;

        //装置钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //卸下钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //获取某个进程的句柄函数  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // 普通按键消息  
        private const int WM_KEYDOWN = 0x100;

        // 系统按键消息  
        private const int WM_SYSKEYDOWN = 0x104;

        //鼠标常量   
        public const int WH_KEYBOARD_LL = 13;

        //声明键盘钩子的封送结构类型   
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;               //表示一个在1到254间的虚似键盘码   
            public int scanCode;             //表示硬件扫描码   
            public int flags;
            public int time;
            public int dwExtraInfo;
        }


        /// 启动键盘钩子  
        /// 截取全局按键，发送新按键，返回  

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                System.Windows.Forms.Keys keyData = (System.Windows.Forms.Keys)MyKeyboardHookStruct.vkCode;

                ///////////////////////////////////////////////////////////键盘钩子 屏蔽按键列表//////////////////////////////////////////////////////
                if (keyData == System.Windows.Forms.Keys.LWin)
                {
                    //  左win
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.RWin)
                {
                    //  右win
                    return 1;
                }
                if(keyData==System.Windows.Forms.Keys.LControlKey)
                {
                    // 左Ctrl
                    return 1;
                }
                if(keyData==System.Windows.Forms.Keys.RControlKey)
                {
                    // 右Ctrl
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Alt)
                {
                    // Alt
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Tab)
                {
                    // Tab
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.F4)
                {
                    // F4
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.RShiftKey)
                {
                    // R shift
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.LShiftKey)
                {
                    // L shift
                    return 1;
                }
                if (keyData >= System.Windows.Forms.Keys.F1 && keyData<=System.Windows.Forms.Keys.F12)
                {
                    // F1  --  F12
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Escape && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control)
                {
                    //  ctrl + esc (开始菜单)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.F4 && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Alt)
                {
                    // alt + F4 (关闭)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Tab && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Alt)
                {
                    // alt + tab (切换)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.E && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.LWin)
                {
                    // win + e (资源管理器)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Space && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Alt)
                {
                    // alt + space (打开快捷方式菜单)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Tab && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Alt)
                {
                    // ctrl + alt + tab(可以在打开的项目中切换)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Space && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.LWin)
                {
                    // win + space (预览桌面)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Up && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.LWin)
                {
                    // win + ↑ (调整窗口大小)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Down && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.LWin)
                {
                    // win + ↓ (调整窗口大小)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Left && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.LWin)
                {
                    // win + ← (调整窗口大小)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Right && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.LWin)
                {
                    // win + →(调整窗口大小)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Up && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Alt)
                {
                    // ctrl + alt + ↑( 屏幕旋转)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Down && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Alt)
                {
                    // ctrl + alt + ↓ ( 屏幕旋转)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Left && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Alt)
                {
                    // ctrl + alt + ←( 屏幕旋转)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Right && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Alt)
                {
                    // ctrl + alt + →( 屏幕旋转)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.A && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Alt)
                {
                    // ctrl + alt + a(qq截图)
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Z && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Alt)
                {
                    // ctrl + alt + a (qq主窗口)
                    return 1;
                }
                if(keyData == System.Windows.Forms.Keys.V && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Control)
                {
                    // ctrl + V
                    return 1;
                }
                if(keyData==System.Windows.Forms.Keys.Control&&(int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Space)
                {
                    //Ctrl + Space
                    return 1;
                }
                if(keyData == System.Windows.Forms.Keys.Alt && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Escape)
                {
                    //alt+esc
                    return 1;
                }
                if(keyData == System.Windows.Forms.Keys.Alt && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.LShiftKey + (int)System.Windows.Forms.Keys.Escape)
                {
                    //alt+shift+esc
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Alt && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.RShiftKey + (int)System.Windows.Forms.Keys.Escape)
                {
                    //alt+shift+esc
                    return 1;
                }
                if (keyData == System.Windows.Forms.Keys.Alt && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Shift + (int)System.Windows.Forms.Keys.Tab)
                {
                    //alt+shift+esc
                    return 1;
                }
            }
            return 0;
        }

        #endregion

        //////////////////////////////////////////////////////////////////////关闭窗口函数/////////////////////////////////////////////////////////////////////////////

        #region
        public void close_lock()
        {
            //关闭计时器
            clock.Stop();
            //卸载钩子
            bool retKeyboard = true;

            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }

            if (!(retKeyboard)) throw new Exception("Keyboard hook uninstall Error!");
            //关闭窗
            Close();

            on_or_off = true;

        }
        #endregion

        /////////////////////////////////////////////////////////窗口初始化创建计时器、初始化属性、安装键盘钩子///////////////////////////////////////////////////////////////

        #region
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            on_or_off = false;
            //设置计时器属性
            clock.Interval = new TimeSpan(0, 0, 0, 0, 10);
            clock.Tick += new EventHandler(timer_kkick);
            clock.Start();

            //设置控件image与电脑分辨率相同
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;

            image.Width = SystemParameters.PrimaryScreenWidth;
            image.Height = SystemParameters.PrimaryScreenHeight;

            R1.Width = SystemParameters.PrimaryScreenWidth;
            R1.Height = SystemParameters.PrimaryScreenHeight;

            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;

            //让 passwordbox 在窗体载入同时获得输入焦点
            passwordBox.Focus();     
            
            //装载钩子
            if (hKeyboardHook == 0)
            {
                //实例化委托  
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                Process curProcess = Process.GetCurrentProcess();
                ProcessModule curModule = curProcess.MainModule;
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(curModule.ModuleName), 0);
            }

            //初始化窗口UI

          //  初始化top line坐标、h w
            re1.Width = SystemParameters.PrimaryScreenWidth;
            re1.Height = 50;
            re1.Margin = new Thickness(0, 0, 0, 0);


         //   初始化Center Black Background
            re2.Width = 301;
            re2.Height = 336;
            re2.Margin = new Thickness((SystemParameters.PrimaryScreenWidth - re2.Width) / 2, (SystemParameters.PrimaryScreenHeight - re2.Height) / 2, 0, 0);

        //    初始化密码框

            passwordBox.Width = 187;
            passwordBox.Height = 28;

            passwordBox.Margin = new Thickness(((SystemParameters.PrimaryScreenWidth - re2.Width) / 2) + 57, ((SystemParameters.PrimaryScreenHeight - re2.Height) / 2) + 265, 0, 0);
            passwordBox.Background = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0));
            passwordBox.Foreground = new SolidColorBrush(Color.FromRgb(86, 157, 229));
            passwordBox.CaretBrush = new SolidColorBrush(Color.FromRgb(86, 157, 229));

            textBlock2.Margin = new Thickness(((SystemParameters.PrimaryScreenWidth - re2.Width) / 2) + 57, ((SystemParameters.PrimaryScreenHeight - re2.Height) / 2) + 269, 0, 0);
            textBlock2.Foreground = new SolidColorBrush(Color.FromRgb(86, 157, 229));

        }
        #endregion

        /////////////////////////////////////////////////////----解锁----、计时器、KILL进程、判断密码、卸载钩子/////////////////////////////////////////////////////////////

        #region
        public void timer_kkick(object sender, EventArgs e)
        {
            //textBox.Text = "当前时间：" + DateTime.Now.ToLongTimeString();
            Topmost = true;                                                                   //窗口置顶

            Process[] kill = Process.GetProcesses();                                          //获取当前任务管理器所有运行中程序
            foreach(Process kill1 in kill)
            {
                try
                {
                    if(kill1.ProcessName.ToLower().Trim()=="taskmgr")
                    {
                        kill1.Kill();
                        return;
                    }
                }
                catch
                {
                    return;
                }
            }

            string a = "";
            DateTime t = new DateTime();
            t = DateTime.Now;//当前时间
            a = t.ToString("yyyyMdHm");
       //     textBlock.Text = t.ToString("HH:mm");

            string test = "admin123";//读取测试密码
            
            //判断密码
            if (passwordBox.Password == test)
            {
                close_lock();
            }

            if(u_drive&&a_kill)
            {
                Close();
            }

        }
        #endregion

        ///////////////////////////////////////////////////////////////////passwordBox输入事件////////////////////////////////////////////////////////////////////////
 
        #region
        private void passwordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
        #endregion

        ///////////////////////////////////////////////////////////////////密码锁////////////////////////////////////////////////////////////////////////

        #region
        private void passwordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                if (passwordBox.Password.Length == 1)
                {
                    textBlock2.Visibility = Visibility.Visible;
                }
            }
            else
            {
                textBlock2.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }
}
