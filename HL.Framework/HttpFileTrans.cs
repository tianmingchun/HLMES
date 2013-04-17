namespace HL.Framework
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    
    public class HttpFileTrans
    {
        public readonly int CacheLength = 0x1000;
        private FileInfo localFileInfo;
        private Uri serverFileUri;

        public event EventHandler Transing;

        public HttpFileTrans(Uri serverFile, FileInfo localFile)
        {
            this.serverFileUri = serverFile;
            this.localFileInfo = localFile;
        }

        public void Download()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.serverFileUri);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            try
            {
                FileStream stream = this.LocalFileInfo.Open(FileMode.Create);
                using (stream)
                {
                    long contentLength = response.ContentLength;
                    Stream responseStream = response.GetResponseStream();
                    using (responseStream)
                    {
                        byte[] buffer = new byte[this.CacheLength];
                        int num3 = (((int) contentLength) / this.CacheLength) / 20;
                        int num4 = 0;
                        long num5 = 0L;
                        while (num5 < contentLength)
                        {
                            int count = responseStream.Read(buffer, 0, this.CacheLength);
                            stream.Write(buffer, 0, count);
                            num5 += count;
                            num4++;
                            if ((num4 > num3) && (this.Transing != null))
                            {
                                this.Transing(Convert.ToInt32((long) ((num5 * 100L) / contentLength)), EventArgs.Empty);
                                num4 = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageShow.Alert("error", exception.Message);
            }
        }

        public FileInfo LocalFileInfo
        {
            get
            {
                return this.localFileInfo;
            }
            set
            {
                this.localFileInfo = value;
            }
        }

        public Uri ServerFileUri
        {
            get
            {
                return this.serverFileUri;
            }
            set
            {
                this.serverFileUri = value;
            }
        }
    }
}

