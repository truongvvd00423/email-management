using Limilabs.Client.IMAP;
using Limilabs.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace email_management
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Imap imap;

        public static string email;

        public static string password;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnLoginClick(object sender, RoutedEventArgs e)
        {
            email = txtEmail.Text;
            password = txtPassword.Password.ToString();

            imap = new Imap();
               await imap.ConnectSSL("imap.gmail.com");  // or ConnectSSL for SSL
            try
            {
                await imap.UseBestLoginAsync(email, password);
            }
            catch(Exception ex)
            {
                txtError.Text = "Invalid email or password! " + ex.Message;
                return;
            }
            
            this.Frame.Navigate(typeof(main));
        }
    }
}
