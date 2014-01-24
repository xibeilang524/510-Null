VERSION 5.00
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "MSCOMM32.OCX"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "Comdlg32.ocx"
Begin VB.Form MainForm 
   Caption         =   "2010版CAN总线停车场系统调试与设置程式20101212  请选择20100318日期的数据库下载，否则出错！"
   ClientHeight    =   8730
   ClientLeft      =   525
   ClientTop       =   1065
   ClientWidth     =   12840
   ClipControls    =   0   'False
   BeginProperty Font 
      Name            =   "Wingdings"
      Size            =   9.75
      Charset         =   2
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H0000FFFF&
   LinkTopic       =   "Form2"
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   13942.56
   ScaleMode       =   0  'User
   ScaleWidth      =   27439.91
   Begin VB.CommandButton Tran2 
      BackColor       =   &H8000000D&
      Caption         =   "读取ID号"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   1
      Left            =   360
      MaskColor       =   &H00FF8080&
      TabIndex        =   60
      Top             =   7440
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton Tran2 
      BackColor       =   &H8000000D&
      Caption         =   "读取ID和优惠"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   6
      Left            =   1320
      MaskColor       =   &H00FF8080&
      TabIndex        =   59
      Top             =   6840
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton Tran2 
      BackColor       =   &H8000000D&
      Caption         =   "Mf停止读卡"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   0
      Left            =   360
      MaskColor       =   &H00FF8080&
      TabIndex        =   58
      Top             =   6840
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton Tran2 
      BackColor       =   &H8000000D&
      Caption         =   "Mf生成临时卡"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   9
      Left            =   2280
      MaskColor       =   &H00FF8080&
      TabIndex        =   57
      Top             =   6840
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton Tran2 
      BackColor       =   &H8000000D&
      Caption         =   "Mf生成优惠储值卡"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   10
      Left            =   2280
      MaskColor       =   &H00FF8080&
      TabIndex        =   56
      Top             =   7440
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton Tran2 
      BackColor       =   &H8000000D&
      Caption         =   "Mf临时卡进场"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   11
      Left            =   3240
      MaskColor       =   &H00FF8080&
      TabIndex        =   55
      Top             =   6840
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton Tran2 
      BackColor       =   &H8000000D&
      Caption         =   "Mf优惠充值"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   12
      Left            =   3240
      MaskColor       =   &H00FF8080&
      TabIndex        =   54
      Top             =   7440
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "事件有效"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   29
      Left            =   6840
      MaskColor       =   &H00FF8080&
      TabIndex        =   53
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "手动落闸"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   20
      Left            =   5760
      MaskColor       =   &H00FF8080&
      TabIndex        =   51
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "下载备用设置"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   44
      Left            =   11280
      MaskColor       =   &H00FF8080&
      TabIndex        =   43
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   1335
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "发布广告"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   18
      Left            =   2520
      MaskColor       =   &H00FF8080&
      TabIndex        =   50
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "在线查询"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   12
      Left            =   2520
      MaskColor       =   &H00FF8080&
      TabIndex        =   49
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "实时模式"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   10
      Left            =   1440
      MaskColor       =   &H00FF8080&
      TabIndex        =   48
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "自动模式"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   9
      Left            =   1440
      MaskColor       =   &H00FF8080&
      TabIndex        =   47
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "出卡一张"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   51
      Left            =   4680
      MaskColor       =   &H00FF8080&
      TabIndex        =   46
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "开关出卡"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   50
      Left            =   4680
      MaskColor       =   &H00FF8080&
      TabIndex        =   45
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.TextBox Text2 
      BackColor       =   &H00FF0000&
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   405
      Left            =   360
      OLEDragMode     =   1  'Automatic
      OLEDropMode     =   2  'Automatic
      ScrollBars      =   2  'Vertical
      TabIndex        =   42
      Text            =   "1"
      Top             =   4680
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "抬闸自落"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   19
      Left            =   5760
      MaskColor       =   &H00FF8080&
      TabIndex        =   41
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H000000FF&
      Caption         =   "选择数据文件"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   615
      Index           =   49
      Left            =   11280
      MaskColor       =   &H8000000D&
      Style           =   1  'Graphical
      TabIndex        =   40
      Top             =   4560
      Width           =   1335
   End
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   10920
      Top             =   120
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "下载系统设置"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   48
      Left            =   9840
      MaskColor       =   &H00FF8080&
      TabIndex        =   39
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   1335
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "总位余位设置"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   47
      Left            =   8400
      MaskColor       =   &H00FF8080&
      TabIndex        =   38
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   1335
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "区位引导设置"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   46
      Left            =   8400
      MaskColor       =   &H00FF8080&
      TabIndex        =   37
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   1335
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "下载收费标准"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   45
      Left            =   9840
      MaskColor       =   &H00FF8080&
      TabIndex        =   36
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   1335
   End
   Begin VB.TextBox Text3 
      BackColor       =   &H00FF0000&
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   555
      Left            =   1440
      OLEDragMode     =   1  'Automatic
      OLEDropMode     =   2  'Automatic
      ScrollBars      =   2  'Vertical
      TabIndex        =   35
      Text            =   " 中国首家 CAN BUS IC/ID卡 脱机系统 "
      Top             =   4560
      Width           =   9735
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "时钟下载"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   17
      Left            =   6840
      MaskColor       =   &H00FF8080&
      TabIndex        =   34
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "客户名称"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   16
      Left            =   3600
      MaskColor       =   &H00FF8080&
      TabIndex        =   33
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "付机复位"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   14
      Left            =   360
      MaskColor       =   &H00FF8080&
      TabIndex        =   32
      Top             =   6000
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "直通显示"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   15
      Left            =   3600
      MaskColor       =   &H00FF8080&
      TabIndex        =   31
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.CommandButton Tran1 
      BackColor       =   &H8000000D&
      Caption         =   "主机复位"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Index           =   11
      Left            =   360
      MaskColor       =   &H00FF8080&
      TabIndex        =   30
      Top             =   5400
      UseMaskColor    =   -1  'True
      Width           =   975
   End
   Begin VB.Timer Timer1 
      Interval        =   1000
      Left            =   11640
      Top             =   120
   End
   Begin VB.Frame Frame3 
      Caption         =   "接收"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2775
      Left            =   240
      TabIndex        =   9
      Top             =   1440
      Width           =   12375
      Begin VB.TextBox Text4 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   1575
         Left            =   120
         MultiLine       =   -1  'True
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         ScrollBars      =   2  'Vertical
         TabIndex        =   21
         Text            =   "CommTest.frx":0000
         Top             =   960
         Width           =   12135
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   345
         Index           =   6
         Left            =   11640
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         TabIndex        =   20
         Tag             =   "100"
         Text            =   "06"
         Top             =   600
         Width           =   375
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   345
         Index           =   5
         Left            =   8640
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         TabIndex        =   19
         Text            =   "05"
         Top             =   600
         Width           =   3015
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   345
         Index           =   4
         Left            =   8280
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         TabIndex        =   18
         Text            =   "04"
         Top             =   600
         Width           =   375
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   345
         Index           =   3
         Left            =   5520
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         TabIndex        =   17
         Text            =   "03"
         Top             =   600
         Width           =   2775
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   345
         Index           =   2
         Left            =   3360
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         TabIndex        =   16
         Text            =   "02"
         Top             =   600
         Width           =   2175
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   345
         Index           =   1
         Left            =   1200
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         TabIndex        =   15
         Text            =   $"CommTest.frx":000D
         Top             =   600
         Width           =   2175
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00FF00FF&
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   345
         Index           =   0
         Left            =   120
         OLEDragMode     =   1  'Automatic
         OLEDropMode     =   2  'Automatic
         TabIndex        =   14
         Text            =   $"CommTest.frx":0015
         Top             =   600
         Width           =   1095
      End
      Begin VB.Label Label2 
         Caption         =   "CRC校验"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   14
         Left            =   11400
         TabIndex        =   28
         Top             =   360
         Width           =   735
      End
      Begin VB.Label Label2 
         Caption         =   "指令参数明细"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   17
         Left            =   9360
         TabIndex        =   27
         Top             =   360
         Width           =   1335
      End
      Begin VB.Label Label2 
         Caption         =   "参数长度"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   18
         Left            =   8040
         TabIndex        =   26
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label2 
         Caption         =   "指令名称"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   19
         Left            =   6480
         TabIndex        =   25
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label2 
         Caption         =   "指令起始符"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   23
         Left            =   240
         TabIndex        =   24
         Top             =   360
         Width           =   975
      End
      Begin VB.Label Label2 
         Caption         =   "发送设备"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   25
         Left            =   1920
         TabIndex        =   23
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label2 
         Caption         =   "指令类别"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   26
         Left            =   4080
         TabIndex        =   22
         Top             =   360
         Width           =   855
      End
   End
   Begin VB.Frame 串口选择 
      Caption         =   "串口选择"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   735
      Left            =   240
      TabIndex        =   4
      Top             =   600
      Width           =   4095
      Begin VB.OptionButton Option1 
         Caption         =   "COM1"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   0
         Left            =   240
         TabIndex        =   8
         Top             =   240
         Width           =   855
      End
      Begin VB.OptionButton Option1 
         Caption         =   "COM2"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   1
         Left            =   1200
         TabIndex        =   7
         Top             =   240
         Width           =   855
      End
      Begin VB.OptionButton Option1 
         Caption         =   "COM3"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   2
         Left            =   2160
         TabIndex        =   6
         Top             =   240
         Width           =   855
      End
      Begin VB.OptionButton Option1 
         Caption         =   "COM4"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   3
         Left            =   3120
         TabIndex        =   5
         Top             =   240
         Width           =   855
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "波特率选择"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   735
      Left            =   4440
      TabIndex        =   1
      Top             =   600
      Width           =   5775
      Begin VB.OptionButton Option2 
         Caption         =   "115200"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   5
         Left            =   4680
         TabIndex        =   13
         Top             =   240
         Width           =   855
      End
      Begin VB.OptionButton Option2 
         Caption         =   "57600"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   4
         Left            =   3720
         TabIndex        =   12
         Top             =   240
         Width           =   855
      End
      Begin VB.OptionButton Option2 
         Caption         =   "38400"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   3
         Left            =   2760
         TabIndex        =   11
         Top             =   240
         Width           =   855
      End
      Begin VB.OptionButton Option2 
         Caption         =   "19200"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   2
         Left            =   1800
         TabIndex        =   10
         Top             =   240
         Width           =   855
      End
      Begin VB.OptionButton Option2 
         Caption         =   "9600"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   1
         Left            =   960
         TabIndex        =   3
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton Option2 
         Caption         =   "2400"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   9
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Index           =   0
         Left            =   120
         TabIndex        =   2
         Top             =   240
         Width           =   735
      End
   End
   Begin MSCommLib.MSComm MSComm1 
      Left            =   10320
      Top             =   720
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   393216
      DTREnable       =   -1  'True
      InBufferSize    =   512
      InputMode       =   1
   End
   Begin VB.Label Label3 
      Caption         =   "2006-10-18"
      BeginProperty Font 
         Name            =   "Wingdings"
         Size            =   14.25
         Charset         =   2
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   375
      Index           =   0
      Left            =   11040
      TabIndex        =   52
      Top             =   600
      Width           =   1575
   End
   Begin VB.Label Label2 
      Caption         =   "选择机号"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   0
      Left            =   480
      TabIndex        =   44
      Top             =   4440
      Width           =   855
   End
   Begin VB.Label Label3 
      Caption         =   "12:12:30"
      BeginProperty Font 
         Name            =   "Wingdings"
         Size            =   14.25
         Charset         =   2
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   375
      Index           =   1
      Left            =   11160
      TabIndex        =   29
      Top             =   960
      Width           =   1215
   End
   Begin VB.Label Label4 
      Caption         =   "2010版CAN总线停车场系统调试与设置程式"
      BeginProperty Font 
         Name            =   "隶书"
         Size            =   21.75
         Charset         =   134
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   495
      Left            =   2040
      TabIndex        =   0
      Top             =   0
      Width           =   8775
   End
   Begin VB.Menu DataEdit 
      Caption         =   "系统设置及收费标准修改"
   End
End
Attribute VB_Name = "MainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim RxCommByteCount As Integer
Dim RxData(512) As Byte
Dim Database1 As Database
'Dim TableRxCommand As TableDef
Dim RecordsetRx As Recordset
Dim RecordsetAddress As Recordset
Dim RecordsetCommand As Recordset

