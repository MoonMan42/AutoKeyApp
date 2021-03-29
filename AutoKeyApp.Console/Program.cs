using System;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace AutoKeyApp.Console
{
    class Program
    {
        // installed InputSimulator nuget package
        //https://ourcodeworld.com/articles/read/520/simulating-keypress-in-the-right-way-using-inputsimulator-with-csharp-in-winforms

        static void Main(string[] args)
        {
            InputSimulator keySim = new InputSimulator();

            //keySim.Keyboard.KeyPress(VirtualKeyCode.VK_F);
            //keySim.Keyboard.KeyPress(VirtualKeyCode.VK_U);
            //keySim.Keyboard.KeyPress(VirtualKeyCode.VK_C);
            //keySim.Keyboard.KeyPress(VirtualKeyCode.VK_K);

            while (true)
            {
                keySim.Keyboard.KeyPress(VirtualKeyCode.F5);
                Thread.Sleep(1000 * 10); // 10 secs
            }
        }
    }
}
