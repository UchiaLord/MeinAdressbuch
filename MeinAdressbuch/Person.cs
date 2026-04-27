using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinAdressbuch
{
    internal class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Geburtsdatum { get; set; }
        public string Adresse { get; set; }
        public string Postleitzahl { get; set; }
        public string Telefonnummer { get; set; }

        public Person()
        {
            Vorname = "";
            Nachname = "";
            Geburtsdatum = "";
            Adresse = "";
            Postleitzahl = "";
            Telefonnummer = "";
        }

    }
}
