using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private readonly WindowsServices windowsServices;

        public Main()
        {
            InitializeComponent();

            windowsServices = new WindowsServices();

            BuildForm();
        }

        private void BuildForm()
        {
            Controls.Clear();

            var services = windowsServices.GetConfiguredServices();

            if (services.Any())
                BuildForm(services);
            else
                BuildEmptyForm();                      
        }

        private void BuildEmptyForm()
        {
            var label = CreateLabel(@"There are currently no services referenced.", "lblServiceName", 0);
            label.Left = LeftMargin;
            label.Top = TopMargin;

            var addButton = CreateButton(@"Add Service", BtnAddService);
            addButton.Top = label.Top + label.Height + TopMargin;
            addButton.Left = (label.Left + label.Width) - ButtonWidth;

            Margin = new Padding(10);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void BuildForm(IEnumerable<ServiceModel> serviceModels)
        {
            var labelNo = 0;
            var startAllLeft = LeftMargin;
            var stopAllLeft = startAllLeft + LeftMargin + ButtonWidth;

            foreach (var service in serviceModels)
            {
                BuildFormRow(service, labelNo, ref startAllLeft, ref stopAllLeft);

                labelNo++;
            }

            var startAllButton = CreateButton(@"Start All", BtnStartAllServices);
            startAllButton.Top = TopMargin + ((ControlHeight + TopMargin) * labelNo);
            startAllButton.Left = startAllLeft;

            var stopAllButton = CreateButton(@"Stop All", BtnStopAllServices);
            stopAllButton.Top = startAllButton.Top;
            stopAllButton.Left = stopAllLeft;

            var addButton = CreateButton(@"Add Service", BtnAddService);
            addButton.Top = startAllButton.Top;
            addButton.Left = (stopAllLeft < startAllLeft ? stopAllLeft : startAllLeft) - LeftMargin - ButtonWidth;

            Margin = new Padding(10);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink; 
        }

        private void BuildFormRow(ServiceModel serviceModel, int labelNo, ref int startAllLeft, ref int stopAllLeft)
        {
            var removeButton = CreateButton(@"X", BtnRemoveService);
            removeButton.Top = TopMargin + ((ControlHeight + TopMargin) * labelNo);
            removeButton.Left = LeftMargin;
            removeButton.Width = ControlHeight;
            removeButton.Tag = serviceModel;

            var label = CreateLabel(serviceModel.DisplayName, "lblServiceName", labelNo);
            label.Top = TopMargin + ((ControlHeight + TopMargin) * labelNo);
            label.Left = removeButton.Left + removeButton.Width + LeftMargin;

            var runningLabel = CreateLabel(GetServiceStatusText(serviceModel), "lblRunning", labelNo);
            runningLabel.Top = label.Top;
            runningLabel.Left = label.Left + label.Width + LeftMargin;
            runningLabel.Width = RunningLabelWidth;
            runningLabel.Tag = serviceModel;

            var startButton = CreateButton(@"Start", BtnStartService);
            startButton.Top = runningLabel.Top;
            startButton.Left = runningLabel.Left + runningLabel.Width + LeftMargin;
            startButton.Tag = serviceModel;
            startAllLeft = startButton.Left;

            var stopButton = CreateButton(@"Stop", BtnStopService);
            stopButton.Top = runningLabel.Top;
            stopButton.Left = startButton.Left + startButton.Width + LeftMargin;
            stopButton.Tag = serviceModel;
            stopAllLeft = stopButton.Left;
        }

        private Button CreateButton(string text, EventHandler clickEvent)
        {
            var button = new Button { Height = ControlHeight, Width = ButtonWidth, Text = text };
            button.Click += clickEvent;
            Controls.Add(button);

            return button;
        }

        private Label CreateLabel(string text, string labelName, int labelNumber)
        {
            var label = new Label
                {
                    Height = ControlHeight,
                    Text = text,
                    Name = string.Format("{0}{1}", labelName, labelNumber),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Width = LabelWidth
                };
            Controls.Add(label);

            return label;
        }

        private void BtnStartService(object sender, EventArgs e)
        {
            var button = sender as Button;
            ServiceModel serviceModel = null;
            if (button != null)
                serviceModel = button.Tag as ServiceModel;

            if (serviceModel == null)
                return;

            windowsServices.StartService(serviceModel);
            CheckServices();
        }

        private void BtnStartAllServices(object sender, EventArgs e)
        {
            var services = windowsServices.GetConfiguredServices();
            foreach (var service in services)
            {
                windowsServices.StartService(service);
            }

            CheckServices();
        }

        private void BtnStopService(object sender, EventArgs e)
        {
            var button = sender as Button;
            ServiceModel serviceModel = null;
            if (button != null)
                serviceModel = button.Tag as ServiceModel;

            if (serviceModel == null)
                return;

            windowsServices.StopService(serviceModel);
            CheckServices();
        }

        private void BtnStopAllServices(object sender, EventArgs e)
        {
            var services = windowsServices.GetConfiguredServices();
            foreach (var service in services)
            {
                windowsServices.StopService(service);
            }

            CheckServices();
        }

        private void BtnRemoveService(object sender, EventArgs e)
        {
            if (sender == null) throw new ArgumentNullException("sender");

            var confirmation = MessageBox.Show(@"Are you sure you want to remove the reference to this service?",
                                               @"Confirmation",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
                return;

            var serviceModel = (sender as Button).Tag as ServiceModel;

            windowsServices.RemoveServiceReference(serviceModel);

            BuildForm();
        }

        private void BtnAddService(object sender, EventArgs e)
        {
            var addService = new AddService();
            addService.ShowDialog();
            addService.Dispose();

            BuildForm();
        }

        private void CheckServices()
        {
            foreach (var control in Controls)
            {
                if (!(control is Label)) continue;

                var name = (control as Label).Name;
                if (!name.StartsWith("lblRunning"))
                    continue;

                var serviceModel = (control as Label).Tag as ServiceModel;
                windowsServices.GetStatus(serviceModel);
                (control as Label).Text = GetServiceStatusText(serviceModel);
            }
        }

        private string GetServiceStatusText(ServiceModel serviceModel)
        {
            switch (serviceModel.Status)
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
    }
}