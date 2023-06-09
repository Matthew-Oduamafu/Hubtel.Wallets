USE [hubtelWalletsDB]
GO
/****** Object:  View [dbo].[vwTnsTypeAndSchemes]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwTnsTypeAndSchemes]
AS
SELECT        dbo.tblAsAccountScheme.asIDpk, dbo.tblAsAccountScheme.asTypeIDfk, dbo.tblTType.tIDpk, dbo.tblTType.tTypeName, dbo.tblAsAccountScheme.asSchemeName
FROM            dbo.tblAsAccountScheme INNER JOIN
                         dbo.tblTType ON dbo.tblAsAccountScheme.asTypeIDfk = dbo.tblTType.tIDpk
GO
/****** Object:  View [dbo].[vwUcaUserCreditAccounts]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwUcaUserCreditAccounts]
AS
SELECT        dbo.tblUcaUserCreditAccounts.ucaIDpk, dbo.tblUcaUserCreditAccounts.ucaUserIDfk, dbo.AspNetUsers.Id AS userId, dbo.AspNetUsers.FirstName, dbo.AspNetUsers.LastName, dbo.AspNetUsers.Email, 
                         dbo.AspNetUsers.UserName, dbo.AspNetUsers.PhoneNumber, dbo.tblUcaUserCreditAccounts.ucaTypeIDfk, dbo.tblTType.tIDpk, dbo.tblAsAccountScheme.asIDpk, dbo.tblUcaUserCreditAccounts.ucaSchemeIDfk, 
                         dbo.tblTType.tTypeName, dbo.tblAsAccountScheme.asSchemeName, dbo.tblUcaUserCreditAccounts.ucaAccountNumber, dbo.tblUcaUserCreditAccounts.ucaCreationDate, dbo.tblUcaUserCreditAccounts.ucaEditedDate
FROM            dbo.AspNetUsers INNER JOIN
                         dbo.tblUcaUserCreditAccounts ON dbo.AspNetUsers.Id = dbo.tblUcaUserCreditAccounts.ucaUserIDfk INNER JOIN
                         dbo.tblTType ON dbo.tblUcaUserCreditAccounts.ucaTypeIDfk = dbo.tblTType.tIDpk INNER JOIN
                         dbo.tblAsAccountScheme ON dbo.tblUcaUserCreditAccounts.ucaSchemeIDfk = dbo.tblAsAccountScheme.asIDpk
GO
/****** Object:  View [dbo].[vwUserRolesAndClaims]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwUserRolesAndClaims]
AS
SELECT        dbo.AspNetUsers.Id AS tblUsersUserId, dbo.AspNetUsers.FirstName, dbo.AspNetUsers.LastName, dbo.AspNetUsers.PhoneNumber, dbo.AspNetUsers.UserName, dbo.AspNetUsers.NormalizedUserName, 
                         dbo.AspNetUsers.Email, dbo.AspNetUsers.NormalizedEmail, dbo.AspNetUserRoles.UserId, dbo.AspNetUserRoles.RoleId, dbo.AspNetRoles.Id AS tblRolesId, dbo.AspNetRoles.Name AS roleName, 
                         dbo.AspNetRoles.NormalizedName AS roleNormalizedName
FROM            dbo.AspNetUsers INNER JOIN
                         dbo.AspNetUserRoles ON dbo.AspNetUsers.Id = dbo.AspNetUserRoles.UserId INNER JOIN
                         dbo.AspNetRoles ON dbo.AspNetUserRoles.RoleId = dbo.AspNetRoles.Id
GO
/****** Object:  StoredProcedure [dbo].[spcAsAccountSchemeDelete]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcAsAccountSchemeDelete]
@asIDpk int

AS
BEGIN
	BEGIN TRANSACTION;

	DELETE FROM dbo.tblAsAccountScheme
	 WHERE asIDpk=@asIDpk;

	COMMIT TRANSACTION;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcAsAccountSchemeExits]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcAsAccountSchemeExits]
@asTypeIDfk int
,@asSchemeName nvarchar(150)

AS
BEGIN
	--DECLARE @RECORD_COUNT INT

	SELECT /*@RECORD_COUNT=*/count(asIDpk)
	FROM dbo.tblAsAccountScheme
	WHERE asTypeIDfk = @asTypeIDfk 
	AND lower(asSchemeName) = lower(replace(trim(@asSchemeName), '''', '""'));

	--RETURN @RECORD_COUNT;
END
GO
/****** Object:  StoredProcedure [dbo].[spcAsAccountSchemeGetAll]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcAsAccountSchemeGetAll]

AS
BEGIN

	SELECT asIDpk
		,asTypeIDfk
		,replace(asSchemeName, '""', '''')asSchemeName
	FROM dbo.tblAsAccountScheme

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcAsAccountSchemeGetSingle]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcAsAccountSchemeGetSingle]
@asIDpk int

