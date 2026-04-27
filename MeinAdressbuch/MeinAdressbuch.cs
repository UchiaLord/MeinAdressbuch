namespace MeinAdressbuch
{
    internal class MeinAdressbuch
    {
        static void Main(string[] args)
        {
          Adressbuch meinAdressbuch = new Adressbuch();
          Console.WriteLine("Willkommen zu meinem Adressbuch!");
          meinAdressbuch.Start();
          Console.WriteLine("Vielen Dank für die Nutzung meines Adressbuchs!");
        }
    }
}
