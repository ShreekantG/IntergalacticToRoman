using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;


namespace IntergalacticToRoman
{
    class Program
    {
        static void Main(string[] args)
        {
            var processManager = new ProcessInputManager();
            do
            {
                try
                {
                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) continue;
                    var status = processManager.ProcessInput(input);
                    if (!status.Equals(Constanst.Constants.Success))
                        Console.WriteLine(status);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }

    }
}