Dim DatabaseTran As Database
Dim DataTollTableName As Recordset
Dim RecordsetTran As Recordset

Dim RxText As String
Dim RelayNum As Byte
Dim CardWait As Byte
Dim CardValid As Byte
Dim CardInvalid As Byte
Dim LinkMode1 As Byte
Dim Volume As Byte
Dim VehicleType As Byte


Const CONCommSyn1 = &HFE
Const CONCommSyn2 = &HFD
Const CONCommSyn3 = &HFE
Const ADDR_Broadcast = &HF0                                      '广播地址所有设备broadcast
Const ADDR_NotAssign = &HFF                                      '未分配地址设备
                                                                 '设备专用指令可用0x10-0x1D(0x01-0x0F通用指令)；避开CONCommSyn 1E1F
Const ADDR_Myself = &H0                                            '一切接收自身232口指令接收设备的本机概念
Const TYPE_Itself = &H0                                          '一切接收设备的本机概念
Const ADDR_RS232_1 = &HF1                                         '接收设备的1号(默认的)串口
Const ADDR_RS232_2 = &HF2                                         '接收设备的2号串口

Const ADDR_CanManager = &H1                                      '对次级而言的CAN管理主机
Const ADDR_SystemManageMachine = &H1                             '系统管理机地址(01-1D)对PC而言
Const ADDR_PassageController = &H0                               '车场通道控制器,主板广播地址= &H20,原则上同样的硬件可互换,不分出入口主板,配置在上(管理主机)不在下,复杂车场只需修改管理机配置.当然配置也可以下载.
Const ADDR_EntranceController = &H20                             '车场通道控制器,主板广播地址0x20,原则上同样的硬件可互换,不分出入口主板,配置在上(管理主机)不在下,复杂车场只需修改管理机配置.当然配置也可以下载.
'Const ADDR_ExitController = &H40                                '车场通道控制器,主板广播地址0x20,原则上同样的硬件可互换,不分出入口主板,配置在上(管理主机)不在下,复杂车场只需修改管理机配置.当然配置也可以下载.
                                                                 '门禁控制器，背板公用的指令集应统一，范围与地址约同
                                                                 '消费处理机，背板公用的指令集应统一，范围与地址约同
                                                                 '电梯控制器，背板公用的指令集应统一，范围与地址约同
Const ADDR_LedScreen = &H60                                      '汉字显示屏含数字显示屏，指令集为非字符区
Const ADDR_HeatPrinter = &H70                                    '热敏纸票机指令集为非字符区
Const ADDR_VehicleDetector = &H80                                '车辆检测器容量32个满足区域引导
Const ADDR_CardProvider = &HA0                                   '自动出卡机，含自动收卡机，主控器可挂接此设备，指令集应统一
Const ADDR_Reader = &HB0                                         'ID号读卡头，主控器可挂接此设备，指令集应统一
Const ADDR_AutoBarrier = &HC0                                    '自动道闸机，主控器可挂接此设备，指令集应统一
Const ADDR_IOModel = &HD0                                        '通用IO模块，一级总线设备，背板公用的指令集应统一，范围与地址约同
Const ADDR_ParkingGuider = &HE0                                  '停车引导器，一级总线设备，背板公用的指令集应统一，用作超声波实时车位引导
Const ADDR_Other = &HF0                                          '其他设备

  '电脑发送的指令
Const COMR_SoftReset = &H2                                       '工作方式+1字节参数,参数=1=软件复位
Const COMR_SetDateTime = &H3                                     '时钟设置+year,month,day,hour,minute,second,week
Const COMR_Beep = &H7                                            '蜂鸣器响+1字节参数,参数为响声次数
'以上为可广播的指令
Const LEDR_Display = &H10                                        '直通显示指令+
Const LEDR_StoreSentance = &H13                                  '保存显示指令+
Const SMMR_LinkMode = &H20                                       '联机脱机工作模式+1字节参数,参数=0/1=脱机/联机
Const SMMR_LoadCardsPrefix = &H21                                '装载或追加卡片档案通知,后随2字节起始序号(0-65534)，再加2字节卡片档案总数(1-65535),建议将所购卡片一次排序全部装入,不用的卡片先加锁.现决定不采用1字节批号（256条/1整批）,
Const SMMR_LoadOneCard = &H22                                    '装载或追加卡片档案一条,后随2字节序号(0-65534)，卡片档案必须按ID号升序排序,装载卡片期间系统停止工作.
Const SMMR_ModifyOneCard = &H23                                  '修改卡片档案一条,用于卡片挂失/加锁/延期等,修改卡片期间系统继续工作.
Const SMMR_FetchCardsRequest = &H25                             '提取卡片档案申请
Const SMMR_ReceiveCardsResult = &H26                            '收卡片档案应答
Const SMMR_LoadTimeTable = &H27                                 '下载收费时刻表
Const SMMR_LoadTollTable = &H28                                 '下载收费金额表
Const SMMR_LoadTotalPlace = &H29                                '下载各车场总车位数表,参数为：车场数量[1](1-31)+1号车场总车位数[2]+2号车场总车位数[2]+...
Const SMMR_SetVacant = &H2A                                     '下载各车场剩余车位数表,参数为：1号车场剩余车位数[2]+2号车场剩余车位数[2]+...
Const SMMR_WriteParameterTable = &H2B                           '写入各种参数表 1字节表序号+表参数。
Const SMMR_ReadParameterTable = &H2C                            '读取各种参数表 1字节起始表序号+1字节终止表序号。
Const SMMR_Volume = &H2D                                        '音量+1字节参数;0=关闭;1=原始保真音量;2=X2加大一倍;3=X4再加大一倍。
Const SMMR_LoadAudioPrefix = &H2E                               '装载或追加语音数据通知,后随2字节起始序号(0-65534)，再加2字节卡片档案总数(1-65535),建议将所购卡片一次排序全部装入,不用的卡片先加锁.现决定不采用1字节批号（256条/1整批）,
Const SMMR_LoadAudioBlock = &H2F                                '装载或追加语音数据块,后随2字节数据块序号(0-65534)，200字节数据,
Const SMMR_EventWait = &H30                                     '现场事件收悉等待（PC机必须在3秒内响应，据此判断是否联机）
     Const PCWAT_Capture = 0                                    '等待入口图像捕捉  '有效分型便于在岗亭内的显示屏上提示
     Const PCWAT_Compare = 1                                    '等待出口图像对比
     Const PCWAT_Toll = 2                                       '等待出口收费完成
     Const PCWAT_CompareToll = 3                                '等待出口图像对比及收费完成
Const SMMR_EventValid = &H31                                    '现场事件确认有效（人工抬闸事件记录有效，入口图像捕捉，出口对比与收费有效）
     Const PCVID_Capture = 0                                    '入口图像捕捉有效  '有效分型便于在岗亭内的显示屏上提示
     Const PCVID_Compare = 1                                    '出口图像对比有效
     Const PCVID_ChangeOperater = 3                             '操作员换班完成有效
     Const PCVID_Toll = 4                                       '出口收费完成有效
     Const PCVID_CompareToll = 5                                '出口图像对比及收费完成有效
     Const PCVID_Event = 6                                      '事件记录完成有效
Const SMMR_EventInvalid = &H32                                  '现场事件确认无效（出口图像对比不符无效）
     Const PCINV_Compare = 0                                    '出口图像对比无效
Const SMMR_FetchEvent = &H33                                    '提取事件记录申请
Const SMMR_EventOk = &H34                                       '收妥事件记录应答
Const SMMR_EventPointerRequest = &H35                           '查询事件记录指针(下一条记录的序号)
Const SMMR_SetEventPointer = &H36                               '设置事件记录指针(下一条记录的序号),事件记录指针改变后,再提取重设指针之前的历史事件,提取结果将会有误!!


Const PCMR_CardWait = &H60                                       '卡号收妥请等待指令+n字节参数
     '第一参数
     Const WAT_Reply = 0                                        '显示:请等待确认
     Const WAT_Capture = 1                                      '显示:等图像捕捉
     Const WAT_Picture = 2                                      '显示:等图像对比
     Const WAT_Toll = 3                                         '显示:显示收费信息
     '显示收费信息后续参数
     Const INF_Toll1 = 2                                        '参数:moneyL moneyH  显示：收费XXXX元
     Const INF_Toll2 = 8                                        '参数:HourIn,MinuteIn,SecondIn,HourOut,MinuteOut,SecondOut,moneyL,moneyH显示XX:XX:XX入;XX:XX:XX出;收费XXXX元
     Const INF_Toll3 = 14                                       '参数:YearIn,MonthIn,DayIn,HourIn,MinuteIn,SecondIn,YearOut,MonthOut,DayOut,HourOut,MinuteOut,SecondOut,moneyL,moneyH显示YY/MM/DD XX:XX:XX入;YY/MM/DD XX:XX:XX出;收费XXXX元
Const PCMR_CardValid = &H63                                     '卡有效指令+1字节参数
     Const VID_In = 0                                           '显示:欢迎进场！
     Const VID_Out = 1                                          '显示:一路平安！
     Const VID_CollectCard = 2                                  '显示:请插卡回收
Const PCMR_CardInvalid = &H64                                   '卡无效指令+1字节参数
     Const INV_ReadCard = 0                                     '显示:请再读卡！用于作读写器使用
     Const INV_NotRegister = 1                                  '显示:此卡未登记
     Const INV_HaveIn = 2                                       '显示:此卡已在场
     Const INV_StillOut = 3                                     '显示:此卡未进场
     Const INV_CardLoss = 4                                     '显示:此卡已挂失
     Const INV_CardLock = 5                                     '显示:此卡已锁定
     Const INV_OverDate = 6                                     '显示:此卡已过期
     Const INV_NotPaid = 7                                      '显示:此卡未交费
     Const INV_OverTime = 8                                     '显示:超时补交费
     Const INV_Picture = 9                                      '显示:图像不符！
     Const INV_ParkFull = 10                                    '显示:车位已满！
Const PCMR_Information = &H67                                   '显示+2字节参数信息
     Const INF_ValidDate = 0                                    'Month,day      显示：XX月XX止效
     Const INF_RemainDay = 1                                    'dayL,dayH      显示：余期XXXX天
     Const INF_RemainMoney = 2                                  'RemainMoneyL,H 显示：余值XXXX元
     Const INF_ValidDate2 = 3                                   'year,month,day 显示：XX-XX-XX止
Const ABMR_Operation = &HC0                                     '道闸直通操作指令+2字节参数"ho"/"ca"/"nc""dw"/"st"=抬闸保持/抬闸计数自落/抬闸不计数自落/落闸/停闸
Const IOMR_SingleOutput = &HD0                                  '动作数据0x00=OFF,0x01=ON;0x02/3=TOGGLE;1字节通道号(继电器号)0-255;1字节归位时间参数:0=保持不归位,1-255=0.1-25.5sec;1字节归位操作方式0x00=OFF=回零/释放,0x01=ON=回一/吸合,0x02=TOGGLE翻转保持, 0x03=循环翻转
Const IOMR_BatchOutput = &HD1                                   '动作数据字节数n,n字节动作数据,对应0至(nx8-1)号继电器,对应BIT=0=OFF,1=ON;1字节归位时间参数:0=保持不归位,1=255=0.1-25.5sec;1字节归位操作方式0x00=OFF=回零/释放,0x01=ON=回一/吸合,0x02=TOGGLE翻转保持, 0x03=循环翻转
Const IOMR_SingleInput = &HD2                                   '+通道号0-255，查询单通道输入电平
Const IOMR_BatchInput = &HD3                                    '+通道号0-255，查询所有通道输入电平
Const IOMR_SingleADC = &HD4                                     '+通道号0-255，查询单通道模数转换结果
Const IOMR_BatchADC = &HD5                                      '+通道号0-255，查询所有通道模数转换结果
Const IOMR_SingleDAC = &HD6                                     '+通道号0-255，输出单通道数模转换电压
Const IOMR_BatchDAC = &HD7                                      '+通道号0-255，输出所有通道数模转换电压
Const PGMR_SetPlace = &HE0                                       '车位信息指令+车场号+总车位数2byte+剩余车位数2字节 总车位车场号=0
Const CPMR_Lock = &HA0                                      '开关发卡机 0xA0
Const CPMR_Cardout = &HA1                                   '0xA1出卡机出卡指令

 ' 电脑接收的指令
