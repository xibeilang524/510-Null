
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
	exec ('alter table Entrance add CarPlateNotifyIP nvarchar(20)')
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

--2013-10-6 视频图像抓拍表增加一列，用于记录视频图像抓拍时的卡号，旧数据新增列的卡号从卡片进出事件表中获取
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardID' AND id = OBJECT_ID(N'[dbo].[SnapShot]')) 
BEGIN
	exec ('alter table SnapShot add CardID nvarchar(50)')
	exec ('update SnapShot set CardID=CardEvent.CardID from CardEvent where SnapShot.ShotAt=CardEvent.EventDateTime')
end
go

--2013-12-18 卡片表增加字段用于全球通大厦
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Telphone' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add Telphone nvarchar(50) null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SheetID' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add SheetID NVARCHAR(50) null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LimitationRemain' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add LimitationRemain decimal(10,2) null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LimitationTimestamp' AND id = OBJECT_ID(N'[dbo].[Card]')) 
BEGIN
	exec ('alter table Card add LimitationTimestamp datetime null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Limitation' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table CardEvent add Limitation decimal(10,2) null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LimitationRemain' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
BEGIN
	exec ('alter table CardEvent add LimitationRemain decimal(10,2) null')
end
go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ECardRecord]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ECardRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SheetID] [nvarchar](50) NULL,
	[CardID] [nvarchar](50) NULL,
	[Carplate] [nvarchar](50) NULL,
	[EventDt] [datetime] NOT NULL,
	[EnterDt] [datetime] NULL,
	[Limitation] [decimal](10, 2) NULL,
	[LimitationRemain] [decimal](10, 2) NULL,
 CONSTRAINT [PK_ECardRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
END
GO

