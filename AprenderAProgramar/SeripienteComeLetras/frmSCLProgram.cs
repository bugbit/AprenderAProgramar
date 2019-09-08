using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprenderAProgramar.SeripienteComeLetras
{
    [AprenderAProgramar.Comun.Programa(Grupo = new[] { "Juegos", "Lectura" }, Nombre = "Serpiente como letras")]
    public partial class frmSCLProgram : Form
    {
        public frmSCLProgram()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSCLProgram());
        }
    }
}