Const COMT_ResetOk = &H1                                         '上线报告+1字节参数,参数=1=上电上线复位OK
Const VDMT_VDCar = &H98                                          '车辆检测器指令+1字节参数,参数=0/1=车走/车到
Const CPMT_CardButt = &HA8                                       '按取卡按钮
Const CPMT_CardOut = &HA9                                        '出卡机出卡一张卡
Const CPMT_CardIn = &HAA                                         '收卡机收卡一张卡
Const CPMT_CardNone = &HAB                                       '出卡机无卡
Const CPMT_CardFew = &HAC                                        '出卡机缺卡
Const CPMT_CardLoad = &HAD                                       '出卡机装卡
Const CPMT_CardAdd = &HAE                                        '出卡机添卡
Const CPMT_CardJam = &HAF                                        '出卡机塞卡
Const RWMT_CardID = &HB8                                         '上传卡号+CardKind+4bytesID; +0-nbytesData改变参数长度方式可增加数据。
     '设CardKind避免重号00-0F为本场卡，10-1F为流通卡便于计费扣费
     Const CARD_Local = &H0                                           '本场卡
     Const CARD_Mifare = &H1                                          'Mifare
     Const CARD_Legic = &H2                                           'LEGIC
     Const CARD_Ti = &H3                                              'Ti
     Const CARD_Wg26 = &H4                                            '无法确定的WG26卡(EM卡/微波卡)
     Const CARD_Wg34 = &H5                                            '无法确定的WG34卡(EM卡/微波卡
     Const CARD_EM = &H6                                              'EM
     Const CARD_Motorola = &H7                                        'Motorola
     Const CARD_HID = &H8                                             'HID

     Const CARD_Public = &H10                                         '流通卡
     Const CARD_SZT = &H11                                            '深圳通
     Const CARD_YCT = &H12                                            '羊城通
     '指令来源代码,停车场管理控制器外挂读头需识别以便判断台式读写器
     Const ADDR_Mifare = &HB1                                         'Mifare读头
     Const ADDR_Reader232 = &HB2                                      '232LEGIC/485羊城通/TiRFM001
     Const ADDR_Wg1 = &HB3                                            'wg1ID读头
     Const ADDR_Wg2 = &HB4                                            'wg2ID读头
     Const ADDR_Wg3 = &HB5                                            'wg3ID读头


Const CONCardRecordMax = 4096                       '最大可装载卡片总数
Const LEDR_Append = &H11                            '参数不定指令，不可重复  // disp immediately current input
Const LEDR_LoopStart = &H12                         '参数不定指令，不可重复  // set loop disp start point or End loop disp
Const LEDR_StoreSentence = &H13                     '0-255  sentence addr
Const LEDR_StoreLoopSentence = &H14                 '0-255  sentence addr
Const LEDR_SelectSentence = &H15                    '0-255  sentence addr
Const LEDR_DispMode = &H16                          'disp mode 00-99
Const LEDR_SetStayTime = &H17                       '0-255  Sec
'---------------------------------内部显示控制符--------------------------------------------------------------------------------
Const LEDD_LF = &H13                                'shift_up a new line
Const LEDD_CR = &H14                                'restart a new line



Private Sub DataEdit_Click()
Load SystemSet
SystemSet.Show
End Sub

Private Sub Form_Load()
'ChDir App.Path    'Change the working directory to the application located path.
'ChDrive App.Path


MSComm1.RThreshold = 1
ReceCommandByteCount = 0
RxText = "接收指令原码（双击本框可清除）：" + vbCrLf


Option2(1).Value = True
Option1(0).Value = True
RelayNum = 0
CardWait = 0
CardValid = 0
CardInvalid = 0
LinkMode1 = 1
Volume = 0
VehicleType = 0
End Sub
Private Sub MainForm_Unload()
Workspaces(0).Close
End Sub

Private Sub MSComm1_OnComm()
On Error GoTo ErrorHandler
Dim a, b, c, crc As Byte
Dim RxDa() As Byte
Dim DataRommandNew As Recordset
Retry:
Select Case MSComm1.CommEvent
                     ' Errors
Case comBreak:     Text4.Text = "A Break was received."
Case comCDTO:      Text4.Text = "CD (RLSD) Timeout."
Case comCTSTO:     Text4.Text = "CTS Timeout."
Case comDSRTO:     Text4.Text = "DSR Timeout."
Case comFrame:     Text4.Text = "Framing Error"
Case comOverrun:   Text4.Text = "Data Lost."
Case comRxOver:    Text4.Text = "Receive buffer overflow."
Case comRxParity:  Text4.Text = "Parity Error."
Case comTxFull:    Text4.Text = "Transmit buffer full."
                     ' Events
Case comEvCD:      Text4.Text = "Change in the CD line."
Case comEvCTS:     Text4.Text = "Change in the CTS line."
Case comEvDSR:     Text4.Text = "Change in the DSR line."
Case comEvRing:    Text4.Text = "Change in the Ring Indicator."
Case comEvSend:    Text4.Text = "There are SThreshold number of characters in the transmit buffer."
Case comEvReceive: 'Text4.Text = "Received RThreshold # of chars."
Receive:
  If MSComm1.RThreshold = 1 Then
     MSComm1.InputLen = 1
     RxDa = MSComm1.Input
     RxData(RxCommByteCount) = RxDa(0)
     c = RxData(RxCommByteCount)
     RxCommByteCount = RxCommByteCount + 1
     Select Case RxCommByteCount
     Case 1:
          If c <> CONCommSyn1 Then
             RxCommByteCount = 0
          End If
     Case 2:
          If c <> CONCommSyn2 Then
             RxCommByteCount = 0
          End If
     Case 3:
          If c <> CONCommSyn3 Then
             RxCommByteCount = 0
          End If
     Case 4: '发送设备地址
     Case 5: '动作设备地址
     Case 6: '设备指令
     Case 7: '参数长度
             MSComm1.RThreshold = c + 1  '参数长度+CRC
     Case 8: GoTo ReceiveComplted        '但参数长度为0时
     End Select
     If Len(Hex(c)) = 1 Then
         RxText = RxText + "0" + Hex(c) + " "
     Else
         RxText = RxText + Hex(c) + " "
     End If
  Else
     MSComm1.InputLen = MSComm1.RThreshold
     RxDa = MSComm1.Input
     a = MSComm1.RThreshold - 1
     For c = 0 To a
       RxData(RxCommByteCount) = RxDa(c)
       RxCommByteCount = RxCommByteCount + 1
     Next c
ReceiveComplted:
     s = ""
     For c = 0 To RxData(6) + 8 - 1           '头7byte+crc=8byte,-1序号从零起
         If Len(Hex(RxData(c))) = 1 Then
            s = s + "0" + Hex(RxData(c)) + " "
         Else
            s = s + Hex(RxData(c)) + " "
         End If
     Next c
     crc = 0
     For c = 3 To RxData(6) + 8 - 1 - 1       '头7byte+crc=8byte,-1序号从零起，-1不含CRC
         crc = crc Xor RxData(c)
     Next c
     If crc = RxData(RxData(6) + 7) Then
        RxText = RxText + Mid(s, 22) + vbCrLf
       ' RxText = Mid(s, 22) + vbCrLf
        Text1(0).Text = Mid(s, 1, 9)
        Text1(4).Text = Mid(s, 19, 2)
        Text1(5).Text = Mid(s, 22, Len(s) - 21 - 3)
        Text1(6).Text = Mid(s, Len(s) - 2, 2)
        
     Else
        RxText = RxText + Mid(s, 22) + "CRC_Error!" + vbCrLf
      '  RxText = Mid(s, 22) + "CRC_Error!" + vbCrLf
     End If
     MSComm1.RThreshold = 1
     RxCommByteCount = 0
     If Len(RxText) > 2500 Then
        RxText = Mid(RxText, Len(RxText) - 2500)
     End If
  End If
  Text4.Text = RxText
  If (MSComm1.InBufferCount >= MSComm1.RThreshold) Then
      GoTo Receive
  End If
End Select
If Len(RxText) > 2600 Then
   MSComm1.PortOpen = False
End If
Exit Sub
ErrorHandler:
WhaToDo% = MsgBox("Error" & Err.Number & Err.Description & " occurred", vbRetryCancel)
If WhaToDo% = vbRetry Then Resume

End Sub

Private Sub option1_Click(Index As Integer)
I = MSComm1.CommPort
On Error GoTo CommErrorHandler
If MSComm1.PortOpen = True Then
   MSComm1.PortOpen = False
End If
MSComm1.CommPort = Index + 1

MSComm1.PortOpen = True
If Option2(0).Value = True Then
   MSComm1.Settings = "2400,n,8,1"
   Text4.Text = "选择串口" + Str(Index + 1) + ",参数:2400,N,8,1"
ElseIf Option2(1).Value = True Then
   MSComm1.Settings = "9600,n,8,1"
   Text4.Text = "选择串口" + Str(Index + 1) + ",参数:9600,N,8,1"
ElseIf Option2(2).Value = True Then
   MSComm1.Settings = "19200,n,8,1"
   Text4.Text = "选择串口" + Str(Index + 1) + ",参数:19200,N,8,1"
ElseIf Option2(3).Value = True Then
   MSComm1.Settings = "57600,n,8,1"
   Text4.Text = "选择串口" + Str(Index + 1) + ",参数:57600,N,8,1"
End If
MSComm1.InputMode = comInputModeBinary
MSComm1.RThreshold = 1
Exit Sub
CommErrorHandler:
Select Case Err.Number
Case 8000: Text = "Operation not valid while the port is opened"
Case 8001: Text = "Timeout value must be greater than zero"
Case 8002: Text = "Invalid Port Number 无效的串口号！"
Case 8003: Text = "Property available only at run time"
Case 8004: Text = "Property is read only at runtime"
Case 8005: Text = "Port already open"
Case 8006: Text = "The device identifier is invalid or unsupported"
Case 8007: Text = "The Device 's baud rate is unsupported"
Case 8008: Text = "The specified byte size is invalid"
Case 8009: Text = "The default parameters are in error"
Case 8010: Text = "The hardware is not available (locked by another device)"
Case 8011: Text = "The function cannot allocate the queues"
Case 8012: Text = "The device is not open"
Case 8013: Text = "The device is already open "
Case 8014: Text = "Could not enable comm notification"
Case 8015: Text = "Could not set comm state"
Case 8016: Text = "Could not set comm event mask"
Case 8018: Text = "Operation valid only when the port is open"
Case 8019: Text = "Device busy"
Case 8020: Text = "Error reading comm device"
Case Else: Text = "串口出错，请另行选择。"
End Select
WhaToDo% = MsgBox(Text, vbRetryCancel)
If WhaToDo% = vbRetry Then Resume

hu:
MSComm1.CommPort = I
MSComm1.PortOpen = True
Option1(Index).Value = False
Option1(I - 1).Value = True

End Sub
Private Sub Option2_Click(Index As Integer)
Select Case Index
Case 0:
   MSComm1.Settings = "2400,n,8,1"
   Text4.Text = "选择串口" + Str(MSComm1.CommPort) + ",参数:2400,N,8,1"
Case 1:
   MSComm1.Settings = "9600,n,8,1"
   Text4.Text = "选择串口" + Str(MSComm1.CommPort) + ",参数:9600,N,8,1"
Case 2:
   MSComm1.Settings = "19200,n,8,1"
   Text4.Text = "选择串口" + Str(MSComm1.CommPort) + ",参数:19200,N,8,1"
Case 3:
   MSComm1.Settings = "38400,n,8,1"
   Text4.Text = "选择串口" + Str(MSComm1.CommPort) + ",参数:38400,N,8,1"
Case 4:
   MSComm1.Settings = "57600,n,8,1"
   Text4.Text = "选择串口" + Str(MSComm1.CommPort) + ",参数:57600,N,8,1"
Case 5:
   MSComm1.Settings = "115200,n,8,1"
   Text4.Text = "选择串口" + Str(MSComm1.CommPort) + ",参数:115200,N,8,1"
End Select
End Sub

Private Sub Timer1_Timer()
Label3(0).Caption = Format(Now, "yyyy-mm-dd")
Label3(1).Caption = Format(Now, "hh:mm:ss")
End Sub
Private Sub Text4_DblClick()
Text4.Text = "接收指令原码（双击本框可清除）：" + vbCrLf
RxText = "接收指令原码（双击本框可清除）：" + vbCrLf
If MSComm1.PortOpen = False Then MSComm1.PortOpen = True
End Sub

