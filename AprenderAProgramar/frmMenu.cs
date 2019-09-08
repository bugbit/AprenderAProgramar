using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprenderAProgramar
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            treePrograms.Nodes.Clear();

            var pNode = new TreeNode { Text = "Programas" };

            treePrograms.Nodes.Add(pNode);

            foreach (var t in GetType().Assembly.GetTypes())
            {
                var pPrgAttr = t.GetCustomAttributes(typeof(Comun.ProgramaAttribute), false).FirstOrDefault() as Comun.ProgramaAttribute;

                if (pPrgAttr != null)
                {
                    var pNodeG = FindTreeNode(pNode, pPrgAttr.Grupo);
                    var pNodeP = new TreeNode { Name = pPrgAttr.Nombre, Text = pPrgAttr.Nombre, Tag = t };

                    pNodeG.Nodes.Add(pNodeP);
                }
            }

            treePrograms.ExpandAll();
        }

        private TreeNode FindTreeNode(TreeNode argNode, string[] argGrupos)
        {
            var pNode = argNode;

            foreach (var pGrupo in argGrupos)
            {
                var pNodeG = FindTreeNode(pNode, pGrupo);

                pNode = pNodeG;
            }

            return pNode;
        }

        private TreeNode FindTreeNode(TreeNode argNode, string argGrupo)
        {
            var pNodes = argNode.Nodes.Find(argGrupo, false);
            var pNode = pNodes.FirstOrDefault();

            if (pNode != null)
                return pNode;

            pNode = new TreeNode { Name = argGrupo, Text = argGrupo };
            argNode.Nodes.Add(pNode);

            return pNode;
        }

        private void Run(Type argPrgType)
        {
            var pMain =
               argPrgType.GetMethod("Main", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) ??
               argPrgType.GetMethod("Main", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] {
                typeof(object),
                typeof(object)
           }, null);

            if (pMain == null)
                return;

            try
            {
                Visible = false;

                var pSandbox = AppDomain.CreateDomain("Sandbox");
                var pRunner = new ProgramRunner(pMain);
                var pCross = new CrossAppDomainDelegate(pRunner.Invoke);

                pSandbox.DoCallBack(pCross);
                AppDomain.Unload(pSandbox);

            }
            finally
            {
                Visible = true;
            }
        }

        private void RunNodeSelected()
        {
            var pPrgType = (treePrograms.SelectedNode)?.Tag as Type;

            if (pPrgType == null)
                return;

            Run(pPrgType);
        }

        private void treePrograms_DoubleClick(object sender, EventArgs e)
        {
            RunNodeSelected();
        }
    }
}
