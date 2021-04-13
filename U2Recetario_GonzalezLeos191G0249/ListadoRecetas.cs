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

namespace U2Recetario_GonzalezLeos191G0249
{
    public enum Vistas { Lista, Agregar, Editar, Eliminar}
    public class ListadoRecetas : INotifyPropertyChanged
    {
        public ICommand CambiarVistasCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand FiltrarCommand { get; set; }


        public ObservableCollection<Receta> ListaRecetas { get; set; }
        public ObservableCollection<Receta> RecetasFiltrada { get; set; } = new ObservableCollection<Receta>();
        public ListadoRecetas()
        {
            Load();
            Filtrar(null);
            CambiarVistasCommand = new RelayCommand<Vistas>(CambiarVista);
            AgregarCommand = new RelayCommand(Agregar);
            GuardarCommand = new RelayCommand(Editar);
            EliminarCommand = new RelayCommand(Eliminar);
            FiltrarCommand = new RelayCommand<Dificultades?>(Filtrar);
        }

        private void CambiarVista(Vistas obj)
        {
            Vista = obj;
            if(obj== Vistas.Agregar)
            {
                Receta = new Receta();
            }
            if(obj==Vistas.Editar)
            {
                indiceRecetaOriginal = ListaRecetas.IndexOf(Receta);

                var clon= new Receta { Nombre = receta.Nombre, Dificultad = receta.Dificultad, Tiempo = receta.Tiempo, Ingredientes = receta.Ingredientes, Procedimiento = receta.Procedimiento, Nota = receta.Nota, PeliculaUrl = receta.PeliculaUrl, PlatilloUrl = receta.PlatilloUrl };
                Receta = clon;
            }
        }

        private Receta receta;
        public Receta Receta
        {
            get { return receta; }
            set
            {
                receta = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Receta"));
            }
        }
        private string error;

        public string Error
        {
            get { return error; }
            set { error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
            }
        }

      
        private Vistas vistas;

        public  Vistas Vista
        {
            get { return vistas; }
            set { vistas = value;
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vista)));
            }
        }
        public Array ListaDificultad
        {
            get { return Enum.GetValues(typeof(Dificultades)); }
        }
        public void Agregar()
        {
            if (Receta != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Receta.Nombre))
                {
                    Error = "El nombre de la receta no puede estar vacio";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Receta.Tiempo))
                {
                    Error = "Ingrese el tiempo estimado para la receta";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Receta.Ingredientes))
                {
                    Error = "Agregue los ingredientes de la receta";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Receta.Procedimiento))
                {
                    Error = "Escriba el procedimiento de la receta";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Receta.Nota))
                {
                    Error = "Ingrese una nota";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Receta.PeliculaUrl))
                {
                    Error = "Ingrese la URL de la pelicula";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Receta.PlatilloUrl))
                {
                    Error = "Ingrese la URL de la imagen del platillo";
                    return;
                }

                ListaRecetas.Add(Receta);
                Save();
                CambiarVista(Vistas.Lista);
            }
            
        }
        public void Eliminar()
        {
            if(ListaRecetas.Remove(Receta))
            {
                Save();
            }
            CambiarVista(Vistas.Lista);
            
        }
        int indiceRecetaOriginal;
        public void Editar()
        {
            Error = "";
           if(Receta != null)
             
            {
                if (ListaRecetas != null)
                {
                    if (string.IsNullOrWhiteSpace(Receta.Nombre))
                    {
                        Error = "El nombre de la receta no puede estar vacio";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Receta.Tiempo))
                    {
                        Error = "Ingrese el tiempo estimado para la receta";
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Receta.Ingredientes))
                    {
                        Error = "Agruegue los ingredientes de la receta";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Receta.Procedimiento))
                    {
                        Error = "Escriba el procedimiento de la receta";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Receta.Nota))
                    {
                        Error = "Ingrese una nota";
                        return;
                    }
                    Receta original = ListaRecetas[indiceRecetaOriginal];
                    if (original.Nombre != receta.Nombre)
                    {
                        if (ListaRecetas.Any(x => x.Nombre.ToUpper() == Receta.Nombre.ToUpper()))
                        {
                            Error = "Ya existe una receta con este nombre";
                            return;
                        }
                    }
                    ListaRecetas[indiceRecetaOriginal] = Receta;
                    Save();
                    CambiarVista(Vistas.Lista);
                }
            }

        }
        public void Filtrar(Dificultades? dificultad)
        {
            
            var datos = ListaRecetas.Where(x => dificultad == null || x.Dificultad == dificultad).OrderBy(x => x.Nombre);
            RecetasFiltrada.Clear();
            foreach (var receta in datos)
            {
                RecetasFiltrada.Add(receta);
            }
        }
      
        private void Save()
        {
            FileStream fs = File.Create("U2Recetario_GonzalezLeos191G0249.dat");
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, ListaRecetas);
            fs.Close();
        }
        private void Load()
        {
            try
            {
                FileStream fs = File.OpenRead("U2Recetario_GonzalezLeos191G0249.dat");
                BinaryFormatter bf = new BinaryFormatter();
                ListaRecetas = (ObservableCollection<Receta>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                ListaRecetas = new ObservableCollection<Receta>();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
