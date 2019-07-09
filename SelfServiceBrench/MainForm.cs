using System.Windows.Forms;

namespace SelfServiceBrench
{
    public partial class MainForm : Form
    {
        public IServiceManager Service { get; set; }

        public MainForm()
        {
            
            InitializeComponent();
        }
    }
}
