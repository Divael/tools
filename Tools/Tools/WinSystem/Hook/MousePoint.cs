// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.MousePoint
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System.Drawing;

namespace System.WinSystem.Hook
{
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }

        public static implicit operator Point(MousePoint p)
        {
            return new Point(p.X, p.Y);
        }

        public static MousePoint Parse(string s)
        {
            string str = s.Replace(',', ' ');
            char[] chArray = new char[1];
            int index = 0;
            int num = 32;
            chArray[index] = (char)num;
            string[] strArray = str.Split(chArray);
            return new MousePoint()
            {
                X = int.Parse(strArray[0].Trim()),
                Y = int.Parse(strArray[1].Trim())
            };
        }
    }
}