Private Sub Tran1_Click(Index As Integer)
Dim OutData() As Byte
Dim Data(256) As Byte
Dim RecordTotal As Integer
Dim RecordCount As Integer
Dim DataLen As Long
Const CONFirstLoadCardNum = 100  '<256
If MSComm1.PortOpen = False Then MSComm1.PortOpen = True
   

Select Case Index
Case 0                                          '下载卡片档案通知指令
     l = 11                                     '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                   '主机地址
     OutData(4) = TYPE_Itself                   '本机
     OutData(5) = SMMR_LoadCardsPrefix          '下载卡片档案通知指令
     OutData(6) = 4                             '参数长度
     If DataCards.Recordset.RecordCount = 0 Then
        MsgBox "当前卡片档案库无数据,请先添加数据!"
       ' DataCards.Recordset.AddNew
        Exit Sub
     Else
        DataCards.Recordset.MoveLast
        DataCards.Recordset.MoveFirst
     End If
     If DataCards.Recordset.RecordCount > CONCardRecordMax Then
        MsgBox "当前卡片档案库记录总数超过了" + Str(CONCardRecordMax) + "条,请先处理数据!"
        Exit Sub
     End If
     RecordTotal = DataCards.Recordset.RecordCount
     If RecordTotal > CONFirstLoadCardNum Then
        RecordTotal = CONFirstLoadCardNum
     End If
     OutData(7) = 0                             '准备下载记录起始序号(0-65534)低位字节
     OutData(8) = 0                             '准备下载记录起始序号高位字节
     OutData(9) = RecordTotal Mod 256           '准备下载记录总数(1-65535)低位字节
     OutData(10) = RecordTotal \ 256             '准备下载记录总数记录高位字节
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     '-------------------------------------------------------------------------------------------------------------------------
     l = 49                                     '=最后元素序号
     ReDim OutData(l)
     DataCards.Recordset.MoveFirst              '奇怪名称为data3时不支持
