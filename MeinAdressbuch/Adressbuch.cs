using System;
using System.Collections.Generic;
using System.IO;

namespace MeinAdressbuch
{
    internal class Adressbuch
    {
        private Personen personen = new Personen();
        private string path;

        public void Start()
        {
            string searchNachname;

            try
            {
                Console.WriteLine("Bitte geben Sie den relativen Pfad zur Kontakte-Datei ein:");
                path = Console.ReadLine();

                if (File.Exists(path))
                {
                    string[] lines = File.ReadAllLines(path);

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        string[] personData = line.Split(';');

                        if (personData.Length < 6)
                        {
                            Console.WriteLine($"Ungültiger Datensatz übersprungen: {line}");
                            continue;
                        }

                        Person p = new Person();
                        p.Vorname = personData[0];
                        p.Nachname = personData[1];
                        p.Geburtsdatum = personData[2];
                        p.Adresse = personData[3];
                        p.Postleitzahl = personData[4];
                        p.Telefonnummer = personData[5];

                        personen.AddPerson(p);
                    }

                    while (true)
                    {
                        Console.WriteLine($"Derzeitige Anzahl an Personen beträgt: {personen.GetCount()}");
                        Console.WriteLine();
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.WriteLine("Ihre Auswahlmöglichkeiten");
                        Console.WriteLine("1.) H: Hinzufügen");
                        Console.WriteLine("2.) S: Suchen");
                        Console.WriteLine("3.) A: Alle anzeigen");
                        Console.WriteLine("4.) L: Löschen");
                        Console.WriteLine("5.) X: Speichern");
                        Console.WriteLine("6.) B: Bearbeiten");
                        Console.WriteLine("7.) C: Beenden");

                        switch ((Console.ReadLine() ?? "").ToUpper())
                        {
                            case "H" or "1":
                                {
                                    Person ph = new Person();

                                    Console.WriteLine("Geben Sie den Vorname der Person ein:");
                                    ph.Vorname = Console.ReadLine();

                                    Console.WriteLine("Geben Sie den Nachnamen der Person ein:");
                                    ph.Nachname = Console.ReadLine();

                                    Console.WriteLine("Geben Sie das Geburtsdatum der Person ein:");
                                    ph.Geburtsdatum = Console.ReadLine();

                                    Console.WriteLine("Geben Sie die Adresse der Person ein:");
                                    ph.Adresse = Console.ReadLine();

                                    Console.WriteLine("Geben Sie die Postleitzahl der Person ein:");
                                    ph.Postleitzahl = Console.ReadLine();

                                    Console.WriteLine("Geben Sie die Telefonnummer der Person ein:");
                                    ph.Telefonnummer = Console.ReadLine();

                                    personen.AddPerson(ph);
                                    break;
                                }

                            case "S" or "2":
                                {
                                    Console.WriteLine("Geben Sie den Nachnamen der Person ein, die Sie suchen möchten:");
                                    searchNachname = Console.ReadLine();

                                    bool found = false;

                                    Console.WriteLine("Vorname Nachname Geburtsdatum Adresse Postleitzahl Telefonnummer");

                                    foreach (Person p in personen.GetAllPersons())
                                    {
                                        if (p.Nachname.Equals(searchNachname, StringComparison.OrdinalIgnoreCase))
                                        {
                                            found = true;
                                            Console.WriteLine("Vorname: Nachname: Geburtsdatum: Adresse: Postleitzahl: Telefonnummer:");
                                            Console.WriteLine($"{p.Vorname} {p.Nachname} {p.Geburtsdatum} {p.Adresse} {p.Postleitzahl} {p.Telefonnummer}");
                                            Console.WriteLine("-------------------------------------------------------------");
                                        }
                                    }

                                    if (!found)
                                    {
                                        Console.WriteLine("Keine Person mit diesem Nachnamen gefunden.");
                                    }

                                    break;
                                }

                            case "A" or "3":
                                {
                                    foreach (Person p in personen.GetAllPersons())
                                    {
                                        Console.WriteLine("Vorname: Nachname: Geburtsdatum: Adresse: Postleitzahl: Telefonnummer:");
                                        Console.WriteLine($"{p.Vorname} {p.Nachname} {p.Geburtsdatum} {p.Adresse} {p.Postleitzahl} {p.Telefonnummer}");
                                        Console.WriteLine("-------------------------------------------------------------");
                                    }

                                    break;
                                }

                            case "L" or "4":
                                {
                                    Console.WriteLine("Geben Sie den Nachnamen der Person ein, die Sie löschen möchten:");
                                    searchNachname = Console.ReadLine();

                                    List<Person> matchedPersonen = new List<Person>();

                                    foreach (Person p in personen.GetAllPersons())
                                    {
                                        if (p.Nachname.Equals(searchNachname, StringComparison.OrdinalIgnoreCase))
                                        {
                                            matchedPersonen.Add(p);
                                        }
                                    }

                                    if (matchedPersonen.Count == 0)
                                    {
                                        Console.WriteLine("Keine Person mit diesem Namen gefunden.");
                                    }
                                    else if (matchedPersonen.Count == 1)
                                    {
                                        personen.RemovePerson(matchedPersonen[0]);
                                        Console.WriteLine("Person erfolgreich gelöscht.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Es wurden {matchedPersonen.Count} Personen mit diesem Nachnamen gefunden.");

                                        foreach (Person p in matchedPersonen)
                                        {
                                            Console.WriteLine("Vorname: Nachname: Geburtsdatum: Adresse: Postleitzahl: Telefonnummer:");
                                            Console.WriteLine($"{p.Vorname} {p.Nachname} {p.Geburtsdatum} {p.Adresse} {p.Postleitzahl} {p.Telefonnummer}");
                                            Console.WriteLine("-------------------------------------------------------------");
                                        }

                                        Console.WriteLine("Welche wollen Sie genau löschen?");
                                        Console.WriteLine("Geben Sie den Vornamen der Person ein:");
                                        string deleteVorname = Console.ReadLine();

                                        Console.WriteLine("Geben Sie den Nachnamen der Person ein:");
                                        string deleteNachname = Console.ReadLine();

                                        Console.WriteLine("Geben Sie das Geburtsdatum der Person ein:");
                                        string deleteGeburtsdatum = Console.ReadLine();

                                        Person personToDelete = null;

                                        foreach (Person person in matchedPersonen)
                                        {
                                            if (person.Vorname.Equals(deleteVorname, StringComparison.OrdinalIgnoreCase)
                                                && person.Nachname.Equals(deleteNachname, StringComparison.OrdinalIgnoreCase)
                                                && person.Geburtsdatum.Equals(deleteGeburtsdatum, StringComparison.OrdinalIgnoreCase))
                                            {
                                                personToDelete = person;
                                                break;
                                            }
                                        }

                                        if (personToDelete != null)
                                        {
                                            personen.RemovePerson(personToDelete);
                                            Console.WriteLine("Person erfolgreich gelöscht.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Keine eindeutig passende Person gefunden.");
                                        }
                                    }

                                    break;
                                }

                            case "X" or "5":
                                {
                                    personen.SaveToFile();
                                    Console.WriteLine("Adressbuch erfolgreich gespeichert.");
                                    break;
                                }

                            case "B" or "6":
                                {
                                    Console.WriteLine("Geben Sie den Nachnamen der Person ein, die Sie bearbeiten möchten:");
                                    searchNachname = Console.ReadLine();

                                    List<Person> matchedPersonen = new List<Person>();

                                    foreach (Person p in personen.GetAllPersons())
                                    {
                                        if (p.Nachname.Equals(searchNachname, StringComparison.OrdinalIgnoreCase))
                                        {
                                            matchedPersonen.Add(p);
                                        }
                                    }

                                    Person personToEdit = null;

                                    if (matchedPersonen.Count == 0)
                                    {
                                        Console.WriteLine("Keine Person mit diesem Namen gefunden.");
                                        break;
                                    }
                                    else if (matchedPersonen.Count == 1)
                                    {
                                        personToEdit = matchedPersonen[0];
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Es wurden {matchedPersonen.Count} Personen mit diesem Nachnamen gefunden.");

                                        foreach (Person p in matchedPersonen)
                                        {
                                            Console.WriteLine("Vorname: Nachname: Geburtsdatum: Adresse: Postleitzahl: Telefonnummer:");
                                            Console.WriteLine($"{p.Vorname} {p.Nachname} {p.Geburtsdatum} {p.Adresse} {p.Postleitzahl} {p.Telefonnummer}");
                                            Console.WriteLine("-------------------------------------------------------------");
                                        }

                                        Console.WriteLine("Welche wollen Sie genau bearbeiten?");
                                        Console.WriteLine("Geben Sie den Vornamen der Person ein:");
                                        string editVorname = Console.ReadLine();

                                        Console.WriteLine("Geben Sie den Nachnamen der Person ein:");
                                        string editNachname = Console.ReadLine();

                                        Console.WriteLine("Geben Sie das Geburtsdatum der Person ein:");
                                        string editGeburtsdatum = Console.ReadLine();

                                        foreach (Person person in matchedPersonen)
                                        {
                                            if (person.Vorname.Equals(editVorname, StringComparison.OrdinalIgnoreCase)
                                                && person.Nachname.Equals(editNachname, StringComparison.OrdinalIgnoreCase)
                                                && person.Geburtsdatum.Equals(editGeburtsdatum, StringComparison.OrdinalIgnoreCase))
                                            {
                                                personToEdit = person;
                                                break;
                                            }
                                        }
                                    }

                                    if (personToEdit == null)
                                    {
                                        Console.WriteLine("Person nicht gefunden.");
                                        break;
                                    }

                                    Console.WriteLine("Neuen Vornamen eingeben oder leer lassen:");
                                    string newVorname = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newVorname))
                                    {
                                        personToEdit.Vorname = newVorname;
                                    }

                                    Console.WriteLine("Neuen Nachnamen eingeben oder leer lassen:");
                                    string newNachname = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newNachname))
                                    {
                                        personToEdit.Nachname = newNachname;
                                    }

                                    Console.WriteLine("Neues Geburtsdatum eingeben oder leer lassen:");
                                    string newGeburtsdatum = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newGeburtsdatum))
                                    {
                                        personToEdit.Geburtsdatum = newGeburtsdatum;
                                    }

                                    Console.WriteLine("Neue Adresse eingeben oder leer lassen:");
                                    string newAdresse = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newAdresse))
                                    {
                                        personToEdit.Adresse = newAdresse;
                                    }

                                    Console.WriteLine("Neue Postleitzahl eingeben oder leer lassen:");
                                    string newPostleitzahl = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newPostleitzahl))
                                    {
                                        personToEdit.Postleitzahl = newPostleitzahl;
                                    }

                                    Console.WriteLine("Neue Telefonnummer eingeben oder leer lassen:");
                                    string newTelefonnummer = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newTelefonnummer))
                                    {
                                        personToEdit.Telefonnummer = newTelefonnummer;
                                    }

                                    Console.WriteLine("Person erfolgreich bearbeitet.");
                                    break;
                                }
                            case "C" or "7":
                                {
                                    Console.WriteLine("Programm wird beendet.");
                                    return;
                                }

                            default:
                                {
                                    Console.WriteLine("Ungültige Eingabe.");
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Die angegebene Datei existiert nicht.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
            }
        }
    }
}