namespace EsercitazioneLogin

{
    using System;
    using System.Collections.Generic;

    public static class Utente
    {
        public static string Username { get; private set; }
        public static string Password { get; private set; }
        public static DateTime? UltimoLogin { get; private set; }
        public static List<DateTime> StoricoAccessi { get; } = new List<DateTime>();

        public static bool IsAutenticato => !string.IsNullOrEmpty(Username);

        public static void Login(string username, string password, string confermaPassword)
        {
            if (IsAutenticato)
            {
                Console.WriteLine("Utente già autenticato. Eseguire il logout prima di effettuare un nuovo login.");
                return;
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || password != confermaPassword)
            {
                Console.WriteLine("Errore durante il login. Assicurarsi di inserire una username e password corrette.");
                return;
            }

            Username = username;
            Password = password;
            UltimoLogin = DateTime.Now;
            StoricoAccessi.Add(UltimoLogin.Value);

            Console.WriteLine($"Login effettuato con successo per l'utente {Username}.");
        }

        public static void Logout()
        {
            if (!IsAutenticato)
            {
                Console.WriteLine("Nessun utente autenticato. Impossibile effettuare il logout.");
                return;
            }

            Console.WriteLine($"Logout effettuato per l'utente {Username}.");
            Username = null;
            Password = null;
            UltimoLogin = null;
        }

        public static void VerificaOraDataLogin()
        {
            if (!IsAutenticato)
            {
                Console.WriteLine("Nessun utente autenticato. Impossibile verificare l'ora e la data del login.");
                return;
            }

            Console.WriteLine($"L'utente {Username} si è autenticato in data {UltimoLogin}.");
        }

        public static void ListaAccessi()
        {
            if (StoricoAccessi.Count == 0)
            {
                Console.WriteLine("Nessun accesso registrato.");
                return;
            }

            Console.WriteLine("Storico accessi:");
            foreach (var accesso in StoricoAccessi)
            {
                Console.WriteLine(accesso);
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("===============OPERAZIONI==============");
                Console.WriteLine("Scegli l'operazione da effettuare:");
                Console.WriteLine("1.: Login");
                Console.WriteLine("2.: Logout");
                Console.WriteLine("3.: Verifica ora e data di login");
                Console.WriteLine("4.: Lista degli accessi");
                Console.WriteLine("5.: Esci");
                Console.WriteLine("========================================");

                string scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        Console.Write("Inserisci username: ");
                        string username = Console.ReadLine();
                        Console.Write("Inserisci password: ");
                        string password = Console.ReadLine();
                        Console.Write("Conferma password: ");
                        string confermaPassword = Console.ReadLine();
                        Utente.Login(username, password, confermaPassword);
                        break;

                    case "2":
                        Utente.Logout();
                        break;

                    case "3":
                        Utente.VerificaOraDataLogin();
                        break;

                    case "4":
                        Utente.ListaAccessi();
                        break;

                    case "5":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Scelta non valida. Riprova.");
                        break;
                }

                Console.WriteLine("\nPremi un tasto per continuare...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
