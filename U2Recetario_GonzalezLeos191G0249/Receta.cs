using System;
using System.Collections.Generic;
using System.Text;

namespace U2Recetario_GonzalezLeos191G0249
{
   
    public enum Dificultades { Facil, Medio, Dificil}


    [Serializable]
    public class Receta
    {
        public string Nombre { get; set; }
        public string Tiempo { get; set; }
        public Dificultades Dificultad { get; set; }
        public string Ingredientes { get; set; }
        public string Procedimiento { get; set; }
        public string PeliculaUrl { get; set; }
        public string PlatilloUrl { get; set; }
        public string Nota { get; set; }
    }
}
