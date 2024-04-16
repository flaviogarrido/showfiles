namespace showfiles;

public partial class MainForm : Form
{

    private FilesForm filesForm;
    private ContentForm contentForm;

    public MainForm()
    {
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // Instanciando e configurando FilesForm
        filesForm = new FilesForm
        {
            MdiParent = this,
            Text = "Files Explorer",
            Width = (int)(this.ClientSize.Width * 0.3),
            Height = this.ClientSize.Height
        };
        filesForm.Show();



        // Instanciando e configurando ContentForm
        contentForm = new ContentForm()
        {
            MdiParent = this,
            Text = "Content Viewer",
            Width = (int)(this.ClientSize.Width * 0.7),
            Height = this.ClientSize.Height
        };
        contentForm.Show();
        ContentForm.instance = contentForm;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        RepositionChildren();
    }

    private void RepositionChildren()
    {
        if (filesForm != null && contentForm != null)
        {
            int width = this.ClientSize.Width-4;
            int height = this.ClientSize.Height-4;

            filesForm.Size = new Size((int)(width * 0.3), height);
            contentForm.Size = new Size((int)(width * 0.7), height);

            filesForm.Location = new Point(0, 0);
            contentForm.Location = new Point(filesForm.Width, 0);
        }
    }
}
