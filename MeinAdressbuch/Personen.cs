using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinAdressbuch
{
    internal class Personen
    {
        private List<Person> personen;

        public Personen()
        {
            personen = new List<Person>();
        }

        public void AddPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person), "Person darf nicht null sein.");
            }

            if (string.IsNullOrWhiteSpace(person.Vorname))
            {
                throw new ArgumentException("Vorname darf nicht leer sein.");
            }

            if (string.IsNullOrWhiteSpace(person.Nachname))
            {
                throw new ArgumentException("Nachname darf nicht leer sein.");
            }

            if (string.IsNullOrWhiteSpace(person.Geburtsdatum))
            {
                throw new ArgumentException("Geburtsdatum darf nicht leer sein.");
            }
            if (string.IsNullOrWhiteSpace(person.Adresse))
            {
                throw new ArgumentException("Adresse darf nicht leer sein.");
            }
            if (string.IsNullOrWhiteSpace(person.Postleitzahl))
            {
                throw new ArgumentException("Postleitzahl darf nicht leer sein.");
            }
            if (string.IsNullOrWhiteSpace(person.Telefonnummer))
            {
                throw new ArgumentException("Telefonnummer darf nicht leer sein.");
            }

            if (personen.Any(p => p.Vorname == person.Vorname &&
            p.Nachname == person.Nachname &&
            p.Geburtsdatum == person.Geburtsdatum &&
            p.Adresse == person.Adresse &&
            p.Postleitzahl == person.Postleitzahl &&
            p.Telefonnummer == person.Telefonnummer))
            {
                throw new InvalidOperationException("Diese Person ist bereits im Adressbuch vorhanden.");
            }



            personen.Add(person);
        }

        public void RemovePerson(Person person)
        {
            personen.Remove(person);
        }

        public int GetCount()
        {
            return personen.Count;
        }

        public List<Person> GetAllPersons()
        {
            return personen;
        }

        public void SaveToFile()
        {
            string filePath = "MeinAdressbuch.txt";
            List<string> lines = new List<string>();

            foreach (Person p in GetAllPersons())
            {
                string line = string.Join(";", new[]
                {
                    p.Vorname,
                    p.Nachname,
                    p.Geburtsdatum,
                    p.Adresse,
                    p.Postleitzahl,
                    p.Telefonnummer
                });
                lines.Add(line);



            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