NextCardRecord:
         OutData(0) = CONCommSyn1
         OutData(1) = CONCommSyn2
         OutData(2) = CONCommSyn3
         OutData(3) = Val(Text2.Text)               '主机地址
         OutData(4) = TYPE_Itself               '本机
         OutData(5) = SMMR_LoadOneCard          '下载卡片档案一条
         OutData(6) = 42                        '参数长度,不含本身及CRC
         OutData(7) = DataCards.Recordset.AbsolutePosition Mod 256      '卡片档案序号(0-65534)低位字节
         OutData(8) = DataCards.Recordset.AbsolutePosition \ 256        '卡片档案序号(0-65534)高位字节
         s = DataCards.Recordset.Fields("卡片内码").Value
         OutData(9) = Val("&H" + Mid(s, 7, 2))
         OutData(10) = Val("&H" + Mid(s, 5, 2))
         OutData(11) = Val("&H" + Mid(s, 3, 2))
         OutData(12) = Val("&H" + Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("注册标志").Value
         OutData(13) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(13) = OutData(13) + 2 ^ (8 - m)
            End If
         Next m
         OutData(14) = DataCards.Recordset.Fields("卡片类型").Value
         s = DataCards.Recordset.Fields("止效日期").Value
         OutData(15) = Val(Mid(s, 5, 2))
         OutData(16) = Val(Mid(s, 3, 2))
         OutData(17) = Val(Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("入场权限").Value
         OutData(18) = 0
         For m = 1 To 8
            If Mid(s, 24 + m, 1) = "1" Then                             '符合低字节在前格式，最低位代表0号停车场
               OutData(18) = OutData(18) + 2 ^ (8 - m)
            End If
         Next m
         OutData(19) = 0
         For m = 1 To 8
            If Mid(s, 16 + m, 1) = "1" Then
               OutData(19) = OutData(19) + 2 ^ (8 - m)
            End If
         Next m
         OutData(20) = 0
         For m = 1 To 8
            If Mid(s, 8 + m, 1) = "1" Then
               OutData(20) = OutData(20) + 2 ^ (8 - m)
            End If
         Next m
         OutData(21) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(21) = OutData(21) + 2 ^ (8 - m)
            End If
         Next m
         s = DataCards.Recordset.Fields("卡片编号").Value
         I = Val(s) Mod 65536
         OutData(22) = I Mod 256                                        '卡片编号(0-65535)低位字节
         OutData(23) = I \ 256                                          '卡片编号(0-65535)高位字节
         s = DataCards.Recordset.Fields("车牌问候").Value
         OutData(24) = Asc(Mid(s, 1, 1))
         OutData(25) = Asc(Mid(s, 2, 1))
         OutData(26) = Asc(Mid(s, 3, 1))
         OutData(27) = Asc(Mid(s, 4, 1))
         OutData(28) = Asc(Mid(s, 5, 1))
         OutData(29) = Asc(Mid(s, 6, 1))
         OutData(30) = Asc(Mid(s, 7, 1))
         OutData(31) = Asc(Mid(s, 8, 1))
         OutData(32) = Asc(Mid(s, 9, 1))
         OutData(33) = Asc(Mid(s, 10, 1))
         s = Hex(DataCards.Recordset.Fields("储值余额").Value)
         m = Len(s)
         If m >= 2 Then
            OutData(34) = Val("&H" + Mid(s, m - 1, 2))
         Else
            OutData(34) = Val("&H" + s)
         End If
         If m >= 4 Then
            OutData(35) = Val("&H" + Mid(s, m - 3, 2))
         Else
            If m = 3 Then
                OutData(35) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(35) = 0
            End If
         End If
         If m >= 6 Then
            OutData(36) = Val("&H" + Mid(s, m - 5, 2))
         Else
            If m = 5 Then
                OutData(36) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(36) = 0
            End If
         End If
         If m >= 8 Then
            OutData(37) = Val("&H" + Mid(s, m - 5, 2))
         Else
            If m = 7 Then
                OutData(37) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(37) = 0
            End If
         End If
         s = DataCards.Recordset.Fields("停车标志").Value
         OutData(38) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(38) = OutData(38) + 2 ^ (8 - m)
            End If
         Next m
         s = DataCards.Recordset.Fields("进场时间").Value
         OutData(39) = Val(Mid(s, 11, 2))
         OutData(40) = Val(Mid(s, 9, 2))
         OutData(41) = Val(Mid(s, 7, 2))
         OutData(42) = Val(Mid(s, 5, 2))
         OutData(43) = Val(Mid(s, 3, 2))
         OutData(44) = Val(Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("停车位置").Value
         OutData(45) = Asc(Mid(s, 1, 1))
         OutData(46) = Asc(Mid(s, 2, 1))
         OutData(47) = Asc(Mid(s, 3, 1))
         OutData(48) = Asc(Mid(s, 4, 1))
         OutData(l) = 0
          For I = 3 To l - 1
         OutData(l) = OutData(l) Xor OutData(I)
         Next I
         MSComm1.Output = OutData
         DataCards.Recordset.MoveNext
     If DataCards.Recordset.AbsolutePosition < RecordTotal Then GoTo NextCardRecord
Case 1                                          '追加下载卡片档案通知
     l = 11                                     '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                   '主机地址
     OutData(4) = TYPE_Itself                   '本机
     OutData(5) = SMMR_LoadCardsPrefix          '下载卡片档案通知指令
     OutData(6) = 4                             '参数长度
     If DataCards.Recordset.RecordCount = 0 Then
        MsgBox "当前卡片档案库无数据,请先添加数据!"
       ' DataCards.Recordset.AddNew
        Exit Sub
     Else
        DataCards.Recordset.MoveLast
        DataCards.Recordset.MoveFirst
     End If
     If DataCards.Recordset.RecordCount > CONCardRecordMax Then
        MsgBox "当前卡片档案库记录总数超过了" + Str(CONCardRecordMax) + "条,请先处理数据!"
        Exit Sub
     End If
     RecordTotal = DataCards.Recordset.RecordCount
     If RecordTotal <= CONFirstLoadCardNum Then
        MsgBox "当前卡片档案库记录已全部下载了，无追加记录！"
        Exit Sub
     End If
     OutData(7) = CONFirstLoadCardNum Mod 256   '准备下载记录起始序号(0-65534)低位字节
     OutData(8) = CONFirstLoadCardNum \ 256     '准备下载记录起始序号高位字节
     OutData(9) = RecordTotal Mod 256           '准备下载记录总数(1-65535)低位字节
     OutData(10) = RecordTotal \ 256            '准备下载记录总数记录高位字节
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     '-------------------------------------------------------------------------------------------------------------------------
     l = 49                                     '=最后元素序号
     ReDim OutData(l)
     DataCards.Recordset.MoveFirst              '奇怪名称为data3时不支持
NextCardRecord1:
     DataCards.Recordset.MoveNext
     If DataCards.Recordset.AbsolutePosition < CONFirstLoadCardNum Then GoTo NextCardRecord1
NextCardRecord2:
         OutData(0) = CONCommSyn1
         OutData(1) = CONCommSyn2
         OutData(2) = CONCommSyn3
         OutData(3) = Val(Text2.Text)               '主机地址
         OutData(4) = TYPE_Itself               '本机
         OutData(5) = SMMR_LoadOneCard          '下载卡片档案一条
         OutData(6) = 42                        '参数长度,不含本身及CRC
         OutData(7) = DataCards.Recordset.AbsolutePosition Mod 256     '卡片档案序号(0-65534)低位字节
         OutData(8) = DataCards.Recordset.AbsolutePosition \ 256        '卡片档案序号(0-65534)高位字节
         s = DataCards.Recordset.Fields("卡片内码").Value
         OutData(9) = Val("&H" + Mid(s, 7, 2))
         OutData(10) = Val("&H" + Mid(s, 5, 2))
         OutData(11) = Val("&H" + Mid(s, 3, 2))
         OutData(12) = Val("&H" + Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("注册标志").Value
         OutData(13) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(13) = OutData(13) + 2 ^ (8 - m)
            End If
         Next m
         OutData(14) = DataCards.Recordset.Fields("卡片类型").Value
         s = DataCards.Recordset.Fields("止效日期").Value
         OutData(15) = Val(Mid(s, 5, 2))
         OutData(16) = Val(Mid(s, 3, 2))
         OutData(17) = Val(Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("入场权限").Value
         OutData(18) = 0
         For m = 1 To 8
            If Mid(s, 24 + m, 1) = "1" Then                             '符合低字节在前格式，最低位代表0号停车场
               OutData(18) = OutData(18) + 2 ^ (8 - m)
            End If
         Next m
         OutData(19) = 0
         For m = 1 To 8
            If Mid(s, 16 + m, 1) = "1" Then
               OutData(19) = OutData(19) + 2 ^ (8 - m)
            End If
         Next m
         OutData(20) = 0
         For m = 1 To 8
            If Mid(s, 8 + m, 1) = "1" Then
               OutData(20) = OutData(20) + 2 ^ (8 - m)
            End If
         Next m
         OutData(21) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(21) = OutData(21) + 2 ^ (8 - m)
            End If
         Next m
         s = DataCards.Recordset.Fields("卡片编号").Value
         I = Val(s) Mod 65536
         OutData(22) = I Mod 256                                        '卡片编号(0-65535)低位字节
         OutData(23) = I \ 256                                          '卡片编号(0-65535)高位字节
         s = DataCards.Recordset.Fields("车牌问候").Value
         OutData(24) = Asc(Mid(s, 1, 1))
         OutData(25) = Asc(Mid(s, 2, 1))
         OutData(26) = Asc(Mid(s, 3, 1))
         OutData(27) = Asc(Mid(s, 4, 1))
         OutData(28) = Asc(Mid(s, 5, 1))
         OutData(29) = Asc(Mid(s, 6, 1))
         OutData(30) = Asc(Mid(s, 7, 1))
         OutData(31) = Asc(Mid(s, 8, 1))
         OutData(32) = Asc(Mid(s, 9, 1))
         OutData(33) = Asc(Mid(s, 10, 1))
         s = Hex(DataCards.Recordset.Fields("储值余额").Value)
         m = Len(s)
         If m >= 2 Then
            OutData(34) = Val("&H" + Mid(s, m - 1, 2))
         Else
            OutData(34) = Val("&H" + s)
         End If
         If m >= 4 Then
            OutData(35) = Val("&H" + Mid(s, m - 3, 2))
         Else
            If m = 3 Then
                OutData(35) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(35) = 0
            End If
         End If
         If m >= 6 Then
            OutData(36) = Val("&H" + Mid(s, m - 5, 2))
         Else
            If m = 5 Then
                OutData(36) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(36) = 0
            End If
         End If
         If m >= 8 Then
            OutData(37) = Val("&H" + Mid(s, m - 5, 2))
         Else
            If m = 7 Then
                OutData(37) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(37) = 0
            End If
         End If
         s = DataCards.Recordset.Fields("停车标志").Value
         OutData(38) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(38) = OutData(38) + 2 ^ (8 - m)
            End If
         Next m
         s = DataCards.Recordset.Fields("进场时间").Value
         OutData(39) = Val(Mid(s, 11, 2))
         OutData(40) = Val(Mid(s, 9, 2))
         OutData(41) = Val(Mid(s, 7, 2))
         OutData(42) = Val(Mid(s, 5, 2))
         OutData(43) = Val(Mid(s, 3, 2))
         OutData(44) = Val(Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("停车位置").Value
         OutData(45) = Asc(Mid(s, 1, 1))
         OutData(46) = Asc(Mid(s, 2, 1))
         OutData(47) = Asc(Mid(s, 3, 1))
         OutData(48) = Asc(Mid(s, 4, 1))
         OutData(l) = 0
          For I = 3 To l - 1
         OutData(l) = OutData(l) Xor OutData(I)
         Next I
         MSComm1.Output = OutData
         DataCards.Recordset.MoveNext
     If DataCards.Recordset.EOF = False Then GoTo NextCardRecord2
Case 2                                          '修改卡片档案一条,
     '-------------------------------------------------------------------------------------------------------------------------
     l = 49                                     '=最后元素序号
     ReDim OutData(l)
     DataCards.Recordset.MoveFirst              '奇怪名称为data3时不支持
     DataCards.Recordset.MoveNext
     DataCards.Recordset.MoveNext
     DataCards.Recordset.MoveNext
         OutData(0) = CONCommSyn1
         OutData(1) = CONCommSyn2
         OutData(2) = CONCommSyn3
         OutData(3) = Val(Text2.Text)               '主机地址
         OutData(4) = TYPE_Itself               '本机
         OutData(5) = SMMR_ModifyOneCard        '修改卡片档案一条
         OutData(6) = 42                        '参数长度,不含本身及CRC
         OutData(7) = DataCards.Recordset.AbsolutePosition Mod 256      '卡片档案序号(0-65534)低位字节
         OutData(8) = DataCards.Recordset.AbsolutePosition \ 256        '卡片档案序号(0-65534)高位字节
         s = DataCards.Recordset.Fields("卡片内码").Value
         OutData(9) = Val("&H" + Mid(s, 7, 2))
         OutData(10) = Val("&H" + Mid(s, 5, 2))
         OutData(11) = Val("&H" + Mid(s, 3, 2))
         OutData(12) = Val("&H" + Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("注册标志").Value
         OutData(13) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(13) = OutData(13) + 2 ^ (8 - m)
            End If
         Next m
         OutData(14) = DataCards.Recordset.Fields("卡片类型").Value
         s = DataCards.Recordset.Fields("止效日期").Value
         OutData(15) = Val(Mid(s, 5, 2))
         OutData(16) = Val(Mid(s, 3, 2))
         OutData(17) = Val(Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("入场权限").Value
         OutData(18) = 0
         For m = 1 To 8
            If Mid(s, 24 + m, 1) = "1" Then                             '符合低字节在前格式，最低位代表0号停车场
               OutData(18) = OutData(18) + 2 ^ (8 - m)
            End If
         Next m
         OutData(19) = 0
         For m = 1 To 8
            If Mid(s, 16 + m, 1) = "1" Then
               OutData(19) = OutData(19) + 2 ^ (8 - m)
            End If
         Next m
         OutData(20) = 0
         For m = 1 To 8
            If Mid(s, 8 + m, 1) = "1" Then
               OutData(20) = OutData(20) + 2 ^ (8 - m)
            End If
         Next m
         OutData(21) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(21) = OutData(21) + 2 ^ (8 - m)
            End If
         Next m
         s = DataCards.Recordset.Fields("卡片编号").Value
         I = Val(s) Mod 65536
         OutData(22) = I Mod 256                                        '卡片编号(0-65535)低位字节
         OutData(23) = I \ 256                                          '卡片编号(0-65535)高位字节
         s = DataCards.Recordset.Fields("车牌问候").Value
         OutData(24) = Asc(Mid(s, 1, 1))
         OutData(25) = Asc(Mid(s, 2, 1))
         OutData(26) = Asc(Mid(s, 3, 1))
         OutData(27) = Asc(Mid(s, 4, 1))
         OutData(28) = Asc(Mid(s, 5, 1))
         OutData(29) = Asc(Mid(s, 6, 1))
         OutData(30) = Asc(Mid(s, 7, 1))
         OutData(31) = Asc(Mid(s, 8, 1))
         OutData(32) = Asc(Mid(s, 9, 1))
         OutData(33) = Asc(Mid(s, 10, 1))
         s = Hex(DataCards.Recordset.Fields("储值余额").Value)
         m = Len(s)
         If m >= 2 Then
            OutData(34) = Val("&H" + Mid(s, m - 1, 2))
         Else
            OutData(34) = Val("&H" + s)
         End If
         If m >= 4 Then
            OutData(35) = Val("&H" + Mid(s, m - 3, 2))
         Else
            If m = 3 Then
                OutData(35) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(35) = 0
            End If
         End If
         If m >= 6 Then
            OutData(36) = Val("&H" + Mid(s, m - 5, 2))
         Else
            If m = 5 Then
                OutData(36) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(36) = 0
            End If
         End If
         If m >= 8 Then
            OutData(37) = Val("&H" + Mid(s, m - 5, 2))
         Else
            If m = 7 Then
                OutData(37) = Val("&H" + Mid(s, 1, 1))
            Else
                OutData(37) = 0
            End If
         End If
         s = DataCards.Recordset.Fields("停车标志").Value
         OutData(38) = 0
         For m = 1 To 8
            If Mid(s, m, 1) = "1" Then
               OutData(38) = OutData(38) + 2 ^ (8 - m)
            End If
         Next m
         s = DataCards.Recordset.Fields("进场时间").Value
         OutData(39) = Val(Mid(s, 11, 2))
         OutData(40) = Val(Mid(s, 9, 2))
         OutData(41) = Val(Mid(s, 7, 2))
         OutData(42) = Val(Mid(s, 5, 2))
         OutData(43) = Val(Mid(s, 3, 2))
         OutData(44) = Val(Mid(s, 1, 2))
         s = DataCards.Recordset.Fields("停车位置").Value
         OutData(45) = Asc(Mid(s, 1, 1))
         OutData(46) = Asc(Mid(s, 2, 1))
         OutData(47) = Asc(Mid(s, 3, 1))
         OutData(48) = Asc(Mid(s, 4, 1))
         OutData(l) = 0
          For I = 3 To l - 1
         OutData(l) = OutData(l) Xor OutData(I)
         Next I
         MSComm1.Output = OutData
Case 3                                      '提取卡片档案申请
     l = 7                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)        '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = SMMR_FetchCardsRequest    '提取完整卡片档案申请
     OutData(6) = 0                         '参数长度
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 9                                          '自动模式
     l = 8                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = 7 'ADDR_Myself                   'RS232本机地址
     OutData(4) = TYPE_Itself                   '接收本机
     OutData(5) = SMMR_LinkMode                 '模式切换联机/脱机
     OutData(6) = 1                             '参数长度
     OutData(7) = 0                             '自动模式
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 10                                         '发行模式
     l = 8                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself                   'RS232本机地址
     OutData(4) = TYPE_Itself                   '接收本机
     OutData(5) = SMMR_LinkMode                 '模式切换联机/脱机
     OutData(6) = 1                             '参数长度
     OutData(7) = 3                             '4=发行模式 3=实时模式
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData


Case 11                                         '蜂鸣器响
     l = 8                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                   'RS232本机地址
     OutData(4) = TYPE_Itself                   '接收本机
     OutData(5) = COMR_SoftReset
     OutData(6) = 1                             '参数长度
     OutData(7) = 1                             '参数
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 12                                         '在线查询
     l = 8                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself                 '0号入口通道控制器
     OutData(4) = TYPE_Itself                   '接收本机
     OutData(5) = COMR_SoftReset
     OutData(6) = 1                             '参数长度
     OutData(7) = 5                            '响2声
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 14                                         '总线广播复位
     l = 8                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Broadcast                'CAN总线广播地址
     OutData(4) = TYPE_Itself                   '本机
     OutData(5) = COMR_SoftReset
     OutData(6) = 1                             '参数长度
     OutData(7) = 1                             '参数
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 15                                         '主板直通显示:Hellow hyn
     l = 17                                      '=最后元素序号
     S1 = Text3.Text
     l1 = 0
     For I = 1 To Len(S1)
       s = Mid(S1, I, 1)
       If (s > Chr(127)) Then
           l1 = l1 + 2
       Else
           l1 = l1 + 1
       End If
     Next I
     l = l1 + 7
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = 97 'ADDR_Broadcast                'CAN总线广播地址
'     OutData(4) = TYPE_Itself                   '本机
     OutData(4) = 0 'ADDR_LedScreen
'     OutData(4) = TYPE_Itself                   '本机
     OutData(5) = LEDR_Display
     OutData(6) = l1                            '参数长度
     k = 7
     For I = 1 To Len(S1)
       s = Mid(S1, I, 1)
       If (s > Chr(127)) Then
          OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 1, 2))
          k = k + 1
          OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 3, 2))
       Else
          OutData(k) = Asc(s)
       End If
          k = k + 1
     Next I
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData

Case 16                                         '保存显示:
     S1 = Text3.Text
     l1 = 0
     For I = 1 To Len(S1)
       s = Mid(S1, I, 1)
       If (s > Chr(127)) Then
           l1 = l1 + 2
       Else
           l1 = l1 + 1
       End If
     Next I
     l = l1 + 7 + 1
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Broadcast                'CAN总线广播地址
'     OutData(4) = TYPE_Itself                   '本机
     OutData(4) = ADDR_LedScreen
'     OutData(4) = TYPE_Itself                   '本机
     OutData(5) = LEDR_StoreSentance
     OutData(6) = l1 + 1                        '参数长度
     OutData(7) = 0                             '保存语句号
     k = 8
     For I = 1 To Len(S1)
       s = Mid(S1, I, 1)
       If (s > Chr(127)) Then
          OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 1, 2))
          k = k + 1
          OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 3, 2))
       Else
          OutData(k) = Asc(s)
       End If
          k = k + 1
     Next I
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData

Case 17                                         '时钟下载
     l = 15                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text) 'ADDR_Broadcast '   '车场通道控制器
     OutData(4) = TYPE_Itself  'ADDR_Broadcast '   '本机
     OutData(5) = COMR_SetDateTime
     OutData(6) = 8                             '参数长度
     t = Now
     OutData(7) = Second(t)
     OutData(8) = Minute(t)
     OutData(9) = Hour(t)
     OutData(10) = Day(t)
     OutData(11) = Month(t)
     OutData(12) = Year(t) - 2000
     OutData(13) = Weekday(t)                '1~7=星期日~六
     OutData(14) = 2                    '显示模式1=中文滚动时间，2=只显时间，3=英文日期时间，4=中文日期时间，5=中英文日期时间
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 18                                         '广告发布 保存显示:
     S1 = Text3.Text
     l1 = 0
     For I = 1 To Len(S1)
       s = Mid(S1, I, 1)
       If (s > Chr(127)) Then
           l1 = l1 + 2
       Else
           l1 = l1 + 1
       End If
     Next I
     l = l1 + 7 + 1
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Broadcast                'CAN总线广播地址
'     OutData(4) = TYPE_Itself                   '本机
     OutData(4) = 111 'ADDR_LedScreen+
