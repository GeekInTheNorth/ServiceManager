using System;
using System.ServiceProcess;
using System.Windows.Forms;
using ServiceManager.Models;
using ServiceManager.Repositories;

namespace ServiceManager
{
    public partial class Main : Form
    {
        private const int TopMargin = 10;

        private const int LeftMargin = 10;

        private const int ControlHeight = 30;

        private const int ButtonWidth = 100;

        private const int LabelWidth = 120;

        private const int RunningLabelWidth = 60;

        public Main()
        {
            InitializeComponent();
            BuildForm();
            CheckServices();
        }

        private void BuildForm()
        {
            var services = new ServiceRepository().GetAll();
            var labelNo = 0;

            foreach (var service in services)
            {
                var label = new Label();
                label.Top = TopMargin + ((ControlHeight + TopMargin) * labelNo);
                label.Left = LeftMargin;
                label.Text = service.DisplayName;
                label.Width = LabelWidth;
                Controls.Add(label);

                var runningLabel = new Label();
                runningLabel.Top = label.Top;
                runningLabel.Left = LeftMargin + LabelWidth + LeftMargin;
                runningLabel.Text = @"Unknown";
                runningLabel.Width = RunningLabelWidth;
                runningLabel.Tag = service;
                runningLabel.Name = string.Format("lblRunning{0}", labelNo);
                Controls.Add(runningLabel);

                var startButton = new Button();
                startButton.Top = runningLabel.Top;
                startButton.Left = runningLabel.Left + runningLabel.Width + LeftMargin;
                startButton.Text = @"Start";
                startButton.Click += BtnStartService;
                startButton.Tag = service;
                startButton.Width = ButtonWidth;
                Controls.Add(startButton);

                var stopButton = new Button();
                stopButton.Top = runningLabel.Top;
                stopButton.Left = startButton.Left + startButton.Width + LeftMargin;
                stopButton.Text = @"Stop";
                stopButton.Click += BtnStopService;
                stopButton.Tag = service;
                stopButton.Width = ButtonWidth;
                Controls.Add(stopButton);

                labelNo++;
            }

            var startAllButton = new Button();
            startAllButton.Top = TopMargin + ((ControlHeight + TopMargin) * labelNo);
            startAllButton.Left = (LeftMargin * 3) + LabelWidth + RunningLabelWidth;
            startAllButton.Text = @"Start All";
            startAllButton.Click += BtnStartAllServices;
            startAllButton.Width = ButtonWidth;
            Controls.Add(startAllButton);

            var stopAllButton = new Button();
            stopAllButton.Top = startAllButton.Top;
            stopAllButton.Left = startAllButton.Left + startAllButton.Width + LeftMargin;
            stopAllButton.Text = @"Stop All";
            stopAllButton.Click += BtnStopAllServices;
            stopAllButton.Width = ButtonWidth;
            Controls.Add(stopAllButton);

            Margin = new Padding(10);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;            
        }

        private void BtnStartService(object sender, EventArgs e)
        {
            var button = sender as Button;
            Service service = null;
            if (button != null)
                service = button.Tag as Service;

            if (service == null)
                return;

            StartService(service);
            CheckServices();
        }

        private void BtnStartAllServices(object sender, EventArgs e)
        {
            var services = new ServiceRepository().GetAll();
            foreach (var service in services)
            {
                StartService(service);
            }

            CheckServices();
        }

        private void BtnStopService(object sender, EventArgs e)
        {
            var button = sender as Button;
            Service service = null;
            if (button != null)
                service = button.Tag as Service;

            if (service == null)
                return;

            StopService(service);
            CheckServices();
        }

        private void BtnStopAllServices(object sender, EventArgs e)
        {
            var services = new ServiceRepository().GetAll();
            foreach (var service in services)
            {
                StopService(service);
            }

            CheckServices();
        }

        private void CheckServices()
        {
            foreach (var control in Controls)
            {
                if (!(control is Label)) continue;

                var name = (control as Label).Name;
                if (!name.StartsWith("lblRunning"))
                    continue;

                var service = (control as Label).Tag as Service;
                (control as Label).Text = GetServiceStatusText(service);
            }
        }

        private string GetServiceStatusText(Service service)
        {
            var sc = new ServiceController(service.ServiceName);

            switch (sc.Status)
            {
                case ServiceControllerStatus.Paused:
                    return "Paused";
                case ServiceControllerStatus.Running:
                    return "Running";
                case ServiceControllerStatus.Stopped:
                    return "Stopped";
                case ServiceControllerStatus.StartPending:
                    return "Starting";
                case ServiceControllerStatus.PausePending:
                    return "Pausing";
                case ServiceControllerStatus.StopPending:
                    return "Stopping";                   
                default:
                    return "Unknown";
            }
        }

        private void ServiceCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckServices();
        }

        private void StartService(Service service)
        {
            var sc = new ServiceController(service.ServiceName);
            if ((sc.Status == ServiceControllerStatus.Stopped) || (sc.Status == ServiceControllerStatus.Paused))
            {
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running);
            }        
        }

        private void StopService(Service service)
        {
            var sc = new ServiceController(service.ServiceName);
            if ((sc.Status == ServiceControllerStatus.Running) || (sc.Status == ServiceControllerStatus.Paused))
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }
    }
}
