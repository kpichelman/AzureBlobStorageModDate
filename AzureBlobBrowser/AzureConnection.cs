using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;

namespace AzureBlobBrowser
{
    class AzureConnection
    {

        public string SetupConnection(string accountName, string accountKey, string containerName)
        {
            Microsoft.WindowsAzure.Storage.Auth.StorageCredentials creds = new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(accountName, accountKey);
            _storageAccount = new CloudStorageAccount(creds, false); // The boolean is use HTTPS or not
            _blobClient = _storageAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(containerName);
            _connectionSetup = true;
            return "\nConnected to " + accountName;
        }

        public string ListFiles()
        {
            if (!_connectionSetup)
            {
                return "\nError, cannot list files when not connection not setup.";
            }

            string output = "";
            try
            {
                IEnumerable<IListBlobItem> items = _blobContainer.ListBlobs("", true, BlobListingDetails.All, null, null);
                foreach (IListBlobItem item in items)
                {
                    try
                    {
                        if (item.GetType() == typeof(CloudBlockBlob))
                        {
                            CloudBlockBlob blob = (CloudBlockBlob)item;
                            output += "\nBlock blob " + blob.Uri + " Mod date: " + blob.Properties.LastModified + "\n";
                        }
                        else if (item.GetType() == typeof(CloudPageBlob))
                        {
                            CloudPageBlob pageBlob = (CloudPageBlob)item;
                            output += "\nPage blob " + pageBlob.Uri + " Mod date: " + pageBlob.Properties.LastModified + "\n";

                        }
                        else if (item.GetType() == typeof(CloudBlobDirectory))
                        {
                            CloudBlobDirectory directory = (CloudBlobDirectory)item;
                            output += "\nDirectory: " + directory.Uri;
                        }
                    } catch (Exception ex1)
                    {
                        output += "\nError listing file: " + ex1.Message;
                    }

                }
            } catch (Exception ex)
            {
                output += "\nError listing files: " + ex.Message;
            }

            output += "\n\nList Complete\n";
            return output;
        }

        bool _connectionSetup = false;
        CloudStorageAccount _storageAccount;
        CloudBlobClient _blobClient;
        CloudBlobContainer _blobContainer;
    }
}
