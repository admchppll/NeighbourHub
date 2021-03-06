USE [Volunteer]
GO
/****** Object:  StoredProcedure [dbo].[createGeoLocationAddress]    Script Date: 24/04/2017 03:46:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[createGeoLocationAddress] (@AddressID INT)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	UPDATE	Address
	SET	GeoLocation = GEOGRAPHY::Point(Lat, Long, 4326)
	WHERE	ID = @AddressID
END

GO
