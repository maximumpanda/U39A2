using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Panda_Explorer.PandaSettings;

namespace Panda_Explorer.Core {
    internal static class Debugger {
        internal static Dictionary<string, string> DebugValues = new Dictionary<string, string>();
        internal static TableLayoutPanel OutputPanel;
        internal static event EventHandler Updated;

        internal static void CreateDebugWindow(TableLayoutPanel parentPanel) {
            if (Settings.EnableDebugger) {
                OutputPanel = new TableLayoutPanel {
                    Parent = parentPanel,
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    ColumnCount = 1,
                    Name = "Output",
                    RowCount = 1,
                    TabIndex = 0
                };
                OutputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                OutputPanel.RowStyles.Add(new RowStyle());
                parentPanel.Controls.Add(OutputPanel);
                Updated += DebugUpdated;
            }
        }
        private static void DebugUpdated(object sender, EventArgs e) {
            for (int i = 0; i < DebugValues.Count; i++)
                if (OutputPanel.Controls.ContainsKey(DebugValues.ElementAt(i).Key))
                    OutputPanel.Controls.Find(DebugValues.ElementAt(i).Key, false)[0].Text = GenerateOutput(i);
                else OutputPanel.Controls.Add(GenerateNewDebugLabel(i));
        }
        private static Label GenerateNewDebugLabel(int i) {
            Label newLabel = new Label {
                Name = $"{DebugValues.ElementAt(i).Key}",
                Dock = DockStyle.Fill,
                Text = GenerateOutput(i)
            };
            return newLabel;
        }
        internal static string GenerateOutput(int index) {
            return $"{DebugValues.ElementAt(index).Key}: {DebugValues.ElementAt(index).Value}";
        }
        internal static void Report(string value) {
            string methodName = new StackFrame(1).GetMethod().Name;
            if (DebugValues.ContainsKey(methodName)) DebugValues[methodName] = value;
            else DebugValues.Add(methodName, value);
            Updated?.Invoke(null, EventArgs.Empty);
        }
        internal static void Report(string key, string value) {
            if (DebugValues.ContainsKey(key)) DebugValues[key] = value;
            else DebugValues.Add(key, value);
            Updated?.Invoke(null, EventArgs.Empty);
        }
    }
}