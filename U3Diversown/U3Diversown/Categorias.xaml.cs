using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace U3Diversown
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categorias : ContentPage
    {
        public Categorias()
        {
            InitializeComponent();
        }

      

        

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuizView());
        }
    }
}