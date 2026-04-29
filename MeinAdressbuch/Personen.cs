using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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
            try
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

                ValidateBirthdate(person.Geburtsdatum);


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
            catch (Exception ex) 
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
            
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
            List<byte> allData = new List<byte>();

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

                byte[] lineByte =  Encoding.UTF8.GetBytes(line);
                string base64Line = Convert.ToBase64String(lineByte);
                byte[] encodedBytes = Encoding.UTF8.GetBytes(base64Line + "\n");
                allData.AddRange(encodedBytes);



            }
            File.WriteAllBytes(filePath, allData.ToArray());
        }

        public void ValidateBirthdate(string input)
        {
            string format = "dd.MM.yyyy";

          
            if (!DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthdate))
            {
                throw new Exception($"Das Geburtsdatumformat von {input} ist ungültig");
            }

            if(birthdate > DateTime.Today)
            {
                throw new Exception($"Die Person kann nicht in der Zukunft geboren sein");
            }
        }
    }
}
