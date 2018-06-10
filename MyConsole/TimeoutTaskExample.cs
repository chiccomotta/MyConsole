using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyConsole
{
    public class TimeoutTaskExample
    {
        // Declare a System.Threading.CancellationTokenSource.  
        CancellationTokenSource cts;
     
        public async Task StartTask()
        {
            // Instantiate the CancellationTokenSource.  
            cts = new CancellationTokenSource();            ;

            try
            {
                // ***Set up the CancellationTokenSource to cancel after 2.5 seconds. (You  
                // can adjust the time.)  
                cts.CancelAfter(2500);

                await AccessTheWebAsync(cts.Token);
                Console.WriteLine("\r\nDownloads succeeded.\r\n");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\r\nDownloads canceled.\r\n");
            }
            catch (Exception)
            {
                Console.WriteLine("\r\nDownloads failed.\r\n");
            }

            cts = null;
        }

        // You can still include a Cancel button if you want to.  
        //private void cancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (cts != null)
        //    {
        //        cts.Cancel();
        //    }
        //}

        async Task AccessTheWebAsync(CancellationToken ct)
        {
            // Declare an HttpClient object.  
            HttpClient client = new HttpClient();

            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList();

            foreach (var url in urlList)
            {
                // GetAsync returns a Task<HttpResponseMessage>.   
                // Argument ct carries the message if the Cancel button is chosen.   
                // Note that the Cancel button cancels all remaining downloads.  
                HttpResponseMessage response = await client.GetAsync(url, ct);

                // Retrieve the website contents from the HttpResponseMessage.  
                byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

                Console.WriteLine(String.Format("\r\nLength of the downloaded string: {0}.\r\n", urlContents.Length));
            }
        }

        private List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.microsoft.com/library/hh290136.aspx",
                "http://msdn.microsoft.com/library/ee256749.aspx",
                "http://msdn.microsoft.com/library/ms404677.aspx",
                "http://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }
    }
}

