
-- Store
INSERT [dbo].[CMS_Store] 
		([ID]
		, [CompanyID]
		, [Name]
		, [IndustryID]
		, [Description]
		, [Address]
		, [IsActive]
		, [Street]
		, [City]
		, [Country]
		, [Zipcode]
		, [GSTRegNo]
		, [TimeZone]
		, [Status]
		, [CreatedDate]
		, [CreatedUser]
		, [ModifiedUser]
		, [LastModified]
		, [ImageUrl]
		, [Email]
		, [Phone]
		, [StoreCode]) 
VALUES 
		(N'Spa'
		, N'1'
		, N'Spa'
		, N'1'
		, N'1'
		, N'1'
		, 1
		, N'1'
		, N'1'
		, N'1'
		, N'1'
		, N'1'
		, N'1'
		, 1
		, CAST(N'2018-07-24 00:00:00.000' AS DateTime)
		, N'1'
		, N'1'
		, CAST(N'2018-07-24 00:00:00.000' AS DateTime)
		, NULL
		, N'voquoctuanqn@gmail.com'
		, N'0975224063'
		, NULL
		)