'     OutData(4) = TYPE_Itself                   '本机
     OutData(5) = LEDR_StoreSentance
     OutData(6) = l1 + 1                        '参数长度
     OutData(7) = 0                             '保存语句号
     k = 8
     For I = 1 To Len(S1)
       s = Mid(S1, I, 1)
       If (s > Chr(127)) Then
          OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 1, 2))
          k = k + 1
          OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 3, 2))
       Else
          OutData(k) = Asc(s)
       End If
          k = k + 1
     Next I
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData


Case 19                                         '升闸自落
     l = 9                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text) 'ADDR_Broadcast
     OutData(4) = TYPE_Itself 'ADDR_AutoBarrier + 1            '本机
     OutData(5) = ABMR_Operation
     OutData(6) = 2                            '参数长度
     OutData(7) = Asc("u")
     OutData(8) = 2
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData

Case 20                                         '落闸
     l = 9                                      '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text) 'ADDR_Broadcast
     OutData(4) = TYPE_Itself 'ADDR_AutoBarrier + 1            '本机
     OutData(5) = ABMR_Operation
     OutData(6) = 2                            '参数长度
     OutData(7) = Asc("d")
     OutData(8) = Asc("w")
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData

Case 29                                          '等待事件有效
     l = 10                                     '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself
     OutData(4) = TYPE_Itself      '本机
     OutData(5) = SMMR_EventValid
     OutData(6) = 3                             '参数长度
     OutData(7) = 1                             '通道地址
     OutData(8) = 88                            '操作员编号
     OutData(9) = 0                             '入口有效
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData

Case 45 '新收费费率下载
    
     s = Text3.Text
     Set DatabaseTran = DBEngine.Workspaces(0).OpenDatabase(s)
     Set RecordsetTran = DatabaseTran.OpenRecordset("数据表", dbOpenSnapshot) 'source,type=1Table数据表用于在数据服务器中直接修改,2=Dynaset动态集将数据读入内存中快速操作,4=Snapshot快照在内存中查询不能修改

     If RecordsetTran.RecordCount = 0 Then
        MsgBox "当前停车费率表无数据,请先添加数据!"
        Exit Sub
     Else
        RecordsetTran.MoveLast
        RecordsetTran.MoveFirst
     End If
     If RecordsetTran.RecordCount < 8 Then
        MsgBox "当前停车费率表数据不足" + Str(TimeTotal) + "条,请先处理数据!"
        Exit Sub
     End If
     '--------------------------------------------------下载Holiday选项及列表-----------------------------------------------------------------------------
     DataLen = 243
     Page = &HD0 - &HC0                                 '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                       '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     p = 8
     
     '-------------1字节，单字节二进制选项
     RecordsetTran.FindFirst "Index=101"                '头条记录为空格
     For a = 0 To 0                                     '仅一个字节
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Holiday").Value
         If IsNull(s) Then s = "0"
         ls = Len(s)
         If ls > 8 Then                                 '删除左边超长位
            s = Mid(s, ls - 7, 8)
            ls = 8
         End If
         I = 0
         For m = 0 To ls - 1                            '可以少于8位
             If Mid(s, ls - m, 1) = "1" Then
                I = I + 2 ^ m
             End If
         Next m
         OutData(p + a) = I
     Next a
     p = p + 1
     
     '-------------15字节，单字节选项                   '无空格
     For a = 0 To 14
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Holiday").Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     p = p + 15
     
     '------------64字节，32个双字节日期:MM-DD
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     For a = 0 To 31                                    '2字节参数
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Holiday").Value
         If IsNull(s) Then s = "0"
         OutData(p) = Val(Mid(s, 4, 2))                 'MM-DD
         OutData(p + 1) = Val(Mid(s, 1, 2))
         p = p + 2
     Next a
     
     '------------162字节，54个3字节日期:MM-DD
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     For a = 0 To 53                                    '3字节参数
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Holiday").Value
         If IsNull(s) Then s = "0"
         OutData(p) = Val(Mid(s, 9, 2))                 '列表日期:YYYY-MM-DD
         OutData(p + 1) = Val(Mid(s, 6, 2))
         OutData(p + 2) = Val(Mid(s, 3, 2))
         p = p + 3
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
    '---------------------------------------------------下载Comsuption参数表-----------------------------------------------------------------------------
     '--------------------------------------------------下载Toll选项及列表-----------------------------------------------------------------------------
     RecordsetTran.FindFirst "Index=103"                '头条记录为空格
     s = RecordsetTran.Fields("Toll").Value
     If IsNull(s) Then s = "0"
     DecimalDigits = Val(s)
     If DecimalDigits > 2 Then DecimalDigits = 2       '限制两位小数
    

     DataLen = 161
     Page = &HD2 - &HC0                                 '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                       '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     p = 8
     
     '-------------16字节，单字节选项                   '无空格
     RecordsetTran.FindFirst "Index=101"                '头条记录为空格
     For a = 0 To 15
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Toll").Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     p = p + 16
     
     '------------16字节-------------------
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     For a = 0 To 1                                     '仅2个双字节二进制
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Toll").Value
         If IsNull(s) Then s = "0"
         ls = Len(s)
         If ls > 16 Then                                 '删除左边超长位
            s = Mid(s, ls - 15, 16)
            ls = 16
         End If
         I = 0
         For m = 0 To ls - 1                            '可以少于16位
             If Mid(s, ls - m, 1) = "1" Then
                I = I + 2 ^ m
             End If
         Next m
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2
     Next a
     For a = 0 To 5                                     '6个备用双字节参数
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Toll").Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2
     Next a
     
     '-------------8X(4+4)X2=128字节，8组4个入4个出双字节二进制
     For a = 0 To 7
         RecordsetTran.MoveNext                              '空两格
         RecordsetTran.MoveNext
         
         For b = 0 To 3
             RecordsetTran.MoveNext
             s = RecordsetTran.Fields("Toll").Value
             If IsNull(s) Then s = "0"
             ls = Len(s)
             If ls > 16 Then                                 '删除左边超长位
                s = Mid(s, ls - 15, 16)
                ls = 16
             End If
             I = 0
             For m = 0 To ls - 1                             '可以少于16位
                 If Mid(s, ls - m, 1) = "1" Then
                    I = I + 2 ^ m
                 End If
             Next m
             OutData(p) = I Mod 256
             OutData(p + 1) = I \ 256
             p = p + 2
         Next b
         RecordsetTran.MoveNext                              '空1格
         For b = 0 To 3
             RecordsetTran.MoveNext
             s = RecordsetTran.Fields("Toll").Value
             If IsNull(s) Then s = "0"
             ls = Len(s)
             If ls > 16 Then                                 '删除左边超长位
                s = Mid(s, ls - 15, 16)
                ls = 16
             End If
             I = 0
             For m = 0 To ls - 1                             '可以少于16位
                 If Mid(s, ls - m, 1) = "1" Then
                    I = I + 2 ^ m
                 End If
             Next m
             OutData(p) = I Mod 256
             OutData(p + 1) = I \ 256
             p = p + 2
         Next b
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
    '---------------------------------------------------下载Category0-15各种车型收费表-------------------------------------------------------------------
     Dim FieldName(16) As String
     FieldName(0) = "Category0"
     FieldName(1) = "Category1"
     FieldName(2) = "Category2"
     FieldName(3) = "Category3"
     FieldName(4) = "Category4"
     FieldName(5) = "Category5"
     FieldName(6) = "Category6"
     FieldName(7) = "Category7"
     FieldName(8) = "Category8"
     FieldName(9) = "Category9"
     FieldName(10) = "Category10"
     FieldName(11) = "Category11"
     FieldName(12) = "Category12"
     FieldName(13) = "Category13"
     FieldName(14) = "Category14"
     FieldName(15) = "Category15"
     
 For Category = 0 To 15
     DataLen = 240
     Page = &HD3 - &HC0 + Category                      '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     '-------------16字节，单字节选项-------------------
     RecordsetTran.FindFirst "Index=101"                '头条记录为空格
     p = 8
     For a = 0 To 15
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     p = p + 16
     
     '-------------以下为公共收费参数-------------------
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     
     RecordsetTran.MoveNext                             '1字节无入场记录收费模式
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     OutData(p) = Val(s)
     p = p + 1
     
     RecordsetTran.MoveNext                             '无入场记录按上次出场时间计算免费时间(分钟)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '无入场记录按次收费收费金额(元)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     I = I * 10 ^ DecimalDigits                         '收费金额(小数->整数元)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '备用1字节
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     OutData(p) = Val(s)
     p = p + 1
     
     RecordsetTran.MoveNext                             '公用的入场停车免费时间(分钟)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '公用的过点免费零头时间(分钟)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '中央收费允许超时时间(分钟)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '公用的周期限额计费时间(小时)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s) * 60
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '公用的周期限额计费金额(元)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     I = I * 10 ^ DecimalDigits                         '收费金额(小数->整数元)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     
     '---------不分时段普通收费参数---------------------
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     For a = 0 To 3                                     '普通收费模式等4个单字节
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     p = p + 4
    
     RecordsetTran.MoveNext                             '入场收费计费时间(分钟)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
    
     RecordsetTran.MoveNext                             '入场收费收费金额(元)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     I = I * 10 ^ DecimalDigits                         '收费金额(小数->整数元)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '单位时间收费：计费时间(分钟)
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
    
     RecordsetTran.MoveNext                             '单位时间收费金额(元 )
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     I = I * 10 ^ DecimalDigits                         '收费金额(小数->整数元)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '备用双字节1
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext                             '备用双字节2
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     '---------不规则收费参数--------------------------
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     OutData(p) = Val(s)
     p = p + 1
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(Mid(s, 1, 2)) * 60 + Val(Mid(s, 4, 2))     'HH:MM
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     OutData(p) = Val(s)
     p = p + 1
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields(FieldName(Category)).Value
     If IsNull(s) Then s = "0"
     I = Val(s)
     OutData(p) = I Mod 256
     OutData(p + 1) = I \ 256
     p = p + 2
     
     For a = 0 To 9 Step 2
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         OutData(p + a) = I Mod 256
         OutData(p + a + 1) = I \ 256
     Next a
     p = p + 10
     
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     
     For a = 0 To 7                                     '8个单字节
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     p = p + 8
     
     '---------5个19字节----------------------
         RecordsetTran.MoveNext                             '空1格
     For a = 0 To 4
         RecordsetTran.MoveNext                             '空1格
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(Mid(s, 1, 2)) * 60 + Val(Mid(s, 4, 2))     '(HH:MM-HH:MM)
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         I = Val(Mid(s, 7, 2)) * 60 + Val(Mid(s, 10, 2))    '(HH:MM-HH:MM)
         OutData(p + 2) = I Mod 256
         OutData(p + 3) = I \ 256
         p = p + 4
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(Mid(s, 1, 2)) * 60 + Val(Mid(s, 4, 2))     '(HH:MM-HH:MM)
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         I = Val(Mid(s, 7, 2)) * 60 + Val(Mid(s, 10, 2))    '(HH:MM-HH:MM)
         OutData(p + 2) = I Mod 256
         OutData(p + 3) = I \ 256
         p = p + 4
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         I = I * 10 ^ DecimalDigits
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2

         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         OutData(p) = Val(s)
         p = p + 1
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         I = I * 10 ^ DecimalDigits
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2

         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         I = I * 10 ^ DecimalDigits
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2
     Next a
     
     '---------18个4字节的收费列表--------------------------
         RecordsetTran.MoveNext                             '空两格
         RecordsetTran.MoveNext
     For a = 0 To 17
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s) * 60
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields(FieldName(Category)).Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         I = I * 10 ^ DecimalDigits
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         p = p + 2
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
Next Category

