SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Entrance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Entrance](
	[EntranceID] [int] IDENTITY(1,1) NOT NULL,
	[EntranceName] [nvarchar](50) NOT NULL,
	[ParkID] [int] NOT NULL,
	[Address] [int] NOT NULL,
	[IPAddress] [nvarchar](50) NULL,
	[IPMask] [nvarchar](50) NULL,
	[Gateway] [nvarchar](50) NULL,
	[ControlPort] [int] NULL,
	[EventPort] [int] NULL,
	[MasterIP] [nvarchar](50) NULL,
	[MACAddress] [nvarchar](50) NULL,
	[WorkMode] [int] NOT NULL,
	[ReadCardInterval] [int] NULL,
	[TicketPrinterCOMPort] [tinyint] NOT NULL,
	[TicketReaderCOMPort] [tinyint] NOT NULL,
	[ParkFullLedCOMPort] [tinyint] NULL,
	[TempCard] [int] NOT NULL,
	[Memo] [nvarchar](200) NULL,
	[CarPlateIP] [nvarchar](20) NULL,
	[VideoID] [int] NULL,
	[PaymentEventIndex] [int] NOT NULL,
	[CardTypeProperty] [nvarchar](200) NULL,
 CONSTRAINT [PK_Entrance] PRIMARY KEY CLUSTERED 
(
	[EntranceID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通道ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'EntranceID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通道名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'EntranceName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车场ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'ParkID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通道控制器地址' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'Address'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通道控制器IP' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'IPAddress'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子网掩码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'IPMask'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网关' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'Gateway'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制端口' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'ControlPort'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件端口' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'EventPort'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主控制器IP' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'MasterIP'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MAC地址' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'MACAddress'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作模式' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'WorkMode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'读卡时间间隔' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'ReadCardInterval'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纸票打印机串口号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'TicketPrinterCOMPort'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纸票阅读器串口' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'TicketReaderCOMPort'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满位屏串口' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'ParkFullLedCOMPort'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'临时卡数量' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'TempCard'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entrance', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[RoleID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Permission] [nvarchar](200) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Role', @level2type=N'COLUMN', @level2name=N'RoleID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Role', @level2type=N'COLUMN', @level2name=N'Name'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Role', @level2type=N'COLUMN', @level2name=N'Description'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Role', @level2type=N'COLUMN', @level2name=N'Permission'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shop]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Shop](
	[ShopID] [nvarchar](50) NOT NULL,
	[ShopName] [nvarchar](50) NOT NULL,
	[Adress] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Linkman] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[ShopID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'ShopID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'ShopName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'Adress'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'Phone'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Shop', @level2type=N'COLUMN', @level2name=N'Linkman'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sysparameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Sysparameter](
	[Parameter] [nvarchar](50) NOT NULL,
	[ParameterValue] [text] NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sysparameter] PRIMARY KEY CLUSTERED 
(
	[Parameter] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Sysparameter', @level2type=N'COLUMN', @level2name=N'Parameter'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数值' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Sysparameter', @level2type=N'COLUMN', @level2name=N'ParameterValue'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数描述' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Sysparameter', @level2type=N'COLUMN', @level2name=N'Description'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VideoSource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VideoSource](
	[VideoID] [int] IDENTITY(1,1) NOT NULL,
	[VideoName] [nvarchar](50) NOT NULL,
	[MediaSource] [nvarchar](200) NOT NULL,
	[Channel] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ControlPort] [int] NOT NULL,
	[StreamPort] [int] NOT NULL,
	[AutoReconnect] [bit] NOT NULL,
	[AutoReconnectInterval] [int] NOT NULL,
	[ConnectTimeOut] [int] NOT NULL,
	[IsForCarPlate] [bit] NOT NULL,
	[EntranceID] [int] NOT NULL,
 CONSTRAINT [PK_VideoSource] PRIMARY KEY CLUSTERED 
(
	[VideoID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'视频ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'VideoID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'视频名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'VideoName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'视频源' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'MediaSource'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'视频通道' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'Channel'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'UserName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'Password'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制端口' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'ControlPort'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流端口' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'StreamPort'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否自动重连' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'AutoReconnect'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自动重连间隔' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'AutoReconnectInterval'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连接超时' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'ConnectTimeOut'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否用于车牌识别' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'IsForCarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在停车场通道' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VideoSource', @level2type=N'COLUMN', @level2name=N'EntranceID'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VipVisitor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VipVisitor](
	[RegPlate] [nvarchar](50) NOT NULL,
	[AddDateTime] [datetime] NOT NULL,
	[ApplyDateTime] [datetime] NOT NULL,
	[ExpiredDateTime] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[LastDateTime] [datetime] NULL,
 CONSTRAINT [PK__VipVisitor__0BC6C43E] PRIMARY KEY CLUSTERED 
(
	[RegPlate] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登记车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VipVisitor', @level2type=N'COLUMN', @level2name=N'RegPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登记时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VipVisitor', @level2type=N'COLUMN', @level2name=N'AddDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VipVisitor', @level2type=N'COLUMN', @level2name=N'ApplyDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VipVisitor', @level2type=N'COLUMN', @level2name=N'ExpiredDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VipVisitor', @level2type=N'COLUMN', @level2name=N'Status'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次进出时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'VipVisitor', @level2type=N'COLUMN', @level2name=N'LastDateTime'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkStation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkStation](
	[StationID] [nvarchar](50) NOT NULL,
	[StationName] [nvarchar](50) NOT NULL,
	[EntranceID] [nvarchar](200) NULL,
	[CenterCharge] [varchar](1) NOT NULL,
	[Cards] [int] NOT NULL,
	[Cash] [decimal](10, 2) NOT NULL,
	[OndutyOperator] [nvarchar](50) NULL,
	[OndutyDateTime] [datetime] NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_WorkStation] PRIMARY KEY CLUSTERED 
(
	[StationID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'StationName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理的通道' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'EntranceID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是中央收费处' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'CenterCharge'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站当前临时卡数量' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'Cards'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站当前的现金数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'Cash'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站当班操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'OndutyOperator'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当班时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'OndutyDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkStation', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WaitingCommand]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WaitingCommand](
	[CardID] [nvarchar](50) NOT NULL,
	[EntranceID] [int] NOT NULL,
	[Command] [tinyint] NOT NULL,
 CONSTRAINT [PK_WaitingCommand] PRIMARY KEY CLUSTERED 
(
	[EntranceID] ASC,
	[Command] ASC,
	[CardID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WaitingCommand', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车场ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WaitingCommand', @level2type=N'COLUMN', @level2name=N'EntranceID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'执行动作' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WaitingCommand', @level2type=N'COLUMN', @level2name=N'Command'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[maxCount]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[maxCount](
	[tableID] [int] NOT NULL,
	[maxCount] [varchar](14) NOT NULL,
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APM]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SerialNum] [nvarchar](50) NOT NULL,
	[IP] [nvarchar](50) NULL,
	[MAC] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[Coin] [int] NOT NULL,
	[CashAmount] [decimal](10, 2) NOT NULL,
	[LastBalance] [decimal](10, 2) NOT NULL,
	[CheckOutTime] [datetime] NOT NULL,
	[ActiveDateTime] [datetime] NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_APM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增，主键' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'ID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'SerialNum'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机的IP' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'IP'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机的MAC地址' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'MAC'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机当前状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'Status'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费机内硬币数量' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'Coin'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费机内纸币数量' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'CashAmount'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费机上次取钱时留下的金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'LastBalance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费机上次取钱的时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'CheckOutTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自助缴费机的一些描述性说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APM', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APMLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APMLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SerialNumber] [nvarchar](50) NOT NULL,
	[LogDateTime] [datetime] NOT NULL,
	[LogType] [tinyint] NOT NULL,
	[CardID] [varchar](20) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[MID] [nvarchar](50) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_APMLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号，自动增加' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'ID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费流水号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'SerialNumber'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志记录时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'LogDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'LogType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志描述' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'Description'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缴费机编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'MID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'APMLog', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardLostRestore]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardLostRestore](
	[CardID] [nvarchar](50) NOT NULL,
	[LostDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[LostOperator] [nvarchar](50) NOT NULL,
	[LostStationID] [nvarchar](50) NOT NULL,
	[LostMemo] [nvarchar](200) NULL,
	[CardStatus] [tinyint] NOT NULL,
	[PaymentMode] [tinyint] NULL,
	[LostCardCost] [decimal](10, 2) NULL,
	[SettleDateTime] [datetime] NULL,
	[RestoreDateTime] [datetime] NULL,
	[RestoreOperator] [nvarchar](50) NULL,
	[RestoreStationId] [nvarchar](50) NULL,
	[RestoreMemo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardLostRestore] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[LostDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂失时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'LostDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂失操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'LostOperator'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂失工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'LostStationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂失说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'LostMemo'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂失前的卡片状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'CardStatus'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂失费用支付方式' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'PaymentMode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'遗失工本费' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'LostCardCost'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'恢复时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'RestoreDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'恢复操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'RestoreOperator'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'恢复工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'RestoreStationId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'恢复说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardLostRestore', @level2type=N'COLUMN', @level2name=N'RestoreMemo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OperatorSettleLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OperatorSettleLog](
	[SettleDateTime] [datetime] NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[SettleFrom] [datetime] NULL,
	[StationID] [nvarchar](50) NULL,
	[CashParkFact] [decimal](10, 2) NOT NULL,
	[CashOperatorCard] [decimal](10, 2) NOT NULL,
	[CashParkDiscount] [decimal](10, 2) NOT NULL,
	[CashOfCard] [decimal](10, 2) NOT NULL,
	[CashOfDeposit] [decimal](10, 2) NOT NULL,
	[CashOfCardRecycle] [decimal](10, 2) NOT NULL,
	[CashOfCardLost] [decimal](10, 2) NOT NULL,
	[HandInCash] [decimal](10, 2) NULL,
	[NonCashParkFact] [decimal](10, 2) NOT NULL,
	[NonCashParkDiscount] [decimal](10, 2) NOT NULL,
	[NonCashOfCard] [decimal](10, 2) NOT NULL,
	[NonCashOfDeposit] [decimal](10, 2) NOT NULL,
	[NonCashOfCardLost] [decimal](10, 2) NOT NULL,
	[TempCardRecycle] [int] NOT NULL,
	[OpenDoorCount] [int] NOT NULL,
 CONSTRAINT [PK_SettleDatetime] PRIMARY KEY CLUSTERED 
(
	[SettleDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'从某个时间起开始结算' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'SettleFrom'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车费用收现' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'CashParkFact'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车费用现金折扣' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'CashParkDiscount'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'固定卡收现' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'CashOfCard'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金收取的卡片押金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'CashOfDeposit'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金返还的卡片回收金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'CashOfCardRecycle'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金收取的卡片遗失金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'CashOfCardLost'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上交现金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'HandInCash'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'非现金停车费用' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'NonCashParkFact'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'非现金停车折扣' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'NonCashParkDiscount'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'非现金固定卡收费' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'NonCashOfCard'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'非现金卡片押金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'NonCashOfDeposit'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'非现金遗失工本费' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'NonCashOfCardLost'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收回的临时卡数量' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'TempCardRecycle'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'软件开闸次数统计' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OperatorSettleLog', @level2type=N'COLUMN', @level2name=N'OpenDoorCount'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'DeleteDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'CardType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片余额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'Balance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片有效期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'ValidDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片押金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'Deposit'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDeleteRecord', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Card]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Card](
	[CardID] [nvarchar](50) NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CardType] [tinyint] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CarType] [tinyint] NOT NULL,
	[Index] [smallint] NOT NULL,
	[CardNum] [smallint] NOT NULL,
	[ParkingFlag] [tinyint] NOT NULL,
	[LastDateTime] [datetime] NOT NULL,
	[LastEntrance] [int] NOT NULL,
	[LastCarPlate] [nvarchar](50) NULL,
	[AccessID] [tinyint] NOT NULL,
	[ActivationDate] [datetime] NOT NULL,
	[ValidDate] [datetime] NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[Deposit] [decimal](10, 2) NOT NULL,
	[DiscountHour] [tinyint] NOT NULL,
	[Options] [int] NOT NULL,
	[CarPlate] [nvarchar](50) NULL,
	[Memo] [nvarchar](200) NULL,
	[PaidDateTime] [datetime] NULL,
	[ParkFee] [decimal](10, 2) NOT NULL,
	[TotalPaidFee] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号(原始卡号)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'CardType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'Status'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'CarType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下载记录起始序号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'Index'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'CardNum'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车标志' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'ParkingFlag'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次刷卡时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'LastDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次刷卡通道' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'LastEntrance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次刷卡识别到的车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'LastCarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通道访问权限' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'AccessID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'ActivationDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有限日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'ValidDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片余额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'Balance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片押金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'Deposit'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠时数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'DiscountHour'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片选项' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'Options'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'固定类卡登记车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Card', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardCharge]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardCharge](
	[CardID] [nvarchar](50) NOT NULL,
	[ChargeDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[ChargeAmount] [decimal](10, 2) NOT NULL,
	[PaymentMode] [tinyint] NOT NULL,
	[Payment] [decimal](10, 2) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[ValidDate] [datetime] NOT NULL,
	[SettleDateTime] [datetime] NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardCharge] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[ChargeDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'ChargeDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'ChargeAmount'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'PaymentMode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'Payment'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值后卡片余额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'Balance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值后卡片有效期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'ValidDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardCharge', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDefer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardDefer](
	[CardID] [nvarchar](50) NOT NULL,
	[DeferDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[OriginalDate] [datetime] NULL,
	[CurrentDate] [datetime] NULL,
	[PaymentMode] [tinyint] NOT NULL,
	[DeferMoney] [decimal](10, 2) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[SettleDateTime] [datetime] NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardDefer] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[DeferDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'延期时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'DeferDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'延期前有效期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'OriginalDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'延期后有效期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'CurrentDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'PaymentMode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'DeferMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDefer', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardEvent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardEvent](
	[CardID] [nvarchar](50) NOT NULL,
	[EventDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[ParkID] [int] NOT NULL,
	[EntranceID] [int] NOT NULL,
	[EntranceName] [nvarchar](50) NULL,
	[LastDateTime] [datetime] NULL,
	[CardType] [tinyint] NOT NULL,
	[CarType] [tinyint] NOT NULL,
	[IsExitEvent] [bit] NOT NULL,
	[EventType] [tinyint] NOT NULL,
	[EventStatus] [tinyint] NOT NULL,
	[ParkingStatus] [tinyint] NOT NULL,
	[OperatorNum] [nvarchar](50) NULL,
	[StationID] [nvarchar](50) NULL,
	[SettleDateTime] [datetime] NULL,
 CONSTRAINT [PK_CardEvent] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[EventDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'EventDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'刷卡停车场' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'ParkID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'刷卡通道' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'EntranceID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'刷卡通道名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'EntranceName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次刷卡时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'LastDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'CardType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'CarType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是出场刷卡' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'IsExitEvent'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'EventType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'EventStatus'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'ParkingStatus'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'OperatorNum'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardEvent', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardPaymentRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardPaymentRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[ChargeDateTime] [datetime] NOT NULL,
	[EnterDateTime] [datetime] NULL,
	[CardType] [tinyint] NOT NULL,
	[CarType] [tinyint] NOT NULL,
	[TariffType] [tinyint] NOT NULL,
	[LastTotalPaid] [decimal](10, 2) NOT NULL,
	[LastTotalDiscount] [decimal](10, 2) NOT NULL,
	[Accounts] [decimal](10, 2) NOT NULL,
	[Paid] [decimal](10, 2) NOT NULL,
	[Discount] [decimal](10, 2) NOT NULL,
	[PaymentMode] [tinyint] NOT NULL,
	[DiscountHour] [tinyint] NOT NULL,
	[IsCenterCharge] [bit] NULL,
	[OperatorNum] [nvarchar](50) NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[SettleDateTime] [datetime] NULL,
	[Memo] [nvarchar](200) NULL,
	[ParkFee] [decimal](10, 2) NOT NULL,
	[PaymentCode] [tinyint] NOT NULL,
	[OperatorCardID] [nvarchar](50) NULL,
 CONSTRAINT [PK_CardPaymentRecord_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'ID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收费时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'ChargeDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入场时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'EnterDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'CardType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收费车型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'CarType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记费类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'TariffType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'之前累计收费' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'LastTotalPaid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'之前累计折扣' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'LastTotalDiscount'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次应收' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'Accounts'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次实收' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'Paid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次折扣' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'Discount'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'PaymentMode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠时数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'DiscountHour'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是中央收费' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'IsCenterCharge'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'OperatorNum'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardPaymentRecord', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardRecycle]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardRecycle](
	[CardID] [nvarchar](50) NOT NULL,
	[RecycleDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[CardType] [tinyint] NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[ValidDate] [datetime] NOT NULL,
	[Deposit] [decimal](10, 2) NOT NULL,
	[RecycleMoney] [decimal](10, 2) NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[SettleDateTime] [datetime] NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardRecycle] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[RecycleDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回收时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'RecycleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'CardType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片余额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'Balance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片有效期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'ValidDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片押金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'Deposit'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退回金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'RecycleMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRecycle', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDisableEnable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardDisableEnable](
	[CardID] [nvarchar](50) NOT NULL,
	[DisableDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[DisableOperator] [nvarchar](50) NOT NULL,
	[DisableStationID] [nvarchar](50) NOT NULL,
	[DisableMemo] [nvarchar](200) NULL,
	[CardStatus] [tinyint] NULL,
	[EnableDateTime] [datetime] NULL,
	[EnableOperator] [nvarchar](50) NULL,
	[EnableStationId] [nvarchar](50) NULL,
	[EnableMemo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardDisableEnable] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[DisableDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁用时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'DisableDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁用操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'DisableOperator'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁用工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'DisableStationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁用说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'DisableMemo'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁用前卡片状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'CardStatus'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'EnableDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'EnableOperator'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'EnableStationId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardDisableEnable', @level2type=N'COLUMN', @level2name=N'EnableMemo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardRelease]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardRelease](
	[CardID] [nvarchar](50) NOT NULL,
	[ReleaseDateTime] [datetime] NOT NULL,
	[OwnerName] [nvarchar](50) NULL,
	[CardCertificate] [nvarchar](50) NULL,
	[CarPlate] [nvarchar](50) NULL,
	[CardType] [tinyint] NOT NULL,
	[Balance] [decimal](10, 2) NOT NULL,
	[ActivationDate] [datetime] NOT NULL,
	[ValidDate] [datetime] NOT NULL,
	[Deposit] [decimal](10, 2) NOT NULL,
	[ReleaseMoney] [decimal](10, 2) NOT NULL,
	[PaymentMode] [tinyint] NOT NULL,
	[HolidayEnabled] [bit] NOT NULL,
	[OperatorID] [nvarchar](50) NOT NULL,
	[StationID] [nvarchar](50) NULL,
	[SettleDateTime] [datetime] NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_CardRelease] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[ReleaseDateTime] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发行时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'ReleaseDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'持卡人姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'OwnerName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'CardCertificate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'CarPlate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'CardType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片余额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'Balance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'ActivationDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'ValidDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'押金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'Deposit'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发行收费' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'ReleaseMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'PaymentMode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节假日选项' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'HolidayEnabled'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CardRelease', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alarm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Alarm](
	[AlarmID] [int] IDENTITY(1,1) NOT NULL,
	[AlarmDateTime] [datetime] NOT NULL,
	[AlarmSource] [nvarchar](50) NULL,
	[AlarmType] [int] NOT NULL,
	[AlarmDescr] [nvarchar](300) NULL,
	[OperatorID] [nvarchar](50) NULL,
	[SettleDateTime] [datetime] NULL,
 CONSTRAINT [PK_Alarm] PRIMARY KEY CLUSTERED 
(
	[AlarmID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报警ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Alarm', @level2type=N'COLUMN', @level2name=N'AlarmID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报警时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Alarm', @level2type=N'COLUMN', @level2name=N'AlarmDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警报来源' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Alarm', @level2type=N'COLUMN', @level2name=N'AlarmSource'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警报类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Alarm', @level2type=N'COLUMN', @level2name=N'AlarmType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警报描述' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Alarm', @level2type=N'COLUMN', @level2name=N'AlarmDescr'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Alarm', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Alarm', @level2type=N'COLUMN', @level2name=N'SettleDateTime'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operator]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Operator](
	[OperatorID] [nvarchar](50) NOT NULL,
	[OperatorName] [nvarchar](50) NOT NULL,
	[OperatorPwd] [nvarchar](50) NOT NULL,
	[RoleID] [nvarchar](50) NOT NULL,
	[OperatorNum] [int] NULL,
 CONSTRAINT [PK_Operator] PRIMARY KEY CLUSTERED 
(
	[OperatorID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Operator', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员姓名' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Operator', @level2type=N'COLUMN', @level2name=N'OperatorName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Operator', @level2type=N'COLUMN', @level2name=N'OperatorPwd'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Operator', @level2type=N'COLUMN', @level2name=N'RoleID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Operator', @level2type=N'COLUMN', @level2name=N'OperatorNum'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Park]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Park](
	[ParkID] [int] IDENTITY(1,1) NOT NULL,
	[ParkName] [nvarchar](50) NOT NULL,
	[TotalPosition] [smallint] NOT NULL,
	[MinPosition] [smallint] NOT NULL,
	[Vacant] [smallint] NOT NULL,
	[ParkFullText] [nvarchar](50) NULL,
	[VacantText] [nvarchar](50) NULL,
	[ParkAdapterUri] [nvarchar](200) NULL,
	[Commport] [tinyint] NOT NULL,
	[HostWorkstation] [nvarchar](50) NULL,
	[CarPlateRecognizeUri] [nvarchar](200) NULL,
	[IsNested] [bit] NOT NULL,
	[ParentID] [int] NULL,
	[WorkMode] [tinyint] NOT NULL,
	[DeviceType] [tinyint] NOT NULL,
	[Options] [int] NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_Park] PRIMARY KEY CLUSTERED 
(
	[ParkID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车场ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'ParkID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停车场名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'ParkName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总车位数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'TotalPosition'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最低车位数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'MinPosition'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车位余数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'Vacant'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满位提示字符' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'ParkFullText'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'余位提示字符' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'VacantText'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通讯URI' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'ParkAdapterUri'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通道串口' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'Commport'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'托管工作站' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'HostWorkstation'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌识别URI' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'CarPlateRecognizeUri'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是嵌套车场' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'IsNested'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一级停车场ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'ParentID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Park', @level2type=N'COLUMN', @level2name=N'Memo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnapShot]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SnapShot](
	[ShotAt] [datetime] NOT NULL,
	[VideoSourceID] [int] NOT NULL,
	[Photo] [image] NOT NULL,
 CONSTRAINT [PK_SnapShot] PRIMARY KEY CLUSTERED 
(
	[ShotAt] ASC,
	[VideoSourceID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抓拍时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SnapShot', @level2type=N'COLUMN', @level2name=N'ShotAt'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抓拍视频ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SnapShot', @level2type=N'COLUMN', @level2name=N'VideoSourceID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片内容' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SnapShot', @level2type=N'COLUMN', @level2name=N'Photo'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscountRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DiscountRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShopID] [nvarchar](50) NOT NULL,
	[DiscountDateTime] [datetime] NOT NULL,
	[DiscountHour] [tinyint] NOT NULL,
	[CardID] [nvarchar](50) NOT NULL,
	[OperatorID] [nvarchar](50) NULL,
	[StationID] [nvarchar](50) NOT NULL,
	[Pay] [bit] NULL,
 CONSTRAINT [PK_DiscountRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录序号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'ID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'ShopID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'写入时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'DiscountDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠时数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'DiscountHour'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'CardID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'OperatorID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点序号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'StationID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否结算' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DiscountRecord', @level2type=N'COLUMN', @level2name=N'Pay'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serialNumber]') AND type in (N'P', N'PC'))
BEGIN
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
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setMaxCount]') AND type in (N'P', N'PC'))
BEGIN
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


if not exists (select * from Operator where OperatorID='admin')
insert into operator (operatorID,OperatorName,OperatorPwd,RoleID,OperatorNum) values('admin','admin','123','Admin',1)

if not exists (select * from WorkStation where StationID='DefaultStation')
insert into Workstation (StationID,StationName,EntranceID,CenterCharge,Cards,Cash,memo)
values('DefaultStation','Managemt Central','',0,0,0,'')

if not exists (select * from Role where RoleID='Admin')
insert into Role (RoleID,[Name],Description,Permission) values('Admin','Administrator','Administrator，has all permissions.','all')

if not exists (select * from Role where RoleID='CardOperator')
insert into Role (RoleID,[Name],Description,Permission)
values('CardOperator','Card Operator','','0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,34,35,36,37,38,39,40,41,42,43,44,45')

if not exists (select * from Role where RoleID='PaymentOperator')
insert into Role (RoleID,[Name],Description,Permission)
values('PaymentOperator','Payment Operator','','0,19,20,22,23,25,47,48,34,35,36,37,38,39,40,41,42,43,44,45')
