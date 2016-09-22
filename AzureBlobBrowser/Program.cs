using System;

namespace AzureBlobBrowser
{
    /// <summary>
    ///    Based off of hardcoded values, list the filename and mod date of the blobs in a container.
    ///    
    ///    Make sure to update the following constants below: [ACCOUNT NAME], [ACCOUNT KEY], [CONTAINER NAME]
    /// </summary>
    class AzureBlobBrowserMain
    {
        static void Main(string[] args)
        {
            AzureConnection connection = new AzureConnection();

            Console.WriteLine("Connecting to Azure Account: " + ACCOUNT_NAME);
            char input = ' ';
            while (input != 'x')
            {
                Console.WriteLine("Enter Command (c,l,x): ");
                input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case 'c':
                        Console.WriteLine(connection.SetupConnection(ACCOUNT_NAME, ACCOUNT_KEY, CONTAINER_NAME));
                        break;
                    case 'l':
                        Console.WriteLine(connection.ListFiles());
                        break;
                    case 'x':
                        break;
                    default:
                        Console.WriteLine("Invalid Command.");
                        Console.WriteLine("c = Connection Setup");
                        Console.WriteLine("l = List Files");
                        Console.WriteLine("x = Exit Application.");
                        break;
                }
            }
        }

        private static readonly string ACCOUNT_NAME = "[ACCOUNT NAME]";
        private static readonly string ACCOUNT_KEY = "[ACCOUNT KEY]";
        private static readonly string CONTAINER_NAME = "[CONTAINER NAME]";
    }
}
