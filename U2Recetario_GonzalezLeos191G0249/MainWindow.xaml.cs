using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace U2Recetario_GonzalezLeos191G0249
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            cmbFiltrado.Items.Add("Todos");
            foreach (var item in Enum.GetValues(typeof(Dificultades)))
            {
                cmbFiltrado.Items.Add(item);
            }
            cmbFiltrado.SelectedIndex = 0;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listaRecetas.CambiarVistasCommand.Execute(Vistas.Agregar);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedItem!=null)
            {
                listaRecetas.CambiarVistasCommand.Execute(Vistas.Editar);
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedItem != null)
            {
                listaRecetas.CambiarVistasCommand.Execute(Vistas.Eliminar);
            }
        }

        private void lstRecetas_KeyDown(object sender, KeyEventArgs e)
        {
            if(lstRecetas.SelectedItem!=null && e.Key==Key.Delete)
            {
                listaRecetas.CambiarVistasCommand.Execute(Vistas.Eliminar);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedItem != null)
            {
                listaRecetas.CambiarVistasCommand.Execute(Vistas.Editar);
            }
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedItem != null)
            {
                listaRecetas.CambiarVistasCommand.Execute(Vistas.Eliminar);
            }
        }

        private void cmbFiltrado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiltrado.SelectedItem is string && (string)cmbFiltrado.SelectedItem == "Todos")
            {
                listaRecetas.FiltrarCommand.Execute(null);
            }
            else
            {
                listaRecetas.FiltrarCommand.Execute(cmbFiltrado.SelectedItem);
            }
        }
    }
}
