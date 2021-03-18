using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GonzalezLeosU2_Act1GuiaDEpisodios
{
    public class ListadoEpisodios : INotifyPropertyChanged
    {
        public ICommand MostrarAgregarCommand { get; set; }
        public ListadoEpisodios()
        {
            MostrarAgregarCommand = new RelayCommand(VerAgregar);
        }
        private bool ver;
        public bool MostrarUserControl
        {
            get { return ver; }
            set
            {
                value = ver;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MostrarUserControl"));
            }
        }
        void VerAgregar()
        {
            MostrarUserControl = true;
        }
        void VerTemporada()
        {
            MostrarUserControl = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
