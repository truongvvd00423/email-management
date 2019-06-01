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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace email_management
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class main : Page
    {
        public main()
        {
            this.InitializeComponent();
            loginedEmail.Text = "Hello " + MainPage.email;
            receiveMail();
        }

        private void btnClickCompose(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(compose));
        }

        private void btnClickReturn(object sender, RoutedEventArgs e)
        {
            MainPage.imap.CloseAsync();
            MainPage.email = "";
            MainPage.password = "";
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void receiveMail()
        {
                await MainPage.imap.SelectInboxAsync();
                List<long> uids = await MainPage.imap.SearchAsync(Flag.All);
                foreach (long uid in uids)
                {
                    var eml = await MainPage.imap.GetMessageByUIDAsync(uid);
                    IMail mail = new MailBuilder().CreateFromEml(eml);
                    string content = mail.Subject;
                    listInbox.Items.Add(content);
            }

        }
        }
}
