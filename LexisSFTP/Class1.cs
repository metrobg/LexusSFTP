using System;
using System.IO;
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
       // static string ftpRemoteFolder;
        static string ftpRemoteFile;

        //static string ftplocalFolder;
        static string ftpLocalFile;

        Sftp client;       // initiate ftp

        public Class1()
        {
            // TODO: add code here
            client = new Sftp();
            client.TransferType = SftpTransferType.Ascii;

        }

        public void setRemoteFileName(string remotefile) 
        {
            ftpRemoteFile = remotefile;
        }
        public void setLocalFileName(string localfile)
        {
            ftpLocalFile = localfile;
        }

        public void setTransferType(string xferType)
        {
            if (xferType.ToLower() == "binary")
            {
                client.TransferType = SftpTransferType.Binary;
            }
        }

        public int setConnectionParams(string method, string host, string user, string password)
        {
            ftpOp = method;
            ftpHost = host;
            ftpUser = user;
            ftpPassword = password;

            int rc = 0;
            try
            {

                rc = setUpFileTransfer(client, ftpHost, ftpUser, ftpPassword);
                if (rc > 0)
                {
                    Environment.Exit(9);    // terminate process if login failed
                }
                if (ftpOp == "get")
                {
                    Console.WriteLine("downloading remote file {0} to  local location {1}", ftpRemoteFile, ftpLocalFile);
                }
                if (ftpOp == "put")
                {
                    Console.WriteLine("upoading file {0} to remote location {1}", ftpLocalFile, ftpRemoteFile);
                }
            }
            catch (Rebex.Net.SftpException e)
            {
                Console.WriteLine("exception caught {0}", e.Message);
                return -1;
            }
            return 0;
        }

        public void clientDisconnect()
        {
            client.Disconnect();
        }
        public int putFile(string local, string remote)
        {
            try
            {
                client.PutFile(local, remote);
            }
            catch (SftpException ex)
            {
                Console.WriteLine("exception caught: {0}", ex.Message);
                return 9;
            }
            return 0;
        }

        public int getFile(string remote, string local)
        {
            try
            {
                client.GetFile(remote, local);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception caught: {0}", ex.Message);
                return 9;
            }
            return 0;
        }



        public int setUpFileTransfer(Rebex.Net.Sftp ftpClient, string site, string user, string password)
        {
            try
            {
                ftpClient.Connect(site);
                ftpClient.Login(user, password);
            }
            catch (SftpException ex)
            {
                Console.WriteLine("Login failed: {0}  {1}", ex.Message, ex.Status);
                return 9;
            }
            Console.WriteLine("Login success; connected to host: {0}", site);
            return 0;
        }
    }
}
