using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SQLite;
using Ralid.Parking.POS.Model;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.DAL
{
    public class CardPaymentInfoProvider
    {
        #region 构造函数
        public CardPaymentInfoProvider()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string connStr = "Data Source=" + Path.Combine(appPath, "Parking.db");
            _RepoUri = connStr;
        }
        #endregion

        #region 私有变量
        private string _RepoUri;
        #endregion

        #region 私有方法
        private int ExcuteCmd(string sql)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_RepoUri))
                {
                    con.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = sql;
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return 0;
        }
        #endregion

        #region 公共方法
        public bool Add(CardPaymentInfo info)
        {
            string sql = "insert into CardPaymentRecord values ('" +
                       info.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + info.CardID + "','" +
                      CardPaymentInfoSerializer.Serialize(info) + "')";
            return ExcuteCmd(sql) == 1;
        }

        public bool Delete(CardPaymentInfo info)
        {
            string sql = "delete from CardPaymentRecord where ChargeDateTime='" + info.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            return ExcuteCmd(sql) > 0;
        }

        public List<CardPaymentInfo> GetRecords(DateTime dtBegin, DateTime dtEnd)
        {
            List<CardPaymentInfo> items = null;
            string sql = "select Record from CardPaymentRecord where ChargeDateTime>='" + dtBegin.ToString("yyyy-MM-dd HH:mm:ss") +
                       "' and ChargeDateTime<='" + dtEnd.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_RepoUri))
                {
                    con.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = sql;
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string record = reader.GetString(0);
                            if (!string.IsNullOrEmpty(record))
                            {
                                CardPaymentInfo payment = CardPaymentInfoSerializer.Deserialize(record);
                                if (payment != null)
                                {
                                    if (items == null) items = new List<CardPaymentInfo>();
                                    items.Add(payment);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return items;
        }

        /// <summary>
        /// 收费记录保存到文件
        /// </summary>
        /// <param name="info"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool SaveToFile(CardPaymentInfo info, string file)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine(CardPaymentInfoSerializer.Serialize(info));
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return false;
        }
        #endregion
    }
}
