// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.MouseHook
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.WinSystem.Hook
{
    public class MouseHook : GlobalHook
    {
        public event MouseEventHandler MouseDown;

        public event MouseEventHandler MouseUp;

        public event MouseEventHandler MouseMove;

        public event MouseEventHandler MouseWheel;

        public event EventHandler Click;

        public event EventHandler DoubleClick;

        public MouseHook()
        {
            this._hookType = 14;
        }

        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if (this.ScreenMsg)
                return -1;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (nCode > -1 && (this.MouseDown != null || this.MouseUp != null || this.MouseMove != null))
            {
                GlobalHook.MouseLLHookStruct mouseLlHookStruct = (GlobalHook.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(GlobalHook.MouseLLHookStruct));
                int num1 = (int)this.GetButton(wParam);
                MouseHook.MouseEventType mouseEventType = this.GetEventType(wParam);
                int clicks = mouseEventType == MouseHook.MouseEventType.DoubleClick ? 2 : 1;
                int x = mouseLlHookStruct.pt.x;
                int y = mouseLlHookStruct.pt.y;
                int delta = mouseEventType == MouseHook.MouseEventType.MouseWheel ? (int)(short)(mouseLlHookStruct.mouseData >> 16 & (int)ushort.MaxValue) : 0;
                MouseEventArgs e = new MouseEventArgs((MouseButtons)num1, clicks, x, y, delta);
                int num2 = 2097152;
                if (num1 == num2 && mouseLlHookStruct.flags != 0)
                    mouseEventType = MouseHook.MouseEventType.None;
                switch (mouseEventType)
                {
                    case MouseHook.MouseEventType.MouseDown:
                        // ISSUE: reference to a compiler-generated field
                        if (this.MouseDown != null)
                        {
                            // ISSUE: reference to a compiler-generated field
                            this.MouseDown((object)this, e);
                            break;
                        }
                        break;
                    case MouseHook.MouseEventType.MouseUp:
                        // ISSUE: reference to a compiler-generated field
                        if (this.Click != null)
                        {
                            // ISSUE: reference to a compiler-generated field
                            this.Click((object)this, new EventArgs());
                        }
                        // ISSUE: reference to a compiler-generated field
                        if (this.MouseUp != null)
                        {
                            // ISSUE: reference to a compiler-generated field
                            this.MouseUp((object)this, e);
                            break;
                        }
                        break;
                    case MouseHook.MouseEventType.DoubleClick:
                        // ISSUE: reference to a compiler-generated field
                        if (this.DoubleClick != null)
                        {
                            // ISSUE: reference to a compiler-generated field
                            this.DoubleClick((object)this, new EventArgs());
                            break;
                        }
                        break;
                    case MouseHook.MouseEventType.MouseWheel:
                        // ISSUE: reference to a compiler-generated field
                        if (this.MouseWheel != null)
                        {
                            // ISSUE: reference to a compiler-generated field
                            this.MouseWheel((object)this, e);
                            break;
                        }
                        break;
                    case MouseHook.MouseEventType.MouseMove:
                        // ISSUE: reference to a compiler-generated field
                        if (this.MouseMove != null)
                        {
                            // ISSUE: reference to a compiler-generated field
                            this.MouseMove((object)this, e);
                            break;
                        }
                        break;
                }
            }
            return GlobalHook.CallNextHookEx(this._handleToHook, nCode, wParam, lParam);
        }

        private MouseButtons GetButton(int wParam)
        {
            switch (wParam)
            {
                case 513:
                case 514:
                case 515:
                    return MouseButtons.Left;
                case 516:
                case 517:
                case 518:
                    return MouseButtons.Right;
                case 519:
                case 520:
                case 521:
                    return MouseButtons.Middle;
                default:
                    return MouseButtons.None;
            }
        }

        private MouseHook.MouseEventType GetEventType(int wParam)
        {
            switch (wParam)
            {
                case 512:
                    return MouseHook.MouseEventType.MouseMove;
                case 513:
                case 516:
                case 519:
                    return MouseHook.MouseEventType.MouseDown;
                case 514:
                case 517:
                case 520:
                    return MouseHook.MouseEventType.MouseUp;
                case 515:
                case 518:
                case 521:
                    return MouseHook.MouseEventType.DoubleClick;
                case 522:
                    return MouseHook.MouseEventType.MouseWheel;
                default:
                    return MouseHook.MouseEventType.None;
            }
        }

        private enum MouseEventType
        {
            None,
            MouseDown,
            MouseUp,
            DoubleClick,
            MouseWheel,
            MouseMove,
        }
    }
}
