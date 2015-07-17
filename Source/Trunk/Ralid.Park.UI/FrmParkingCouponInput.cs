using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI
{
    public partial class FrmParkingCouponInput : Form
    {
        public FrmParkingCouponInput()
        {
            InitializeComponent();
        }

        #region 公共只读属性
        /// <summary>
        /// 获取停车券折扣金额
        /// </summary>
        public decimal CouponDiscount
        {
            get
            {
                decimal discount = 0;
                for (int i = 0; i < this.gridParkingCoupon.Rows.Count; i++)
                {
                    if (this.gridParkingCoupon.Rows[i].Tag is ParkingCouponInfo)
                    {
                        int count = Convert.ToInt32(this.gridParkingCoupon.Rows[i].Cells["colCouponCount"].Value);
                        if (count > 0)
                        {
                            ParkingCouponInfo coupon = this.gridParkingCoupon.Rows[i].Tag as ParkingCouponInfo;
                            discount += coupon.CalculateDiscount(count);
                        }
                    }
                }
                return discount;
            }
        }

        /// <summary>
        /// 获取停车券份数描述
        /// </summary>
        public string CouponDescription
        {
            get
            {
                string description = string.Empty;
                for (int i = 0; i < this.gridParkingCoupon.Rows.Count; i++)
                {
                    if (this.gridParkingCoupon.Rows[i].Tag is ParkingCouponInfo)
                    {
                        int count = Convert.ToInt32(this.gridParkingCoupon.Rows[i].Cells["colCouponCount"].Value);
                        if (count > 0)
                        {
                            ParkingCouponInfo coupon = this.gridParkingCoupon.Rows[i].Tag as ParkingCouponInfo;
                            description += string.Format("{0}：{1}；", coupon.Name, count);
                        }
                    }
                }
                return description;
            }
        }
        #endregion

        #region 私有方法
        private bool CheckParkingCouponInput()
        {
            if (this.gridParkingCoupon.Rows.Count > 0)
            {
                int count = 0;

                for (int i = 0; i < this.gridParkingCoupon.Rows.Count; i++)
                {
                    if (this.gridParkingCoupon.Rows[i].Cells["colCouponCount"].Value != null
                            &&!int.TryParse(this.gridParkingCoupon.Rows[i].Cells["colCouponCount"].Value.ToString(), out count))
                    {
                        MessageBox.Show(Resources.Resource1.FrmParkingCouponInput_InvalidCouponCount);
                        this.gridParkingCoupon.Focus();
                        this.gridParkingCoupon.CurrentCell = this.gridParkingCoupon.Rows[i].Cells["colCouponCount"];
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        private void FrmParkingCouponInput_Load(object sender, EventArgs e)
        {
            this.gridParkingCoupon.Rows.Clear();
            if (UserSetting.Current != null
                && UserSetting.Current.ParkingCoupon != null
                && UserSetting.Current.ParkingCoupon.Count > 0)
            {
                foreach (ParkingCouponInfo coupon in UserSetting.Current.ParkingCoupon)
                {
                    int row = this.gridParkingCoupon.Rows.Add();
                    this.gridParkingCoupon.Rows[row].Tag = coupon;
                    this.gridParkingCoupon.Rows[row].Cells["colCouponName"].Value = coupon.Name;
                    this.gridParkingCoupon.Rows[row].Cells["colCouponValue"].Value = coupon.ParValue;
                }
                if (this.gridParkingCoupon.Rows.Count > 0)
                {
                    this.gridParkingCoupon.CurrentCell=this.gridParkingCoupon.Rows[0].Cells["colCouponCount"];
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckParkingCouponInput())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
