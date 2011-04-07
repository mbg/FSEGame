using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using FSEGame.Engine;

namespace FSELevelEditor
{
    public partial class ActorBrowserWindow : Form
    {
        List<Type> actorTypes = new List<Type>();

        public ActorBrowserWindow()
        {
            InitializeComponent();
        }

        private void ActorBrowserWindow_Load(object sender, EventArgs e)
        {
            try
            {
                Assembly gameAssembly = Assembly.LoadFrom("FSEGame.exe");
                

                foreach (Type t in gameAssembly.GetTypes())
                {
                    if (t.IsSubclassOf(typeof(Actor)))
                    {
                        actorTypes.Add(t);
                    }
                }

                TreeNode currentNode = new TreeNode("Actor");
                Type currentBaseType = typeof(Actor);

                this.AddActors(currentNode, currentBaseType);

                currentNode.Tag = currentBaseType;
                this.treeView1.Nodes.Add(currentNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load actors:\n" + ex.Message);
            }
        }

        private void AddActors(TreeNode parent, Type baseType)
        {
            Type[] types = this.actorTypes.ToArray();

            foreach(Type t in types)
            {
                if (t.BaseType == baseType)
                {
                    TreeNode childNode = new TreeNode(t.Name);
                    childNode.Tag = t;

                    this.actorTypes.Remove(t);

                    if (!t.IsAbstract)
                        childNode.NodeFont = new Font(this.treeView1.Font, FontStyle.Bold);

                    this.AddActors(childNode, t);

                    parent.Nodes.Add(childNode);
                }
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Item;
            Type t = (Type)node.Tag;

            if (!t.IsAbstract)
            {
                this.DoDragDrop(t.Name, DragDropEffects.Link);
            }
        }
    }
}
