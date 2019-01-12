// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.GlobalHook
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.WinSystem.Hook
{
  public abstract class GlobalHook
  {
    protected const int WH_MOUSE_LL = 14;
    protected const int WH_KEYBOARD_LL = 13;
    protected const int WH_MOUSE = 7;
    protected const int WH_KEYBOARD = 2;
    protected const int WM_MOUSEMOVE = 512;
    protected const int WM_LBUTTONDOWN = 513;
    protected const int WM_RBUTTONDOWN = 516;
    protected const int WM_MBUTTONDOWN = 519;
    protected const int WM_LBUTTONUP = 514;
    protected const int WM_RBUTTONUP = 517;
    protected const int WM_MBUTTONUP = 520;
    protected const int WM_LBUTTONDBLCLK = 515;
    protected const int WM_RBUTTONDBLCLK = 518;
    protected const int WM_MBUTTONDBLCLK = 521;
    protected const int WM_MOUSEWHEEL = 522;
    protected const int WM_KEYDOWN = 256;
    protected const int WM_KEYUP = 257;
    protected const int WM_SYSKEYDOWN = 260;
    protected const int WM_SYSKEYUP = 261;
    protected const byte VK_SHIFT = (byte) 16;
    protected const byte VK_CAPITAL = (byte) 20;
    protected const byte VK_NUMLOCK = (byte) 144;
    protected const byte VK_LSHIFT = (byte) 160;
    protected const byte VK_RSHIFT = (byte) 161;
    protected const byte VK_LCONTROL = (byte) 162;
    protected const byte VK_RCONTROL = (byte) 3;
    protected const byte VK_LALT = (byte) 164;
    protected const byte VK_RALT = (byte) 165;
    protected const byte LLKHF_ALTDOWN = (byte) 32;
    protected int _hookType;
    protected int _handleToHook;
    protected bool _isStarted;
    protected GlobalHook.HookProc _hookCallback;
    private bool _screenMsg;

    public bool ScreenMsg
    {
      get
      {
        return this._screenMsg;
      }
      set
      {
        this._screenMsg = value;
      }
    }

    public bool IsStarted
    {
      get
      {
        return this._isStarted;
      }
    }

    public GlobalHook()
    {
      Application.ApplicationExit += new EventHandler(this.Application_ApplicationExit);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    protected static extern int SetWindowsHookEx(int idHook, GlobalHook.HookProc lpfn, IntPtr hMod, int dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    protected static extern int UnhookWindowsHookEx(int idHook);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    protected static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

    [DllImport("user32")]
    protected static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

    [DllImport("user32")]
    protected static extern int GetKeyboardState(byte[] pbKeyState);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    protected static extern short GetKeyState(int vKey);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string name);

    public void Start()
    {
      if (this._isStarted || this._hookType == 0)
        return;
      this._hookCallback = new GlobalHook.HookProc(this.HookCallbackProcedure);
      this._handleToHook = GlobalHook.SetWindowsHookEx(this._hookType, this._hookCallback, GlobalHook.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
      if (this._handleToHook == 0)
        return;
      this._isStarted = true;
    }

    public void Stop()
    {
      if (!this._isStarted)
        return;
      GlobalHook.UnhookWindowsHookEx(this._handleToHook);
      this._isStarted = false;
    }

    protected virtual int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
    {
      return 0;
    }

    protected void Application_ApplicationExit(object sender, EventArgs e)
    {
      if (!this._isStarted)
        return;
      this.Stop();
    }

    [StructLayout(LayoutKind.Sequential)]
    protected class POINT
    {
      public int x;
      public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    protected class MouseHookStruct
    {
      public GlobalHook.POINT pt;
      public int hwnd;
      public int wHitTestCode;
      public int dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    protected class MouseLLHookStruct
    {
      public GlobalHook.POINT pt;
      public int mouseData;
      public int flags;
      public int time;
      public int dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    protected class KeyboardHookStruct
    {
      public int vkCode;
      public int scanCode;
      public int flags;
      public int time;
      public int dwExtraInfo;
    }

    protected delegate int HookProc(int nCode, int wParam, IntPtr lParam);
  }
}
