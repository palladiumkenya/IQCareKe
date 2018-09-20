IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_InitialFollowupVisit_DateValidate_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_InitialFollowupVisit_DateValidate_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_InitialFollowupVisit_DateValidate_Constella]    Script Date: 2/23/2018 12:04:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InitialFollowupVisit_DateValidate_Constella]                  

(                  

 @Ptn_pk int,                  

 @VisitDate datetime,

 @location int              

)                  

                  

As                  

                  

Begin     

              

select Visit_Id,Ptn_Pk from   ord_Visit where (Deleteflag =0 or deleteFlag is null ) and VisitType in (17)  

and Ptn_Pk =@Ptn_pk and VisitDate =@VisitDate and LocationID=@location  

        

end