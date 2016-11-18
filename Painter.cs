using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class Painter
    {
        public IntPtr hWnd;

        [DllImport("User32.dll", EntryPoint="FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public Painter()
        {   
            //if make this: "IntPtr hWnd = ...", 
            //the graphic will in hole window, not just in the console.
            //but i do not know why.
            hWnd = FindWindow(null, Console.Title);
        }

        //the following number like "6" has no meanings.
        public void DrawPoint(int x, int y, int right, int down, Color c, int width)
        {
            Graphics g = Graphics.FromHwnd(hWnd);
            g.DrawLine(new Pen(c, width), x, y, right, down);
        }
        public void DrawPoint(int x)
        {
            this.DrawPoint(x, x, x + 6, x + 6, Color.Blue);
        }
    }
