using la_mia_pizzeria_static.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Services
{
    public class FileLogger : ILoggerMs 
    {
        public void Log(string message)
        {
            File.AppendAllText(@"C:\Users\Antonio\source\repos\la-mia-pizzeria-crud-mvc\la-mia-pizzeria-static\my-log.txt", $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} LOG: {message}{Environment.NewLine}");
        }
    }
}
