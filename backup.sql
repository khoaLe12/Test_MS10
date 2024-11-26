USE [Test_MS10]
GO
/****** Object:  Schema [Test_MS10]    Script Date: 11/26/2024 4:55:10 PM ******/
CREATE SCHEMA [Test_MS10]
GO

CREATE SEQUENCE Test_MS10.CountBy1
	AS INT
	START WITH 1
	INCREMENT BY 1;
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/26/2024 4:55:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Code] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DoB] [date] NOT NULL,
	[Position] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_1999_11_26_1', N'Employee 1', CAST(N'1999-11-26' AS Date), 1, 3)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_1997_01_01_2', N'Employee 2', CAST(N'1997-01-01' AS Date), 2, 4)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_1998_11_22_3', N'Employee 3', CAST(N'1998-11-22' AS Date), 3, 5)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_1988_05_22_4', N'Employee 4', CAST(N'1988-05-22' AS Date), 1, 6)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_1998_07_12_5', N'Employee 5', CAST(N'1998-07-12' AS Date), 3, 7)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_2001_07_13_6', N'Employee 6', CAST(N'2001-07-13' AS Date), 3, 8)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_2002_08_13_7', N'Employee 7', CAST(N'2002-08-13' AS Date), 2, 9)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_2003_07_23_8', N'Employee 8', CAST(N'2003-07-23' AS Date), 2, 10)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_2001_04_21_9', N'Employee 9', CAST(N'2001-04-21' AS Date), 1, 11)
INSERT [dbo].[Employee] ([Code], [Name], [DoB], [Position], [Id]) VALUES (N'NV_2002_04_21_10', N'Employee 10', CAST(N'2002-04-21' AS Date), 3, 12)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
