CREATE DATABASE [DEM_users]
GO
USE [DEM_users]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 20.11.2025 15:18:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 20.11.2025 15:18:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[IdRole] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [Title]) VALUES (1, N'Администратор')
GO
INSERT [dbo].[Role] ([Id], [Title]) VALUES (2, N'Модератор')
GO
INSERT [dbo].[Role] ([Id], [Title]) VALUES (3, N'Пользователь')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [Login], [Password], [IdRole]) VALUES (52, N'wan_de_rer', N'1230985566pp', 1)
GO
INSERT [dbo].[User] ([Id], [Login], [Password], [IdRole]) VALUES (60, N'Arthur', N'1234567', 2)
GO
INSERT [dbo].[User] ([Id], [Login], [Password], [IdRole]) VALUES (61, N'Anton', N'12345689', 3)
GO
INSERT [dbo].[User] ([Id], [Login], [Password], [IdRole]) VALUES (66, N'Maxim', N'1234567', 2)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IdRole]  DEFAULT ((3)) FOR [IdRole]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role1] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role1]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [CK_User_Login] CHECK  ((len([Login])>(1)))
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [CK_User_Login]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [CK_User_Password] CHECK  ((len([Password])>(5)))
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [CK_User_Password]
GO
/****** Object:  StoredProcedure [dbo].[sp_CountUserToRole]    Script Date: 20.11.2025 15:18:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CountUserToRole]
AS
BEGIN
	SELECT
    R.[Title] AS 'Роль',
    COUNT(*) AS 'Количество_пользователей'
FROM
    [dbo].[Role] AS R
JOIN
    [dbo].[User] AS U ON R.Id = U.IdRole
GROUP BY
    R.[Title];
END
GO
