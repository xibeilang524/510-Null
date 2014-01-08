using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Imaging;


namespace Ralid.Park.UI
{
    public class DirectPrint
    {
        private IList<Stream> m_streams;
        private int m_currentPageIndex;

        /// <summary>
        /// 执行直接打印
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public bool DoPrint(LocalReport report)
        {
            try
            {
                Export(report);
                m_currentPageIndex = 0;
                if (!NBPrint())
                {
                    return false;
                }
                if (m_streams != null)
                {
                    foreach (Stream stream in m_streams)
                        stream.Close();
                    m_streams = null;
                }
                return true;
            }
            catch
            {
                throw new Exception("在打印过程中出现异常!");

            }

        }
        private string _errormessage = "";
        private string ErrorMessage
        {
            get
            {
                return _errormessage;
            }
        }
        private bool NBPrint()
        {
            if (m_streams == null || m_streams.Count == 0)
            {
                throw new Exception("在打印过程中出现异常!");
                //return false;
            }
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("请添加默认打印机!");
                // return false;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
            return true;
        }

        private void Export(LocalReport report)
        {
            //7.5in 3.66in 0 0 0 0 当前设置为A4纵向
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>20.7cm</PageWidth>" +
              "  <PageHeight>28cm</PageHeight>" +
              "  <MarginTop>0in</MarginTop>" +
              "  <MarginLeft>0in</MarginLeft>" +
              "  <MarginRight>0in</MarginRight>" +
              "  <MarginBottom>0in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;
            }
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            string filenameext = DateTime.Now.Year.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            Stream stream = new FileStream(name + "." + filenameext, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}
