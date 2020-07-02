// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.MouseSimulator
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.WinSystem.Hook
{
    public static class MouseSimulator
    {
        public static MousePoint Position
        {
            get
            {
                return new MousePoint(Cursor.Position);
            }
            set
            {
                Cursor.Position = (Point)value;
            }
        }

        public static int X
        {
            get
            {
                return Cursor.Position.X;
            }
            set
            {
                Cursor.Position = new Point(value, MouseSimulator.Y);
            }
        }

        public static int Y
        {
            get
            {
                return Cursor.Position.Y;
            }
            set
            {
                Cursor.Position = new Point(MouseSimulator.X, value);
            }
        }

        [DllImport("user32.dll")]
        private static extern int ShowCursor(bool show);

        [DllImport("user32.dll")]
        private static extern void mouse_event(MouseSimulator.MouseEventFlag flags, int dX, int dY, int buttons, int extraInfo);

        public static void MouseDown(MouseButtons button)
        {
            if (button != MouseButtons.Left)
            {
                if (button != MouseButtons.Right)
                {
                    if (button != MouseButtons.Middle)
                        return;
                    MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                }
                else
                    MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            }
            else
                MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void MouseUp(MouseButtons button)
        {
            if (button != MouseButtons.Left)
            {
                if (button != MouseButtons.Right)
                {
                    if (button != MouseButtons.Middle)
                        return;
                    MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                }
                else
                    MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }
            else
                MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void WheelDown(int times = 1)
        {
            while (times-- > 0)
                MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_WHEEL, 0, 0, -100, 0);
        }

        public static void WheelUp(int times = 1)
        {
            while (times-- > 0)
                MouseSimulator.mouse_event(MouseSimulator.MouseEventFlag.MOUSEEVENTF_WHEEL, 0, 0, 100, 0);
        }

        public static void Click(MouseButtons button)
        {
            MouseSimulator.MouseDown(button);
            MouseSimulator.MouseUp(button);
        }

        public static void DoubleClick(MouseButtons button)
        {
            MouseSimulator.Click(button);
            MouseSimulator.Click(button);
        }

        public static void Show()
        {
            MouseSimulator.ShowCursor(true);
        }

        public static void Hide()
        {
            MouseSimulator.ShowCursor(false);
        }

        [Flags]
        private enum MouseEventFlag : uint
        {
            MOUSEEVENTF_MOVE = 1U,
            MOUSEEVENTF_LEFTDOWN = 2U,
            MOUSEEVENTF_LEFTUP = 4U,
            MOUSEEVENTF_RIGHTDOWN = 8U,
            MOUSEEVENTF_RIGHTUP = 16U,
            MOUSEEVENTF_MIDDLEDOWN = 32U,
            MOUSEEVENTF_MIDDLEUP = 64U,
            MOUSEEVENTF_WHEEL = 2048U,
            MOUSEEVENTF_VIRTUALDESK = 16384U,
            MOUSEEVENTF_ABSOLUTE = 32768U,
        }
    }
}
