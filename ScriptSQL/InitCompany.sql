USE [dbLamode6]
GO

INSERT INTO [dbo].[CMS_Companies]
           ([Id]
           ,[Name]
           ,[Description]
           ,[Email]
           ,[Phone]
           ,[Address]
           ,[LinkBlog]
           ,[LinkFB]
           ,[LinkTwiter]
           ,[LinkInstagram]
           ,[ImageURL]
           ,[BusinessHour]
           ,[IsActive]
           ,[CreatedBy]
           ,[UpdatedDate]
           ,[UpdatedBy]
           ,[CreatedDate])
     VALUES
           ('0fc574e0-3db9-4253-8fe9-9c3ecb1af2e1'
           ,'Lamode Company'
           ,'Always be the best.'
           ,'lamode@gmail.com'
           ,'123'
           ,'181/29 Âu Dương Lân, P.2, Q8'
		   ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,'initial'
           ,GETDATE()
           ,'initial'
           ,GETDATE())
GO


