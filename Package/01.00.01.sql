/*
Run this script on:

        .\SQL08.ScriptTesting    -  This database will be modified

to synchronize it with:

        .\SQL08.DNNspot_Dev

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.1.0 from Red Gate Software Ltd at 1/27/2010 9:54:35 AM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Altering [dbo].[DNNspot_Store_Product]'
GO
ALTER TABLE [dbo].[DNNspot_Store_Product] ADD
[IsTaxable] [bit] NOT NULL CONSTRAINT [DF_DNNspot_Store_Product_IsTaxable] DEFAULT ((1)),
[CheckoutAssignRoleIds] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_DNNspot_Store_Product_CheckoutAssignRoleIds] DEFAULT ('')
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Refreshing [dbo].[vDNNspot_Store_ProductsSoldCounts]'
GO
EXEC sp_refreshview N'[dbo].[vDNNspot_Store_ProductsSoldCounts]'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Refreshing [dbo].[vDNNspot_Store_MainProductPhoto]'
GO
EXEC sp_refreshview N'[dbo].[vDNNspot_Store_MainProductPhoto]'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vDNNspot_Store_CartItemProductInfo]'
GO




ALTER VIEW [dbo].[vDNNspot_Store_CartItemProductInfo]
AS
SELECT
ci.*
,mainPhoto.MainPhotoFilename
,p.Name as ProductName
,p.Slug as ProductSlug
,p.Price as ProductItemPrice
,p.[Weight] as ProductWeight
,p.ShippingAdditionalFeePerItem as ProductShippingAdditionalFeePerItem
,p.DeliveryMethodId as ProductDeliveryMethodId
,p.IsTaxable as ProductIsTaxable
FROM DNNspot_Store_CartItem ci
INNER JOIN DNNspot_Store_Product p ON ci.ProductId = p.Id
LEFT JOIN vDNNspot_Store_MainProductPhoto mainPhoto ON p.Id = mainPhoto.ProductId







GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
