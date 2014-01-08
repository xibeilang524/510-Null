using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using System.IO;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCarTypeReport : Form
    {

        /// <summary>
        /// 卡片停车收费明细列表
        /// </summary>
        public List<CardPaymentInfo> CardPaymentList { get; set; }

        /// <summary>
        /// 车类型收费统计列表
        /// </summary>
        public List<CollectInfo> CollectList { get; set; }

        CardPaymentRecordBll bll = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
        public FrmCarTypeReport()
        {
            InitializeComponent();
        }

        #region 公共方法和属性

        /// <summary>
        /// 汇总CollectGridView初始化
        /// </summary>
        private void InitCollectGridView()
        {
            DataGridViewTextBoxColumn colCarType;
            DataGridViewTextBoxColumn colBigCar;
            DataGridViewTextBoxColumn colCar;
            DataGridViewTextBoxColumn colTaxi;
            DataGridViewTextBoxColumn colMotoBike2;
            DataGridViewTextBoxColumn colTotal;
            colCarType = new DataGridViewTextBoxColumn();
            colBigCar = new DataGridViewTextBoxColumn();
            colCar = new DataGridViewTextBoxColumn();
            colTaxi = new DataGridViewTextBoxColumn();
            colMotoBike2 = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            // 
            // colCarType
            // 
            colCarType.HeaderText = "车类型";
            colCarType.Name = "Name";
            colCarType.Width = 80;
            // 
            // colBigCar
            // 
            colBigCar.HeaderText = CarTypeSetting.Current.GetDescription(1);
            colBigCar.Name = "Truck";
            colBigCar.ReadOnly = true;
            colBigCar.Width = 70;
            // 
            // colCar
            // 
            colCar.HeaderText = CarTypeSetting.Current.GetDescription(0);
            colCar.Name = "Car";
            colCar.ReadOnly = true;
            colCar.Width = 90;
            // 
            // colTaxi
            // 
            colTaxi.HeaderText = CarTypeSetting.Current.GetDescription(2);
            colTaxi.Name = "SuperTruck";
            colTaxi.ReadOnly = true;
            colTaxi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colTaxi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            colTaxi.Width = 70;
            // 
            // colMotoBike2
            // 
            colMotoBike2.HeaderText = CarTypeSetting.Current.GetDescription(3);
            colMotoBike2.Name = "MotorBike";
            colMotoBike2.ReadOnly = true;
            colMotoBike2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colMotoBike2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colTotal
            // 
            colTotal.HeaderText = "合计";
            colTotal.Name = "TotalMomey";
            colTotal.ReadOnly = true;
            colTotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.CollectGridView.Columns.Clear();
            this.CollectGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            colCarType,
            colBigCar,
            colCar,
            colTaxi,
            colMotoBike2,
            colTotal});

            this.CollectGridView.RowHeadersVisible = false;
            this.CollectGridView.AllowUserToAddRows = false;
            this.CollectGridView.AllowUserToDeleteRows = false;
            this.CollectGridView.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn col in this.CollectGridView.Columns)
            {
                col.ReadOnly = true;
            }
            this.CollectGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// 报表明细初始化
        /// </summary>
        private void InitDetailGrid()
        {
            DataGridViewTextBoxColumn colOperator = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colFeeCarType = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colCarPlate = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colEnterTime = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colChangeTime = new DataGridViewTextBoxColumn();
            //DataGridViewTextBoxColumn colParkTime = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colTotalAccounts = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn colEmpty =  new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colOperator2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colFeeCarType2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colCarPlate2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colEnterTime2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colChangeTime2 = new DataGridViewTextBoxColumn();
            //DataGridViewTextBoxColumn colParkTime2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colTotalAccounts2 = new DataGridViewTextBoxColumn();

            
            // 
            // colOperator
            // 
            colOperator.HeaderText = "操作员";
            colOperator.Name = "colOperator";
            colOperator.Width = 70;
            // 
            // colFeeCarType
            // 
            colFeeCarType.HeaderText = "收费类型";
            colFeeCarType.Name = "colFeeCarType";
            colFeeCarType.ReadOnly = true;
            colFeeCarType.Width = 60;

            // 
            // colCarPlate
            // 
            colCarPlate.HeaderText = "车牌号码";
            colCarPlate.Name = "colCarPlate";
            colCarPlate.ReadOnly = true;
            colCarPlate.Width = 60;

            // 
            // colEnterTime
            // 
            colEnterTime.HeaderText = "入场时间";
            colEnterTime.Name = "colEnterTime";
            colEnterTime.ReadOnly = true;
            colEnterTime.Width = 150;
            // 
            // colChangeTime
            // 
            colChangeTime.HeaderText = "收费时间";
            colChangeTime.Name = "colChangeTime";
            colChangeTime.ReadOnly = true;
            colChangeTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colChangeTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            colChangeTime.Width = 150;
            // 
            // colParkTime
            // 
            //colParkTime.HeaderText = "停车时间";
            //colParkTime.Name = "colParkTime";
            //colParkTime.ReadOnly = true;
            //colParkTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            //colParkTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //colParkTime.Width = 70;
            // 
            // colTotalAccounts
            // 
            colTotalAccounts.HeaderText = "实收金额";
            colTotalAccounts.Name = "colTotalAccounts";
            colTotalAccounts.ReadOnly = true;
            colTotalAccounts.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colTotalAccounts.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            colTotalAccounts.Width = 70;

            // 
            // colEmpty
            // 
            colEmpty.HeaderText = "";
            colEmpty.Name = "colEmpty";
            colEmpty.Width = 20;


            // 
            // colOperator2
            //            
            colOperator2.HeaderText = "操作员";
            colOperator2.Name = "colOperator2";
            colOperator2.Width = 70;

            // 
            // colFeeCarType2
            // 
            colFeeCarType2.HeaderText = "收费类型";
            colFeeCarType2.Name = "colFeeCarType2";
            colFeeCarType2.ReadOnly = true;
            colFeeCarType2.Width = 60;

            //
            // colCarPlate2
            // 
            colCarPlate2.HeaderText = "车牌号码";
            colCarPlate2.Name = "colCarPlate2";
            colCarPlate2.ReadOnly = true;
            colCarPlate2.Width = 60;

            // 
            // colEnterTime2
            // 
            colEnterTime2.HeaderText = "入场时间";
            colEnterTime2.Name = "colEnterTime2";
            colEnterTime2.ReadOnly = true;
            colEnterTime2.Width = 150;

            // 
            // colChangeTime2
            // 
            colChangeTime2.HeaderText = "收费时间";
            colChangeTime2.Name = "colChangeTime2";
            colChangeTime2.ReadOnly = true;
            colChangeTime2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colChangeTime2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            colChangeTime2.Width = 150;

            // 
            // colParkTime2
            // 
            //colParkTime2.HeaderText = "停车时间";
            //colParkTime2.Name = "colParkTime2";
            //colParkTime2.ReadOnly = true;
            //colParkTime2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            //colParkTime2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //colParkTime2.Width = 70;

            // 
            // colTotalAccounts2
            // 
            colTotalAccounts2.HeaderText = "实收金额";
            colTotalAccounts2.Name = "colTotalAccounts2";
            colTotalAccounts2.ReadOnly = true;
            colTotalAccounts2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colTotalAccounts2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            colTotalAccounts2.Width = 70;
            colTotalAccounts2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.DetailGrid.Columns.Clear();
            this.DetailGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
            {
            colOperator,
            colFeeCarType,
            colCarPlate,
            colEnterTime,
            colChangeTime,
            //colParkTime,
            colTotalAccounts,
            colEmpty,
            colOperator2,
            colFeeCarType2,
            colCarPlate2,
            colEnterTime2,
            colChangeTime2,
            //colParkTime2,
            colTotalAccounts2
            });

            this.DetailGrid.RowHeadersVisible = false;
            this.DetailGrid.AllowUserToAddRows = false;
            this.DetailGrid.AllowUserToDeleteRows = false;
            this.DetailGrid.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn col in this.DetailGrid.Columns)
            {
                col.ReadOnly = true;
            }
            this.DetailGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion


        private void FrmReportForm_Load(object sender, EventArgs e)
        {
            this.cmbSort.SelectedItem = "收费时间";
            InitCollectGridView();

            InitDetailGrid();
            // 初始化时间控件
            this.ucDateTimeInterval2.Init();
            if (UserSetting.Current.EnableForceShifting && UserSetting.Current.ForceShiftingTime != null)
            {
                TimeEntity te = UserSetting.Current.ForceShiftingTime;
                if ((DateTime.Now.Hour > te.Hour) ||
                    (DateTime.Now.Hour == te.Hour && DateTime.Now.Minute >= te.Minute))
                {
                    ucDateTimeInterval2.StartDateTime = DateTime.Today.AddHours(te.Hour).AddMinutes(te.Minute);
                    ucDateTimeInterval2.EndDateTime = DateTime.Today.AddDays(1).AddHours(te.Hour).AddMinutes(te.Minute).AddSeconds(-1);
                }
                else
                {
                    ucDateTimeInterval2.StartDateTime = DateTime.Today.AddDays(-1).AddHours(te.Hour).AddMinutes(te.Minute);
                    ucDateTimeInterval2.EndDateTime = DateTime.Today.AddHours(te.Hour).AddMinutes(te.Minute).AddSeconds(-1);
                }
            }

            // 初始化ComboBox1下拉框
            this.operatorComboBox2.Init();
            this.workStationCombobox2.Init();
            // add by tom,2012-2-14
            this.ucEntrance2.Init();
            this.cardTypeComboBox1.Init();
            //end
        }

        /// <summary>
        /// 报表另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DetailGrid != null && CollectGridView != null)
                {
                    saveFileDialog1.Filter = "Excel文档|*.xls|所有文件(*.*)|*.*";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog1.FileName;
                        this.SaveToFile(path);
                        System.Diagnostics.Process.Start(path);
                    }
                }
            }
            catch
            {
                MessageBox.Show("保存到电子表格时出现错误!");
            }
        }
        
        public void SaveToFile(string file)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                using (fs = new FileStream(file, FileMode.Create))
                {
                    using (writer = new StreamWriter(fs, Encoding.Unicode))
                    {
                        writer.WriteLine();
                        writer.WriteLine("开始时间:" + this.ucDateTimeInterval2.StartDateTime + "结束时间:" + this.ucDateTimeInterval2.EndDateTime);
                        writer.Write(GenExcelDataString(this.CollectGridView));

                        writer.WriteLine();
                        writer.WriteLine();
                        writer.Write(GenExcelDataString(this.DetailGrid));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
            finally
            {
                if (writer != null) { writer.Close(); }
                if (fs != null) { fs.Close(); }
            }
        }


        /// <summary>
        /// 报表Excel
        /// </summary>
        /// <param name="writer"></param>
        private string GenExcelDataString( DataGridView GridView)
        {
            StringBuilder excelData = new StringBuilder();
            for (int i = 0; i < GridView.Columns.Count; i++)
            {
                excelData.Append(GridView.Columns[i].HeaderText + "\t");
            }
            excelData.AppendLine();
            
            foreach (DataGridViewRow row in GridView.Rows)
            {                
                for (int i = 0; i < GridView.Columns.Count; i++)
                {
                    if (row.Cells[i].Value != null)
                    {
                        excelData.Append(row.Cells[i].Value.ToString() + "\t");
                    }
                    else
                    {
                        excelData.Append(string.Empty + "\t");
                    }
                }
                excelData.AppendLine();
            }
            return excelData.ToString();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetPayRecord();
            GetCollectDetail();
        }

        private QueryResultList<CardPaymentInfo> GetPayRecord()
        {
            CardPaymentRecordSearchCondition con = new CardPaymentRecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval2.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval2.EndDateTime;
            // add by tom,2012-2-14
            con.CardType = this.cardTypeComboBox1.SelectedCardType;
            con.CardID = this.txtCardID.Text.Trim();
            con.Source = this.ucEntrance2.SelectedEntrances;
            con.CarPlate = this.txtCarPlate.Text;
            // end

            if (this.operatorComboBox2.SelectecOperator != null)
            {
                con.Operator = this.operatorComboBox2.SelectecOperator;
            }
            con.StationID = this.workStationCombobox2.Text;

            QueryResultList<CardPaymentInfo> result = bll.GetItems(con);
            this.CardPaymentList = SortCardPaymentData(result.QueryObjects);

            if (result.Result == ResultCode.Successful)
            {
                ShowReportsOnDetailGrid(result.QueryObjects);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
            return result;
        }

        private void ShowReportsOnDetailGrid(List<CardPaymentInfo> items)
        {
            decimal paid = 0;
            this.DetailGrid.Rows.Clear();            
            int index = 0;
            foreach (CardPaymentInfo info in SortCardPaymentData(items))
            {
                if (index % 2 == 0)
                {
                    // 偶数位显示于左边
                    DetailGrid.Rows.Add();
                    ShowCardPaymentOnDetailGridRowOne(info, index / 2);
                }
                else
                {
                    ShowCardPaymentOnDetailGridRowTwo(info, index / 2);
                }

                index++;
                paid += info.Paid;
            }
            this.txtCount.Text = DetailGrid.Rows.Count.ToString();
            this.txtPaid.Text = paid.ToString();
        }

        /// <summary>
        /// 选择排序类型
        /// </summary>
        /// <param name="items"></param>
        /// <param name="sq"></param>
        /// <returns></returns>
        private List<CardPaymentInfo>  SortCardPaymentData(List<CardPaymentInfo> items)
        {
            var sq = from CardPaymentInfo cr in items orderby cr.ChargeDateTime descending select cr;

            if (this.cmbSort.SelectedItem.ToString() == "收费时间")
            {
                if (chkReverse.Checked == true)
                {
                    sq = from CardPaymentInfo cr in items orderby cr.ChargeDateTime descending select cr;
                }
                else
                {
                    sq = from CardPaymentInfo cr in items orderby cr.ChargeDateTime select cr;
                }
            }
            else if (this.cmbSort.SelectedItem.ToString() == "实收金额")
            {
                if (chkReverse.Checked == true)
                {
                    sq = from CardPaymentInfo cr in items orderby cr.Paid descending select cr;
                }
                else
                {
                    sq = from CardPaymentInfo cr in items orderby cr.Paid select cr;
                }
            }
            else if (this.cmbSort.SelectedItem.ToString() == "收费车型")
            {
                sq = from CardPaymentInfo cr in items orderby cr.CarType select cr;
            }
            else
            {
                if (chkReverse.Checked == true)
                {
                    sq = from CardPaymentInfo cr in items orderby cr.TimeInterval descending select cr;
                }
                else
                {
                    sq = from CardPaymentInfo cr in items orderby cr.TimeInterval select cr;
                }
            }
            return sq.ToList();
        }

        private void ShowCardPaymentOnDetailGridRowOne(CardPaymentInfo info, int row)
        {
            if (info.Paid != (info.Accounts - info.LastTotalPaid))
            {
                DetailGrid.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                DetailGrid.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
            }
            DetailGrid.Rows[row].Cells["colOperator"].Value = info.OperatorID;
            DetailGrid.Rows[row].Cells["colFeeCarType"].Value = info.CarType;
            DetailGrid.Rows[row].Cells["colCarPlate"].Value = info.CarPlate;
            DetailGrid.Rows[row].Cells["colEnterTime"].Value = info.EnterDateTime.ToString();
            DetailGrid.Rows[row].Cells["colChangeTime"].Value = info.ChargeDateTime;
            //DetailGrid.Rows[row].Cells["colParkTime"].Value = info.TimeInterval.Replace("小时", "时").Replace("分钟", "分");
            DetailGrid.Rows[row].Cells["colTotalAccounts"].Value = info.Paid;
        }

        private void ShowCardPaymentOnDetailGridRowTwo(CardPaymentInfo info, int row)
        {
            if (info.Paid != (info.Accounts - info.LastTotalPaid))
            {
                DetailGrid.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                DetailGrid.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
            }
            DetailGrid.Rows[row].Cells["colOperator2"].Value = info.OperatorID;
            DetailGrid.Rows[row].Cells["colFeeCarType2"].Value = info.CarType;
            DetailGrid.Rows[row].Cells["colCarPlate2"].Value = info.CarPlate;
            DetailGrid.Rows[row].Cells["colEnterTime2"].Value = info.EnterDateTime.ToString();
            DetailGrid.Rows[row].Cells["colChangeTime2"].Value = info.ChargeDateTime;
            //DetailGrid.Rows[row].Cells["colParkTime2"].Value = info.TimeInterval.Replace("小时", "时").Replace("分钟", "分");
            DetailGrid.Rows[row].Cells["colTotalAccounts2"].Value = info.Paid;
        }



        //获取汇总详细
        private void GetCollectDetail()
        {
            QueryResultList<CardPaymentInfo> result = GetPayRecord();
            List<CollectInfo> list = new List<CollectInfo>();
            list.Add(this.GetActualPaid(result));
            list.Add(this.GetCarsAccount(result));
            list.Add(this.GetFreeTimeCars(result));
            list.Add(this.GetFreeCars(result));

            CollectList = list;

            this.CollectGridView.Rows.Clear();
            foreach (CollectInfo info in list)
            {
                DataGridViewRow row = CollectGridView.Rows[this.CollectGridView.Rows.Add()];
                row.Cells["Name"].Value = info.Name;
                row.Cells["Car"].Value = info.Car;
                row.Cells["Truck"].Value = info.Truck;
                row.Cells["SuperTruck"].Value = info.SuperTruck;
                row.Cells["MotorBike"].Value = info.MotorBike;
                row.Cells["TotalMomey"].Value = info.TotalMomey;

            }
        }

        /// <summary>
        /// 免费时段车辆数
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private CollectInfo GetFreeTimeCars(QueryResultList<CardPaymentInfo> result)
        {
            var freeCarCounts = from freeCounts in result.QueryObjects
                                where freeCounts.Accounts == 0
                                group freeCounts by freeCounts.CarType into CollectGridView
                                select new { CarType = CollectGridView.Key, Count = CollectGridView.Count() };

            CollectInfo cAcccount = new CollectInfo();
            cAcccount.Name = "免费时段";
            decimal total = 0;
            foreach (var cars in freeCarCounts)
            {
                PropertyInfo carInfo = cAcccount.GetType().GetProperty(cars.CarType.ToString());
                carInfo.SetValue(cAcccount, (decimal)cars.Count, null);
                total += cars.Count;
            }
            cAcccount.TotalMomey = total;
            return cAcccount;
        }

        /// <summary>
        /// 免费车辆数
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private CollectInfo GetFreeCars(QueryResultList<CardPaymentInfo> result)
        {
            var freeCars = from freeCounts in result.QueryObjects
                           where freeCounts.Paid == 0
                           where freeCounts.Memo == "【免费车辆】"
                           group freeCounts by freeCounts.CarType into CollectGridView
                           select new { CarType = CollectGridView.Key, Count = CollectGridView.Count() };

            CollectInfo cAcccount = new CollectInfo();
            cAcccount.Name = "免费车辆";
            decimal total = 0;
            foreach (var cars in freeCars)
            {
                PropertyInfo carInfo = cAcccount.GetType().GetProperty(cars.CarType.ToString());
                carInfo.SetValue(cAcccount, (decimal)cars.Count, null);
                total += cars.Count;
            }
            cAcccount.TotalMomey = total;
            return cAcccount;
        }

        /// <summary>
        /// 实收金额
        /// </summary>
        /// <param name="result"></param>
        /// <param name="list"></param>
        private CollectInfo GetActualPaid(QueryResultList<CardPaymentInfo> result)
        {
            var paid = from c in result.QueryObjects
                       group c by c.CarType into CollectGridView
                       select new { CarType = CollectGridView.Key, Count = CollectGridView.Sum(p => p.Paid) };

            CollectInfo cPaid = new CollectInfo();
            cPaid.Name = "实收金额";
            decimal total = 0;
            foreach (var cTotal in paid)
            {
                PropertyInfo paidInfo = cPaid.GetType().GetProperty(cTotal.CarType.ToString());
                paidInfo.SetValue(cPaid, (decimal)cTotal.Count, null);
                total += cTotal.Count;
            }
            cPaid.TotalMomey = total;
            return cPaid;
        }

        /// <summary>
        /// 总车数
        /// </summary>
        /// <param name="result"></param>
        /// <param name="list"></param>
        private CollectInfo GetCarsAccount(QueryResultList<CardPaymentInfo> result)
        {
            var carCounts = from count in result.QueryObjects
                            group count by count.CarType into CollectGridView
                            select new { CarType = CollectGridView.Key, Count = CollectGridView.Count() };

            CollectInfo cAcccount = new CollectInfo();
            cAcccount.Name = "总车数";
            decimal total = 0;
            foreach (var cars in carCounts)
            {
                PropertyInfo carInfo = cAcccount.GetType().GetProperty(cars.CarType.ToString());
                carInfo.SetValue(cAcccount, (decimal)cars.Count, null);
                total += cars.Count;
            }
            cAcccount.TotalMomey = total;
            return cAcccount;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FrmCarTypeReportView reportView = new FrmCarTypeReportView();
            reportView.Title = string.Format("打印时间范围：从:{0} 到:{1}", this.ucDateTimeInterval2.StartDateTime, this.ucDateTimeInterval2.EndDateTime);
            reportView.CollectList = CollectList;
            reportView.CardPaymentList = CardPaymentList;

            reportView.DoPrint();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            FrmCarTypeReportView reportView = new FrmCarTypeReportView();
            reportView.Title = string.Format("打印时间范围：从:{0} 到:{1}", this.ucDateTimeInterval2.StartDateTime, this.ucDateTimeInterval2.EndDateTime);
            reportView.CollectList = CollectList;
            reportView.CardPaymentList = CardPaymentList;
            reportView.Show();
        }
    }
}
