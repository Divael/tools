// Decompiled with JetBrains decompiler
// Type: System.WinSystem.Hook.GlobalMacroRecorder
// Assembly: Tool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=6a796b320d947832
// MVID: 393BF990-191A-488B-9DBB-114ACC0A58DC
// Assembly location: E:\C#开发程序包\Tools\Tool.dll

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace System.WinSystem.Hook
{
  public class GlobalMacroRecorder
  {
    private List<MacroEvent> events = new List<MacroEvent>();
    private MouseHook mouseHook = new MouseHook();
    private KeyboardHook keyboardHook = new KeyboardHook();
    private int lastTimeRecorded;

    public GlobalMacroRecorder()
    {
      this.mouseHook.MouseMove += new MouseEventHandler(this.mouseHook_MouseMove);
      this.mouseHook.MouseDown += new MouseEventHandler(this.mouseHook_MouseDown);
      this.mouseHook.MouseUp += new MouseEventHandler(this.mouseHook_MouseUp);
      this.keyboardHook.KeyDown += new KeyEventHandler(this.keyboardHook_KeyDown);
      this.keyboardHook.KeyUp += new KeyEventHandler(this.keyboardHook_KeyUp);
    }

    private void mouseHook_MouseMove(object sender, MouseEventArgs e)
    {
      this.events.Add(new MacroEvent(MacroEventType.MouseMove, (EventArgs) e, Environment.TickCount - this.lastTimeRecorded));
      this.lastTimeRecorded = Environment.TickCount;
    }

    private void mouseHook_MouseDown(object sender, MouseEventArgs e)
    {
      this.events.Add(new MacroEvent(MacroEventType.MouseDown, (EventArgs) e, Environment.TickCount - this.lastTimeRecorded));
      this.lastTimeRecorded = Environment.TickCount;
    }

    private void mouseHook_MouseUp(object sender, MouseEventArgs e)
    {
      this.events.Add(new MacroEvent(MacroEventType.MouseUp, (EventArgs) e, Environment.TickCount - this.lastTimeRecorded));
      this.lastTimeRecorded = Environment.TickCount;
    }

    private void keyboardHook_KeyDown(object sender, KeyEventArgs e)
    {
      this.events.Add(new MacroEvent(MacroEventType.KeyDown, (EventArgs) e, Environment.TickCount - this.lastTimeRecorded));
      this.lastTimeRecorded = Environment.TickCount;
    }

    private void keyboardHook_KeyUp(object sender, KeyEventArgs e)
    {
      this.events.Add(new MacroEvent(MacroEventType.KeyUp, (EventArgs) e, Environment.TickCount - this.lastTimeRecorded));
      this.lastTimeRecorded = Environment.TickCount;
    }

    public void Start()
    {
      this.events.Clear();
      this.lastTimeRecorded = Environment.TickCount;
      this.keyboardHook.Start();
      this.mouseHook.Start();
    }

    public void Stop()
    {
      this.keyboardHook.Stop();
      this.mouseHook.Stop();
    }

    public void Clear()
    {
      this.events.Clear();
    }

    public void StopPlay()
    {
      this.events.Clear();
    }

    public void RePlay()
    {
      Common.RunInBackThread((Action) (() =>
      {
        while (this.events.Count > 0)
          this.Play();
      }), true);
    }

    public void Play()
    {
      this.Stop();
      foreach (MacroEvent macroEvent in this.events)
      {
        Common.Sleep(macroEvent.TimeSinceLastEvent);
        switch (macroEvent.MacroEventType)
        {
          case MacroEventType.MouseMove:
            MouseEventArgs mouseEventArgs = (MouseEventArgs) macroEvent.EventArgs;
            MouseSimulator.X = mouseEventArgs.X;
            MouseSimulator.Y = mouseEventArgs.Y;
            continue;
          case MacroEventType.MouseDown:
            MouseSimulator.MouseDown(((MouseEventArgs) macroEvent.EventArgs).Button);
            continue;
          case MacroEventType.MouseUp:
            MouseSimulator.MouseUp(((MouseEventArgs) macroEvent.EventArgs).Button);
            continue;
          case MacroEventType.KeyDown:
            KeyboardSimulator.KeyDown(((KeyEventArgs) macroEvent.EventArgs).KeyCode);
            KeyboardSimulator.KeyUp(Keys.A);
            KeyboardSimulator.KeyPress(Keys.A);
            KeyboardSimulator.SimulateStandardShortcut(StandardShortcut.Copy);
            continue;
          case MacroEventType.KeyUp:
            KeyboardSimulator.KeyUp(((KeyEventArgs) macroEvent.EventArgs).KeyCode);
            continue;
          default:
            continue;
        }
      }
    }
  }
}
