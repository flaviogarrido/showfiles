namespace showfiles;
public partial class FilesForm : Form
{
    public FilesForm()
    {
        InitializeComponent();
        InitializeTreeView();
    }

    private void InitializeTreeView()
    {
        TreeView treeView = new TreeView();
        treeView.Dock = DockStyle.Fill;
        this.Controls.Add(treeView);

        // Exemplo com uma pasta "C:\Temp"
        BuildTree(treeView, "C:\\Temp");
    }

    private void BuildTree(TreeView treeView, string rootDirectory)
    {
        DirectoryInfo info = new DirectoryInfo(rootDirectory);
        if (info.Exists)
        {
            TreeNode rootNode = new TreeNode(info.Name) { Tag = info };
            treeView.Nodes.Add(rootNode);
            BuildDirectoryTree(rootNode, info);
        }

        treeView.NodeMouseDoubleClick += (sender, e) =>
        {
            if (e.Node.Tag is FileInfo)
            {
                FileInfo fileInfo = (FileInfo)e.Node.Tag;

                ContentForm.instance.LoadFile(fileInfo.FullName);
            }
        };
    }

    private void BuildDirectoryTree(TreeNode node, DirectoryInfo directoryInfo)
    {
        foreach (var directory in directoryInfo.GetDirectories())
        {
            TreeNode dirNode = new TreeNode(directory.Name) { Tag = directory };
            node.Nodes.Add(dirNode);
            BuildDirectoryTree(dirNode, directory);
        }

        foreach (var file in directoryInfo.GetFiles())
        {
            TreeNode fileNode = new TreeNode(file.Name) { Tag = file };
            node.Nodes.Add(fileNode);
        }
    }
}
