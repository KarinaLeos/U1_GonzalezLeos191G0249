using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace U3Diversown
{
    public enum ECategorias { Etapas, Partes, Tipos }
    public class Juego : INotifyPropertyChanged
    {
        QuizView quiz = new QuizView();
        
      
        public Juego()
        {
            VerificarCommand = new Command<char>(Verificar);
            JugarCommand = new Command<ECategorias>(Jugar);
        }
        public ICommand JugarCommand { get; set; }
        public ICommand VerificarCommand { get; set; }

        public string Mensaje { get; set; }
        public bool FinJuego { get; set; }

        private ECategorias catego;
        public char letra;
        private int errores;
        public string palabraAdivinar;
        public char[] palabra;
        public ECategorias Catego
        {
            get { return catego; }
            set
            {
                catego = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Catego)));
            }
        }
        public string Imagen
        {
            get
            {

                return $"error{errores}.png";
            }
        }
        public string PalabraEscondida
        {
            get
            {
                if (palabra == null) return "";
                return string.Join(" ", palabra);
            }

        }
        public char[] Abecedario
        {
            get { return "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToCharArray(); }
        }


        private void SeleccionarPalabra()
        {
            var archivo = "U3Diversown." + Catego.ToString() + ".txt";
            var assembly = Assembly.GetExecutingAssembly();
            using (StreamReader s = new StreamReader(assembly.GetManifestResourceStream(archivo)))
            {
                List<string> palabras = new List<string>();

                string p = s.ReadLine();
                while (!string.IsNullOrEmpty(p))
                {
                    palabras.Add(p);
                   p = s.ReadLine();
                }
                s.Close();

                Random r = new Random();

                palabraAdivinar = palabras[r.Next(0, palabras.Count)];
            }

            palabra = new char[palabraAdivinar.Length];

            for (int i = 0; i < palabraAdivinar.Length; i++)
            {
                if (char.IsLetter(palabraAdivinar[i]))
                {
                    palabra[i] = '_';
                }
                else
                {
                    palabra[i] = palabraAdivinar[i];
                }
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public void Jugar(ECategorias obj)
        {
            if(quiz==null)
            {
                quiz = new QuizView() { BindingContext = this };
            }

            Mensaje = "";
            FinJuego = false;
            Catego = obj;
            errores = 0;
            SeleccionarPalabra();
            Application.Current.MainPage.Navigation.PushAsync(quiz);
        }
        public void Verificar(char letra)
        {
            if (palabraAdivinar.Any(x => x == letra))
            {
                for (int i = 0; i < palabraAdivinar.Length; i++)
                {
                    if (palabraAdivinar[i] == letra)
                    {
                        palabra[i] = letra;
                    }
                }

                if (!palabra.Any(x => x == '_'))
                {
                    FinJuego = true;
                    Mensaje = "¡ FELICIDADES, HAS ACERTADO! ";
                }
            }
            else 
            {
                errores++;

                if (errores == 6)
                {
                    FinJuego = true;
                    Mensaje = ":c INTENTALO UNA VEZ MÁS, LA PALABRA ERA: " + palabraAdivinar;
                }

            }
        }

    public Array ECategorias
        {
            get { return Enum.GetValues(typeof(ECategorias)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
