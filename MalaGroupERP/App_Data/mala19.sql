USE [MalaGroupERP]
GO
/****** Object:  ForeignKey [FK_syNavigationNodes_syResources]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syNavigationNodes] DROP CONSTRAINT [FK_syNavigationNodes_syResources]
GO
/****** Object:  ForeignKey [FK_syRoleResources_syResources]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRoleResources] DROP CONSTRAINT [FK_syRoleResources_syResources]
GO
/****** Object:  ForeignKey [FK_syRoleResources_syRoles]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRoleResources] DROP CONSTRAINT [FK_syRoleResources_syRoles]
GO
/****** Object:  ForeignKey [FK_syRolesResourcesLevels_syRoles]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRolesResourcesLevels] DROP CONSTRAINT [FK_syRolesResourcesLevels_syRoles]
GO
/****** Object:  ForeignKey [FK_syUsersRoles_syRoles]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syUsersRoles] DROP CONSTRAINT [FK_syUsersRoles_syRoles]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRightsByRoleIDAndModuleID]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetRightsByRoleIDAndModuleID]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserAccess]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetUserAccess]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetVehicleModels]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetVehicleModels]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetVehicleTypes]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetVehicleTypes]
GO
/****** Object:  Table [dbo].[syNavigationNodes]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syNavigationNodes] DROP CONSTRAINT [FK_syNavigationNodes_syResources]
GO
DROP TABLE [dbo].[syNavigationNodes]
GO
/****** Object:  Table [dbo].[syRoleResources]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRoleResources] DROP CONSTRAINT [FK_syRoleResources_syResources]
GO
ALTER TABLE [dbo].[syRoleResources] DROP CONSTRAINT [FK_syRoleResources_syRoles]
GO
DROP TABLE [dbo].[syRoleResources]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddPageLoginHistory]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_AddPageLoginHistory]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAccountInfoData]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetAccountInfoData]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetLeadInfoData]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetLeadInfoData]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetLeadVehicleInfoData]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetLeadVehicleInfoData]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetOwnerList]    Script Date: 12/19/2018 11:36:26 ******/
DROP PROCEDURE [dbo].[usp_GetOwnerList]
GO
/****** Object:  Table [dbo].[syRolesResourcesLevels]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRolesResourcesLevels] DROP CONSTRAINT [FK_syRolesResourcesLevels_syRoles]
GO
DROP TABLE [dbo].[syRolesResourcesLevels]
GO
/****** Object:  Table [dbo].[syUsersRoles]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syUsersRoles] DROP CONSTRAINT [FK_syUsersRoles_syRoles]
GO
DROP TABLE [dbo].[syUsersRoles]
GO
/****** Object:  Table [dbo].[tbl_Accounts]    Script Date: 12/19/2018 11:36:17 ******/
DROP TABLE [dbo].[tbl_Accounts]
GO
/****** Object:  Table [dbo].[tbl_LeadInformation]    Script Date: 12/19/2018 11:36:17 ******/
DROP TABLE [dbo].[tbl_LeadInformation]
GO
/****** Object:  Table [dbo].[tbl_Notes]    Script Date: 12/19/2018 11:36:17 ******/
DROP TABLE [dbo].[tbl_Notes]
GO
/****** Object:  Table [dbo].[tbl_VehicleLeads]    Script Date: 12/19/2018 11:36:17 ******/
DROP TABLE [dbo].[tbl_VehicleLeads]
GO
/****** Object:  Table [dbo].[tbl_VehicleMake]    Script Date: 12/19/2018 11:36:17 ******/
DROP TABLE [dbo].[tbl_VehicleMake]
GO
/****** Object:  Table [dbo].[tbl_VehicleModel]    Script Date: 12/19/2018 11:36:17 ******/
DROP TABLE [dbo].[tbl_VehicleModel]
GO
/****** Object:  Table [dbo].[tbl_VehicleType]    Script Date: 12/19/2018 11:36:17 ******/
DROP TABLE [dbo].[tbl_VehicleType]
GO
/****** Object:  Table [dbo].[tblLogin]    Script Date: 12/19/2018 11:36:18 ******/
ALTER TABLE [dbo].[tblLogin] DROP CONSTRAINT [DF_tblLogin_IsSuperUser]
GO
DROP TABLE [dbo].[tblLogin]
GO
/****** Object:  Table [dbo].[tblLoginHistory]    Script Date: 12/19/2018 11:36:18 ******/
DROP TABLE [dbo].[tblLoginHistory]
GO
/****** Object:  Table [dbo].[tblMessage]    Script Date: 12/19/2018 11:36:18 ******/
DROP TABLE [dbo].[tblMessage]
GO
/****** Object:  Table [dbo].[tblPagesLoginHistory]    Script Date: 12/19/2018 11:36:18 ******/
DROP TABLE [dbo].[tblPagesLoginHistory]
GO
/****** Object:  Table [dbo].[syRoles]    Script Date: 12/19/2018 11:36:16 ******/
DROP TABLE [dbo].[syRoles]
GO
/****** Object:  Table [dbo].[syResources]    Script Date: 12/19/2018 11:36:16 ******/
DROP TABLE [dbo].[syResources]
GO
/****** Object:  Table [dbo].[syResourceTypes]    Script Date: 12/19/2018 11:36:16 ******/
DROP TABLE [dbo].[syResourceTypes]
GO
/****** Object:  Table [dbo].[syResourceTypes]    Script Date: 12/19/2018 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[syResourceTypes](
	[ResourceTypeId] [int] NOT NULL,
	[ResourceType] [varchar](500) NULL,
 CONSTRAINT [PK_syResourceTypes] PRIMARY KEY CLUSTERED 
(
	[ResourceTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[syResourceTypes] ([ResourceTypeId], [ResourceType]) VALUES (1, N'Module')
INSERT [dbo].[syResourceTypes] ([ResourceTypeId], [ResourceType]) VALUES (2, N'SubModule')
/****** Object:  Table [dbo].[syResources]    Script Date: 12/19/2018 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[syResources](
	[ResourceId] [int] NOT NULL,
	[Resource] [varchar](200) NULL,
	[ResourceTypeId] [tinyint] NULL,
	[Controller] [varchar](200) NULL,
	[Action] [varchar](200) NULL,
	[UsedIn] [int] NULL,
	[IsRight] [int] NULL,
	[TabText] [varchar](200) NULL,
	[PageTitle] [varchar](200) NULL,
	[Icon] [varchar](50) NULL,
 CONSTRAINT [PK_syResources] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[syResources] ([ResourceId], [Resource], [ResourceTypeId], [Controller], [Action], [UsedIn], [IsRight], [TabText], [PageTitle], [Icon]) VALUES (1, N'Admin', 1, NULL, NULL, 1, 0, NULL, NULL, NULL)
INSERT [dbo].[syResources] ([ResourceId], [Resource], [ResourceTypeId], [Controller], [Action], [UsedIn], [IsRight], [TabText], [PageTitle], [Icon]) VALUES (2, N'Leads', 1, NULL, NULL, 1, 0, NULL, NULL, NULL)
/****** Object:  Table [dbo].[syRoles]    Script Date: 12/19/2018 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[syRoles](
	[RoleId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[RoleCode] [varchar](50) NOT NULL,
	[RoleDescription] [varchar](500) NOT NULL,
	[RoleStatus] [bit] NOT NULL,
 CONSTRAINT [PK_syRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[syRoles] ON
INSERT [dbo].[syRoles] ([RoleId], [RoleCode], [RoleDescription], [RoleStatus]) VALUES (1, N'Admin', N'Admin Role', 1)
INSERT [dbo].[syRoles] ([RoleId], [RoleCode], [RoleDescription], [RoleStatus]) VALUES (2, N'User', N'User role', 1)
SET IDENTITY_INSERT [dbo].[syRoles] OFF
/****** Object:  Table [dbo].[tblPagesLoginHistory]    Script Date: 12/19/2018 11:36:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPagesLoginHistory](
	[PHID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[LHID] [bigint] NULL,
	[PageName] [varchar](100) NULL,
	[PageInDateTime] [datetime] NULL,
	[PageOutDateTime] [datetime] NULL,
 CONSTRAINT [PK_tblPagesLoginHistory] PRIMARY KEY CLUSTERED 
(
	[PHID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblPagesLoginHistory] ON
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (1, 20, N'RoleManagement', CAST(0x0000A9B2011596B1 AS DateTime), CAST(0x0000A9B20116451C AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (2, 20, N'RoleManagement', CAST(0x0000A9B20116451C AS DateTime), CAST(0x0000A9B201164E78 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (3, 20, N'RoleManagement', CAST(0x0000A9B201164E78 AS DateTime), CAST(0x0000A9B201165144 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (4, 20, N'RoleManagement', CAST(0x0000A9B201165145 AS DateTime), CAST(0x0000A9B201166754 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (5, 20, N'RoleManagement', CAST(0x0000A9B201166754 AS DateTime), CAST(0x0000A9B2011675F6 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (6, 20, N'RoleManagement', CAST(0x0000A9B2011675FA AS DateTime), CAST(0x0000A9B201167890 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (7, 20, N'RoleManagement', CAST(0x0000A9B201167890 AS DateTime), CAST(0x0000A9B20116917C AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (8, 20, N'RoleManagement', CAST(0x0000A9B20116917D AS DateTime), CAST(0x0000A9B20117F9F5 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (9, 20, N'RoleManagement', CAST(0x0000A9B20117F9F5 AS DateTime), CAST(0x0000A9B2011B1001 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (10, 20, N'RoleManagement', CAST(0x0000A9B2011B1001 AS DateTime), CAST(0x0000A9B2011B5BC9 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (11, 20, N'RoleManagement', CAST(0x0000A9B2011B5BCA AS DateTime), CAST(0x0000A9B2011B9D4E AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (12, 20, N'RoleManagement', CAST(0x0000A9B2011B9D4E AS DateTime), CAST(0x0000A9B2011BA4D2 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (13, 20, N'RoleManagement', CAST(0x0000A9B2011BA4D2 AS DateTime), CAST(0x0000A9B2011BD7E0 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (14, 20, N'RoleManagement', CAST(0x0000A9B2011BD7E0 AS DateTime), CAST(0x0000A9B2011C2B38 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (15, 20, N'RoleManagement', CAST(0x0000A9B2011C2B39 AS DateTime), CAST(0x0000A9B2011D14F0 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (16, 20, N'RoleManagement', CAST(0x0000A9B2011D14F0 AS DateTime), CAST(0x0000A9B2011D4010 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (17, 20, N'RoleManagement', CAST(0x0000A9B2011D4010 AS DateTime), CAST(0x0000A9B2011E21FC AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (18, 20, N'RoleManagement', CAST(0x0000A9B2011E21FC AS DateTime), CAST(0x0000A9B2011E2FE7 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (19, 20, N'RoleManagement', CAST(0x0000A9B2011E2FE7 AS DateTime), CAST(0x0000A9B2011E38F7 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (20, 20, N'RoleManagement', CAST(0x0000A9B2011E38F7 AS DateTime), CAST(0x0000A9B2011E931F AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (21, 20, N'RoleManagement', CAST(0x0000A9B2011E9325 AS DateTime), CAST(0x0000A9B2011FDB95 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (22, 20, N'RoleManagement', CAST(0x0000A9B2011FDB95 AS DateTime), CAST(0x0000A9B2012075BF AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (23, 20, N'RoleManagement', CAST(0x0000A9B2012075C9 AS DateTime), CAST(0x0000A9B20121440E AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (24, 20, N'RoleManagement', CAST(0x0000A9B20121440F AS DateTime), CAST(0x0000A9B201217A4B AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (25, 20, N'RoleManagement', CAST(0x0000A9B201217A4B AS DateTime), CAST(0x0000A9B201218F23 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (26, 20, N'RoleManagement', CAST(0x0000A9B201218F23 AS DateTime), CAST(0x0000A9B20121B032 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (27, 20, N'RoleManagement', CAST(0x0000A9B20121B032 AS DateTime), CAST(0x0000A9B201225474 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (28, 20, N'RoleManagement', CAST(0x0000A9B201225474 AS DateTime), CAST(0x0000A9B201246DD9 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (29, 20, N'RoleManagement', CAST(0x0000A9B201246DD9 AS DateTime), CAST(0x0000A9B20124AA7A AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (30, 20, N'RoleManagement', CAST(0x0000A9B20124AA7B AS DateTime), CAST(0x0000A9B20124D7F0 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (31, 20, N'RoleManagement', CAST(0x0000A9B20124D7F1 AS DateTime), CAST(0x0000A9B201254FAD AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (32, 20, N'RoleManagement', CAST(0x0000A9B201254FAD AS DateTime), CAST(0x0000A9B201256A3F AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (33, 20, N'RoleManagement', CAST(0x0000A9B201256A3F AS DateTime), CAST(0x0000A9B201267913 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (34, 20, N'RoleManagement', CAST(0x0000A9B201267913 AS DateTime), CAST(0x0000A9B20126DC6E AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (35, 20, N'RoleManagement', CAST(0x0000A9B20126DC6E AS DateTime), CAST(0x0000A9B20127CE0F AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (36, 20, N'RoleManagement', CAST(0x0000A9B20127CE0F AS DateTime), CAST(0x0000A9B20127D445 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (37, 20, N'RoleManagement', CAST(0x0000A9B20127D445 AS DateTime), CAST(0x0000A9B201283ACF AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (38, 20, N'RoleManagement', CAST(0x0000A9B201283ACF AS DateTime), CAST(0x0000A9B20128E414 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (39, 20, N'RoleManagement', CAST(0x0000A9B20128E41C AS DateTime), CAST(0x0000A9B201290E58 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (40, 20, N'RoleManagement', CAST(0x0000A9B201290E58 AS DateTime), CAST(0x0000A9B20129AC41 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (41, 20, N'RoleManagement', CAST(0x0000A9B20129AC41 AS DateTime), CAST(0x0000A9B2012B3D01 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (42, 20, N'RoleManagement', CAST(0x0000A9B2012B3D01 AS DateTime), CAST(0x0000A9B2012C004B AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (43, 20, N'RoleManagement', CAST(0x0000A9B2012C004B AS DateTime), CAST(0x0000A9B2012C4648 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (44, 20, N'RoleManagement', CAST(0x0000A9B2012C4648 AS DateTime), CAST(0x0000A9B2012F335A AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (45, 20, N'RoleManagement', CAST(0x0000A9B2012F335A AS DateTime), CAST(0x0000A9B2012FC2AA AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (46, 20, N'RoleManagement', CAST(0x0000A9B2012FC2AA AS DateTime), CAST(0x0000A9B2013004AC AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (47, 20, N'RoleManagement', CAST(0x0000A9B2013004AD AS DateTime), NULL)
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (48, 23, N'AssignRole', CAST(0x0000A9B300C0987E AS DateTime), CAST(0x0000A9B300C40121 AS DateTime))
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (49, 23, N'AssignRole', CAST(0x0000A9B300C40128 AS DateTime), NULL)
INSERT [dbo].[tblPagesLoginHistory] ([PHID], [LHID], [PageName], [PageInDateTime], [PageOutDateTime]) VALUES (50, 31, N'AssignRole', CAST(0x0000A9B400BF80F5 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tblPagesLoginHistory] OFF
/****** Object:  Table [dbo].[tblMessage]    Script Date: 12/19/2018 11:36:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMessage](
	[MessageId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[MessageFrom] [int] NULL,
	[MessageTo] [int] NULL,
	[MessageText] [varchar](8000) NULL,
	[MessageTime] [datetime] NULL,
	[IsRead] [int] NULL,
	[GroupID] [int] NULL,
 CONSTRAINT [PK_tblMessage] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblLoginHistory]    Script Date: 12/19/2018 11:36:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[tblLoginHistory](
	[ID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[UserID] [int] NULL,
	[SessionID] [varchar](500) NULL,
	[IPAddress] [varchar](50) NULL,
	[PageName] [varchar](100) NULL,
	[LoginDateTime] [datetime] NULL,
	[LogoutDateTime] [datetime] NULL,
 CONSTRAINT [PK_tblLoginHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblLoginHistory] ON
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (1, 3, N'tpfirkzyqhg5ywpl3d4nz14q', N'::1', N'Home', CAST(0x0000A9AD0135F227 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (2, 3, N'tpfirkzyqhg5ywpl3d4nz14q', N'::1', N'Home', CAST(0x0000A9AD013663A1 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (3, 3, N'tpfirkzyqhg5ywpl3d4nz14q', N'::1', N'Home', CAST(0x0000A9AD0136C8E8 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (4, 3, N'tpfirkzyqhg5ywpl3d4nz14q', N'::1', N'Home', CAST(0x0000A9AD0136E9A6 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (5, 3, N'tpfirkzyqhg5ywpl3d4nz14q', N'::1', N'Home', CAST(0x0000A9AD0137E9DA AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (6, 3, N'tpfirkzyqhg5ywpl3d4nz14q', N'::1', N'Home', CAST(0x0000A9AD0137FA5F AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (7, 3, N'tpfirkzyqhg5ywpl3d4nz14q', N'::1', N'Home', CAST(0x0000A9AD01382E20 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (8, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013919ED AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (9, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD0139348A AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (10, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013948E5 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (11, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD01395AEF AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (12, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013A1899 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (13, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013A93CC AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (14, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013BA902 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (15, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013BC81B AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (16, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013CD58E AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (17, 3, N'fujsw0shgsnfw0ui3bnqo5zy', N'::1', N'Home', CAST(0x0000A9AD013D2065 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (18, 3, N'knnmxkpesfkseri0whjuwtdl', N'::1', N'Home', CAST(0x0000A9AD013D809B AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (19, 3, N'33x1l0bdnz2ffqzdbfnoot0h', N'::1', N'Home', CAST(0x0000A9AD013E17CF AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (20, 3, N'mrdhnma3e211wfyl0ulibphs', N'::1', N'Home', CAST(0x0000A9B201158920 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (21, 3, N'mrdhnma3e211wfyl0ulibphs', N'127.0.0.1', N'Home', CAST(0x0000A9B201306B20 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (22, 3, N'mrdhnma3e211wfyl0ulibphs', N'127.0.0.1', N'Home', CAST(0x0000A9B2014E8107 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (23, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B300C078ED AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (24, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B300D8130C AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (25, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B30100A250 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (26, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B301059C6F AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (27, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B3010CB9CE AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (28, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B30111C964 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (29, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B30131E457 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (30, 3, N'smt4snezcdnbr4mup3x0rso4', N'::1', N'Home', CAST(0x0000A9B301329BAF AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (31, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B400BEEC5A AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (32, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B400E30531 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (33, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B4010BA25C AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (34, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B4010EA1BC AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (35, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B4010F0FC4 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (36, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B40110215F AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (37, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B401221D0B AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (38, 3, N'i2fultgraakopmj1qslnzx03', N'::1', N'Home', CAST(0x0000A9B4012DAA6F AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (39, 3, N'r53pcuj5n0f4xv1bbiatltvy', N'::1', N'Home', CAST(0x0000A9B500CDF7F1 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (40, 3, N'qbm20txljg2bg0ksix4q4kgp', N'::1', N'Home', CAST(0x0000A9B500FBC348 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (41, 3, N'qbm20txljg2bg0ksix4q4kgp', N'::1', N'Home', CAST(0x0000A9B500FDA24A AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (42, 3, N'qbm20txljg2bg0ksix4q4kgp', N'::1', N'Home', CAST(0x0000A9B501015F49 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (43, 3, N'qbm20txljg2bg0ksix4q4kgp', N'::1', N'Home', CAST(0x0000A9B501017F16 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (44, 3, N'qbm20txljg2bg0ksix4q4kgp', N'::1', N'Home', CAST(0x0000A9B501037AE6 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (45, 3, N'qbm20txljg2bg0ksix4q4kgp', N'::1', N'Home', CAST(0x0000A9B501086467 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (46, 3, N'qbm20txljg2bg0ksix4q4kgp', N'::1', N'Home', CAST(0x0000A9B5010F527F AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (47, 3, N'yukqscca01oiwnlvacam2wcp', N'::1', N'Home', CAST(0x0000A9B50145C1F9 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (48, 3, N'wnzyywxxh10dhvschacz5nnh', N'::1', N'Home', CAST(0x0000A9B600C1FF1E AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (49, 3, N'jfqlhryvkdij2zjrbm4vfdqc', N'::1', N'Home', CAST(0x0000A9BA00F9F6F9 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (50, 3, N'jfqlhryvkdij2zjrbm4vfdqc', N'::1', N'Home', CAST(0x0000A9BA0102DE53 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (51, 3, N'jfqlhryvkdij2zjrbm4vfdqc', N'::1', N'Home', CAST(0x0000A9BA0104CAA8 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (52, 3, N'jfqlhryvkdij2zjrbm4vfdqc', N'::1', N'Home', CAST(0x0000A9BA0106EC78 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (53, 3, N'jfqlhryvkdij2zjrbm4vfdqc', N'::1', N'Home', CAST(0x0000A9BA011B0BB9 AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (54, 3, N'jfqlhryvkdij2zjrbm4vfdqc', N'::1', N'Home', CAST(0x0000A9BA01230B2F AS DateTime), NULL)
INSERT [dbo].[tblLoginHistory] ([ID], [UserID], [SessionID], [IPAddress], [PageName], [LoginDateTime], [LogoutDateTime]) VALUES (55, 3, N'jfqlhryvkdij2zjrbm4vfdqc', N'::1', N'Home', CAST(0x0000A9BA012420AA AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tblLoginHistory] OFF
/****** Object:  Table [dbo].[tblLogin]    Script Date: 12/19/2018 11:36:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLogin](
	[UserID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Username] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[ClientOrVendorID] [int] NULL,
	[IsClientOrVendor] [int] NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Email] [varchar](260) NULL,
	[IsSuperUser] [int] NULL CONSTRAINT [DF_tblLogin_IsSuperUser]  DEFAULT ((0)),
	[SortOrderSFP] [int] NULL,
	[Address1] [varchar](200) NULL,
	[Address2] [varchar](200) NULL,
	[City] [varchar](100) NULL,
	[StateID] [varchar](50) NULL,
	[ZipCode] [varchar](10) NULL,
	[WorkPhone] [varchar](15) NULL,
	[CellPhone] [varchar](15) NULL,
	[IsActive] [int] NULL,
	[Extension] [varchar](5) NULL,
	[VendorID] [varchar](10) NULL,
	[EmployeeID] [bigint] NULL,
	[AddToGroup] [int] NULL,
	[UserType] [int] NULL,
	[StationMarketID] [int] NULL,
	[ShowNotification] [int] NULL,
	[UserCode] [varchar](10) NULL,
	[Timezone] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblLogin] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblLogin] ON
INSERT [dbo].[tblLogin] ([UserID], [Username], [Password], [ClientOrVendorID], [IsClientOrVendor], [FirstName], [LastName], [Email], [IsSuperUser], [SortOrderSFP], [Address1], [Address2], [City], [StateID], [ZipCode], [WorkPhone], [CellPhone], [IsActive], [Extension], [VendorID], [EmployeeID], [AddToGroup], [UserType], [StationMarketID], [ShowNotification], [UserCode], [Timezone]) VALUES (3, N'NTSRAdm', N'123', 0, 1, N'NTSR', N'Admin', N'0', 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'1', NULL)
SET IDENTITY_INSERT [dbo].[tblLogin] OFF
/****** Object:  Table [dbo].[tbl_VehicleType]    Script Date: 12/19/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_VehicleType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VehicleMake] [int] NOT NULL,
	[VehicleType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tbl_VehicleType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_VehicleType] ON
INSERT [dbo].[tbl_VehicleType] ([ID], [VehicleMake], [VehicleType]) VALUES (3, 4, N'XUV')
INSERT [dbo].[tbl_VehicleType] ([ID], [VehicleMake], [VehicleType]) VALUES (4, 4, N'SUV')
INSERT [dbo].[tbl_VehicleType] ([ID], [VehicleMake], [VehicleType]) VALUES (5, 1, N'Hatchback')
INSERT [dbo].[tbl_VehicleType] ([ID], [VehicleMake], [VehicleType]) VALUES (6, 4, N'Sedan')
INSERT [dbo].[tbl_VehicleType] ([ID], [VehicleMake], [VehicleType]) VALUES (7, 1, N'Sedan')
INSERT [dbo].[tbl_VehicleType] ([ID], [VehicleMake], [VehicleType]) VALUES (8, 1, N'Basic')
INSERT [dbo].[tbl_VehicleType] ([ID], [VehicleMake], [VehicleType]) VALUES (9, 1, N'dfdf')
SET IDENTITY_INSERT [dbo].[tbl_VehicleType] OFF
/****** Object:  Table [dbo].[tbl_VehicleModel]    Script Date: 12/19/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VehicleModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VehicleMake] [int] NOT NULL,
	[VehicleType] [int] NOT NULL,
	[VehicleModel] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbl_VehicleModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_VehicleModel] ON
INSERT [dbo].[tbl_VehicleModel] ([ID], [VehicleMake], [VehicleType], [VehicleModel]) VALUES (1, 1, 5, N'City')
INSERT [dbo].[tbl_VehicleModel] ([ID], [VehicleMake], [VehicleType], [VehicleModel]) VALUES (2, 1, 5, N'Amchi')
INSERT [dbo].[tbl_VehicleModel] ([ID], [VehicleMake], [VehicleType], [VehicleModel]) VALUES (3, 4, 3, N'FFFF33')
SET IDENTITY_INSERT [dbo].[tbl_VehicleModel] OFF
/****** Object:  Table [dbo].[tbl_VehicleMake]    Script Date: 12/19/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_VehicleMake](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VehicleMake] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_tbl_VehicleMake] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_VehicleMake] ON
INSERT [dbo].[tbl_VehicleMake] ([ID], [VehicleMake]) VALUES (1, N'Honda')
INSERT [dbo].[tbl_VehicleMake] ([ID], [VehicleMake]) VALUES (2, N'TATA')
INSERT [dbo].[tbl_VehicleMake] ([ID], [VehicleMake]) VALUES (4, N'Jeep')
INSERT [dbo].[tbl_VehicleMake] ([ID], [VehicleMake]) VALUES (5, N'Mahindra')
INSERT [dbo].[tbl_VehicleMake] ([ID], [VehicleMake]) VALUES (7, N'Hundai')
INSERT [dbo].[tbl_VehicleMake] ([ID], [VehicleMake]) VALUES (8, N'ddsd')
SET IDENTITY_INSERT [dbo].[tbl_VehicleMake] OFF
/****** Object:  Table [dbo].[tbl_VehicleLeads]    Script Date: 12/19/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VehicleLeads](
	[ID] [bigint] NOT NULL,
	[VehicleMake] [int] NOT NULL,
	[VehicleType] [int] NOT NULL,
	[VehicleModel] [int] NOT NULL,
	[VehicleYear] [varchar](10) NOT NULL,
	[VINNo] [varchar](20) NOT NULL,
	[LicensePlate] [varchar](50) NOT NULL,
	[Dealership] [varchar](100) NOT NULL,
	[FinanceCompany] [varchar](100) NOT NULL,
	[LeadID] [bigint] NOT NULL,
 CONSTRAINT [PK_tbl_VehicleLeads] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Notes]    Script Date: 12/19/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Notes](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[Photo] [nvarchar](max) NULL,
	[NotesDate] [datetime] NOT NULL,
	[UserID] [bigint] NOT NULL,
 CONSTRAINT [PK_tbl_Notes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_Notes] ON
INSERT [dbo].[tbl_Notes] ([ID], [Title], [Notes], [Photo], [NotesDate], [UserID]) VALUES (1, N'Test notes', N'ddwdwd', NULL, CAST(0x0000A87F00000000 AS DateTime), 3)
INSERT [dbo].[tbl_Notes] ([ID], [Title], [Notes], [Photo], [NotesDate], [UserID]) VALUES (2, N'Test nfffdfdf', N'jnjnljknjknjknj', NULL, CAST(0x0000A8E90020F580 AS DateTime), 3)
INSERT [dbo].[tbl_Notes] ([ID], [Title], [Notes], [Photo], [NotesDate], [UserID]) VALUES (3, N'xdxaaaaaaaaaa', N'aaaxx', NULL, CAST(0x0000A9BA0104A690 AS DateTime), 0)
INSERT [dbo].[tbl_Notes] ([ID], [Title], [Notes], [Photo], [NotesDate], [UserID]) VALUES (4, N'sssssssss', N'ssssssssssssssssss', NULL, CAST(0x0000A9BA0104ECE0 AS DateTime), 0)
INSERT [dbo].[tbl_Notes] ([ID], [Title], [Notes], [Photo], [NotesDate], [UserID]) VALUES (5, N'Test ', N'tt', NULL, CAST(0x0000A9BA01053330 AS DateTime), 3)
INSERT [dbo].[tbl_Notes] ([ID], [Title], [Notes], [Photo], [NotesDate], [UserID]) VALUES (6, N'TEST 18 DEC', N'ffffffffffffffffffffffffffffffffffffffffffffffffff
efefef', NULL, CAST(0x0000A9BA01064C70 AS DateTime), 3)
INSERT [dbo].[tbl_Notes] ([ID], [Title], [Notes], [Photo], [NotesDate], [UserID]) VALUES (7, N'Test new', N'ffffff', NULL, CAST(0x0000A9BA010838A0 AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[tbl_Notes] OFF
/****** Object:  Table [dbo].[tbl_LeadInformation]    Script Date: 12/19/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LeadInformation](
	[LeadID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[CompanyEmail] [varchar](20) NOT NULL,
	[LeadStatus] [int] NOT NULL,
	[LeadOwner] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[PrimaryPhone] [varchar](20) NOT NULL,
	[SecondaryPhone] [varchar](20) NOT NULL,
	[LeadEmail] [varchar](20) NOT NULL,
	[ListCode] [varchar](20) NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[Warranty] [varchar](20) NOT NULL,
	[Language] [varchar](100) NOT NULL,
	[ListCode2] [varchar](20) NOT NULL,
	[Address] [varchar](500) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[PinNo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbl_LeadInformation] PRIMARY KEY CLUSTERED 
(
	[LeadID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Accounts]    Script Date: 12/19/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Accounts](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountName] [varchar](100) NULL,
	[Phone] [varchar](20) NULL,
	[Website] [varchar](50) NULL,
	[Type] [int] NULL,
	[AccountOwner] [int] NULL,
	[ParentAccount] [int] NULL,
	[Industry] [int] NULL,
	[Employee] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[BillingAddress] [nvarchar](500) NULL,
	[BillingCity] [varchar](50) NULL,
	[BillingState] [varchar](50) NULL,
	[BillingZip] [varchar](10) NULL,
	[BillingCountry] [varchar](50) NULL,
	[ShippingAddress] [nvarchar](500) NULL,
	[ShippingCity] [varchar](50) NULL,
	[ShippingState] [varchar](50) NULL,
	[ShippingZip] [varchar](10) NULL,
	[ShippingCountry] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_Acoounts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Accounts] ON
INSERT [dbo].[tbl_Accounts] ([ID], [AccountName], [Phone], [Website], [Type], [AccountOwner], [ParentAccount], [Industry], [Employee], [Description], [BillingAddress], [BillingCity], [BillingState], [BillingZip], [BillingCountry], [ShippingAddress], [ShippingCity], [ShippingState], [ShippingZip], [ShippingCountry]) VALUES (1, N'Test Account', N'4343434489', N'www.ntsr.com', 3, 3, 0, 3, 78, N'wewedwewe', N'ffffdfd', N'Chicago', N'MH', N'323232', N'US', N'fdbhgng', N'fdbhgng', N'NA', N'6566', N'US')
SET IDENTITY_INSERT [dbo].[tbl_Accounts] OFF
/****** Object:  Table [dbo].[syUsersRoles]    Script Date: 12/19/2018 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[syUsersRoles](
	[UserRoleId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_syUsersRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[syUsersRoles] ON
INSERT [dbo].[syUsersRoles] ([UserRoleId], [UserId], [RoleId]) VALUES (1, 3, 1)
SET IDENTITY_INSERT [dbo].[syUsersRoles] OFF
/****** Object:  Table [dbo].[syRolesResourcesLevels]    Script Date: 12/19/2018 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[syRolesResourcesLevels](
	[RRLId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[RoleId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[AccessLevel] [int] NOT NULL,
	[HasSpecialRight] [int] NOT NULL,
 CONSTRAINT [PK_syRolesResourcesLevels] PRIMARY KEY CLUSTERED 
(
	[RRLId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetOwnerList]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [usp_GetOwnerList] 'a'

CREATE PROCEDURE [dbo].[usp_GetOwnerList] 
	@UserName	VARCHAR(500)=null
	
AS
BEGIN

SELECT UserID,Username,FirstName,LastName
FROM tblLogin 
WHERE Username  LIKE '%'+ISNULL(@UserName,'')+'%'  ORDER BY UserID ASC

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetLeadVehicleInfoData]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC usp_GetLeadVehicleInfoData 1
CREATE PROCEDURE [dbo].[usp_GetLeadVehicleInfoData]

	 @LeadID        BIGINT
AS
BEGIN
	
	
	
	SELECT VL.LeadID,VL.VehicleMake,VM.VehicleMake AS VehicleMakeText,VL.VehicleType,VT.VehicleType AS VehicleTypeText,VL.VehicleModel,VMO.VehicleModel AS VehicleModelText,VehicleYear,VINNo,LicensePlate,Dealership,FinanceCompany 
	FROM tbl_VehicleLeads  VL
	LEFT OUTER JOIN tbl_VehicleMake VM  ON VL.VehicleMake=VM.ID
	LEFT OUTER JOIN tbl_VehicleType VT  ON VL.VehicleMake=VT.VehicleMake AND VL.VehicleType=VT.ID
	LEFT OUTER JOIN tbl_VehicleModel VMO  ON VL.VehicleMake=VMO.VehicleMake AND VL.VehicleType=VMO.VehicleType AND VL.VehicleModel=VMO.ID
	
	WHERE LeadID=@LeadID

    
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetLeadInfoData]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC usp_GetLeadInfoData null
CREATE PROCEDURE [dbo].[usp_GetLeadInfoData]

	 @LeadID        BIGINT
AS
BEGIN
	
	SELECT LeadID,CompanyName,Title,Phone,CompanyEmail,LeadStatus,LeadOwner,Name,PrimaryPhone,SecondaryPhone,LeadEmail,ListCode,ExpirationDate,Warranty,Language,
	ListCode2,
	Address,Password,PinNo FROM tbl_LeadInformation WHERE LeadID=@LeadID

    
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAccountInfoData]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC usp_GetAccountInfoData null
CREATE PROCEDURE [dbo].[usp_GetAccountInfoData]

	 @AccountID       BIGINT
AS
BEGIN
	SELECT ID
      ,A.AccountName
      ,A.Phone
      ,A.Website,A.Type,
      A.AccountOwner,
      L.Username
      ,A.ParentAccount
      ,A.Industry
      ,A.Employee
      ,A.Description
      ,A.BillingAddress
      ,A.BillingCity
      ,A.BillingState
      ,A.BillingZip
      ,A.BillingCountry
      ,A.ShippingAddress
      ,A.ShippingCity
      ,A.ShippingState
      ,A.ShippingZip
      ,A.ShippingCountry
  FROM tbl_Accounts A INNER JOIN tblLogin L ON A.AccountOwner =L.UserID
   WHERE ID=@AccountID

    
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AddPageLoginHistory]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_AddPageLoginHistory]
@UserID	     INT =null,
@SessionID	 VARCHAR(50) =null, 
@PageName	 VARCHAR(100) =null 
AS
BEGIN
	DECLARE @LHID BIGINT
    SET @LHID= (SELECT ID FROM tblLoginHistory WHERE SessionID = @SessionID AND UserID=@UserID AND LogoutDateTime IS NULL)
		 IF @LHID > 0
		  BEGIN
			
			UPDATE [tblPagesLoginHistory]
            SET [PageOutDateTime] = GETDATE()
            WHERE  LHID = @LHID AND PageOutDateTime IS NULL
            if(@PageName!='')
            BEGIN
            INSERT INTO [tblPagesLoginHistory]([LHID],[PageName],[PageInDateTime])
            VALUES(@LHID,@PageName,GETDATE())
            end
	       END
END
GO
/****** Object:  Table [dbo].[syRoleResources]    Script Date: 12/19/2018 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[syRoleResources](
	[RoleResourceId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[RoleId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
 CONSTRAINT [PK_syRoleResources] PRIMARY KEY CLUSTERED 
(
	[RoleResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[syRoleResources] ON
INSERT [dbo].[syRoleResources] ([RoleResourceId], [RoleId], [ResourceId]) VALUES (4, 1, 1)
INSERT [dbo].[syRoleResources] ([RoleResourceId], [RoleId], [ResourceId]) VALUES (5, 1, 2)
SET IDENTITY_INSERT [dbo].[syRoleResources] OFF
/****** Object:  Table [dbo].[syNavigationNodes]    Script Date: 12/19/2018 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[syNavigationNodes](
	[NodeID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[DisplayIndex] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[IsPopupWindow] [bit] NULL,
 CONSTRAINT [PK_syNavigationNodes] PRIMARY KEY CLUSTERED 
(
	[NodeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[syNavigationNodes] ON
INSERT [dbo].[syNavigationNodes] ([NodeID], [DisplayIndex], [ResourceId], [ParentId], [IsPopupWindow]) VALUES (1, 1, 1, NULL, 1)
INSERT [dbo].[syNavigationNodes] ([NodeID], [DisplayIndex], [ResourceId], [ParentId], [IsPopupWindow]) VALUES (2, 2, 2, NULL, 1)
SET IDENTITY_INSERT [dbo].[syNavigationNodes] OFF
/****** Object:  StoredProcedure [dbo].[usp_GetVehicleTypes]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [usp_GetVehicleTypes] ''

CREATE PROCEDURE [dbo].[usp_GetVehicleTypes] 
	@VehicleType	VARCHAR(500)=null
	
AS
BEGIN

SELECT VT.ID as VTID, VM.VehicleMake,VT.VehicleType
FROM tbl_VehicleType VT INNER JOIN tbl_VehicleMake VM ON VT.VehicleMake=VM.ID
WHERE VT.VehicleType  LIKE '%'+ISNULL(@VehicleType,'')+'%'  ORDER BY VT.VehicleMake ASC

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetVehicleModels]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [usp_GetVehicleModels] ''

CREATE PROCEDURE [dbo].[usp_GetVehicleModels] 
	@VehicleModel	VARCHAR(500)=null
	
AS
BEGIN

SELECT VMO.ID as VMID, VM.VehicleMake,VT.VehicleType, VMO.VehicleModel
FROM tbl_VehicleType VT INNER JOIN tbl_VehicleMake VM ON VT.VehicleMake=VM.ID
INNER JOIN tbl_VehicleModel VMO ON VMO.VehicleType=VT.ID
WHERE VMO.VehicleModel  LIKE '%'+ISNULL(@VehicleModel,'')+'%'  ORDER BY VMO.VehicleModel ASC

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserAccess]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetUserAccess]
@UserID		INT,
@Controller VARCHAR(500)	
AS
BEGIN
	
	DECLARE @AccessRight	INT
	DECLARE @HasRight		INT 
	DECLARE @EditRight		INT 
	DECLARE @AddRight		INT		
	DECLARE @DeleteRight	INT
	DECLARE @DisplayRight	INT
	DECLARE @IsSuperUser	INT
	DECLARE @ResourceID		INT
	
	SET @HasRight = 0
	SET @EditRight = 0
	SET @AddRight = 0
	SET @DeleteRight = 0
	SET @DisplayRight = 0
	
	CREATE TABLE #AccessLevel
	(
		AccessLevel INT
	)
	
	CREATE TABLE #SpecialRights
	(
		RightName		VARCHAR(500),
		SpecialRight	INT
	)
	
	SET @IsSuperUser=ISNULL((SELECT IsSuperUser FROM tblLogin WHERE UserID=@UserID),0)
		
	IF(EXISTS( SELECT * FROM syResources R INNER JOIN syRolesResourcesLevels RRL ON R.ResourceId=RRL.ResourceId	INNER JOIN syRoles RL ON RRL.RoleId=RL.RoleId
			   INNER JOIN syUsersRoles UR ON UR.RoleId=RL.RoleId WHERE R.Controller=@Controller AND UR.UserId=@UserID))
	BEGIN
		
		INSERT INTO #AccessLevel
		SELECT DISTINCT AccessLevel FROM syResources R INNER JOIN syRolesResourcesLevels RRL ON R.ResourceId=RRL.ResourceId INNER JOIN syRoles RL ON RRL.RoleId=RL.RoleId
		INNER JOIN syUsersRoles UR ON UR.RoleId=RL.RoleId WHERE R.Controller=@Controller AND UR.UserId=@UserID
		
		SET @ResourceID= ISNULL((SELECT TOP 1 ISNULL(R.ResourceId,0) FROM syResources R INNER JOIN syRolesResourcesLevels RRL ON R.ResourceId=RRL.ResourceId INNER JOIN syRoles RL ON RRL.RoleId=RL.RoleId
		INNER JOIN syUsersRoles UR ON UR.RoleId=RL.RoleId WHERE R.Controller=@Controller AND UR.UserId=@UserID),0)
		
		SET @AccessRight=ISNULL((SELECT TOP 1 AccessLevel FROM #AccessLevel WHERE AccessLevel=15),0)
		
		IF @AccessRight=0
		BEGIN
			UPDATE #AccessLevel SET AccessLevel=AccessLevel-1 WHERE AccessLevel>1
			SET @AccessRight =(SELECT SUM(AccessLevel) FROM #AccessLevel)
		END
		
		-- Full Right --
		IF @AccessRight=15
		BEGIN
			SET @HasRight = 1
			SET @EditRight = 1
			SET @AddRight = 1
			SET @DeleteRight = 1
			SET @DisplayRight = 1
		END
		
		-- Edit Right --
		IF (@AccessRight-8 >= 0)
		BEGIN
			SET @HasRight = 1
			SET @EditRight = 1
			SET @DisplayRight = 1
			SET @AccessRight=@AccessRight-8
		END
		
		-- Add Right --
		IF (@AccessRight-4 >= 0)
		BEGIN
			SET @HasRight = 1
			SET @AddRight = 1
			SET @DisplayRight = 1
			SET @AccessRight=@AccessRight-4
		END
		
		-- Delete Right --
		IF (@AccessRight-2 >= 0)
		BEGIN
			SET @HasRight = 1
			SET @DeleteRight = 1
			SET @DisplayRight = 1
			SET @AccessRight=@AccessRight-2
		END
		
		-- Display Right --
		IF (@AccessRight-1 >= 0)
		BEGIN
			SET @HasRight = 1
			SET @DisplayRight = 1
			SET @AccessRight=@AccessRight-1
		END
	END

	IF(@IsSuperUser=1)
	BEGIN
		INSERT INTO #SpecialRights
		SELECT R.Resource, 1 AS HasSpecialRight FROM syResources R  WHERE R.ResourceId IN (SELECT ResourceId FROM syNavigationNodes WHERE ParentId=@ResourceID)
	END
	ELSE
	BEGIN
		INSERT INTO #SpecialRights
		SELECT R.Resource, ISNULL(HasSpecialRight,0) FROM syResources R INNER JOIN syRolesResourcesLevels RRL ON R.ResourceId=RRL.ResourceId INNER JOIN syRoles RL ON RRL.RoleId=RL.RoleId
		INNER JOIN syUsersRoles UR ON UR.RoleId=RL.RoleId WHERE R.ResourceId IN (SELECT ResourceId FROM syNavigationNodes WHERE ParentId=@ResourceID) AND (UR.UserId=@UserID)
	END
	IF(@IsSuperUser=1)
	BEGIN
		SET @HasRight = 1
		SET @EditRight = 1
		SET @AddRight = 1
		SET @DeleteRight = 1
		SET @DisplayRight = 1
		
		UPDATE #SpecialRights SET SpecialRight=1
	END
	
	SELECT @HasRight AS HasRight, @EditRight AS EditRight, @AddRight AS AddRight, @DeleteRight AS DeleteRight, @DisplayRight AS DispalyRight
	SELECT RightName, SpecialRight FROM #SpecialRights
	DROP TABLE #AccessLevel
	DROP TABLE #SpecialRights
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRightsByRoleIDAndModuleID]    Script Date: 12/19/2018 11:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetRightsByRoleIDAndModuleID] 
	@RoleID		INT =1,
	@ModuleID	INT=1 
AS
BEGIN

DECLARE @ParentID				INT
DECLARE @ResourceID				INT
DECLARE @ResourceType			INT
DECLARE @IsForsave				INT
DECLARE @IsSpecialRight			INT
DECLARE @DisplayIndex			INT
DECLARE @RowCount				INT
DECLARE @AccessRight			INT
DECLARE @SpecialRight			INT
DECLARE @FullRight				INT
DECLARE @EditRight				INT
DECLARE @AddRight				INT
DECLARE @DeleteRight			INT
DECLARE @DisplayRight			INT
DECLARE @HasSubModules			INT
DECLARE @CountOfChildRow		INT
DECLARE @CountOfFullRights		INT
DECLARE @CountOfEditRights		INT
DECLARE @CountOfAddRights		INT
DECLARE @CountOfDeleteRights	INT
DECLARE @CountOfDisplayRights	INT
DECLARE @Depth					INT
DECLARE @Count					INT
DECLARE @RID					INT
DECLARE @LRID					INT
DECLARE @HasController			INT

CREATE TABLE #Resources
(
	ResourceID		INT,
	ResourceType	INT,
	ParentID		INT,
	DisplayIndex	INT,
	Depth			INT,
	IsSpecialRight	INT,
	IsProcess		INT
)

CREATE TABLE #Rights
(
	ID				INT IDENTITY(1,1),
	RID				INT,
	ResourceID		INT,
	ParentID		INT,
	IsForSave		INT,
	IsSpecialRight	INT,
	Resource		VARCHAR(500),
	SpecialRight	INT,
	FullRight		INT,
	EditRight		INT,
	AddRight		INT,
	DeleteRight		INT,
	DisplayRight	INT,
	DisplayIndex	INT,
	LoopThrough		VARCHAR(20),
	IsProcess		INT
)

INSERT INTO #Resources (ResourceID, ResourceType, ParentID, DisplayIndex, IsSpecialRight,IsProcess) 
SELECT R.ResourceId, R.ResourceTypeId, ISNULL(NN.ParentId,0), NN.DisplayIndex,ISNULL(R.IsRight,0), 0 FROM syResources R INNER JOIN syNavigationNodes NN ON R.ResourceId=NN.ResourceId WHERE R.ResourceId IN (@ModuleID) AND R.UsedIn=1

SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Resources WHERE IsProcess=0)
SET @ResourceID=(SELECT TOP 1 ResourceID FROM #Resources WHERE IsProcess=0 ORDER BY DisplayIndex)

WHILE(@RowCount>0)
BEGIN
	
	INSERT INTO #Resources (ResourceID, ResourceType, ParentID, DisplayIndex, IsSpecialRight,IsProcess) 
	SELECT R.ResourceId, R.ResourceTypeId, ISNULL(NN.ParentId,0), NN.DisplayIndex,ISNULL(R.IsRight,0), 0 FROM syResources R INNER JOIN syNavigationNodes NN ON R.ResourceId=NN.ResourceId WHERE NN.ParentId IN (@ResourceID) AND R.UsedIn=1

	UPDATE #Resources SET IsProcess=1 WHERE ResourceID=@ResourceID
	SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Resources WHERE IsProcess=0)
	SET @ResourceID=(SELECT TOP 1 ResourceID FROM #Resources WHERE IsProcess=0)
	
END

UPDATE #Resources SET IsProcess=0

;WITH Descendants AS (
 SELECT			R.ResourceID
                , R.ParentID
                , 0 AS HLevel
 FROM #Resources R  
 WHERE ResourceID = @ModuleID
 UNION ALL
 SELECT			R.ResourceID
                , R.ParentID
                , H.HLevel+1
 FROM #Resources R  
 INNER JOIN Descendants H
 ON H.ResourceID=R.ParentID  
)  
SELECT			d.ResourceID
                , d.HLevel+1 AS Depth
INTO #ResourceDepth
FROM Descendants d

UPDATE R SET R.Depth=RD.Depth FROM #ResourceDepth RD INNER JOIN #Resources R ON RD.ResourceID=R.ResourceID

SET @Depth=1

INSERT INTO #Rights (ResourceID, ParentID, IsForSave, IsSpecialRight,DisplayIndex, Resource, SpecialRight,FullRight, EditRight, AddRight, DeleteRight, DisplayRight, IsProcess) 
SELECT R.ResourceId, ISNULL(NN.ParentId,0), 0, 0,0, REPLICATE('&nbsp;&nbsp;&nbsp;&nbsp;', @Depth-1)+ R.Resource,0, 0, 0, 0, 0, 0, 1 
FROM syResources R INNER JOIN syNavigationNodes NN ON R.ResourceId=NN.ResourceId WHERE NN.ResourceId IN (@ModuleID) AND R.UsedIn=1


SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Resources WHERE IsProcess=0 )

SELECT TOP 1 @ResourceID=ResourceID, @ResourceType=ResourceType, @DisplayIndex=DisplayIndex, @Depth=Depth FROM #Resources WHERE IsProcess=0 ORDER BY DisplayIndex
WHILE(@RowCount>0)
BEGIN
	
	SET @AccessRight =ISNULL((SELECT TOP 1 AccessLevel FROM syRolesResourcesLevels WHERE RoleId=@RoleID AND ResourceId=@ResourceID),0)
	SET @SpecialRight =ISNULL((SELECT TOP 1 CASE WHEN HasSpecialRight =1 THEN 1 ELSE 0 END FROM syRolesResourcesLevels WHERE RoleId=@RoleID AND ResourceId=@ResourceID),0)
	SET @FullRight = 0
	SET @EditRight = 0
	SET @AddRight = 0
	SET @DeleteRight = 0
	SET @DisplayRight = 0
	PRINT @AccessRight
	-- Full Right --
	IF @AccessRight=15
	BEGIN
		SET @FullRight = 1
		SET @EditRight = 1
		SET @AddRight = 1
		SET @DeleteRight = 1
		SET @DisplayRight = 1
	END
	
	-- Edit Right --
	IF (@AccessRight-8 >= 0)
	BEGIN
		SET @EditRight = 1
		SET @AccessRight=@AccessRight-8
	END
	
	-- Add Right --
	IF (@AccessRight-4 >= 0)
	BEGIN
		SET @AddRight = 1
		SET @AccessRight=@AccessRight-4
	END
	
	-- Delete Right --
	IF (@AccessRight-2 >= 0)
	BEGIN
		SET @DeleteRight = 1
		SET @AccessRight=@AccessRight-2
	END
	
	-- Display Right --
	IF (@AccessRight-1 >= 0)
	BEGIN
		SET @DisplayRight = 1
		SET @AccessRight=@AccessRight-1
	END
	
	SET @IsForsave=CASE WHEN @ResourceType=1 THEN 0 ELSE 1 END
	SET @IsSpecialRight=CASE WHEN @ResourceType=7 THEN 1 ELSE 0 END
	SET @HasSubModules=(SELECT COUNT(*) FROM #Resources WHERE ParentID=@ResourceID AND @ResourceID<>@ModuleID AND IsSpecialRight=0)
	SET @HasController=(SELECT CASE WHEN Controller IS NULL THEN 0 ELSE 1 END  FROM syResources WHERE ResourceId=@ResourceID )
	IF @HasSubModules > 0
	BEGIN
		INSERT INTO #Rights (ResourceID, ParentID, IsForSave, IsSpecialRight,DisplayIndex, Resource, SpecialRight,FullRight, EditRight, AddRight, DeleteRight, DisplayRight, IsProcess) 
		SELECT R.ResourceId, ISNULL(NN.ParentId,0), 0, @IsSpecialRight,@DisplayIndex, REPLICATE('&nbsp;&nbsp;&nbsp;&nbsp;', @Depth-1)+ R.Resource,0, 0, 0, 0, 0, 0, 0 
		FROM syResources R INNER JOIN syNavigationNodes NN ON R.ResourceId=NN.ResourceId WHERE NN.ResourceId IN (@ResourceID) AND R.UsedIn=1
		SET @Depth=@Depth+1
	END
	
	IF (@HasController>0)
	BEGIN
		INSERT INTO #Rights (ResourceID, ParentID, IsForSave, IsSpecialRight,DisplayIndex, Resource, SpecialRight,FullRight, EditRight, AddRight, DeleteRight, DisplayRight, IsProcess) 
		SELECT R.ResourceId,@ResourceID , 1 , @IsSpecialRight,@DisplayIndex,CASE WHEN ISNULL(R.TabText,'')<>'' THEN  REPLICATE('&nbsp;&nbsp;&nbsp;&nbsp;', @Depth-1) + R.TabText ELSE  REPLICATE('&nbsp;&nbsp;&nbsp;&nbsp;', @Depth-1) + R.Resource END, @SpecialRight,@FullRight, @EditRight, @AddRight, @DeleteRight, @DisplayRight,@IsForsave 
		FROM syResources R INNER JOIN syNavigationNodes NN ON R.ResourceId=NN.ResourceId WHERE NN.ResourceId IN (@ResourceID) AND R.UsedIn=1
	END
	UPDATE #Resources SET IsProcess=1 WHERE ResourceID=@ResourceID
	SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Resources WHERE IsProcess=0)
	SELECT TOP 1 @ResourceID=ResourceID, @ResourceType=ResourceType, @DisplayIndex=DisplayIndex, @Depth=Depth FROM #Resources WHERE IsProcess=0
	
END


SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Rights WHERE IsProcess=0 )
SELECT TOP 1 @ResourceID=ResourceID FROM #Rights WHERE IsProcess=0 ORDER BY DisplayIndex DESC
WHILE(@RowCount>0)
BEGIN
	SET @CountOfChildRow = (SELECT COUNT(*) FROM #Rights WHERE ParentID=@ResourceID)
	SET @CountOfFullRights = (SELECT COUNT(*) FROM #Rights WHERE ParentID=@ResourceID AND FullRight=1)
	SET @CountOfEditRights = (SELECT COUNT(*) FROM #Rights WHERE ParentID=@ResourceID AND EditRight=1)
	SET @CountOfAddRights = (SELECT COUNT(*) FROM #Rights WHERE ParentID=@ResourceID AND AddRight=1)
	SET @CountOfDeleteRights = (SELECT COUNT(*) FROM #Rights WHERE ParentID=@ResourceID AND DeleteRight=1)
	SET @CountOfDisplayRights = (SELECT COUNT(*) FROM #Rights WHERE ParentID=@ResourceID AND DisplayRight=1)
	
	UPDATE #Rights SET IsProcess=1,
	FullRight= CASE WHEN @CountOfChildRow=@CountOfFullRights THEN 1 ELSE 0 END,
	EditRight= CASE WHEN @CountOfChildRow=@CountOfEditRights THEN 1 ELSE 0 END,
	AddRight= CASE WHEN @CountOfChildRow=@CountOfAddRights THEN 1 ELSE 0 END,
	DeleteRight= CASE WHEN @CountOfChildRow=@CountOfDeleteRights THEN 1 ELSE 0 END,
	DisplayRight= CASE WHEN @CountOfChildRow=@CountOfDisplayRights THEN 1 ELSE 0 END
	WHERE ResourceID=@ResourceID AND IsProcess=0
	SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Rights WHERE IsProcess=0 )
	SELECT TOP 1 @ResourceID=ResourceID FROM #Rights WHERE IsProcess=0 ORDER BY DisplayIndex DESC
END

UPDATE #Rights SET IsProcess=0
SET @Count=1
SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Rights WHERE IsProcess=0 )
SELECT TOP 1 @ResourceID=ResourceID, @ParentID= ParentID FROM #Rights WHERE IsProcess=0 ORDER BY DisplayIndex, IsForSave ASC
WHILE(@RowCount>0)
BEGIN
	UPDATE #Rights SET IsProcess=1,RID=@Count
	WHERE ResourceID=@ResourceID AND IsProcess=0 AND ParentID=@ParentID
	
	SET @Count=@Count+1
	SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Rights WHERE IsProcess=0 )
	SELECT TOP 1 @ResourceID=ResourceID, @ParentID= ParentID FROM #Rights WHERE IsProcess=0 ORDER BY DisplayIndex, IsForSave ASC
END

UPDATE #Rights SET IsProcess=0 WHERE IsForSave=0
SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Rights WHERE IsProcess=0 )
SELECT TOP 1 @ResourceID=ResourceID, @RID= RID FROM #Rights WHERE IsProcess=0 ORDER BY DisplayIndex, IsForSave ASC
WHILE(@RowCount>0)
BEGIN
	IF (@RID=1)
	BEGIN
		SELECT @LRID=MAX(RID) FROM #Rights
	END
	ELSE
	BEGIN
		SELECT @LRID=MAX(RID) FROM #Rights WHERE ParentID=@ResourceID
	END

	UPDATE #Rights SET IsProcess=1, LoopThrough=CONVERT(VARCHAR,@RID)+'-'+CONVERT(VARCHAR,@LRID)
	WHERE ResourceID=@ResourceID AND IsProcess=0 AND RID=@RID
	
	SET @RowCount=(SELECT COUNT(*) ResourceID FROM #Rights WHERE IsProcess=0 )
	SELECT TOP 1 @ResourceID=ResourceID, @RID= RID FROM #Rights WHERE IsProcess=0 ORDER BY DisplayIndex, IsForSave ASC
END

IF ((SELECT COUNT(*) FROM #Rights WHERE IsSpecialRight=0)=2)
BEGIN
	UPDATE #Rights SET LoopThrough=CONVERT(VARCHAR,RID)+'-'+CONVERT(VARCHAR,(SELECT COUNT(*) FROM #Rights WHERE IsSpecialRight=0)) WHERE ID=1
	UPDATE #Rights SET Resource='&nbsp;&nbsp;&nbsp;&nbsp;'+Resource, LoopThrough=CONVERT(VARCHAR,(SELECT COUNT(*) FROM #Rights WHERE IsSpecialRight=0))+'-'+CONVERT(VARCHAR,(SELECT COUNT(*) FROM #Rights WHERE IsSpecialRight=0)) WHERE ID=2
END
ELSE
BEGIN
	UPDATE #Rights SET LoopThrough=CONVERT(VARCHAR,RID)+'-'+CONVERT(VARCHAR,RID) WHERE LoopThrough IS NULL
END

UPDATE #Rights SET Resource='&nbsp;&nbsp;&nbsp;&nbsp;'+Resource WHERE IsSpecialRight=1

UPDATE RS SET Rs.IsSpecialRight=R.IsRight FROM #Rights RS INNER JOIN syResources R ON RS.ResourceID=R.ResourceId

SELECT ID,
	RID, ResourceID, ParentID, IsForSave, IsSpecialRight, Resource, SpecialRight, FullRight, EditRight, AddRight,
	DeleteRight, DisplayRight, DisplayIndex, LoopThrough, IsProcess 
FROM #Rights ORDER BY DisplayIndex ASC

DROP TABLE #ResourceDepth
DROP TABLE #Rights
DROP TABLE #Resources

END
GO
/****** Object:  ForeignKey [FK_syNavigationNodes_syResources]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syNavigationNodes]  WITH CHECK ADD  CONSTRAINT [FK_syNavigationNodes_syResources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[syResources] ([ResourceId])
GO
ALTER TABLE [dbo].[syNavigationNodes] CHECK CONSTRAINT [FK_syNavigationNodes_syResources]
GO
/****** Object:  ForeignKey [FK_syRoleResources_syResources]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRoleResources]  WITH CHECK ADD  CONSTRAINT [FK_syRoleResources_syResources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[syResources] ([ResourceId])
GO
ALTER TABLE [dbo].[syRoleResources] CHECK CONSTRAINT [FK_syRoleResources_syResources]
GO
/****** Object:  ForeignKey [FK_syRoleResources_syRoles]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRoleResources]  WITH CHECK ADD  CONSTRAINT [FK_syRoleResources_syRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[syRoles] ([RoleId])
GO
ALTER TABLE [dbo].[syRoleResources] CHECK CONSTRAINT [FK_syRoleResources_syRoles]
GO
/****** Object:  ForeignKey [FK_syRolesResourcesLevels_syRoles]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syRolesResourcesLevels]  WITH CHECK ADD  CONSTRAINT [FK_syRolesResourcesLevels_syRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[syRoles] ([RoleId])
GO
ALTER TABLE [dbo].[syRolesResourcesLevels] CHECK CONSTRAINT [FK_syRolesResourcesLevels_syRoles]
GO
/****** Object:  ForeignKey [FK_syUsersRoles_syRoles]    Script Date: 12/19/2018 11:36:16 ******/
ALTER TABLE [dbo].[syUsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_syUsersRoles_syRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[syRoles] ([RoleId])
GO
ALTER TABLE [dbo].[syUsersRoles] CHECK CONSTRAINT [FK_syUsersRoles_syRoles]
GO
