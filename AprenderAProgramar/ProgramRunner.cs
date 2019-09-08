using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprenderAProgramar
{
    [Serializable]
    class ProgramRunner
    {
        private MethodInfo mMain;

        public ProgramRunner(MethodInfo argMain)
        {
            mMain = argMain;
        }

        public void Invoke()
        {
            try
            {
                mMain.Invoke(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en programa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
