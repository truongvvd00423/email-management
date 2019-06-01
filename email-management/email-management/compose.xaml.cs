using Limilabs.Client.SMTP;
using Limilabs.Mail;
using Limilabs.Mail.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
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
    public sealed partial class compose : Page
    {
        FileOpenPicker FilePick;

        StorageFile AttachFile;

        public compose()
        {
            this.InitializeComponent();
        }

        private async void btnAttackFile(object sender, RoutedEventArgs e)

        {

            FilePick = new FileOpenPicker();

            FilePick.FileTypeFilter.Clear();

            FilePick.FileTypeFilter.Add(".doc");

            FilePick.FileTypeFilter.Add(".png");

            FilePick.FileTypeFilter.Add(".txt");

            FilePick.FileTypeFilter.Add(".jpg");

            AttachFile = await FilePick.PickSingleFileAsync();

        }

        private void btnClickReturn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(main));
        }

        private async void btnSend(object sender, RoutedEventArgs e)
        {
            MailBuilder myMail = new MailBuilder();

            myMail.Html = txtContent.Text;

            myMail.Subject = txtSubject.Text;

            await myMail.AddAttachment(AttachFile);

            myMail.To.Add(new MailBox(txtTo.Text));

            myMail.From.Add(new MailBox(MainPage.email));



            IMail email = myMail.Create();



            try

            {

                using (Smtp smtp = new Smtp())

                {

                    await smtp.Connect("smtp.gmail.com", 587);

                    await smtp.UseBestLoginAsync(MainPage.email, MainPage.password);

                    await smtp.SendMessageAsync(email);

                    await smtp.CloseAsync();

                    MessageDialog msg = new MessageDialog("Mail has been sucessfully sent");

                    msg.ShowAsync();

                }

            }

            catch (Exception ex)

            {

                MessageDialog msg = new MessageDialog("Can not send mail! " + ex.Message);

                msg.ShowAsync();

            }
        }
    }
}
