using System;
using Rebex;
using Rebex.Net;


namespace LexisSFTP
{
    public class Class1
    {
        static string ftpHost;
        static string ftpUser;
        static string ftpPassword;
        static string ftpOp;
        static string ftpRemoteFolder;
        static string ftpRemoteFile;

        static string ftplocalFolder;
        static string ftplocalFile;

        Sftp client;       // initiate ftp

        public Class1()
        {
            // TODO: add code here
            client = new Sftp();

        }

        public void setRemoteFileName(string remotefile)
        {
            ftpRemoteFile = remotefile;
        }

        public void setTransferType(string xferType)
        {
            client.TransferType = SftpTransferType.Ascii;
            if (xferType.ToUpper() == "BINARY")
            {
                client.TransferType = SftpTransferType.Binary;
            }
        }
        public void setLocalFileName(string localfile)
        {
            ftplocalFile = localfile;
        }
        public int setConnectionParams(string mode, string host, string user, string password)
        {
            ftpOp = mode;
            ftpHost = host;
            ftpUser = user;
            ftpPassword = password;


            try
            {

                setUpFileTransfer(client, ftpHost, ftpUser, ftpPassword);
                if (ftpOp == "get")
                {
                    Console.WriteLine("downloading file {0}", ftpRemoteFile);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("exception caught {0}", e.ToString());
                return -1;
            }

            return 0;
        }

        public void clientDisconnect()
        {
            client.Disconnect();
        }
        public int sendFile(string local, string remote)
        {
            client.PutFile(local, remote);
            return 0;
        }

        public int getFile(string remote, string local)
        {
            client.GetFile(remote, local);
            return 0;
        }



        static void setUpFileTransfer(Rebex.Net.Sftp ftpClient, string site, string user, string password)
        {

            Console.WriteLine("Connecting to Host: {0}", site);

            ftpClient.Connect(site);
            ftpClient.Login(user, password);

        }
    }
}