Case 46 '车位上下限下载含余位
     '----------------------------------------------------下载Vacant余位变量-----------------------------------------------------------------------------
    
     s = Text3.Text
     Set DatabaseTran = DBEngine.Workspaces(0).OpenDatabase(s)
     Set RecordsetTran = DatabaseTran.OpenRecordset("数据表", dbOpenSnapshot) '在此打开可自动更新
     
     RecordsetTran.FindFirst "Index=100"               '分场数量
     s = RecordsetTran.Fields("VacantLimit").Value
     If IsNull(s) Then s = "0"
     ParkAmount = Val(s)
     DataLen = 2 * ParkAmount
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_SetVacant                        '下载各车场剩余车位数表,参数为：总车场剩余车位数[2]+1号车场剩余车位数[2]+2号车场剩余车位数[2]+...
     OutData(6) = DataLen                               '参数长度
     
     
     For a = 0 To ParkAmount - 1
         s = RecordsetTran.Fields("Vacant").Value
         If IsNull(s) Then s = "0"
         I = Val(s)                                    '剩余车位
         OutData(7 + a * 2) = I Mod 256
         OutData(8 + a * 2) = I \ 256
         RecordsetTran.MoveNext
     Next a
     
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
     

    
    '---------------------------------------------------下载VacantLimit车位上下限参数表-----------------------------------------------------------------------------
     DataLen = 3 + 4 * (ParkAmount + 1)
     Page = 9                                           '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     RecordsetTran.FindFirst "Index=100"                '头条记录
     s = RecordsetTran.Fields("VacantLimit").Value
     If IsNull(s) Then s = "0"
     OutData(8) = Val(s)                                '分车场数量(0-11)

     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("VacantLimit").Value
     If IsNull(s) Then s = "0"
     OutData(9) = Val(s)                                '备用1字节
     
     For a = 0 To ParkAmount
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("VacantLimit").Value
         If IsNull(s) Then s = "0"
         I = Val(s)                                     '车位上限
         OutData(10 + a * 4) = I Mod 256
         OutData(11 + a * 4) = I \ 256
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("VacantLimit").Value
         If IsNull(s) Then s = "0"
         Dim Int1 As Integer
         Int1 = Val(s)                                  '车位下限
         If Int1 >= 0 Then
             OutData(12 + a * 4) = Int1 Mod 256
             OutData(13 + a * 4) = Int1 \ 256
         Else
             s = Hex(Int1)                                  'Int1<0 ,S长度=4
             OutData(12 + a * 4) = Val("&H" + Mid(s, 3, 2))
             OutData(13 + a * 4) = Val("&H" + Mid(s, 1, 2))
         End If
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
    
    '---------------------------------------------------下载VacantText余位字符-----------------------------------------------------------------------------
     DataLen = 5 + 20 * (ParkAmount + 1)
     Page = 10                                         '下载页号

     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                       '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     RecordsetTran.FindFirst "Index=100"                '头条记录
     s = RecordsetTran.Fields("VacantText").Value
     If IsNull(s) Then s = "0"
     OutData(8) = Val(s)                                '余位字符长度

     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("VacantText").Value
     If IsNull(s) Then s = "0"
     OutData(9) = Val(s)                                '余位数字位数
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("VacantText").Value
     If IsNull(s) Then s = "0"
     OutData(10) = Val(s)                               '车位屏数量模式
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("VacantText").Value
     If IsNull(s) Then s = "0"
     OutData(11) = Val(s)                               '数字显示模式
     
     For a = 0 To ParkAmount
         RecordsetTran.MoveNext
         S1 = RecordsetTran.Fields("VacantText").Value
         If IsNull(S1) Then s = "No Text"
         k = 12 + 20 * a
         For I = 0 To 19
             OutData(k + I) = Asc(" ")                  '初始化为20个空格
         Next I
         For I = 1 To Len(S1)                           '字符超长没关系,下一循环又被覆盖
             s = Mid(S1, I, 1)
             If (s > Chr(127)) Then
                 OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 1, 2))
                 k = k + 1
                 OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 3, 2))
             Else
                 OutData(k) = Asc(s)
             End If
             k = k + 1
         Next I
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
    '---------------------------------------------------下载ParkFullText满位字符-----------------------------------------------------------------------------
     DataLen = 5 + 20 * (ParkAmount + 1)
     Page = 11                                         '下载页号

     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     RecordsetTran.FindFirst "Index=100"                '头条记录
     s = RecordsetTran.Fields("ParkFullText").Value
     If IsNull(s) Then s = "0"
     OutData(8) = Val(s)                                '满位字符长度

     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("ParkFullText").Value
     If IsNull(s) Then s = "0"
     OutData(9) = Val(s)                                '备用1字节
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("ParkFullText").Value
     If IsNull(s) Then s = "0"
     OutData(10) = Val(s)                               '备用1字节
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("ParkFullText").Value
     If IsNull(s) Then s = "0"
     OutData(11) = Val(s)                               '满位显示模式
     
     For a = 0 To ParkAmount
         RecordsetTran.MoveNext
         S1 = RecordsetTran.Fields("ParkFullText").Value
         If IsNull(S1) Then s = "No Text"
         k = 12 + 20 * a
         For I = 0 To 19
             OutData(k + I) = Asc(" ")                  '初始化为20个空格
         Next I
         For I = 1 To Len(S1)                           '字符超长没关系,下一循环又被覆盖
             s = Mid(S1, I, 1)
             If (s > Chr(127)) Then
                 OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 1, 2))
                 k = k + 1
                 OutData(k) = Val("&H" + Mid(Hex(Asc(s)), 3, 2))
             Else
                 OutData(k) = Asc(s)
             End If
             k = k + 1
         Next I
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep

    '---------------------------------------------------下载DirectionVD参数表-----------------------------------------------------------------------------
     RecordsetTran.FindFirst "Index=100"                '头条记录
     s = RecordsetTran.Fields("DirectionVD").Value
     If IsNull(s) Then s = "0"
     VDAmount = Val(s)
     DataLen = 3 + VDAmount
     Page = 36                                          '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     OutData(8) = VDAmount                              'VD数量

     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("DirectionVD").Value
     If IsNull(s) Then s = "0"
     OutData(9) = Val(s)                                '备用1字节
     
     For a = 0 To VDAmount - 1
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("DirectionVD").Value
         If IsNull(s) Then s = "255"
         I = Val(s)                                     '联动地感
         OutData(10 + a) = I
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
    
    '---------------------------------------------------下载EnterArea参数表-----------------------------------------------------------------------------
     DataLen = 3 + VDAmount
     Page = 37                                          '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号

     RecordsetTran.FindFirst "Index=100"                '头条记录
     s = RecordsetTran.Fields("EnterArea").Value
     If IsNull(s) Then s = "0"
     OutData(8) = Val(s)                                '备用1字节
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("EnterArea").Value
     If IsNull(s) Then s = "0"
     OutData(9) = Val(s)                                '备用1字节
     
     For a = 0 To VDAmount - 1
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("EnterArea").Value
         If IsNull(s) Then s = "0"
         I = Val(s)                                     '联动地感
         OutData(10 + a) = I
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
    
    '---------------------------------------------------下载LeaveArea参数表-----------------------------------------------------------------------------
     DataLen = 3 + VDAmount
     Page = 38                                          '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号

     RecordsetTran.FindFirst "Index=100"                '头条记录
     s = RecordsetTran.Fields("LeaveArea").Value
     If IsNull(s) Then s = "0"
     OutData(8) = Val(s)                                '备用1字节
     
     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("LeaveArea").Value
     If IsNull(s) Then s = "0"
     OutData(9) = Val(s)                                '备用1字节
     
     For a = 0 To VDAmount - 1
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("LeaveArea").Value
         If IsNull(s) Then s = "0"
         I = Val(s)                                     '联动地感
         OutData(10 + a) = I
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep

Case 47 '余位下载
     '----------------------------------------------------下载Vacant余位变量-----------------------------------------------------------------------------
    
     s = Text3.Text
     Set DatabaseTran = DBEngine.Workspaces(0).OpenDatabase(s)
     Set RecordsetTran = DatabaseTran.OpenRecordset("数据表", dbOpenSnapshot) '在此打开可自动更新
     
     RecordsetTran.FindFirst "Index=100"               '分场数量
     s = RecordsetTran.Fields("VacantLimit").Value
     If IsNull(s) Then s = "0"
     ParkAmount = Val(s)
     DataLen = 2 * ParkAmount
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_SetVacant                        '下载各车场剩余车位数表,参数为：总车场剩余车位数[2]+1号车场剩余车位数[2]+2号车场剩余车位数[2]+...
     OutData(6) = DataLen                               '参数长度
     
     
     For a = 0 To ParkAmount - 1
         s = RecordsetTran.Fields("Vacant").Value
         If IsNull(s) Then s = "0"
         I = Val(s)                                    '剩余车位
         OutData(7 + a * 2) = I Mod 256
         OutData(8 + a * 2) = I \ 256
         RecordsetTran.MoveNext
     Next a
     
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep

    '---------------------------------------------------下载VacantLimit车位上下限参数表-----------------------------------------------------------------------------
     DataLen = 3 + 4 * (ParkAmount + 1)
     Page = 9                                           '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     RecordsetTran.FindFirst "Index=100"                '头条记录
     s = RecordsetTran.Fields("VacantLimit").Value
     If IsNull(s) Then s = "0"
     OutData(8) = Val(s)                                '分车场数量(0-11)

     RecordsetTran.MoveNext
     s = RecordsetTran.Fields("VacantLimit").Value
     If IsNull(s) Then s = "0"
     OutData(9) = Val(s)                                '备用1字节
     
     For a = 0 To ParkAmount
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("VacantLimit").Value
         If IsNull(s) Then s = "0"
         I = Val(s)                                     '车位上限
         OutData(10 + a * 4) = I Mod 256
         OutData(11 + a * 4) = I \ 256
         
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("VacantLimit").Value
         If IsNull(s) Then s = "0"
         Int1 = Val(s)                                  '车位下限
         If Int1 >= 0 Then
             OutData(12 + a * 4) = Int1 Mod 256
             OutData(13 + a * 4) = Int1 \ 256
         Else
             s = Hex(Int1)                                  'Int1<0 ,S长度=4
             OutData(12 + a * 4) = Val("&H" + Mid(s, 3, 2))
             OutData(13 + a * 4) = Val("&H" + Mid(s, 1, 2))
         End If
     Next a
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
     
