using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace Algoritmos_de_busqueda
{
    public class Cronometro
    {
        private System.Windows.Forms.Timer Tiempo { get; set; }
        public Double Result { get; set; }
        public Cronometro()
        {
            Tiempo=new System.Windows.Forms.Timer();
            Tiempo.Tick += new EventHandler(Tiempo_Tick);
            Tiempo.Interval = 100;
        }
        void Tiempo_Tick(object sender, EventArgs e)
        {
            Result++;
            //cada tick representa 100 milisegundos
        }
        public void Start()
        {
            Tiempo.Start();
        }

        public void Pause()
        {
            //
        }

        public void Stop()
        {
            Tiempo.Stop();
        }
        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0} ms",
                Result);

        }

    }
}
