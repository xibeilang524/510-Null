if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Deposit' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table card add Deposit decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Deposit' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add Deposit decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Balance' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add Balance decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ValidDate' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add ValidDate datetime not null default '2011-1-1'
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentMode' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add PaymentMode tinyint not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardType' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add CardType tinyint not null default 8
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentMode' AND id = OBJECT_ID(N'[dbo].[CardDefer]')) 
begin 
	alter table cardDefer add PaymentMode tinyint not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='PaymentMode' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	alter table cardCharge add PaymentMode tinyint not null default 0
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='OriginalBalance' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	alter table cardCharge drop column OriginalBalance
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='RecieveMoney' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	EXEC   sp_rename   'CardCharge.RecieveMoney',   'Payment',   'COLUMN'
end 
go

if  not exists (SELECT * FROM dbo.syscolumns WHERE name ='Deposit' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	alter table cardRecycle add Deposit decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardType' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	alter table cardRecycle add CardType tinyint not null default 8
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Balance' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	alter table cardRecycle add Balance decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ValidDate' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	alter table cardRecycle add ValidDate datetime not null default '2011-1-1'
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='TempCard' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add TempCard int not null default 0
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='CashRemain' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog drop column cashremain
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='TempCardRemain' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog drop column TempCardRemain
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='PrepayCardFeePlan' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog drop column PrepayCardFeePlan
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='PrepayCardFeeFact' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog drop column PrepayCardFeeFact
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfCard' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog add CashOfCard decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfDeposit' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog add CashOfDeposit decimal(10,2) not null default 0
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashOfCardRecycle' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog add CashOfCardRecycle decimal(10,2) not null default 0
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='NonCashParkPlan' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog add NonCashParkPlan decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='NonCashParkFact' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog add NonCashParkFact decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='NonCashOfCard' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog add NonCashOfCard decimal(10,2) not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='NonCashOfDeposit' AND id = OBJECT_ID(N'[dbo].[OperatorLog]')) 
begin 
	alter table operatorlog add NonCashOfDeposit decimal(10,2) not null default 0
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlate' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
begin 
	alter table CardPaymentRecord add CarPlate varchar(20)
end
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='OperatorNum' and length=1 AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
begin 
	alter table CardPaymentRecord alter column OperatorNum varchar(20)
	update  CardPaymentRecord set OperatorNum=(select operatorID from Operator where Operator.OperatorNum=CardPaymentRecord.OperatorNum) 
end
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='OperatorNum' and length=1 AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
begin 
	alter table CardEvent alter column OperatorNum varchar(20)
	update  CardEvent set OperatorNum=(select operatorID from Operator where Operator.OperatorNum=CardEvent.OperatorNum) 
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='HolidayEnabled'  AND id = OBJECT_ID(N'[dbo].[Card]')) 
	alter table Card add HolidayEnabled bit not null default 1
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ActivationDate' AND id = OBJECT_ID(N'[dbo].[Card]')) 
	alter table Card add ActivationDate datetime not null default '2011-1-1'
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlate' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add CarPlate varchar(20) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OwnerName' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add OwnerName varchar(100) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='HolidayEnabled' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	alter table cardRelease add HolidayEnabled bit not null default 0
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ActivationDate' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
	alter table CardRelease add ActivationDate datetime not null default '2011-1-1'
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlate' AND id = OBJECT_ID(N'[dbo].[CardDefer]')) 
begin 
	alter table carddefer add CarPlate varchar(20) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OwnerName' AND id = OBJECT_ID(N'[dbo].[CardDefer]')) 
begin 
	alter table carddefer add OwnerName varchar(100) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlate' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	alter table cardCharge add CarPlate varchar(20) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OwnerName' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	alter table cardCharge add OwnerName varchar(100) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlate' AND id = OBJECT_ID(N'[dbo].[CardDisableEnable]')) 
