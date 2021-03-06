/****** SSMS 中 SelectTopNRows 命令的指令碼  ******/
SELECT [LungmenSoftwareData].[dbo].[FoxSysSoftwares].[Name] as Software_Name
      ,[LungmenSoftwareData].[dbo].[FoxSysSoftwares].[Rev] as Software_Rev
      ,[LungmenSoftwareData].[dbo].[FoxWorkStations].Name as Workstation_Name
  FROM [LungmenSoftwareData].[dbo].[FoxSysSoftwares], [LungmenSoftwareData].[dbo].[FoxWorkStations]
  Where [LungmenSoftwareData].[dbo].[FoxSysSoftwares].[FoxWorkStationId]=[LungmenSoftwareData].[dbo].[FoxWorkStations].Id
  Order BY Workstation_Name, Software_Name