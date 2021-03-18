
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Input;

namespace U2A1_GuiaDeEpGonzalezLeos191G0249
{
    public class ListadoEpisodios : INotifyPropertyChanged
    {
        public ICommand MostrarAgregarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ListadoEpisodios()
        {
            Load();
            MostrarAgregarCommand = new RelayCommand<string>(VerAgregar);
            CancelarCommand = new RelayCommand(Cancelar);
            AgregarCommand = new RelayCommand(Agregar);
            EliminarCommand = new RelayCommand(Eliminar);
            EditarCommand = new RelayCommand(Editar);
        }

        private void Cancelar()
        {
            VerUserControl = false;
        }

        private bool ver;  
        public bool VerUserControl
        {
            get { return ver; }
            set { ver = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VerUserControl")); }
        }
        int PosicionE;
        void VerAgregar(string modo )
        {
            Modo = modo;
            if (modo == "Agregar")
            {
                Episodio = new Episodio();
                
            }
            else
            {
                Episodio copia = new Episodio()
                {
                    NumEp = Episodio.NumEp,
                    Temporada = Episodio.Temporada,
                    Titulo = Episodio.Titulo,
                    TituloDoblado = Episodio.TituloDoblado,
                    Trama= Episodio.Trama
                };
                PosicionE = ListaEpisodios.IndexOf(Episodio);
                Episodio = copia;

            }
            VerUserControl = true;
        }
        private string modo;

        public string Modo
        {
            get { return  modo; }
            set {  modo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Modo"));
            }
        }


        public ObservableCollection<Episodio> ListaEpisodios { get; set; }
        private Episodio episodio;

        public  Episodio Episodio
        {
            get { return episodio; }
            set { episodio = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Episodio")); }
        }
        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; }
        }

        public void Agregar()
        {
            Error = "";
            if (string.IsNullOrWhiteSpace(Episodio.NumEp))
            {
                Error = "Verifique el numero del episodio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.Temporada))
            {
                Error = "Seleccione una temporada.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.Titulo))
            {
                Error = "El nombre del capitulo no puede estar vacio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.TituloDoblado))
            {
                Error = "El titulo doblado no puede estar vacio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.Trama))
            {
                Error = "Escriba una descripcion para el capitulo.";
                return;
            }
            if(ListaEpisodios.Any(x=>x.NumEp==Episodio.NumEp))

            {
                Error = "Este espisodio ya fue agregado";
                return;
            }
            ListaEpisodios.Add(Episodio);
            Save();
            VerUserControl = false;
        }
        public void Editar()
        {
            if (string.IsNullOrWhiteSpace(Episodio.NumEp))
            {
                Error = "Verifique el numero del episodio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.Temporada))
            {
                Error = "Seleccione una temporada.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.Titulo))
            {
                Error = "El nombre del capitulo no puede estar vacio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.TituloDoblado))
            {
                Error = "El titulo doblado no puede estar vacio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Episodio.Trama))
            {
                Error = "Escriba una descripcion para el capitulo.";
                return;
            }
            if (ListaEpisodios.Any(x => x.NumEp == Episodio.NumEp))

            {
                Error = "Este espisodio ya fue agregado";
                return;
            }
            ListaEpisodios[PosicionE] = Episodio;
            Save();
            VerUserControl = false;
        }
        public void Eliminar()
        {
           if( ListaEpisodios.Remove(Episodio))
            {
                Save();
            }
        }

        private void Save()
        {
            FileStream fs = File.Create("191G0249.dat");
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, ListaEpisodios);
            fs.Close();
        }
        private void Load()
        {
            try
            {
                FileStream fs = File.Create("191G0249.dat");
                BinaryFormatter bf = new BinaryFormatter();
                ListaEpisodios = (ObservableCollection<Episodio>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                ListaEpisodios = new ObservableCollection<Episodio>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}


