
/****** Object:  StoredProcedure [dbo].[pr_OpenDecryptedSession]    Script Date: 6/9/2016 9:22:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_OpenDecryptedSession]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_OpenDecryptedSession]
GO

/****** Object:  StoredProcedure [dbo].[pr_OpenDecryptedSession]    Script Date: 6/9/2016 9:22:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[pr_OpenDecryptedSession]  	With Encryption
	 
AS  
	Begin  

		Declare @SymKey varchar(400)                                    
		Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ '''ttwbvXWpqb5WOLfLrBgisw=='''  + ''                                        
		Exec(@SymKey)                      
	End

GO


/****** Object:  StoredProcedure [dbo].[pr_CloseDecryptedSession]    Script Date: 6/9/2016 9:22:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_CloseDecryptedSession]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_CloseDecryptedSession]
GO

/****** Object:  StoredProcedure [dbo].[pr_CloseDecryptedSession]    Script Date: 6/9/2016 9:22:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[pr_CloseDecryptedSession]  With Encryption
AS  
	Begin  
		Close symmetric key Key_CTC  
	End

GO

