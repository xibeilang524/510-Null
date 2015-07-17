
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarType' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table cardevent add CarType tinyint')
    exec ('update cardevent set carType=0')
    exec ('alter table CardEvent alter column CarType tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LostCardCost' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
BEGIN
	exec ('alter table cardlostRestore add LostCardCost decimal(10,2)')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentMode' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
BEGIN
	exec ('alter table cardlostRestore add PaymentMode tinyint')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
BEGIN
	exec ('alter table cardlostRestore add SettleDateTime datetime')
end
go

--更新各记录的操作员ID为操作员姓名
exec  ('
update CardPaymentRecord set OperatorNum=OperatorName 
   from Operator where CardPaymentRecord.OperatorNum=Operator.OperatorID and CardPaymentRecord.SettleDateTime is null
update CardPaymentRecord set StationID=StationName 
   from WorkStation where CardPaymentRecord.StationID=WorkStation.StationID and CardPaymentRecord.SettleDateTime is null

update CardRelease set OperatorID=OperatorName
   from Operator where CardRelease.OperatorID=Operator.OperatorID and CardRelease.SettleDateTime is null
update CardRelease set StationID=StationName 
   from WorkStation where CardRelease.StationID=WorkStation.StationID and CardRelease.SettleDateTime is null

update CardDefer set OperatorID=OperatorName
   from Operator where CardDefer.OperatorID=Operator.OperatorID and CardDefer.SettleDateTime is null
update CardDefer set StationID=StationName 
   from WorkStation where CardDefer.StationID=WorkStation.StationID and CardDefer.SettleDateTime is null

update CardCharge set OperatorID=OperatorName
   from Operator where CardCharge.OperatorID=Operator.OperatorID and CardCharge.SettleDateTime is null
update CardCharge set StationID=StationName 
   from WorkStation where CardCharge.StationID=WorkStation.StationID and CardCharge.SettleDateTime is null

update CardRecycle set OperatorID=OperatorName
   from Operator where CardRecycle.OperatorID=Operator.OperatorID and CardRecycle.SettleDateTime is null
update CardRecycle set StationID=StationName 
   from WorkStation where CardRecycle.StationID=WorkStation.StationID and CardRecycle.SettleDateTime is null

update CardRecycle set OperatorID=OperatorName
   from Operator where CardRecycle.OperatorID=Operator.OperatorID and CardRecycle.SettleDateTime is null
update CardRecycle set StationID=StationName 
   from WorkStation where CardRecycle.StationID=WorkStation.StationID and CardRecycle.SettleDateTime is null

update CardEvent set OperatorNum=OperatorName
   from Operator where CardEvent.OperatorNum=Operator.OperatorID and CardEvent.SettleDateTime is null
update CardEvent set StationID=StationName 
   from WorkStation where CardEvent.StationID=WorkStation.StationID and CardEvent.SettleDateTime is null
')
go


if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfCardLost' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOfCardLost decimal(10,2)')
    exec ('update OperatorSettleLog set CashOfCardLost=0')
    exec ('alter table OperatorSettleLog alter column CashOfCardLost decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='NonCashOfCardLost' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add NonCashOfCardLost decimal(10,2)')
    exec ('update OperatorSettleLog set NonCashOfCardLost=0')
    exec ('alter table OperatorSettleLog alter column NonCashOfCardLost decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='HandInCash' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add HandInCash decimal(10,2)')
end
go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDeleteRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardDeleteRecord](
	[CardID] [nvarchar](50) NOT NULL,
	[DeleteDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[CardType] [tinyint] NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[ValidDate] [datetime] NOT NULL,
	[Deposit] [decimal](10, 2) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardDeleteRecord] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[DeleteDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='IsCenterCharge' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardpaymentRecord add IsCenterCharge bit')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='IsNested' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add IsNested bit')
	exec ('update Park set IsNested=0')
    exec ('alter table Park alter column IsNested bit not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ParentID' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add ParentID int')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ForceWithCount' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add ForceWithCount bit')
    exec ('update Park set ForceWithCount=0')
    exec ('alter table Park alter column ForceWithCount bit not null')
end
go

--2013-4-28 增加一个保存各车型,卡型车位数的表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ParkCarPort]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ParkCarPort](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ParkID] [int] NOT NULL,
		[CardType] [tinyint] NULL,
		[CarType] [tinyint] NULL,
		[CarPort] [smallint] NOT NULL,
		[Vacant] [smallint] NOT NULL,
	 CONSTRAINT [PK_ParkCarPort] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaidDateTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add PaidDateTime datetime')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ParkFee' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add ParkFee decimal(10,2)')
    exec ('update Card set ParkFee=0')
    exec ('alter table Card alter column ParkFee decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='TotalPaidFee' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add TotalPaidFee decimal(10,2)')
    exec ('update Card set TotalPaidFee=0')
    exec ('alter table Card alter column TotalPaidFee decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ParkFee' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add ParkFee decimal(10,2)')
    exec ('update CardPaymentRecord set ParkFee=0')
    exec ('alter table CardPaymentRecord alter column ParkFee decimal(10,2) not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentCode' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add PaymentCode tinyint')
    exec ('update CardPaymentRecord set PaymentCode=0xB1')
    exec ('alter table CardPaymentRecord alter column PaymentCode tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OperatorCardID' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add OperatorCardID nvarchar(50)')
end
go


if not exists (SELECT * FROM dbo.syscolumns WHERE name ='WorkMode' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add WorkMode tinyint')
    exec ('update Park set WorkMode=1')
    exec ('alter table Park alter column WorkMode tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeviceType' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add DeviceType tinyint')
    exec ('update Park set DeviceType=0')
    exec ('alter table Park alter column DeviceType tinyint not null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Options' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add Options int')
    exec ('update Park set Options=0')
    exec ('alter table Park alter column Options int not null')
end
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='ForceWithCount' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park drop column ForceWithCount')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentEventIndex' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add PaymentEventIndex int')
    exec ('update Entrance set PaymentEventIndex=0')
    exec ('alter table Entrance alter column PaymentEventIndex int not null')
end
go

--2013-5-17 修改WaitingCommand表和主键
if exists (SELECT * FROM dbo.syscolumns WHERE name ='ParkID' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('sp_rename N''[dbo].[WaitingCommand].ParkID'',N''EntranceID'',''COLUMN''')
end
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='Action' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('sp_rename N''[dbo].[WaitingCommand].Action'',N''Command'',''COLUMN''')
end
go

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_WaitingCommand') AND type in (N'PK'))
BEGIN
	exec ('alter table WaitingCommand drop constraint PK_WaitingCommand')
end
go

IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_WaitingCommand') AND type in (N'PK'))
BEGIN
	exec ('alter table WaitingCommand add constraint PK_WaitingCommand primary key(EntranceID,Command,CardID)')
end
go

--2013-6-3 Entrance表中增加三个字段，用于保存车牌识别一体机的IP、视频ID和车牌识别结果需发送的控制器IP
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlateIP' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add CarPlateIP nvarchar(20)')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='VideoID' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add VideoID int')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlateNotifyIP' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add CarPlateNotifyIP nvarchar(60)')
end
else
BEGIN
	exec ('alter table Entrance alter column CarPlateNotifyIP nvarchar(60)')
end
go

--2013-7-3 OperatorSettleLog 增加CashOperatorCard字段用于保存操作员卡收费的现金
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOperatorCard' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOperatorCard decimal(10,2)')
    exec ('update OperatorSettleLog set CashOperatorCard=0')
    exec ('alter table OperatorSettleLog alter column CashOperatorCard decimal(10,2) not null')
end
go

--2013-7-11 Entrance 增加CardTypeProperty字段用于保存控制板的卡片类型属性
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardTypeProperty' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add CardTypeProperty nvarchar(200)')
end
go

--2013-7-11 增加一个保存羊城通扣费记录的表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YangChenTongLog]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[YangChenTongLog](
		[LogID] [int] IDENTITY(1,1) NOT NULL,
		[LogDateTime] [datetime] NOT NULL,
		[CardID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[LogicalID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[Payment] [decimal](10, 2) NOT NULL,
		[Balance] [decimal](10, 2) NOT NULL,
		[Data] [nvarchar](1000) COLLATE Chinese_PRC_CI_AS NOT NULL,
	 CONSTRAINT [PK_YangChenTongLog] PRIMARY KEY CLUSTERED 
	(
		[LogID] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

--2013-7-24 卡片表增加一列，用于记录卡片的入场时间，用于记算停车费用,而之前的LASTDATETIME字段则用于记录最后一次刷卡进出的时间，
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LastNestParkDateTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add LastNestParkDateTime datetime null')
end
go

--2013-8-7 卡片表和缴费记录表分别增加一列，用于记录卡片信息和缴费记录是否已上传到主数据库
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='UpdateFlag' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add UpdateFlag bit null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='UpdateFlag' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add UpdateFlag bit null')
end
go

--2013-10-6 视频图像抓拍表增加一列，用于记录视频图像抓拍时的卡号，旧数据新增列的卡号从卡片进出事件表中获取
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardID' AND id = OBJECT_ID(N'[dbo].[SnapShot]')) 
BEGIN
	exec ('alter table SnapShot add CardID nvarchar(50)')
	exec ('update SnapShot set CardID=CardEvent.CardID from CardEvent where SnapShot.ShotAt=CardEvent.EventDateTime')
end
go

--2014-01-09 刷卡事件表增加一列，用于记录刷卡事件记录是否已上传到主数据库
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='UpdateFlag' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table CardEvent add UpdateFlag bit null')
end
go

--2014-01-09 卡片表增加一列，用于记录卡片的免费时间限期
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='FreeDateTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add FreeDateTime Datetime null')
end
go


--2014-1-9 增加一个通道路口的表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoadWay]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoadWay](
	[RoadID] [int] IDENTITY(1,1) NOT NULL,
	[RoadName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[EntranceIDs] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[Mode] [int] NOT NULL,
 CONSTRAINT [PK_RoadWay] PRIMARY KEY CLUSTERED 
(
	[RoadID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-01-21 工作站表通道ID列由200个字符扩充为8000个字符
if exists (SELECT * FROM dbo.syscolumns WHERE name ='EntranceID' AND id = OBJECT_ID(N'[dbo].[WorkStation]')) 
BEGIN
	exec ('alter table WorkStation alter column EntranceID varchar(8000) null')
end
go


--WaitingCommand增加一个下发状态字段 2014-01-26
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Status' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('alter table WaitingCommand add Status tinyint')
    exec ('update WaitingCommand set Status=0')
    exec ('alter table WaitingCommand alter column Status tinyint not null')
end
go


--2014-12-11 WaitingCommand表增加一个名单类型字段
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardIDType' AND id = OBJECT_ID(N'[dbo].[WaitingCommand]')) 
BEGIN
	exec ('alter table WaitingCommand add CardIDType tinyint null')
end
go



--增加免费授权记录表 2014-02-14 Jan， 2014-05-26 增加工作站列 Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FreeAuthorizationLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FreeAuthorizationLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[LogDateTime] [datetime] NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[BeginDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[InPark] [bit] NOT NULL,
	[CheckOut] [bit] NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_FreeAuthorizationLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
go

--2014-02-16 entrance表增加一个条码枪串口二字段，用于支持一个通道用两个枪的情景
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='TicketReaderCOMPort2' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
BEGIN
	exec ('alter table Entrance add TicketReaderCOMPort2 tinyint null')
end
go

--2014-02-27 角色表权限列表列由200个字符扩充为4000个字符 Jan
if exists (SELECT * FROM dbo.syscolumns WHERE name ='Permission' AND id = OBJECT_ID(N'[dbo].[Role]')) 
BEGIN
	exec ('alter table Role alter column Permission nvarchar(4000) null')
end
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--增加自助缴费机结账记录表 2014-03-13 Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APMCheckOutRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APMCheckOutRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MID] [nvarchar](50) NOT NULL,
	[CheckOutDateTime] [datetime] NOT NULL,
	[LastDateTime] [datetime] NOT NULL,
	[LastBalance] [decimal](10, 2) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[ActualAmount] [decimal](10, 2) NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[PayMoney] [decimal](10, 2) NOT NULL,
	[IncomeMoneny] [decimal](10, 2) NOT NULL,
	[Hundred] [int] NOT NULL,
	[Fifty] [int] NOT NULL,
	[Twenty] [int] NOT NULL,
	[Ten] [int] NOT NULL,
	[Cash] [int] NOT NULL,
	[CashAmount] [decimal](10, 2) NOT NULL,
	[Coin] [int] NOT NULL,
	[APMOperator] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_APMCheckOutRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
Go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--增加自助缴费机退款记录表 2014-03-17 Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APMRefundRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APMRefundRecord](
	[CardID] [nvarchar](50) NOT NULL,
	[RefundDateTime] [datetime] NOT NULL,
	[MID] [nvarchar](50) NOT NULL,
	[PaymentSerialNumber] [nvarchar](50) NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[CardType] [tinyint] NOT NULL,
	[EnterDateTime] [datetime] NOT NULL,
	[PaidDateTime] [datetime] NULL,
	[ParkFee] [decimal](10, 2) NOT NULL,
	[TotalPaidFee] [decimal](10, 2) NOT NULL,
	[RefundMoney] [decimal](10, 2) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](200) NULL,
	[SettleDateTime] [datetime] NULL,
 CONSTRAINT [PK_CardPaymentRefund] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[RefundDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
Go

--2014-03-17 操作员结算记录表增加收费退款列 Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfRefund' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOfRefund decimal(10, 2) null')
end
go

--2014-04-18 操作员结算记录表增加收费pos机收现列 bruce
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfPOS' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add CashOfPOS decimal(10, 2) null')
end
go

--2014-05-26 ，免费授权记录表增加工作站列列 Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='StationID' AND id = OBJECT_ID(N'[dbo].[FreeAuthorizationLog]')) 
BEGIN
	exec ('alter table FreeAuthorizationLog add StationID nvarchar(50) null')
end
go


--2014-08-11 ，停车场表增加GPS坐标列 Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='GPS' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add GPS nvarchar(200) null')
end
go


--2014-08-13 ，卡片表增加部门坐标列 Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Department' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add Department nvarchar(100) null')
end
go


------------------------------------------------------------------------------------
/** 
 * 增加PRE前缀的表，这些表都是停车场优惠录入系统单独使用的表  2014-08-20 Mark
 **/
------------------------------------------------------------------------------------
--操作员表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREOperator]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREOperator](
	[OperatorID] [nvarchar](50) NOT NULL,
	[OperatorName] [nvarchar](50) NOT NULL,
	[OperatorPwd] [nvarchar](50) NOT NULL,
	[RoleID] [nvarchar](50) NOT NULL,
	[OperatorNum] [int] NULL,
 CONSTRAINT [PK_PREOperator] PRIMARY KEY CLUSTERED 
(
	[OperatorID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

if not exists (select * from PREOperator where OperatorID='admin')
insert into PREOperator (operatorID,OperatorName,OperatorPwd,RoleID,OperatorNum) values('admin','admin','123','Admin',1)

--角色表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRERole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PRERole](
	[RoleID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Permission] [nvarchar](4000) NULL,
 CONSTRAINT [PK_PRERole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

if not exists (select * from PRERole where RoleID='Admin')
insert into PRERole (RoleID,[Name],Description,Permission) values('Admin','系统管理员','系统管理员，有系统所有的权限.','all')

--商家信息表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREBusinesses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREBusinesses](
	[BusinessesID] [uniqueidentifier] NOT NULL,
	[BusinessesName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_PREBusinesses] PRIMARY KEY CLUSTERED 
(
	[BusinessesID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--优惠信息表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREPreferential]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREPreferential](
	[PreferentialID] [uniqueidentifier] NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[PreferentialHour] [int] NOT NULL,
	[BusinessesName1] [nvarchar](200) NULL,
	[BusinessesMoney1] [decimal](10,2) NULL,
	[BusinessesName2] [nvarchar](200) NULL,
	[BusinessesMoney2] [decimal](10,2) NULL,
	[BusinessesName3] [nvarchar](200) NULL,
	[BusinessesMoney3] [decimal](10,2) NULL,
	[Notes] [nvarchar](2000) NULL,
	[WorkstationID] [uniqueidentifier] NOT NULL,
	[WorkstationName] [nvarchar](200) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[PreferentialTime] [datetime] NOT NULL,
	[EntranceTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PREPreferential] PRIMARY KEY CLUSTERED 
(
	[PreferentialID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--优惠操作记录档
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PREPreferentialLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PREPreferentialLog](
	[PreferentialID] [uniqueidentifier] NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[PreferentialHour] [int] NOT NULL,
	[BusinessesName1] [nvarchar](200) NULL,
	[BusinessesMoney1] [decimal](10,2) NULL,
	[BusinessesName2] [nvarchar](200) NULL,
	[BusinessesMoney2] [decimal](10,2) NULL,
	[BusinessesName3] [nvarchar](200) NULL,
	[BusinessesMoney3] [decimal](10,2) NULL,
	[Notes] [nvarchar](2000) NULL,
	[WorkstationID] [uniqueidentifier] NOT NULL,
	[WorkstationName] [nvarchar](200) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[OperatorTime] [datetime] NOT NULL,
	[EntranceTime] [datetime] NOT NULL,
	[IsCancel] [bit] NULL,
	[CancelReason] [nvarchar](2000) NULL,
 CONSTRAINT [PK_PREPreferentialLog] PRIMARY KEY CLUSTERED 
(
	[PreferentialID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--给Card表新增一个栏位
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PreferentialTime' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add PreferentialTime datetime')
end
go


--服务器切换记录
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServerSwitchRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ServerSwitchRecord](
	[RecordID] [int] IDENTITY(1,1) NOT NULL,
	[ParkID] [int] NOT NULL,
	[SwitchDateTime] [datetime] NOT NULL,
	[LastDateTime] [datetime] NULL,
	[SwitchServerIP] [nvarchar](50) NOT NULL,
	[LastIP] [nvarchar](50) NULL,
	[SwitchStatus] [tinyint] NOT NULL,
	[LastStatus] [tinyint] NOT NULL,
	[SMSStatus] [tinyint] NOT NULL,
	[Operator] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](2000) NULL,
 CONSTRAINT [PK_ServerSwitchRecord] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO


--给CardPaymentRecord表新增一个栏位
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CurrDiscountHour' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add CurrDiscountHour int')
end
go










/** =======================================================================================================================
 *  将珠海长隆新增需求添加到停车场系统主干代码，拷贝珠海长隆分支文件的内容
 *  2014-09-28 
 *  Mark
 **/
 
--2014-7-31 增加一个部门表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dept]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dept](
	[DeptID] [uniqueidentifier] NOT NULL,
	[DeptName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Descrption] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[ParentID] [uniqueidentifier] NULL,
CONSTRAINT [PK_Dept] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-08-01 工作站表增加一个部门列
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeptID' AND id = OBJECT_ID(N'[dbo].[WorkStation]')) 
BEGIN
	exec ('alter table WorkStation add DeptID uniqueidentifier null')
end
go

--2014-08-01 操作员表增加一个部门列
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeptID' AND id = OBJECT_ID(N'[dbo].[Operator]')) 
BEGIN
	exec ('alter table Operator add DeptID uniqueidentifier null')
end
go

--2014-08-01 结算记录表增加一个部门列
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DeptID' AND id = OBJECT_ID(N'[dbo].[OperatorSettleLog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add DeptID uniqueidentifier null')
end
go

--2014-10-28 停车明细费用记录表增加一个操作员部门列 Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OperatorDeptID' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add OperatorDeptID uniqueidentifier null')
end
go

--2014-10-28 停车明细费用记录表增加一个工作站部门列 Jan
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='StationDeptID' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
BEGIN
	exec ('alter table CardPaymentRecord add StationDeptID uniqueidentifier null')
end
go

--2014-11-04 以下两个表是神华项目超速违章禁止进出功能新增的表 Jan
--2014-10-24 新增未处理超速记录 Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpeedingRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SpeedingRecord](
	[SpeedingID] [uniqueidentifier] NOT NULL,
	[SpeedingDateTime] [datetime] NOT NULL,
	[PlateNumber] [nvarchar](50) NOT NULL,
	[Place] [nvarchar](50) NOT NULL,
	[Speed] [nvarchar](50) NOT NULL,
	[Photo] [nvarchar](1000) NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_SpeedingRecord] PRIMARY KEY CLUSTERED 
(
	[SpeedingID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-10-24 新增已处理超速记录 Jan
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpeedingLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SpeedingLog](
	[SpeedingID] [uniqueidentifier] NOT NULL,
	[SpeedingDateTime] [datetime] NOT NULL,
	[PlateNumber] [nvarchar](50) NOT NULL,
	[Place] [nvarchar](50) NOT NULL,
	[Speed] [nvarchar](50) NOT NULL,
	[Photo] [nvarchar](1000) NOT NULL,
	[Memo] [nvarchar](200) NULL,
	[DealOperatorID] [nvarchar](50) NULL,
	[DealDateTime] [datetime] NULL,
	[DealMemo] [nvarchar](200) NULL,
 CONSTRAINT [PK_SpeedingLog] PRIMARY KEY CLUSTERED 
(
	[SpeedingID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--2014-11-21 卡片表增加一个名单类型
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ListType' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add ListType tinyint not null default 0')
end
go
--2014-11-21 停车场表增加一个进出凭证名单模式
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ListMode' AND id = OBJECT_ID(N'[dbo].[Park]')) 
BEGIN
	exec ('alter table Park add ListMode tinyint not null default 0')
end
go


--2014-12-19 操作员结算表新增HandInPOS 列用于输入POS收费金额
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='HandInPOS' AND id = OBJECT_ID(N'[dbo].[OperatorSettlelog]')) 
BEGIN
	exec ('alter table OperatorSettleLog add HandInPOS decimal(10,2)')
end
go


--2015-01-22 卡片延期表新增CardType 列用于记录延期卡片的卡类型
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardType' AND id = OBJECT_ID(N'[dbo].[CardDefer]')) 
BEGIN
	exec ('alter table CardDefer add CardType tinyint')
	exec ('update CardDefer set CardType=(select Card.CardType from Card where CardDefer.CardID=Card.CardID)')
end
go

--2015-02-09 进出事件表新增Department 列用于记录部门
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Department' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table CardEvent add Department nvarchar(100) null')
	exec ('update CardEvent set Department=(select Card.Department from Card where CardEvent.CardID=Card.CardID)')
end
go

--2015-05-19 视频表新增VideoType（视频服务类型）
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='VideoType' AND id = OBJECT_ID(N'[dbo].[VideoSource]')) 
BEGIN
	exec ('alter table VideoSource add VideoType int null')
end
go

--2015-05-22 报警描述表列由300个非汉字字符扩充为300个汉字字符 Jan
if exists (SELECT * FROM dbo.syscolumns WHERE name ='AlarmDescr' AND id = OBJECT_ID(N'[dbo].[Alarm]')) 
BEGIN
	exec ('alter table Alarm alter column AlarmDescr nvarchar(300) null')
end
go

--生成触发器，这里必须放在最后运行，因为有可能触发器里有新增加的列，前面要先添加新增加的列，触发器才能生成成功
--添加硬件卡片名单触发器 2014-12-11
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[T_AddCardToDevice]') and OBJECTPROPERTY(id, N'IsTrigger') = 1) 
drop trigger T_AddCardToDevice
go
CREATE TRIGGER T_AddCardToDevice
   ON  dbo.Card
   AFTER INSERT
AS 
BEGIN	
	declare @Status tinyint,
		@cardID nvarchar(50), --名单的cardid
		@Options int,
		@listType tinyint,
        @carPlate nvarchar(50)

	select @Status=Status,@cardID=CardID,@Options=Options,@listType=ListType,@carPlate=CarPlate from inserted
	
	if @cardID is null --如果卡号为空的，直接返回
	begin
		return
    end	

	if (@Options&0x01)=0 --如果是脱机摸时按在线处理的卡片，不需要下发名单到控制器
	begin
		return --直接返回
	end

	if @listType=1 --如果是车牌名单
	begin
		if @carPlate is null or @carPlate='' --如果车牌名单的车牌号为空，直接返回
		begin
			return
		end
	end

	if @Status=1 --状态1：卡片已发行
	begin
		declare @entranceID int,
		@command tinyint
		set @command=0 --新增卡片命令：0				

		--只对脱机模式的控制器进行操作
		declare entranceCur cursor for select a.EntranceID from Entrance a,Park b where a.ParkID=b.ParkID and b.WorkMode=0
		open entranceCur
		fetch next from entranceCur into @entranceID
		while @@fetch_Status<>-1
		begin
			--先删除该名单之前的命令
			delete from WaitingCommand where EntranceID=@entranceID and ( CardID=@cardID or (@listType=1 and CardID=@carPlate))
			insert 	WaitingCommand(EntranceID,Command,CardID,Status,CardIDType) values(@entranceID,@command,@cardID,0,null) --直接用卡号来下发
			fetch next from entranceCur into @entranceID
		end
		close entranceCur
		deallocate entranceCur
	end
END
GO

--修改硬件卡片名单触发器 2014-12-11
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[T_UpdateCardToDevice]') and OBJECTPROPERTY(id, N'IsTrigger') = 1) 
drop trigger T_UpdateCardToDevice
go
CREATE TRIGGER T_UpdateCardToDevice
   ON  dbo.Card
   AFTER UPDATE
AS 
BEGIN
	if update(Status) or update(CardType) or update(CarType) or update(Options) or Update(ActivationDate) or update(ValidDate) or update(AccessID) or update(CarPlate)
	begin
		declare @entranceID int,
		@command tinyint,
		@cardIDType tinyint,
		@wcCardID nvarchar(50), --下发命令的cardid
		@cardID nvarchar(50), --名单的cardid
		@Status tinyint,
		@OldOptions int,
		@NewOptions int,
		@oldListType tinyint,
        @oldCarPlate nvarchar(50)
		
		select @OldOptions=Options,@oldListType=ListType,@oldCarPlate=CarPlate from deleted
		select @Status=Status,@cardID=CardID,@NewOptions=Options from inserted

        if @cardID is null --如果卡号为空的，直接返回
		begin
			return
        end

		if (@OldOptions&0x01)=0 --如果之前是脱机摸时按在线处理的
		begin
			if (@NewOptions&0x01)=0 --如果还是脱机摸时按在线处理的，不需要下发名单到控制器
			begin
				return
			end
		end

		if @Status=1 --状态1：卡片已发行
			set @command=1 --修改卡片命令：1
		else 
			set @command=2 --删除卡片命令：2
		
		set @wcCardID=@cardID	--cardid为卡号	

		if @oldListType=1 --如果是车牌名单
		begin
			if @oldCarPlate is null or @oldCarPlate='' --如果车牌名单的车牌号为空，直接返回
			begin
				return
			end

			if @command=2 --如果是删除命令的
			begin
				set @cardIDType=1 --设置成cardid为车牌号
				set @wcCardID=@oldCarPlate --cardid为车牌号
			end
		end
		
		--只对脱机模式的控制器进行操作
		declare entranceCur cursor for select a.EntranceID from Entrance a,Park b where a.ParkID=b.ParkID and b.WorkMode=0
		open entranceCur
		fetch next from entranceCur into @entranceID
		while @@fetch_Status<>-1
		begin
			--先删除该名单之前的命令
			delete from WaitingCommand where EntranceID=@entranceID and ( CardID=@cardID or (@oldListType=1 and CardID=@oldCarPlate))
			insert 	WaitingCommand(EntranceID,Command,CardID,Status,CardIDType) values(@entranceID,@command,@wcCardID,0,@cardIDType)
			fetch next from entranceCur into @entranceID
		end
		close entranceCur
		deallocate entranceCur
	end

END
GO

--删除硬件卡片名单触发器 2014-12-11
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[T_DeleteCardToDevice]') and OBJECTPROPERTY(id, N'IsTrigger') = 1) 
drop trigger T_DeleteCardToDevice
go
CREATE TRIGGER T_DeleteCardToDevice
   ON  dbo.Card
   AFTER DELETE
AS 
BEGIN
	declare @entranceID int,
	@command tinyint,
	@wcCardID nvarchar(50), --下发命令的cardid
	@cardIDType tinyint,
	@cardID nvarchar(50), --名单的cardid
	@listType tinyint,
    @carPlate nvarchar(50)

	set @command=2 --删除卡片命令：2
	select @cardID=CardID,@listType=ListType,@carPlate=CarPlate from deleted

	if @cardID is null --如果卡号为空的，直接返回
	begin
		return
    end

	set @wcCardID=@cardID	--cardid为卡号	

	if @listType=1 --如果是车牌名单
	begin
		if @carPlate is null or @carPlate='' --如果车牌名单的车牌号为空，直接返回
		begin
			return
		end
		
		set @cardIDType=1 --设置成cardid为车牌号
		set @wcCardID=@carPlate --cardid为车牌号
	end	

	--只对脱机模式的控制器进行操作
	declare entranceCur cursor for select a.EntranceID from Entrance a,Park b where a.ParkID=b.ParkID and b.WorkMode=0
	open entranceCur
	fetch next from entranceCur into @entranceID
	while @@fetch_Status<>-1
	begin
		--先删除该名单之前的命令
		delete from WaitingCommand where EntranceID=@entranceID and ( CardID=@cardID or (@listType=1 and CardID=@carPlate))
		insert 	WaitingCommand(EntranceID,Command,CardID,Status,CardIDType) values(@entranceID,@command,@wcCardID,0,@cardIDType)
		fetch next from entranceCur into @entranceID
	end
	close entranceCur
	deallocate entranceCur
END
GO

--添加Entrance表与WaitingCommand表的级联删除 2014-01-26
if not exists(select * from sysobjects where name='FK_WaitingCommand_Entrance' and xtype='F')
begin
--清除WaitingCommand已删除的EntranceID，不然不能成功添加外键
--delete from WaitingCommand where EntranceID=(select b.EntranceID from Entrance a right join WaitingCommand b on a.EntranceID=b.EntranceID where a.EntranceID is null)
--清除所有WaitingCommand命令
delete from WaitingCommand
ALTER TABLE [dbo].[WaitingCommand]  WITH CHECK ADD  CONSTRAINT [FK_WaitingCommand_Entrance] FOREIGN KEY([EntranceID])
REFERENCES [dbo].[Entrance] ([EntranceID])
ON DELETE CASCADE
end
go

-- 触发器生成完成