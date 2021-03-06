﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.OpenCard.OpenCardService.YCT;
using ICSharpCode.SharpZipLib.Zip;

namespace Ralid.OpenCard.YCTFtpTool
{
    public class YCTUploadFileFactory
    {
        #region 私有方法
        private static string GetM1Record(YCTPaymentRecord r)
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\r\n",
                       r.PSN.ToString().PadLeft(8, '0'),
                       r.LCN,
                       r.FCN,
                       r.LPID,
                       r.LTIM,
                       r.PID,
                       r.TIM.ToString("yyyyMMddHHmmss"),
                       r.TF.ToString().PadLeft(7, '0').Insert(5, "."),
                       r.BAL.ToString().PadLeft(7, '0').Insert(5, "."),
                       r.TT.ToString("X2"),
                       r.XRN.ToString().PadLeft(5, '0'),
                       r.EPID,
                       r.ETIM,
                       "00",
                       "0",
                       r.TAC
                );
        }

        private static string GetCPURecord(YCTPaymentRecord r)
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}\r\n",
                       r.PID,
                       r.PSN.ToString().PadLeft(10, '0'),
                       r.TIM.ToString("yyyyMMddHHmmss"),
                       r.LCN,
                       r.FCN,
                       r.TF.ToString().PadLeft(7, '0').Insert(5, "."),
                       r.FEE.ToString().PadLeft(7, '0').Insert(5, "."),
                       r.BAL.ToString().PadLeft(7, '0').Insert(5, "."),
                       r.TT.ToString("X2"),
                       r.ATT.ToString("X2"),
                       r.CRN.ToString().PadLeft(5, '0'),
                       r.XRN.ToString().PadLeft(5, '0'),
                       r.DMON,
                       r.BDCT.ToString().PadLeft(3, '0'),
                       r.MDCT.ToString().PadLeft(3, '0'),
                       r.UDCT.ToString().PadLeft(3, '0'),
                       r.EPID,
                       r.ETIM,
                       r.LPID,
                       r.LTIM,
                       r.AREA,
                       r.ACT,
                       r.SAREA,
                       r.TAC
                );
        }
        #endregion

        #region 公共方法
        public static string CreateM1UploadFile(DateTime dt, string zip, List<YCTPaymentRecord> records, List<YCTBlacklist> blacks)
        {
            records = (from it in records
                       orderby it.PID ascending, it.PSN ascending
                       select it).ToList(); //按交易设备号和流水号排序
            string prefix = Path.GetFileNameWithoutExtension(zip).Substring(2); //从压缩文件名中取出其它文件需要的相同部分,
            string fjy = string.Format("{0}{1}.txt", "JY", prefix);
            string fsy = string.Format("{0}{1}.txt", "SY", prefix);
            string frz = string.Format("{0}{1}.txt", "RZ", prefix);
            string fmd = string.Format("{0}{1}.txt", "MD", prefix);
            StringBuilder jy = new StringBuilder();
            StringBuilder rz = new StringBuilder();
            StringBuilder sy = new StringBuilder();
            StringBuilder md = new StringBuilder();
            for (int i = 0; i < records.Count; i++)
            {
                jy.Append(GetM1Record(records[i]));
            }
            rz.Append(string.Format("{0}\t{1}\t{2}\t{3}\r\n", "0001", fjy, records.Count.ToString().PadLeft(10, '0'), records.Sum(it => it.FEE).ToString().PadLeft(9, '0').Insert(7, ".")));

            var groups = records.GroupBy(it => it.PID);
            int index = 0;
            foreach (var group in groups)
            {
                string strSy = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\r\n",
                                                index.ToString().PadLeft(5, '0'),
                                                group.Key.PadLeft(8, '0'),
                                                dt.ToString("yyyyMMddHHmmss"),
                                                "1",
                                                group.Count<YCTPaymentRecord>().ToString().PadLeft(8, '0'),
                                                "0".PadLeft(8, '0'),
                                                group.Sum(it => it.FEE).ToString().PadLeft(9, '0').Insert(7, "."),
                                                "0000000.00",
                                                group.Max(it => it.PSN).ToString().PadLeft(8, '0'),
                                                new string('0', 128));
                sy.Append(strSy);
                index++;
            }
            rz.Append(string.Format("{0}\t{1}\t{2}\t{3}\r\n", "0002", fsy, index.ToString().PadLeft(10, '0'), records.Sum(it => it.FEE).ToString().PadLeft(9, '0').Insert(7, ".")));

            if (blacks != null)
            {
                foreach (YCTBlacklist b in blacks)
                {
                    md.Append(string.Format("{0}\t{1}\t{2}\r\n", b.FCN, b.LCN, b.Reason));
                }
            }
            string bc = blacks != null ? blacks.Count.ToString() : "0";
            rz.Append(string.Format("{0}\t{1}\t{2}\t{3}\r\n", "0003", fmd, bc.PadLeft(10, '0'), "0".PadLeft(9, '0').Insert(7, ".")));

            string path = FTPFolderFactory.CreateUploadFolder();
            if (string.IsNullOrEmpty(path)) return null;
            string localZip = Path.Combine(path, zip);
            try
            {
                using (ZipFileWriter writer = new ZipFileWriter(localZip))
                {
                    writer.WriteFile(fjy, ASCIIEncoding.ASCII.GetBytes(jy.ToString()));
                    writer.WriteFile(fsy, ASCIIEncoding.ASCII.GetBytes(sy.ToString()));
                    writer.WriteFile(fmd, md.Length == 0 ? null : ASCIIEncoding.ASCII.GetBytes(md.ToString()));
                    writer.WriteFile(frz, ASCIIEncoding.ASCII.GetBytes(rz.ToString()));
                }
                return localZip;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        public static string CreateCPUUploadFile(DateTime dt, string zip, List<YCTPaymentRecord> records, List<YCTBlacklist> blacks)
        {
            records = (from it in records
                       orderby it.PID ascending, it.PSN ascending
                       where !string.IsNullOrEmpty(it.TAC)  //TAC字段不能为空
                       select it).ToList(); //按交易设备号和流水号排序
            string prefix = Path.GetFileNameWithoutExtension(zip).Substring(2); //从压缩文件名中取出其它文件需要的相同部分,
            string fjy = string.Format("{0}{1}.txt", "JY", prefix);
            string frz = string.Format("{0}{1}.txt", "RZ", prefix);
            string fmd = string.Format("{0}{1}.txt", "MD", prefix);
            StringBuilder jy = new StringBuilder();
            StringBuilder rz = new StringBuilder();
            StringBuilder md = new StringBuilder();
            for (int i = 0; i < records.Count; i++)
            {
                jy.Append(GetCPURecord(records[i]));
            }
            if (blacks != null)
            {
                foreach (YCTBlacklist b in blacks)
                {
                    md.Append(string.Format("{0}\t{1}\t{2}\r\n", b.FCN, b.LCN, b.Reason));
                }
            }
            rz.Append(string.Format("{0}\t{1}\t{2}\t{3}\r\n", "00001", fjy, records.Count.ToString().PadLeft(10, '0'), records.Sum(it => it.FEE).ToString().PadLeft(11, '0').Insert(9, ".")));
            string bc = blacks != null ? blacks.Count.ToString() : "0";
            rz.Append(string.Format("{0}\t{1}\t{2}\t{3}\r\n", "00002", fmd, bc.PadLeft(10, '0'), "0".PadLeft(11, '0').Insert(9, ".")));

            string path = FTPFolderFactory.CreateUploadFolder();
            if (string.IsNullOrEmpty(path)) return null;
            string localZip = Path.Combine(path, zip);
            try
            {
                using (ZipFileWriter writer = new ZipFileWriter(localZip))
                {
                    writer.WriteFile(fjy, ASCIIEncoding.ASCII.GetBytes(jy.ToString()));
                    writer.WriteFile(fmd, md.Length == 0 ? null : ASCIIEncoding.ASCII.GetBytes(md.ToString()));
                    writer.WriteFile(frz, ASCIIEncoding.ASCII.GetBytes(rz.ToString()));
                }
                return localZip;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }
        #endregion
    }
}
