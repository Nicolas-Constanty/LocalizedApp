using System;

namespace Common
{
    class Debug
    {
        static public void Infos(String msg)
        {
            PrintColor("Success : " + msg, ConsoleColor.Black, ConsoleColor.Blue);
        }

        static public void Warning(String msg)
        {
            PrintColor("Success : " + msg, ConsoleColor.Black, ConsoleColor.Yellow);
        }

        static public void Error(String msg)
        {
            PrintColor("Success : " + msg, ConsoleColor.Black, ConsoleColor.Red);
        }

        static public void Success(String msg)
        {
            PrintColor("Success : " + msg, ConsoleColor.Black, ConsoleColor.Green);
        }

        static public void PrintColor(String msg, ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
