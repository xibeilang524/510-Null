using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class ParkDataContext : System.Data.Linq.DataContext
    {
        public ParkDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
        }

        public ParkDataContext(IDbConnection connect, System.Data.Linq.Mapping.MappingSource mappingSource)
            : base(connect, mappingSource)
        {
        }

        public System.Data.Linq.Table<AlarmInfo> Alarm
        {
            get
            {
                return this.GetTable<AlarmInfo>();
            }
        }

        public System.Data.Linq.Table<CardChargeRecord> CardCharge
        {
            get
            {
                return this.GetTable<CardChargeRecord>();
            }
        }

        public System.Data.Linq.Table<CardDeferRecord> CardDefer
        {
            get
            {
                return this.GetTable<CardDeferRecord>();
            }
        }

        public System.Data.Linq.Table<CardDisableEnableRecord> CardDisableEnable
        {
            get
            {
                return this.GetTable<CardDisableEnableRecord>();
            }
        }

        public System.Data.Linq.Table<CardEventRecord> CardEvent
        {
            get
            {
                return this.GetTable<CardEventRecord>();
            }
        }


        public System.Data.Linq.Table<CardLostRestoreRecord> CardLostRestore
        {
            get
            {
                return this.GetTable<CardLostRestoreRecord>();
            }
        }

        public System.Data.Linq.Table<CardRecycleRecord> CardRecycle
        {
            get
            {
                return this.GetTable<CardRecycleRecord>();
            }
        }

        public System.Data.Linq.Table<CardReleaseRecord> CardRelease
        {
            get
            {
                return this.GetTable<CardReleaseRecord>();
            }
        }

        public System.Data.Linq.Table<EntranceInfo> Entrance
        {
            get
            {
                return this.GetTable<EntranceInfo>();
            }
        }

        public System.Data.Linq.Table<OperatorSettleLog> OperatorLog
        {
            get
            {
                return this.GetTable<OperatorSettleLog>();
            }
        }

        public System.Data.Linq.Table<SysparameterInfo> Sysparameter
        {
            get
            {
                return this.GetTable<SysparameterInfo>();
            }
        }

        public System.Data.Linq.Table<VideoSourceInfo> VideoSource
        {
            get
            {
                return this.GetTable<VideoSourceInfo>();
            }
        }

        public System.Data.Linq.Table<WaitingCommandInfo> WaitingCommand
        {
            get
            {
                return this.GetTable<WaitingCommandInfo>();
            }
        }

        public System.Data.Linq.Table<WorkStationInfo> WorkStation
        {
            get
            {
                return this.GetTable<WorkStationInfo>();
            }
        }

        public System.Data.Linq.Table<ParkInfo> Park
        {
            get
            {
                return this.GetTable<ParkInfo>();
            }
        }

        public System.Data.Linq.Table<CardEventReport> EventReport
        {
            get
            {
                return this.GetTable<CardEventReport>();
            }
        }

        public System.Data.Linq.Table<SnapShot> SnapShot
        {
            get
            {
                return this.GetTable<SnapShot>();
            }
        }

        public System.Data.Linq.Table<CardInfo> Card
        {
            get
            {
                return this.GetTable<CardInfo>();
            }
        }

        public System.Data.Linq.Table<OperatorInfo> Operator
        {
            get
            {
                return this.GetTable<OperatorInfo>();
            }
        }

        public System.Data.Linq.Table<PREOperatorInfo> PREOperator
        {
            get
            {
                return this.GetTable<PREOperatorInfo>();
            }
        }

        public System.Data.Linq.Table<RoleInfo> Role
        {
            get
            {
                return this.GetTable<RoleInfo>();
            }
        }

        public System.Data.Linq.Table<DeptInfo> Dept
        {
            get
            {
                return this.GetTable<DeptInfo>();
            }
        }

        public System.Data.Linq.Table<PRERoleInfo> PRERole
        {
            get
            {
                return this.GetTable<PRERoleInfo>();
            }
        }

        public System.Data.Linq.Table<PREBusinesses> PREBusinesses
        {
            get
            {
                return this.GetTable<PREBusinesses>();
            }
        }

        public System.Data.Linq.Table<PREPreferentialInfo> PREPreferentialInfo
        {
            get
            {
                return this.GetTable<PREPreferentialInfo>();
            }
        }

        public System.Data.Linq.Table<PREPreferentialLog> PREPreferentialLog
        {
            get
            {
                return this.GetTable<PREPreferentialLog>();
            }
        }

        public System.Data.Linq.Table<RoadWayInfo> RoadWay
        {
            get
            {
                return this.GetTable<RoadWayInfo>();
            }
        }

        public System.Data.Linq.Table<FreeAuthorizationLog> FreeAuthorizationLog
        {
            get
            {
                return this.GetTable<FreeAuthorizationLog>();
            }
        }

        public System.Data.Linq.Table<APMCheckOutRecord> APMCheckOutRecord
        {
            get
            {
                return this.GetTable<APMCheckOutRecord>();
            }
        }

        public System.Data.Linq.Table<APMRefundRecord> APMRefundRecord
        {
            get
            {
                return this.GetTable<APMRefundRecord>();
            }
        }

        public System.Data.Linq.Table<ServerSwitchRecord> ServerSwitchRecord
        {
            get
            {
                return this.GetTable<ServerSwitchRecord>();
            }
        }
    }
}