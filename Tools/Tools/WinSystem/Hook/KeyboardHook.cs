// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.KeyboardHook
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.WinSystem.Hook
{
  public class KeyboardHook : GlobalHook
  {
    public event KeyEventHandler KeyDown;

    public event KeyEventHandler KeyUp;

    public event KeyPressEventHandler KeyPress;

    public KeyboardHook()
    {
      this._hookType = 13;
    }

    protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
    {
      if (this.ScreenMsg)
        return -1;
      bool flag1 = false;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (nCode > -1 && (this.KeyDown != null || this.KeyUp != null || this.KeyPress != null))
      {
        GlobalHook.KeyboardHookStruct keyboardHookStruct = (GlobalHook.KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof (GlobalHook.KeyboardHookStruct));
        bool flag2 = ((int) GlobalHook.GetKeyState(162) & 128) != 0 || ((uint) GlobalHook.GetKeyState(3) & 128U) > 0U;
        bool flag3 = ((int) GlobalHook.GetKeyState(160) & 128) != 0 || ((uint) GlobalHook.GetKeyState(161) & 128U) > 0U;
        bool flag4 = ((int) GlobalHook.GetKeyState(164) & 128) != 0 || ((uint) GlobalHook.GetKeyState(165) & 128U) > 0U;
        bool flag5 = (uint) GlobalHook.GetKeyState(20) > 0U;
        KeyEventArgs e = new KeyEventArgs((Keys) (keyboardHookStruct.vkCode | (flag2 ? 131072 : 0) | (flag3 ? 65536 : 0) | (flag4 ? 262144 : 0)));
        switch (wParam)
        {
          case 256:
          case 260:
            // ISSUE: reference to a compiler-generated field
            if (this.KeyDown != null)
            {
              // ISSUE: reference to a compiler-generated field
              this.KeyDown((object) this, e);
              flag1 = flag1 || e.Handled;
              break;
            }
            break;
          case 257:
          case 261:
            // ISSUE: reference to a compiler-generated field
            if (this.KeyUp != null)
            {
              // ISSUE: reference to a compiler-generated field
              this.KeyUp((object) this, e);
              flag1 = flag1 || e.Handled;
              break;
            }
            break;
        }
        // ISSUE: reference to a compiler-generated field
        if (wParam == 256 && !flag1 && (!e.SuppressKeyPress && this.KeyPress != null))
        {
          byte[] numArray = new byte[256];
          byte[] lpwTransKey = new byte[2];
          GlobalHook.GetKeyboardState(numArray);
          if (GlobalHook.ToAscii(keyboardHookStruct.vkCode, keyboardHookStruct.scanCode, numArray, lpwTransKey, keyboardHookStruct.flags) == 1)
          {
            char ch = (char) lpwTransKey[0];
            if (flag5 ^ flag3 && char.IsLetter(ch))
              ch = char.ToUpper(ch);
            // ISSUE: reference to a compiler-generated field
            this.KeyPress((object) this, new KeyPressEventArgs(ch));
            flag1 = flag1 || e.Handled;
          }
        }
      }
      if (flag1)
        return 1;
      return GlobalHook.CallNextHookEx(this._handleToHook, nCode, wParam, lParam);
    }
  }
}
