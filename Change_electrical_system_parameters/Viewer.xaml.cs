using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autodesk.Revit.DB;

namespace Change_electrical_system_parameters
{
    public partial class Viewer : Window
    {
        public static Document document;
        
        public Viewer(Document viewer_document)
        {
            document = viewer_document;

            InitializeComponent();

            select_method.Text = Command.last_method;

            protection_type.Text = Command.protection_type;
            voltage_loss.Text = Command.voltage_loss.ToString();
            laying_method.Text = Command.laying_method;
        }
         
        public void Button_click(object sender, RoutedEventArgs event_args)
        {
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)event_args.OriginalSource;
            Window current_window = Window.GetWindow(button);

            /*foreach (ManagementObject management_object in new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia").Get())
            {
                DriveInfo drive = DriveInfo.GetDrives().Where(d => Environment.GetEnvironmentVariable("windir").Contains(d.Name)).SingleOrDefault();

                //MessageBox.Show(drive.Name);
                MessageBox.Show(management_object["Name"].ToString());
            }*/

            foreach (ManagementObject management_object in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM CIM_Card").Get())
            {
                MessageBox.Show(management_object["SerialNumber"].ToString());
            }

            foreach (ManagementObject management_object in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get())
            {
                MessageBox.Show(management_object["ProcessorId"].ToString());
            }

            foreach (NetworkInterface network_interface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (network_interface.OperationalStatus == OperationalStatus.Up)
                {
                    MessageBox.Show(network_interface.GetPhysicalAddress().ToString());
                    break;
                }
            }

            if (button.Name == "button_okay" && select_method.SelectedIndex != 1 && protection_type.Text != "" && voltage_loss.Text != "0" && laying_method.Text != "" && select_method.SelectedItem != null)
            {
                Command.ui_approve = true;

                Command.last_method = select_method.Text;

                Command.protection_type = protection_type.Text;
                Command.voltage_loss = Convert.ToInt32(voltage_loss.Text);
                Command.laying_method = laying_method.Text;

                current_window.Hide();
            }
            else if (button.Name == "button_okay" && select_method.SelectedIndex == 1 && protection_type.Text != "" && select_method.SelectedItem != null)
            {
                Command.ui_approve = true;

                Command.last_method = select_method.Text;

                Command.protection_type = protection_type.Text;

                current_window.Hide();
            }
            else if (button.Name == "button_cancel")
            {
                current_window.Close();
            }
            else
            {
                Autodesk.Revit.UI.TaskDialog.Show("Error", "Something set wrong!");

                current_window.Activate();
            }
        }

        public void Selection_changed(object sender, RoutedEventArgs event_args)
        {
            if (select_method.SelectedIndex == 1)
            {
                voltage_loss_label.Visibility = System.Windows.Visibility.Hidden;
                voltage_loss.Visibility = System.Windows.Visibility.Hidden;
                laying_method_label.Visibility = System.Windows.Visibility.Hidden;
                laying_method.Visibility = System.Windows.Visibility.Hidden;

                main_window.Height = 160;
            }
            else
            {
                voltage_loss_label.Visibility = System.Windows.Visibility.Visible;
                voltage_loss.Visibility = System.Windows.Visibility.Visible;
                laying_method_label.Visibility = System.Windows.Visibility.Visible;
                laying_method.Visibility = System.Windows.Visibility.Visible;

                main_window.Height = 240;
            }
        }

        private static void Hex_to_string()
        {
            byte[] public_key, private_key;
            const int KEY_SIZE = 2048;

            // Генерация пары ключей
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KEY_SIZE))
            {
                public_key = rsa.ExportCspBlob(false);
                private_key = rsa.ExportCspBlob(true);
            }

            // Генерация случайных данных для шифрования
            const int MAX_LENGTH = ((KEY_SIZE - 384) / 8) + 7 - 1;
            byte[] data = new byte[MAX_LENGTH];
            Random random = new Random();
            random.NextBytes(data);

            // Шифрование
            byte[] encrypted;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KEY_SIZE))
            {
                rsa.ImportCspBlob(public_key);
                encrypted = rsa.Encrypt(data, true);
            }

            // Дешифрование
            byte[] decrypted;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KEY_SIZE))
            {
                rsa.ImportCspBlob(private_key);
                decrypted = rsa.Decrypt(encrypted, true);
            }

            // Проверка
            Console.WriteLine(data.SequenceEqual(decrypted) ? "Данные успешно расшифрованы" : "Ошибка при расшифровке");
        }

        protected override void OnClosed(EventArgs event_args)
        {
            base.OnClosed(event_args);
            IsClosed = true;

            Command.ui_approve = false;

            if (select_method.SelectedItem != null)
            {
                Command.last_method = select_method.Text;
            }

            Command.protection_type = protection_type.Text;
            Command.voltage_loss = Convert.ToInt32(voltage_loss.Text);
            Command.laying_method = laying_method.Text;
        }

        public bool IsClosed { get; private set; }
    }
}