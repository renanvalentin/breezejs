CREATE PROCEDURE DocumentById 
	@PIN varchar(30)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT PIN FROM Document WHERE PIN = @PIN
END
GO

CREATE PROCEDURE VerifyExintingEmail
	@EMAIL varchar(30)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(Email) FROM [User] WHERE EMAIL = @EMAIL
END
GO

CREATE PROCEDURE InsertUser
	@PIN nvarchar(128), 
	@CompanyName nvarchar(128), 
	@Email nvarchar(30), 
	@Password nvarchar(30), 
	@ReceivePromotions bit, 
	@CallSpecialist bit, 
	@Number bigint, 
	@StreetName nvarchar(10), 
	@Suite nvarchar(max), 
	@City nvarchar(30), 
	@State nvarchar(30), 
	@Zipcode nvarchar(30), 
	@MainContact nvarchar(30), 
	@Position nvarchar(30), 
	@Phone nvarchar(30)
AS 
BEGIN
  INSERT INTO [User] (Suite, CallSpecialist, City, CompanyName, Email, MainContact, Number, PIN, [Password], Phone, Position, ReceivePromotions, [State], StreetName, Zipcode)
  VALUES (@Suite, @CallSpecialist, @City, @CompanyName, @Email, @MainContact, @Number, @PIN, @Password, @Phone, @Position, @ReceivePromotions, @State, @StreetName, @Zipcode)

  SELECT SCOPE_IDENTITY() AS Id
END 
GO

--exec DocumentById @PIN = ''
--exec UserByEmail @EMAIL = 'a@a.com'