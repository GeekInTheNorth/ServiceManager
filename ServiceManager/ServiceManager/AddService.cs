using System.Linq;
using System.Windows.Forms;
using ServiceManager.Models;
using ServiceManager.Repositories;

namespace ServiceManager
{
    public partial class AddService : Form
    {
        private readonly WindowsServices windowsServices;

        public AddService()
        {
            InitializeComponent();
            windowsServices = new WindowsServices();

            var allServices = windowsServices.GetWindowsServices();
            var configuredServiceNames = new WindowsServices().GetConfiguredServices().Select(x => x.ServiceName).ToList();

            allServices = allServices.Where(x => !configuredServiceNames.Contains(x.ServiceName));
            cbServices.DataSource = allServices.OrderBy(x => x.DisplayName).ToList();
            cbServices.DisplayMember = "DisplayName";
            cbServices.SelectedIndex = 0;
        }

        private void bnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void bnOk_Click(object sender, System.EventArgs e)
        {
            var serviceModel = cbServices.SelectedItem as ServiceModel;
            if (serviceModel != null)
            {
                serviceModel.DisplayName = tbServiceDisplayName.Text;
                windowsServices.AddServiceReference(serviceModel);

                DialogResult = DialogResult.Yes;
            }
            else
                DialogResult = DialogResult.No;

            Close();
        }

        private void cbServices_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            tbServiceDisplayName.Text = string.Empty;
            tbServiceName.Text = string.Empty;
            tbServiceStatus.Text = string.Empty;

            var serviceModel = cbServices.SelectedItem as ServiceModel;
            if (serviceModel == null) return;

            tbServiceDisplayName.Text = serviceModel.DisplayName;
            tbServiceName.Text = serviceModel.ServiceName;
            tbServiceStatus.Text = serviceModel.Status.ToString();
        }
    }
}
