using System;
using System.IO;

public class Clean
{
    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("[error] Please provide a list of directories to delete.");
            return 1;
        }

        try
        {
            foreach (var dir in args)
            {
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, true);
                }
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("[error] Failed to clean.\n" + e);
            return 1;
        }

        return 0;
    }
}