-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Omelian Levkovych
-- Create date: 20/01/2022
-- Description:	The stored procedure returns persons by their first and last name.
-- =============================================
CREATE PROCEDURE SelectAllPersonsByName
	-- Add the parameters for the stored procedure here
	@FirstName nvarchar(50), 
	@LastName nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons
	WHERE FirstName = @FirstName AND LastName = @LastName
END
GO
