-- Table creation

CREATE TABLE ConsumerUser(
	Id INT PRIMARY KEY,
    PhoneNumber VARCHAR(12) NOT NULL UNIQUE,
    `Password` VARCHAR(255) NOT NULL,
    `Name` VARCHAR(80) NOT NULL,
    IsActive BOOL NOT NULL,
    IsRoamingActivated BOOL NOT NULL,
    IsIDDActivated BOOL NOT NULL,
    EmailAddress VARCHAR(255) NOT NULL UNIQUE,
    TotalRewardPoints INT NOT NULL
);

CREATE TABLE `Usage`(
	Id INT PRIMARY KEY,
    ConsumerUserId INT NOT NULL REFERENCES ConsumerUser(Id),
    Title VARCHAR(255) NOT NULL,
    CurrentUsage INT NOT NULL,
    UsageLimit INT NOT NULL,
    Unit VARCHAR(255) NOT NULL
);

CREATE TABLE AdminUser(
	Id INT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL UNIQUE,
    `Password` VARCHAR(255) NOT NULL
);

CREATE TABLE Banner(
	Id INT PRIMARY KEY,
    PostCoverUrl TEXT NOT NULL,
    PostTitle VARCHAR(255) NOT NULL,
    PostShortDesc TEXT NOT NULL,
    PostUrl TEXT NOT NULL
);

CREATE TABLE MemberLevel(
	Id INT PRIMARY KEY,
    `Level` INT NOT NULL,
    LevelText VARCHAR(255) NOT NULL,
    Threshold INT NOT NULL
);

CREATE TABLE Reward(
	Id INT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    `Description` TEXT NOT NULL,
    PointsRequired INT NOT NULL,
    ExtraCashRequired BOOL NOT NULL,
    ExtraCashAmount INT,
    ExpiryDate TIMESTAMP NOT NULL,
    MinimumMemberLevelId INT NOT NULL REFERENCES MemberLevel(Id)
);

-- Stored procedures

DELIMITER $$

CREATE PROCEDURE GetUserRewardInfo(
	IN PhoneNumber VARCHAR(12)
)
BEGIN
	DECLARE TotalPoints INT DEFAULT 0;
    DECLARE MemberLevel INT DEFAULT 0;
    DECLARE MemberLevelText VARCHAR(255);

	SELECT TotalRewardPoints INTO TotalPoints FROM CustomerUser cu WHERE cu.PhoneNumber = PhoneNumber;
    SELECT `Level`, LevelText INTO MemberLevel, MemberLevelText FROM MemberLevel
    WHERE TotalPoints >= Threshold
    ORDER BY Threshold DESC
    LIMIT 1;
    
    SELECT TotalPoints, MemberLevel, MemberLevelText;
END $$

DELIMITER ;