-------------bruce 增加羊城通收费记录笔黑名单记录表 2015-08-10-----
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YCTPaymentRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[YCTPaymentRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PSN] [int] NOT NULL,
	[TIM] [datetime] NOT NULL,
	[LCN] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[FCN] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[TF] [int] NOT NULL,
	[FEE] [int] NOT NULL,
	[BAL] [int] NOT NULL,
	[TT] [tinyint] NOT NULL,
	[ATT] [tinyint] NOT NULL,
	[CRN] [int] NOT NULL,
	[XRN] [int] NOT NULL,
	[DMON] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[BDCT] [int] NOT NULL,
	[MDCT] [int] NOT NULL,
	[UDCT] [int] NOT NULL,
	[EPID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ETIM] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[LPID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[LTIM] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AREA] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ACT] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[SAREA] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[TAC] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[MEM] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[EnterDateTime] [datetime] NULL,
	[WalletType] [int] NOT NULL,
	[State] [int] NOT NULL,
	[Data] [nvarchar](1000) COLLATE Chinese_PRC_CI_AS NULL,
	[UploadFile] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_YCTPaymentInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--如果YCTBlacklist表没有LCN列,说明是旧的表,删除表
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YCTBlacklist]') AND type in (N'U'))
BEGIN
	if not exists (SELECT * FROM dbo.syscolumns WHERE name ='LCN' AND id = OBJECT_ID(N'[dbo].[YCTBlacklist]')) 
	BEGIN
		exec ('drop table YCTBlacklist')
	end
end
go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YCTBlacklist]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[YCTBlacklist](
		[LCN] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
		[FCN] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
		[Reason] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
		[WalletType] [int] NULL,
		[AddDateTime] [datetime] NULL,
		[CatchAt] [datetime] NULL,
		[UploadFile] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	 CONSTRAINT [PK_YCTBlacklist] PRIMARY KEY CLUSTERED 
	(
		[LCN] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END 
GO

--2017-5-16 生成创建etc消费记录表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ETCPaymentRecord]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ETCPaymentRecord](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[LaneNo] [nvarchar](50) NOT NULL,
		[Device] [tinyint] NOT NULL,
		[AddTime] [datetime] NOT NULL,
		[Data] [nvarchar](1000) NOT NULL,
		[UploadTime] [datetime] NULL,
	 CONSTRAINT [PK_ETCPaymentRecord1] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
	) ON [PRIMARY]
END 
GO