Case 48 '下载用户系统设置
     '----------------------------------------------------下载Board变量-----------------------------------------------------------------------------
    
     s = Text3.Text
     Set DatabaseTran = DBEngine.Workspaces(0).OpenDatabase(s)
     Set RecordsetTran = DatabaseTran.OpenRecordset("数据表", dbOpenSnapshot) '在此打开可自动更新
     
     DataLen = 129
     Page = &HCE - &HC0                                 '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                       '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     '-------------16字节，单字节二进制板上设备选项
     RecordsetTran.FindFirst "Index=101"                '头条记录为空格
     p = 8
     For a = 0 To 15
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Board").Value
         If IsNull(s) Then s = "0"
         ls = Len(s)
         If ls > 8 Then                                  '删除左边超长位
            s = Mid(s, ls - 7, 8)
            ls = 8
         End If
         I = 0
         For m = 0 To ls - 1                             '可以少于8位
             If Mid(s, ls - m, 1) = "1" Then
                I = I + 2 ^ m
             End If
         Next m
         OutData(p + a) = I
     Next a
     
     '-------------16字节，单字节二进制功能杂项变通选项
     RecordsetTran.MoveNext                              '空两格
     RecordsetTran.MoveNext
     p = p + 16
     For a = 0 To 15
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Board").Value
         If IsNull(s) Then s = "0"
         ls = Len(s)
         If ls > 8 Then                                  '删除左边超长位
            s = Mid(s, ls - 7, 8)
            ls = 8
         End If
         I = 0
         For m = 0 To ls - 1                             '可以少于8位
             If Mid(s, ls - m, 1) = "1" Then
                I = I + 2 ^ m
             End If
         Next m
         OutData(p + a) = I
     Next a
     
     '-------------16字节，单字节100ms选项
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     p = p + 16
     For a = 0 To 15
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Board").Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     
     '------------16字节，双字节参数
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     p = p + 16
     For a = 0 To 15 Step 2                             '2字节参数
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Board").Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         OutData(p + a) = I Mod 256
         OutData(p + 1 + a) = I \ 256
     Next a
     
     '------------32字节，四字节时段参数选项
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     p = p + 16
     For a = 0 To 31 Step 4                             '8个4字节参数
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Board").Value
         If IsNull(s) Then s = "0"
         I = Val(Mid(s, 1, 2)) * 60 + Val(Mid(s, 4, 2)) '开始-结束时间 HH:MM-HH:MM
         OutData(p + a) = I Mod 256
         OutData(p + 1 + a) = I \ 256
         I = Val(Mid(s, 7, 2)) * 60 + Val(Mid(s, 10, 2))
         OutData(p + 2 + a) = I Mod 256
         OutData(p + 3 + a) = I \ 256
     Next a
     
     '-------------32字节，双字节二进制开闸模式
     RecordsetTran.MoveNext                              '空两格
     RecordsetTran.MoveNext
     p = p + 32
     For a = 0 To 31 Step 2                              '2字节参数
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Board").Value
         If IsNull(s) Then s = "0"
         ls = Len(s)
         If ls > 16 Then                                  '删除左边超长位
            s = Mid(s, ls - 15, 16)
            ls = 16
         End If
         I = 0
         For m = 0 To ls - 1                             '可以少于16位
             If Mid(s, ls - m, 1) = "1" Then
                I = I + 2 ^ m
             End If
         Next m
         OutData(p + a) = I Mod 256
         OutData(p + 1 + a) = I \ 256
     Next a

     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
     
     '----------------------------------------------------下载Manage变量-----------------------------------------------------------------------------
     DataLen = 113
     Page = &HCF - &HC0                                 '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                       '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     '-------------16字节，单字节选项-------
     RecordsetTran.FindFirst "Index=101"                '头条记录为空格
     p = 8
     For a = 0 To 15
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Manage").Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     
     '------------16字节，8个双字节参数
     RecordsetTran.MoveNext                             '空两格
     RecordsetTran.MoveNext
     p = p + 16
     For a = 0 To 15 Step 2                             '2字节参数
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Manage").Value
         If IsNull(s) Then s = "0"
         I = Val(s)
         OutData(p + a) = I Mod 256
         OutData(p + 1 + a) = I \ 256
     Next a
     
     RecordsetTran.MoveNext
     RecordsetTran.MoveNext
     RecordsetTran.MoveNext
     RecordsetTran.MoveNext
     RecordsetTran.MoveNext
     RecordsetTran.MoveNext
     RecordsetTran.MoveNext
     RecordsetTran.MoveNext
     
     '-------------16字节5组，单字节二进制16类卡通用/入口/出口/内场入口/内场出口选项
     p = p + 16 - 16                                         '下面又加16了
     For b = 0 To 3
         RecordsetTran.MoveNext                              '空两格
         RecordsetTran.MoveNext
         p = p + 16
         For a = 0 To 15
             RecordsetTran.MoveNext
             s = RecordsetTran.Fields("Manage").Value
             If IsNull(s) Then s = "0"
             ls = Len(s)
             If ls > 8 Then                                  '删除左边超长位
                s = Mid(s, ls - 7, 8)
                ls = 8
             End If
             I = 0
             For m = 0 To ls - 1                             '可以少于8位
                 If Mid(s, ls - m, 1) = "1" Then
                    I = I + 2 ^ m
                 End If
             Next m
             OutData(p + a) = I
         Next a
     Next b
    
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
    
     '----------------------------------------------------下载Access变量-----------------------------------------------------------------------------
     DataLen = 255 - 12                                 '=255 ,虽然RXBuf=512，但同时接收两个表时会溢出
     Page = &HCC - &HC0                                 '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                       '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     '-------------26字节，单字节选项-------
     RecordsetTran.FindFirst "Index=101"                '头条记录为空格
     p = 8
     For a = 0 To 25
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Access").Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     
     p = p + 26 - 12
     '-------------12字节18组，双字节二进制开闸模式
     For b = 0 To 17 '18
         RecordsetTran.MoveNext                              '空两格
         RecordsetTran.MoveNext
         p = p + 12
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("Access").Value
         If IsNull(s) Then s = "0"
         I = Val(Mid(s, 1, 2)) * 60 + Val(Mid(s, 4, 2))     '开始-结束时间 HH:MM-HH:MM
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         I = Val(Mid(s, 7, 2)) * 60 + Val(Mid(s, 10, 2))
         OutData(p + 2) = I Mod 256
         OutData(p + 3) = I \ 256
         For a = 0 To 7 Step 2                               '4个2字节参数
             RecordsetTran.MoveNext
             s = RecordsetTran.Fields("Access").Value
             If IsNull(s) Then s = "0"
             ls = Len(s)
             If ls > 16 Then                                  '删除左边超长位
                s = Mid(s, ls - 15, 16)
                ls = 16
             End If
             I = 0
             For m = 0 To ls - 1                             '可以少于16位
                 If Mid(s, ls - m, 1) = "1" Then
                    I = I + 2 ^ m
                 End If
             Next m
             OutData(p + 4 + a) = I Mod 256
             OutData(p + 4 + a + 1) = I \ 256
         Next a
     Next b
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep
     '----------------------------------------------------下载HolidayAccess变量-----------------------------------------------------------------------------
     DataLen = 255 - 12                                 '=255 ,虽然RXBuf=512，但同时接收两个表时会溢出
     Page = &HCD - &HC0                                 '下载页号
     
     l = 7 + DataLen                                    '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                       '主机地址
     OutData(4) = TYPE_Itself                           '本机
     OutData(5) = SMMR_WriteParameterTable              '写入各种参数表 1字节表序号+表参数。
     OutData(6) = DataLen                               '参数长度
     OutData(7) = Page                                  '下载页号
     
     '-------------26字节，单字节选项-------
     RecordsetTran.FindFirst "Index=101"                '头条记录为空格
     p = 8
     For a = 0 To 25
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("HolidayAccess").Value
         If IsNull(s) Then s = "0"
         OutData(p + a) = Val(s)
     Next a
     
     p = p + 26 - 12
     '-------------12字节18组，双字节二进制开闸模式
     For b = 0 To 17 '18
         RecordsetTran.MoveNext                              '空两格
         RecordsetTran.MoveNext
         p = p + 12
         RecordsetTran.MoveNext
         s = RecordsetTran.Fields("HolidayAccess").Value
         If IsNull(s) Then s = "0"
         I = Val(Mid(s, 1, 2)) * 60 + Val(Mid(s, 4, 2))     '开始-结束时间 HH:MM-HH:MM
         OutData(p) = I Mod 256
         OutData(p + 1) = I \ 256
         I = Val(Mid(s, 7, 2)) * 60 + Val(Mid(s, 10, 2))
         OutData(p + 2) = I Mod 256
         OutData(p + 3) = I \ 256
         For a = 0 To 7 Step 2                               '4个2字节参数
             RecordsetTran.MoveNext
             s = RecordsetTran.Fields("HolidayAccess").Value
             If IsNull(s) Then s = "0"
             ls = Len(s)
             If ls > 16 Then                                  '删除左边超长位
                s = Mid(s, ls - 15, 16)
                ls = 16
             End If
             I = 0
             For m = 0 To ls - 1                             '可以少于16位
                 If Mid(s, ls - m, 1) = "1" Then
                    I = I + 2 ^ m
                 End If
             Next m
             OutData(p + 4 + a) = I Mod 256
             OutData(p + 4 + a + 1) = I \ 256
         Next a
     Next b
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     Beep

Case 49
     CommonDialog1.Filter = "Access Files (*.mdb)|*.mdb"
     CommonDialog1.FilterIndex = 1      ' Specify default filter.
     CommonDialog1.ShowOpen             ' Display the File Open dialog box.
     Text3.Text = CommonDialog1.FileName

Case 50                                     '开关发卡机+1字节参数;0=关闭;1=启用
     l = 8                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = CPMR_Lock                 '开关发卡机 0xA0
     OutData(6) = 1                         '参数长度
     Volume = Volume + 1
     If Volume >= 2 Then Volume = 0
     OutData(7) = Volume
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData

Case 51                                     '音量+1字节参数;0=关闭;1=原始保真音量;2=X2加大一倍;3=X4再加大一倍。
     l = 7                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = Val(Text2.Text)                           '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = CPMR_Cardout              ' 0xA1出卡机出卡指令
     OutData(6) = 0                         '参数长度
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
     

End Select
End Sub

Private Sub Tran2_Click(Index As Integer)
Dim OutData() As Byte
If MSComm1.PortOpen = False Then MSComm1.PortOpen = True
Select Case Index
Case 0                                       '停止寻卡
     l = 8                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself               '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = &H84                      'MFRWR_ReadWrite
     OutData(6) = 1                        '参数长度
     OutData(7) = &H0                       'MFRW_Halt
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 1                                       '读取ID
     l = 8                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself               '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = &H84                      'MFRWR_ReadWrite
     OutData(6) = 1                        '参数长度
     OutData(7) = &H1                       'MFRW_ReadID
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 6                                       '读取ReadID和Privilege
     l = 8                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself               '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = &H84                      'MFRWR_ReadWrite
     OutData(6) = 1                        '参数长度
     OutData(7) = &H6                       'MFRW_ReadPrivilege
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 9                                       '格式化生成临时卡
     l = 13                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself               '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = &H84                      'MFRWR_ReadWrite
     OutData(6) = 6                         '参数长度
     OutData(7) = &HE                       'MFRW_FormatCard
     s = Text1(5).Text
     OutData(8) = Val("&H" + Mid(s, 4, 2)) '目标卡ID号【4】
     OutData(9) = Val("&H" + Mid(s, 7, 2))
     OutData(10) = Val("&H" + Mid(s, 10, 2))
     OutData(11) = Val("&H" + Mid(s, 13, 2))
     OutData(12) = 9                        '车型卡类[1]：0x09=临时卡；0x11=优惠储值卡
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 10                                       '格式化生成优惠储值卡
     l = 13                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself               '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = &H84                      'MFRWR_ReadWrite
     OutData(6) = 6                         '参数长度
     OutData(7) = &HE                       'MFRW_FormatCard
     s = Text1(5).Text
     OutData(8) = Val("&H" + Mid(s, 4, 2)) '目标卡ID号【4】
     OutData(9) = Val("&H" + Mid(s, 7, 2))
     OutData(10) = Val("&H" + Mid(s, 10, 2))
     OutData(11) = Val("&H" + Mid(s, 13, 2))
     OutData(12) = &H11                        '车型卡类[1]：0x09=临时卡；0x11=优惠储值卡
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 11                                      '临时卡进场
     l = 16                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself               '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = &H84                      'MFRWR_ReadWrite
     OutData(6) = 9                         '参数长度
     OutData(7) = &HD                       'PrivilegeSave
     s = Text1(5).Text
     OutData(8) = Val("&H" + Mid(s, 4, 2)) '目标卡ID号【4】
     OutData(9) = Val("&H" + Mid(s, 7, 2))
     OutData(10) = Val("&H" + Mid(s, 10, 2))
     OutData(11) = Val("&H" + Mid(s, 13, 2))
     OutData(12) = 9                        '车型卡类[1]                 0x09=临时卡；0x11=优惠储值卡
     OutData(13) = &H80                     '停车及优惠状态[1]           临时卡：BIT7=进场标志，BIT1=有优惠；  优惠储值卡：BIT1=1=有优惠储值，BIT1=0=优惠储值已转移至Pos机
     OutData(14) = 0                        '临时卡=优惠机号[1]          优惠储值卡=优惠次数低位字节
     OutData(15) = 0                        '临时卡=优惠种类编码[1]      优惠储值卡=优惠次数高位字节
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
Case 12                                      '写优惠停车
     l = 16                                  '=最后元素序号
     ReDim OutData(l)
     OutData(0) = CONCommSyn1
     OutData(1) = CONCommSyn2
     OutData(2) = CONCommSyn3
     OutData(3) = ADDR_Myself               '主机地址
     OutData(4) = TYPE_Itself               '本机
     OutData(5) = &H84                      'MFRWR_ReadWrite
     OutData(6) = 9                         '参数长度
     OutData(7) = &HD                       'PrivilegeSave
     s = Text1(5).Text
     OutData(8) = Val("&H" + Mid(s, 4, 2)) '目标卡ID号【4】
     OutData(9) = Val("&H" + Mid(s, 7, 2))
     OutData(10) = Val("&H" + Mid(s, 10, 2))
     OutData(11) = Val("&H" + Mid(s, 13, 2))
     OutData(12) = &H11                      '车型卡类[1]                 0x09=临时卡；0x11=优惠储值卡
     OutData(13) = &H1                    '停车及优惠状态[1]           临时卡：BIT7=进场标志，BIT1=有优惠；  优惠储值卡：BIT1=1=有优惠储值，BIT1=0=优惠储值已转移至Pos机
     OutData(14) = 100                      '临时卡=优惠机号[1]          优惠储值卡=优惠次数低位字节
     OutData(15) = 0                        '临时卡=优惠种类编码[1]      优惠储值卡=优惠次数高位字节
     
     OutData(l) = 0
     For I = 3 To l - 1
     OutData(l) = OutData(l) Xor OutData(I)
     Next I
     MSComm1.Output = OutData
End Select

End Sub