AS
BEGIN

	SELECT asIDpk
		,asTypeIDfk
		,replace(asSchemeName, '""', '''')asSchemeName
	FROM dbo.tblAsAccountScheme
	WHERE asIDpk = @asIDpk;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcAsAccountSchemeInsert]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcAsAccountSchemeInsert]
@asTypeIDfk int
,@asSchemeName nvarchar(150)

AS
BEGIN
	DECLARE @RC INT;
	BEGIN TRANSACTION;

	INSERT INTO dbo.tblAsAccountScheme
			   (asTypeIDfk
			   ,asSchemeName)
		 VALUES
			   (@asTypeIDfk
			   ,replace(trim(@asSchemeName), '''', '""'));

	COMMIT TRANSACTION;

	SET @RC = SCOPE_IDENTITY();
	RETURN @RC;
END
GO
/****** Object:  StoredProcedure [dbo].[spcAsAccountSchemeUpdate]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcAsAccountSchemeUpdate]
@asIDpk int
,@asTypeIDfk int
,@asSchemeName nvarchar(150)
,@EditedDate datetime2(7)

AS
BEGIN
	BEGIN TRANSACTION;

	UPDATE dbo.tblAsAccountScheme
	   SET asTypeIDfk = @asTypeIDfk
		  ,asSchemeName = replace(trim(@asSchemeName), '''', '""')
		  ,EditedDate = format(@EditedDate, 'yyyy-MM-dd HH:mm:ss')
	 WHERE asIDpk=@asIDpk;

	COMMIT TRANSACTION;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcGetAllCreditAccountsForUser]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcGetAllCreditAccountsForUser]
	@Email nvarchar(MAX)
	,@userId nvarchar(MAX)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT ucaIDpk
		,tIDpk
		,asIDpk
		,replace(tTypeName, '""', '''')tTypeName
		,replace(asSchemeName, '""', '''')asSchemeName
		,replace(ucaAccountNumber, '""', '''')ucaAccountNumber
		,ucaCreationDate
	FROM dbo.vwUcaUserCreditAccounts
	  WHERE lower(userId) = lower(replace(trim(@userId), '''', '""'))
	  OR lower(Email) = lower(replace(trim(@Email), '''', '""'))
	  ORDER BY ucaIDpk;

END
GO
/****** Object:  StoredProcedure [dbo].[spcGetAllSchemeByType]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcGetAllSchemeByType]
@asTypeIDfk int

AS
BEGIN

	SELECT asIDpk
		,asTypeIDfk
		,replace(asSchemeName, '""', '''')asSchemeName
	FROM dbo.tblAsAccountScheme
	WHERE asTypeIDfk = @asTypeIDfk;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcGetAllTypesAndSchemes]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcGetAllTypesAndSchemes]

AS
BEGIN

	SET NOCOUNT ON;

	SELECT asIDpk
		  ,tIDpk
		  ,replace(tTypeName, '""', '''')tTypeName
		  ,replace(asSchemeName, '""', '''')asSchemeName
	  FROM dbo.vwTnsTypeAndSchemes
	  ORDER BY tTypeName,asSchemeName;

END
GO
/****** Object:  StoredProcedure [dbo].[spcGetSingleCreditAccountsForUser]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcGetSingleCreditAccountsForUser]
	@ucaIDpk int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT ucaIDpk
		,tIDpk
		,asIDpk
		,replace(tTypeName, '""', '''')tTypeName
		,replace(asSchemeName, '""', '''')asSchemeName
		,replace(ucaAccountNumber, '""', '''')ucaAccountNumber
		,ucaCreationDate
	FROM dbo.vwUcaUserCreditAccounts
	  WHERE ucaIDpk = @ucaIDpk;

END
GO
/****** Object:  StoredProcedure [dbo].[spcTTypeDelete]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcTTypeDelete]
@tIDpk int

AS
BEGIN
	BEGIN TRANSACTION;

	DELETE FROM dbo.tblTType
	 WHERE tIDpk=@tIDpk;

	COMMIT TRANSACTION;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcTTypeGetAll]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcTTypeGetAll]

AS
BEGIN

	SELECT tIDpk
		,replace(tTypeName, '""', '''')tTypeName
	FROM dbo.tblTType;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcTTypeGetSingle]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcTTypeGetSingle]
@tIDpk int

AS
BEGIN

	SELECT tIDpk
		,replace(tTypeName, '""', '''')tTypeName
	FROM dbo.tblTType
	WHERE tIDpk = @tIDpk;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcTTypeInsert]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcTTypeInsert]
@tTypeName nvarchar(100)

AS
BEGIN
	DECLARE @RC INT;
	BEGIN TRANSACTION;

	INSERT INTO dbo.tblTType
			   (tTypeName)
		 VALUES
			   (replace(trim(@tTypeName), '''', '""'));
	COMMIT TRANSACTION;

	SET @RC = SCOPE_IDENTITY();
	RETURN @RC;
END
GO
/****** Object:  StoredProcedure [dbo].[spcTTypeUpdate]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcTTypeUpdate]
@tIDpk int
,@tTypeName nvarchar(100)
,@EditedDate datetime2(7)

AS
BEGIN
	BEGIN TRANSACTION;

	UPDATE dbo.tblTType
	   SET tTypeName = replace(trim(@tTypeName), '''', '""')
		  ,EditedDate = format(@EditedDate, 'yyyy-MM-dd HH:mm:ss')
	 WHERE tIDpk=@tIDpk;

	COMMIT TRANSACTION;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcUcaUserCreditAccountsDelete]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcUcaUserCreditAccountsDelete]
@ucaIDpk int

AS
BEGIN
	BEGIN TRANSACTION;

	DELETE FROM dbo.tblUcaUserCreditAccounts
	 WHERE ucaIDpk=@ucaIDpk;

	COMMIT TRANSACTION;

	RETURN @@Error;
END
GO
/****** Object:  StoredProcedure [dbo].[spcUcaUserCreditAccountsGetAll]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcUcaUserCreditAccountsGetAll]
	
AS
BEGIN

	SET NOCOUNT ON;

		SELECT ucaIDpk
			,ucaUserIDfk
			,userId
			,replace(FirstName, '""', '''')FirstName
			,replace(LastName, '""', '''')LastName
			,replace(Email, '""', '''')Email
			,replace(UserName, '""', '''')UserName
			,replace(PhoneNumber, '""', '''')PhoneNumber
			,ucaTypeIDfk
			,tIDpk
			--,asTypeIDfk
			,ucaSchemeIDfk
			,asIDpk
			,replace(tTypeName, '""', '''')tTypeName
			,replace(asSchemeName, '""', '''')asSchemeName
			,replace(ucaAccountNumber, '""', '''')ucaAccountNumber
			,ucaCreationDate
			,ucaEditedDate
		FROM dbo.vwUcaUserCreditAccounts
		ORDER BY UserName;

END
GO
/****** Object:  StoredProcedure [dbo].[spcUcaUserCreditAccountsInsert]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcUcaUserCreditAccountsInsert]
@ucaUserIDfk nvarchar(50)
,@ucaTypeIDfk int
,@ucaSchemeIDfk int
,@ucaAccountNumber nvarchar(50)

AS
BEGIN
	DECLARE @RC INT;
	BEGIN TRANSACTION;

	INSERT INTO dbo.tblUcaUserCreditAccounts
			   (ucaUserIDfk
			   ,ucaTypeIDfk
			   ,ucaSchemeIDfk
			   ,ucaAccountNumber)
		 VALUES
			   (replace(trim(@ucaUserIDfk), '''', '""')
			   ,@ucaTypeIDfk
			   ,@ucaSchemeIDfk
			   ,replace(trim(@ucaAccountNumber), '''', '""'));

	COMMIT TRANSACTION;

	SET @RC = SCOPE_IDENTITY();
	RETURN @RC;
END
GO
/****** Object:  StoredProcedure [dbo].[spcUcaUserCreditAccountsUpdate]    Script Date: 19/05/2023 6:27:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Matthew Oduamafu
-- Create date: 16TH May, 2023
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spcUcaUserCreditAccountsUpdate]
@ucaIDpk int
,@ucaUserIDfk nvarchar(50)
,@ucaTypeIDfk int
,@ucaSchemeIDfk int
,@ucaAccountNumber nvarchar(50)
,@ucaEditedDate datetime2(7)

AS
BEGIN
	BEGIN TRANSACTION;

	UPDATE dbo.tblUcaUserCreditAccounts
	SET ucaUserIDfk = replace(trim(@ucaUserIDfk), '''', '""')
		,ucaTypeIDfk = @ucaTypeIDfk
		,ucaSchemeIDfk = @ucaSchemeIDfk
		,ucaAccountNumber = @ucaAccountNumber
		,ucaEditedDate = format(@ucaEditedDate, 'yyyy-MM-dd HH:mm:ss')
	 WHERE ucaIDpk=@ucaIDpk;

	COMMIT TRANSACTION;

	RETURN @@Error;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[13] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblAsAccountScheme"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "tblTType"
            Begin Extent = 
               Top = 24
               Left = 405
               Bottom = 154
               Right = 575
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwTnsTypeAndSchemes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwTnsTypeAndSchemes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[32] 2[18] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "tblUcaUserCreditAccounts"
            Begin Extent = 
               Top = 7
               Left = 436
               Bottom = 205
               Right = 633
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblTType"
            Begin Extent = 
               Top = 182
               Left = 241
               Bottom = 312
               Right = 411
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblAsAccountScheme"
            Begin Extent = 
               Top = 152
               Left = 18
               Bottom = 307
               Right = 192
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2295
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3135
         Alias = 900
         Table = 1170
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUcaUserCreditAccounts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUcaUserCreditAccounts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUcaUserCreditAccounts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[18] 2[9] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspNetRoles"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 20
               Left = 528
               Bottom = 150
               Right = 752
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUserRoles"
            Begin Extent = 
               Top = 39
               Left = 274
               Bottom = 187
               Right = 444
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2985
         Alias = 2955
         Table = 1995
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUserRolesAndClaims'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUserRolesAndClaims'
GO
