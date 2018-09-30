using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace StockManagementCore
{
    public class FileLocking
    {
        #region Variables
        private FileStream configurationFile;
        private FileStream configurationFile_Open_Read;

        public FileStream LockedFile
        {
            get
            {
                configurationFile.Position = 0;
                return configurationFile;
            }
        }
        #endregion

        public void Dispose()
        {
            configurationFile.Dispose();
        }

        public FileStream ReadFile(string _FilePath, ref Exception _Error)
        {
            try
            {
                configurationFile_Open_Read = new FileStream(_FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                return configurationFile_Open_Read;

                FileStream inf = new FileStream("path1", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream outf = new FileStream("path2", FileMode.Create);
                int a;
                while ((a = inf.ReadByte()) != -1)
                {
                    outf.WriteByte((byte)a);
                }
                inf.Close();
                inf.Dispose();
                outf.Close();
                outf.Dispose();
            }
            catch (Exception err)
            {
                CustomMessageBox.Show("asdsad");
                _Error = err;
                return null;
            }
        }

        public bool OpenAndLock(string _FilePath, FileAccess _FileAccessMode, FileShare _FileShareMode, ref Exception _Error)
        {
            try
            {
                configurationFile = new FileStream(_FilePath, FileMode.Open, _FileAccessMode, _FileShareMode);
                //configurationFile.Lock(1, configurationFile.Length);
                return true;
            }
            catch (Exception err)
            {
                _Error = err;
                return false;
            }
        }

        public bool CreateAndLock(string _FilePath, FileAccess _FileAccessMode, FileShare _FileShareMode, ref Exception _Error)
        {
            try
            {
                configurationFile = new FileStream(_FilePath, FileMode.CreateNew, _FileAccessMode, _FileShareMode);
                //configurationFile.Lock(1, configurationFile.Length);
                return true;
            }
            catch (Exception err)
            {
                _Error = err;
                return false;
            }
        }

        public void WriteXDocument(XDocument _PassedXDocument)
        {
            MemoryStream bufferStream = new MemoryStream();
            
            // Reset the position and length of the file stream
            configurationFile.Position = 0;
            configurationFile.SetLength(0);
            
            // Save the document to a bufferstream
            _PassedXDocument.Save(bufferStream);

            // Set the original stream-length to the buffer stream length
            configurationFile.SetLength(bufferStream.Length);

            // Finally save the document to the file with the correct filestream length
            _PassedXDocument.Save(configurationFile);
        }
    }
}