begin 
	alter table cardDisableEnable add CarPlate varchar(20) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OwnerName' AND id = OBJECT_ID(N'[dbo].[CardDisableEnable]')) 
begin 
	alter table cardDisableEnable add OwnerName varchar(100) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlate' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
begin 
	alter table cardLostRestore add CarPlate varchar(20) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OwnerName' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
begin 
	alter table cardLostRestore add OwnerName varchar(100) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CarPlate' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	alter table cardRecycle add CarPlate varchar(20) null 
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OwnerName' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	alter table cardRecycle add OwnerName varchar(100) null 
end 
go

if  exists (SELECT * FROM dbo.syscolumns WHERE name ='AlarmDescr' AND id = OBJECT_ID(N'[dbo].[Alarm]')) 
begin 
	alter table Alarm alter column AlarmDescr varchar(300) null 
end
go


--  Add By Tom,2012-1-12 新增Card表中的VipParking，具体授权进入哪个贵宾停车场
if  exists (SELECT * FROM dbo.syscolumns WHERE name ='VipParking' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table Card drop column VipParking
end 
go


--  Add By Tom,2011-12-15 新增Card表中的IsPowerCard，是否特权通行卡
if  exists (SELECT * FROM dbo.syscolumns WHERE name ='IsPowerCard' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table Card drop column IsPowerCard
end 
go


--  Add By Tom,2012-1-12 新增RuthorizationRecord表(Vip授权记录表)
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RuthorizationRecord]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin 
	drop table RuthorizationRecord
end
go 


-- Add By Jan,2012-4-15 新增Card表字段DiscountHour，优惠时数
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DiscountHour' AND id = OBJECT_ID(N'[dbo].Card')) 
begin 
	alter table Card add DiscountHour tinyint not null default 0
end 
go

-- Add By Jan,2012-4-15 新增CardPaymentRecord表字段DiscountHour，优惠时数
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DiscountHour' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
begin 
	alter table CardPaymentRecord add DiscountHour tinyint not null default 0
end 
go

-- Add By Jan,2012-4-15 新增CardPaymentRecord表字段DiscountHourPaid，优惠金额
if not exists (SELECT * FROM dbo.syscolumns WHERE name ='DiscountHourPaid' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
begin 
	alter table CardPaymentRecord add DiscountHourPaid decimal(10,2) not null default 0
end 
go

--  Add By Jan,2012-4-15 新增DiscountRecord表(优惠记录表)
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DiscountRecord]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin 
	CREATE TABLE [dbo].[DiscountRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShopID] [varchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[DiscountDateTime] [datetime] NOT NULL,
	[DiscountHour] [tinyint] NOT NULL,
	[CardID] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[OperatorID] [varchar](20) COLLATE Chinese_PRC_CI_AS NULL,
	[StationID] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Pay] [bit] NULL,
 CONSTRAINT [PK_DiscountRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录序号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'ID'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'ShopID'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'写入时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'DiscountDateTime'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠时数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'DiscountHour'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'CardID'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'OperatorID'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点序号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'StationID'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否结算' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'Pay'
end
go 

--  Add By Jan,2012-4-15 新增Shop表(商家信息表)
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Shop]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin 
	CREATE TABLE [dbo].[Shop](
	[ShopID] [varchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ShopName] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Adress] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Phone] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Linkman] [varchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[ShopID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'ShopID'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'ShopName'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'Adress'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'Phone'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'Linkman'
end
go 

if not  exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table Card add CardCertificate varchar(50) null 
end 
go


if exists (SELECT * FROM dbo.syscolumns WHERE name ='LastAddress' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	EXEC   sp_rename   'Card.LastAddress',   'LastEntrance',   'COLUMN'
    exec   ('alter table card alter column LastEntrance int not null')
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='Deposit' AND id = OBJECT_ID(N'[dbo].[Car]')) 
begin 
	alter table car drop column Deposit
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='AccessFlag' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table card drop column AccessFlag
end 
go

if  exists (SELECT * FROM dbo.syscolumns WHERE name ='Regard' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table card drop column Regard
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='PortNum' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table card drop column PortNum
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='GroupNum' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table card drop column GroupNum
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Options' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table card add Options int null
    exec  ('update card Set Options=256')
    exec  ('alter table card alter column Options int not null')
    exec  ('update card set options=0 where holidayenabled=0')
    exec  ('update card set status=1 where status=0x00')
	exec  ('update card set status=2 where status=0x08')
	exec  ('update card set status=3 where status=0x80')
	exec  ('update card set status=4 where status=0x40')
	exec  ('update card set status=5 where status=0x20')
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='HolidayEnabled' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	alter table card alter column holidayenabled bit null
end 
go


if not exists (SELECT * FROM dbo.syscolumns WHERE name ='EntranceID' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
begin 
	alter table cardEvent add EntranceID int null
    exec  ('update CardEvent set EntranceID=0')
    exec  ('alter table CardEvent Alter column EntranceID int not null')
    exec  ('UPDATE cardEvent set EntranceID=Entrance.EntranceID from Entrance where Entrance.ParkID=CardEvent.ParkID AND Entrance.Address=CardEvent.Address')
    exec  ('alter table CardEvent alter column Address int null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='IsExitEvent' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
begin 
	alter table cardEvent add IsExitEvent bit  null 
    exec  ('update cardevent set IsExitEvent=0')
    exec  ('alter table cardevent alter column IsExitEvent bit not null')
    exec  ('update cardevent set isexitevent=address % 2')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='IPAddress' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add IPAddress varchar(20) null
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='IPMask' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add IPMask varchar(20) null
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Gateway' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add Gateway varchar(20) null
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ControlPort' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add ControlPort int  null
    exec  ('update Entrance set Controlport=0')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='EventPort' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add EventPort int null 
    exec  ('update Entrance set EventPort=0')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='MasterIP' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add MasterIP varchar(20) null
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='MACAddress' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add MACAddress varchar(20) null
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='WorkMode' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add WorkMode int null
    execute ('update entrance set workmode=0')
    execute ('alter table entrance alter column workmode int not null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='MinTempCard' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add MinTempCard int  null
    exec  ('update entrance set mintempcard=-1 where mintempcard is null')
    exec  ('alter table Entrance alter column mintempcard int null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ReadCardInterval' AND id = OBJECT_ID(N'[dbo].[Entrance]')) 
begin 
	alter table Entrance add ReadCardInterval int  null
    exec  ('update entrance set readcardinterval=0 where readcardinterval is null')
    exec  ('alter table entrance alter column ReadCardInterval int not null')
end 
go

alter table Operator alter column OperatorPWD varchar(50) not null
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='LastAccounts' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
begin 
	EXEC   sp_rename   'CardPaymentRecord.LastAccounts',   'LastTotalDiscount',   'COLUMN'
    EXEC   sp_rename   'CardPaymentRecord.HavePaid',   'LastTotalPaid',   'COLUMN'
    EXEC   sp_rename   'CardPaymentRecord.DiscountHourPaid',   'Discount',   'COLUMN'
    exec   ('update CardPaymentRecord set LastTotalDiscount=LastTotalDiscount-LastTotalPaid')
    Exec   ('update CardPaymentRecord set Accounts=Accounts-LastTotalDiscount-LastTotalPaid')
    exec   ('update CardPaymentRecord set Discount=Accounts-paid')
end
go

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[APM]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin 
	CREATE TABLE [dbo].[APM](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[SerialNum] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[IP] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
		[MAC] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
		[UserName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
		[Password] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
		[Status] [int] NOT NULL,
		[Memo] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	 CONSTRAINT [PK_APM] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增，主键' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'ID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'SerialNum'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机的IP' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'IP'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机的MAC地址' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'MAC'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机登录用户名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'UserName'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自动缴费机登录密码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'Password'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机当前状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'Status'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机的一些描述性说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'Memo'
end 
go


if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[APMLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin 
	CREATE TABLE [dbo].[APMLog](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[SerialNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[LogDateTime] [datetime] NOT NULL,
        [LogType] [Tinyint] NOT NULL,
		[CardID] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[Description] [varchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[MID] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[OperatorID] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	 CONSTRAINT [PK_APMLog] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号，自动增加' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'ID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费流水号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'SerialNumber'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志记录时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'LogDateTime'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'CardID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志描述' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'Description'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费机编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'MID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'OperatorID'
end
GO
SET ANSI_PADDING OFF
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='AlarmType' AND id = OBJECT_ID(N'[dbo].[Alarm]') and length>4)
begin
	exec ('update alarm set alarmtype=2 where ltrim(rtrim(alarmtype))=''人工抬闸''')
    exec ('update alarm set alarmtype=3 where ltrim(rtrim(alarmtype))=''人工落闸''')
    exec ('update alarm set alarmtype=5 where ltrim(rtrim(alarmtype))=''取消收费''')
    exec ('update alarm set alarmtype=4 where ltrim(rtrim(alarmtype))=''修改收费记录''')
    exec ('update alarm set alarmtype=2 where isnumeric(alarmtype)=0')
    exec ('alter table alarm alter column alarmtype int not null')
end
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='AlarmValue' AND id = OBJECT_ID(N'[dbo].[Alarm]') and length>4)
begin
	exec ('alter table alarm drop column alarmvalue')
end
go


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[maxCount]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[maxCount](
	[tableID] [int] NOT NULL,
	[maxCount] [varchar](14) COLLATE Chinese_PRC_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tableID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setMaxCount]') AND type in (N'P', N'PC'))
BEGIN
--这个存储过程用于外部调用   返回一个流水号 

--用与判定天数是否是第二天如果是跟新最大号表   这个存储过程外部不调用 

EXEC dbo.sp_executesql @statement = N'  CREATE   proc   [dbo].[setMaxCount] 
as 
declare   @count   varchar(14)   ,@nowDay   varchar(8),@oldDay   varchar(8) ,@len int
select   @count=maxCount   from   maxCount   where   tableID=1 
if isnull(@count,'''')=''''
	begin	
		delete maxCount where tableID=1
		insert into maxCount(tableID,maxCount) values(1,convert(char(8),getdate(),112)+ ''000000'' )
	end
else
	if len(@count)!=14
		begin
			set @count=''20120101000000'' 
		end
	else
		select   @count=maxCount   from   maxCount   where   tableID=1
			set   @nowDay=convert(char(8),getdate(),112)
            set   @oldDay=(SUBSTRING(@count,0,5)+SUBSTRING(@count,5,2)+SUBSTRING(@count,7,2)) 
            if   (@oldDay <@nowDay) 
				begin 
					update   maxCount   set   maxCount=convert(char(8),getdate(),112)+ ''000000''
                       where   tableID=1 
            end    ' 

END
go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serialNumber]') AND type in (N'P', N'PC'))
BEGIN
--这个存储过程用于外部调用   返回一个流水号 

--用与判定天数是否是第二天如果是跟新最大号表   这个存储过程外部不调用 

EXEC dbo.sp_executesql @statement = N'create         proc   [dbo].[serialNumber] 
@number   varchar(14)   output 
as 
exec   setMaxCount   --调用存储过程setMaxCount    
begin   transaction 
declare   @temp   decimal     
select   @number=maxCount   from   maxCount   where   tableID=1          
set   @temp=convert(decimal,@number)+1 
set   @number=convert(varchar(14),@temp) 
update   maxCount   set   maxCount=@number   where   tableID=1 
commit   transaction '
END
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Balance' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	exec  ('alter table CardCharge add Balance decimal(10,2)  null')
    exec  ('update cardcharge set balance=0')
    exec  ('alter table cardcharge alter column balance decimal(10,2) not null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ValidDate' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	exec  ('alter table CardCharge add ValidDate datetime null')
    exec  ('update cardcharge set validDate=''2012-1-1''')
    exec  ('alter table cardcharge alter column validdate datetime not null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='ActiveDateTime' AND id = OBJECT_ID(N'[dbo].[APM]')) 
begin 
	exec  ('alter table apm add ActiveDateTime datetime null')
end
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Coin' AND id = OBJECT_ID(N'[dbo].[APM]')) 
begin 
	exec  ('alter table APM add Coin int  null')
    exec  ('update APM set Coin=0')
    exec  ('alter table APM alter column Coin int not null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CashAmount' AND id = OBJECT_ID(N'[dbo].[APM]')) 
begin 
	exec  ('alter table APM add CashAmount decimal(10,2)  null')
    exec  ('update APM set CashAmount=0')
    exec  ('alter table APM alter column CashAmount decimal(10,2) not null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LastBalance' AND id = OBJECT_ID(N'[dbo].[APM]')) 
begin 
	exec  ('alter table APM add LastBalance decimal(10,2)  null')
    exec  ('update APM set LastBalance=0')
    exec  ('alter table APM alter column LastBalance decimal(10,2) not null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CheckOutTime' AND id = OBJECT_ID(N'[dbo].[APM]')) 
begin 
	exec  ('alter table APM add CheckOutTime Datetime null')
    exec  ('update APM set CheckOutTime=2012-1-1')
    exec  ('alter table APM alter column CheckOutTime datetime not null')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='Name' AND id = OBJECT_ID(N'[dbo].[Role]')) 
begin 
	exec  ('alter table Role add Name nvarchar(50) null')
    exec  ('update Role set name=roleid')
    exec  ('alter table role alter column name nvarchar(50) not null')
    exec  ('update role set roleid=''admin'' where roleid=''系统管理员''')
    exec  ('update role set roleid=''CardOperator'' where roleid=''卡片操作员''')
    exec  ('update role set roleid=''PaymentOperator'' where roleid=''收费操作员''')
    exec  ('update operator set roleid=''admin'' where roleid=''系统管理员''')
    exec  ('update operator set roleid=''CardOperator'' where roleid=''卡片操作员''')
    exec  ('update operator set roleid=''PaymentOperator'' where roleid=''收费操作员''')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	exec  ('alter table CardCharge add CardCertificate nvarchar(50)')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardDefer]')) 
begin 
	exec  ('alter table CardDefer add CardCertificate nvarchar(50)')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardDisableEnable]')) 
begin 
	exec  ('alter table CardDisableEnable add CardCertificate nvarchar(50)')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
begin 
	exec  ('alter table CardEvent add CardCertificate nvarchar(50)')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardLostRestore]')) 
begin 
	exec  ('alter table CardLostRestore add CardCertificate nvarchar(50)')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
begin 
	exec  ('alter table CardPaymentRecord add CardCertificate nvarchar(50)')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	exec  ('alter table CardRecycle add CardCertificate nvarchar(50)')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='CardCertificate' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	exec  ('alter table CardRelease add CardCertificate nvarchar(50)')
end 
go

if exists (SELECT * FROM dbo.syscolumns WHERE name ='StationID' AND id = OBJECT_ID(N'[dbo].[WorkStation]')) 
begin 
	exec  ('update workstation set stationid=''DefaultStation'' where stationid=''管理中心''')
end 
go

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OperatorSettleLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin 
		CREATE TABLE [dbo].[OperatorSettleLog](
			[SettleDateTime] [datetime] NOT NULL,
			[OperatorID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
			[SettleFrom] [datetime] NULL,
			[StationID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
			[CashParkFact] [decimal](10, 2) NOT NULL,
            [CashParkDiscount] [decimal](10, 2) NOT NULL,  
			[CashOfCard] [decimal](10, 2) NOT NULL,
			[CashOfDeposit] [decimal](10, 2) NOT NULL,
			[CashOfCardRecycle] [decimal](10, 2) NOT NULL,
			[NonCashParkFact] [decimal](10, 2) NOT NULL,
            [NonCashParkDiscount] [decimal](10, 2) NOT NULL,
			[NonCashOfCard] [decimal](10, 2) NOT NULL,
			[NonCashOfDeposit] [decimal](10, 2) NOT NULL,
            [TempCardRecycle] [int] NOT NULL,
            [OpenDoorCount] [int] not null
		 CONSTRAINT [PK_SettleDatetime] PRIMARY KEY CLUSTERED 
		(
			[SettleDateTime] ASC
		)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]
       
		exec ('insert into operatorsettlelog (SettleDateTime,operatorid,settleFrom,[StationID],
		[CashParkFact],[CashParkDiscount],[CashOfCard],[CashOfDeposit],[CashOfCardRecycle],
        [NonCashParkFact],[NonCashParkDiscount],[NonCashOfCard],[NonCashOfDeposit],
        [TempCardRecycle],[OpenDoorCount])
        select offdutydatetime,operatorid,ondutydatetime,[StationID],
		[CashParkFact],[CashParkPlan]-[CashParkFact],[CashOfCard],[CashOfDeposit],[CashOfCardRecycle],
        [NonCashParkFact],[NonCashParkPlan]-[NonCashParkFact],[NonCashOfCard],[NonCashOfDeposit],
        [TempCardRecycle],0 
        from operatorlog')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardPaymentRecord]')) 
begin 
	exec  ('alter table CardPaymentRecord add SettleDateTime Datetime')
    exec  ('update cardpaymentrecord set settledatetime=a.settledatetime 
            from operatorsettlelog a 
            where cardpaymentrecord.operatornum=a.operatorID and 
            cardpaymentrecord.stationid=a.stationid and
            cardpaymentrecord.chargedatetime>=a.settlefrom and
            cardpaymentrecord.chargedatetime<=a.settledatetime')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardRelease]')) 
begin 
	exec  ('alter table CardRelease add SettleDateTime datetime')
    exec  ('update Cardrelease set SettleDateTime=''2000-1-1''')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardCharge]')) 
begin 
	exec  ('alter table CardCharge add SettleDateTime datetime')
    exec  ('update Cardcharge set SettleDateTime=''2000-1-1''')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardDefer]')) 
begin 
	exec  ('alter table CardDefer add SettleDateTime datetime')
    exec  ('update CardDefer set SettleDateTime=''2000-1-1''')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardRecycle]')) 
begin 
	exec  ('alter table CardRecycle add SettleDateTime datetime')
    exec  ('update CardRecycle set SettleDateTime=''2000-1-1''')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[CardEvent]')) 
begin 
	exec  ('alter table CardEvent add SettleDateTime datetime')
    exec  ('update cardEvent set settledatetime=a.settledatetime 
            from operatorsettlelog a 
            where cardevent.operatornum=a.operatorID and 
            cardevent.eventdatetime>=a.settlefrom and
            cardevent.eventdatetime<=a.settledatetime')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='SettleDateTime' AND id = OBJECT_ID(N'[dbo].[Alarm]')) 
begin 
	exec  ('alter table Alarm add SettleDateTime datetime')
    exec  ('update Alarm set SettleDateTime=''2000-1-1''')
end 
go

if not exists (SELECT * FROM dbo.syscolumns WHERE name ='OwnerName' AND id = OBJECT_ID(N'[dbo].[Card]')) 
begin 
	exec  ('alter table Card add OwnerName nvarchar(50)')
    exec  ('alter table Card add CarPlate nvarchar(50)')
    exec  ('alter table Card add Memo nvarchar(200)')
    exec  ('update card set OwnerName=a.OwnerName,CarPlate=a.Carplate,Memo=a.Memo 
           from Car a where Card.CarID=a.CarID')
    exec  ('alter table Card drop column CarID')
end 
go

--纸票由之前的16变成15
update card set CardType=15 where CardType=16  
GO






