// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.MacroEvent
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System;

namespace System.WinSystem.Hook
{
  [Serializable]
  public class MacroEvent
  {
    public MacroEventType MacroEventType;
    public EventArgs EventArgs;
    public int TimeSinceLastEvent;

    public MacroEvent(MacroEventType macroEventType, EventArgs eventArgs, int timeSinceLastEvent)
    {
      this.MacroEventType = macroEventType;
      this.EventArgs = eventArgs;
      this.TimeSinceLastEvent = timeSinceLastEvent;
    }
  }
}
