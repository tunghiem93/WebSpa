INSERT INTO [dbo].[CMS_Employee] (
		[ID]
		,[Name] 
		,[Email]  
		,[Password] 
		,[IsActive]  
		,[Status] 
		,[CreatedDate] 
		,[CreatedUser] 
		,[ModifiedUser]  
		,[LastModified]  
		,[Phone]  
		,[PinCode]  
		,[Gender]    
		,[Marital] 
		,[HiredDate]
		,[BirthDate]
		,[Street]  
		,[City]
		,[ZipCode] 
		,[Country] 
		,[ImageUrl]
		,[StoreID]
		,[IsSupperAdmin])
VALUES 
		('123abcdafaafsda'
		,'admin' 
		,'admin@gmail.com' 
		,'5bodbeBGJXU=' 
		,1
		,1 
		,getdate()
		,'initial'
		,'initial' 
		,getdate() 
		,''
		,1234512345 
		,1
		,0 
		,GETDATE() 
		,GETDATE()
		,''
		,''
		,''
		,''
		,''
		,'Spa'
		,1)
