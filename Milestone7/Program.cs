using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Office.Interop.Outlook;
using Microsoft.SharePoint.Client;
using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

class Program
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

    static async Task Main(string[] args)
    {
        XmlConfigurator.Configure();  // Log4net configuration

        // Example usage of SharePoint and Outlook functionalities
        string siteUrl = "https://yourtenant.sharepoint.com/sites/yoursite";
        string libraryName = "Documents";

        try
        {
            await ListSharePointDocuments(siteUrl, libraryName);
            await UploadDocumentToSharePoint(siteUrl, libraryName, @"C:\path\to\yourfile.docx");
            await RetrieveOutlookCalendarEvents();
            SendOutlookEmail(@"C:\path\to\report.pdf");
            await RetrieveSharePointListItems(siteUrl, "Tasks");
            await UpdateSharePointListItem(siteUrl, "Tasks", 1, "Complete the monthly report");
        }
        catch (Exception ex)
        {
            logger.Error("An error occurred: " + ex.Message);
        }
    }

    // 1. List documents in SharePoint library
    private static async Task ListSharePointDocuments(string siteUrl, string libraryName)
    {
        try
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();

            using (ClientContext context = new ClientContext(siteUrl))
            {
                context.Credentials = new SharePointOnlineCredentials(email, new SecureString(password));
                List documentsList = context.Web.Lists.GetByTitle(libraryName);
                CamlQuery query = CamlQuery.CreateAllItemsQuery();
                ListItemCollection items = documentsList.GetItems(query);

                context.Load(items);
                context.ExecuteQuery();

                Console.WriteLine("Documents List:");
                foreach (ListItem item in items)
                {
                    Console.WriteLine("- " + item["FileLeafRef"]);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error listing documents: " + ex.Message);
        }
    }

    // 2. Upload a document to SharePoint library
    private static async Task UploadDocumentToSharePoint(string siteUrl, string libraryName, string filePath)
    {
        try
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();

            using (ClientContext context = new ClientContext(siteUrl))
            {
                context.Credentials = new SharePointOnlineCredentials(email, new SecureString(password));
                FileCreationInformation newFile = new FileCreationInformation
                {
                    ContentStream = new FileStream(filePath, FileMode.Open),
                    Url = Path.GetFileName(filePath),
                    Overwrite = true
                };

                Microsoft.SharePoint.Client.File uploadFile = context.Web.Lists.GetByTitle(libraryName).RootFolder.Files.Add(newFile);
                context.ExecuteQuery();

                Console.WriteLine($"File uploaded successfully: {uploadFile.Name}");
                logger.Info($"File uploaded: {uploadFile.Name}");
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error uploading document: " + ex.Message);
        }
    }

    // 3. Retrieve Outlook calendar events
    private static async Task RetrieveOutlookCalendarEvents()
    {
        try
        {
            var clientId = "your-client-id";
            var tenantId = "your-tenant-id";
            var clientSecret = "your-client-secret";

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
                .Build();

            var graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                var result = await app.AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" }).ExecuteAsync();
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
            }));

            var events = await graphClient.Me.Events
                .Request()
                .GetAsync();

            Console.WriteLine("Upcoming Events:");
            foreach (var calendarEvent in events)
            {
                Console.WriteLine($"- {calendarEvent.Subject}: {calendarEvent.Start.DateTime}");
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error retrieving calendar events: " + ex.Message);
        }
    }

    // 4. Send an email via Outlook with an attachment
    private static void SendOutlookEmail(string attachmentPath)
    {
        try
        {
            Application outlookApp = new Application();
            MailItem mailItem = (MailItem)outlookApp.CreateItem(OlItemType.olMailItem);
            mailItem.To = "recipient@example.com";
            mailItem.Subject = "Monthly Report";
            mailItem.Body = "Please find the attached report.";
            mailItem.Attachments.Add(attachmentPath);

            mailItem.Send();
            Console.WriteLine("Email sent successfully to " + mailItem.To);
            logger.Info($"Email sent to: {mailItem.To}");
        }
        catch (Exception ex)
        {
            logger.Error("Error sending email: " + ex.Message);
        }
    }

    // 5. Retrieve SharePoint list items
    private static async Task RetrieveSharePointListItems(string siteUrl, string listName)
    {
        try
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();

            using (ClientContext context = new ClientContext(siteUrl))
            {
                context.Credentials = new SharePointOnlineCredentials(email, new SecureString(password));
                List tasksList = context.Web.Lists.GetByTitle(listName);
                CamlQuery query = CamlQuery.CreateAllItemsQuery();
                ListItemCollection items = tasksList.GetItems(query);

                context.Load(items);
                context.ExecuteQuery();

                Console.WriteLine("Tasks List:");
                foreach (ListItem item in items)
                {
                    Console.WriteLine($"- {item["Title"]}");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error retrieving list items: " + ex.Message);
        }
    }

    // 6. Update a SharePoint list item
    private static async Task UpdateSharePointListItem(string siteUrl, string listName, int itemId, string updatedTitle)
    {
        try
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();

            using (ClientContext context = new ClientContext(siteUrl))
            {
                context.Credentials = new SharePointOnlineCredentials(email, new SecureString(password));
                List tasksList = context.Web.Lists.GetByTitle(listName);
                ListItem item = tasksList.GetItemById(itemId);

                context.Load(item);
                context.ExecuteQuery();

                item["Title"] = updatedTitle;
                item.Update();
                context.ExecuteQuery();

                Console.WriteLine($"Item updated successfully: Task ID {itemId}");
                logger.Info($"Updated Task ID: {itemId} with title: {updatedTitle}");
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error updating list item: " + ex.Message);
        }
    }

    // Helper method to read password input
    private static string ReadPassword()
    {
        string password = string.Empty;
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }

    // Helper method to create a SecureString from the password
    private static SecureString SecureString(string password)
    {
        SecureString securePassword = new SecureString();
        foreach (char c in password)
        {
            securePassword.AppendChar(c);
        }
        return securePassword;
    }
}