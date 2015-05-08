namespace Dash
{
    using System.IO;
    using System.Windows.Forms;

    using Dash.Properties;

    public class FilesHelper
    {
        public FilesHelper(DashGlobal dashGlobal)
        {
            this.DashGlobal = dashGlobal;
        }

        public DashGlobal DashGlobal { get; set; }

        public void SetTreeviewDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();

            if (path == string.Empty)
            {
                return;
            }

            try
            {
                var rootDirectoryInfo = new DirectoryInfo(path);
                treeView.Nodes.Add(this.CreateDirectoryNode(rootDirectoryInfo));
                treeView.Nodes[0].Expand();

                if (Settings.Default.AutoExpandTreeView)
                {
                    foreach (TreeNode node in treeView.Nodes[0].Nodes)
                    {
                        node.Expand();
                    }
                }

                // Scroll to top
                treeView.Nodes[0].EnsureVisible();
            }
            catch
            {
                MessageBox.Show("Unable to open directory");
            }
        }

        public string GetLangFromFile(string file)
        {
            var parts = file.Split('.');
            return parts[parts.Length - 1];
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name, 0, 0)
                                    {
                                        Tag = directoryInfo.FullName, 
                                        Name = directoryInfo.FullName
                                    };

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(this.CreateDirectoryNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                var nodeImageIndex = 1;
                var nodeImageSelected = 1;

                // TODO -- .ext .cfg .fsm files

                // Display icons for file types
                if (file.Extension == ".sqf")
                {
                    nodeImageIndex = 2;
                    nodeImageSelected = 2;
                }

                if (file.Extension == ".cpp" || file.Extension == ".hpp" || file.Extension == ".h")
                {
                    nodeImageIndex = 3;
                    nodeImageSelected = 3;
                }

                if (file.Extension == ".pbo")
                {
                    nodeImageIndex = 4;
                    nodeImageSelected = 4;
                }

                if (file.Extension == ".bin")
                {
                    nodeImageIndex = 5;
                    nodeImageSelected = 5;
                }

                if (file.Extension == ".sqm")
                {
                    nodeImageIndex = 6;
                    nodeImageSelected = 6;
                }

                if (file.Extension == ".dll" || file.Extension == ".exe")
                {
                    nodeImageIndex = 6;
                    nodeImageSelected = 6;
                }

                directoryNode.Nodes.Add(
                    new TreeNode
                        {
                            Name = file.FullName, 
                            Text = file.Name, 
                            ImageIndex = nodeImageIndex, 
                            SelectedImageIndex = nodeImageSelected, 
                            Tag = file.FullName // File path on disk
                        });
            }

            return directoryNode;
        }
    }
}