-- Table creation

CREATE TABLE ConsumerUser(
	Id INT PRIMARY KEY AUTO_INCREMENT,
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
	Id INT PRIMARY KEY AUTO_INCREMENT,
    ConsumerUserId INT NOT NULL REFERENCES ConsumerUser(Id),
    Title VARCHAR(255) NOT NULL,
    CurrentUsage INT NOT NULL,
    UsageLimit INT NOT NULL,
    Unit VARCHAR(255) NOT NULL
);

CREATE TABLE AdminUser(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(255) NOT NULL UNIQUE,
    `Password` VARCHAR(255) NOT NULL
);

CREATE TABLE Banner(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    PostCoverUrl TEXT NOT NULL,
    PostTitle VARCHAR(255) NOT NULL,
    PostShortDesc TEXT,
    PostUrl TEXT NOT NULL
);

CREATE TABLE MemberLevel(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    `Level` INT NOT NULL,
    LevelText VARCHAR(255) NOT NULL,
    Threshold INT NOT NULL
);

CREATE TABLE Reward(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    Title VARCHAR(255) NOT NULL,
    `Description` TEXT NOT NULL,
    PointsRequired INT NOT NULL,
    ExtraCashRequired BOOL NOT NULL,
    ExtraCashAmount INT,
    ExpiryDate TIMESTAMP NOT NULL,
    MinimumMemberLevelId INT NOT NULL REFERENCES MemberLevel(Id)
);

CREATE TABLE RewardRedemption(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    ConsumerUserId INT NOT NULL REFERENCES ConsumerUser(Id),
    RewardTitle VARCHAR(255) NOT NULL,
    PointsSpent INT NOT NULL,
    ExtraCashSpent INT NOT NULL,
    RedemptionDate TIMESTAMP NOT NULL
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

	SELECT TotalRewardPoints INTO TotalPoints FROM ConsumerUser cu WHERE cu.PhoneNumber = PhoneNumber;
    SELECT `Level`, LevelText INTO MemberLevel, MemberLevelText FROM MemberLevel
    WHERE TotalPoints >= Threshold
    ORDER BY Threshold DESC
    LIMIT 1;
    
    SELECT TotalPoints, MemberLevel, MemberLevelText;
END $$

CREATE PROCEDURE GetConsumerUserUsage(
	IN PhoneNumber VARCHAR(12)
)
BEGIN
	SELECT u.Title, u.CurrentUsage, u.UsageLimit, u.Unit FROM `Usage` u
	JOIN ConsumerUser cu ON cu.Id = u.ConsumerUserId
	WHERE cu.PhoneNumber = PhoneNumber;
END $$

CREATE PROCEDURE RedeemReward(
	IN PhoneNumber VARCHAR(12),
    IN RewardId INT,
    OUT ReturnValue INT
)
sp: BEGIN
    DECLARE CurrentRewardPoints INT DEFAULT NULL;
	DECLARE MinimumLevel INT;
    DECLARE MinimumPoints INT;
    DECLARE ConsumerUserId INT;
    DECLARE RewardTitle VARCHAR(255) DEFAULT NULL;
    DECLARE ExtraCashSpent INT;
    DECLARE RewardExists BOOL;
    
    SELECT EXISTS(SELECT true FROM Reward WHERE Id = RewardId) INTO RewardExists;

	CALL GetUserRewardInfo(PhoneNumber, @TotalPoints, @MemberLevel, @MemberLevelText);
    SELECT r.Title, ml.`Level`, r.PointsRequired, r.ExtraCashAmount INTO RewardTitle, MinimumLevel, MinimumPoints, ExtraCashSpent
    FROM Reward r JOIN MemberLevel ml ON r.MinimumMemberLevelId = ml.Id WHERE r.Id = RewardId;

	IF @TotalPoints = NULL THEN
		SET ReturnValue = -10;
        LEAVE sp;
	ELSEIF @TotalPoints <= 0 THEN
		SET ReturnValue = -20;
        LEAVE sp;
	ELSEIF RewardExists = false THEN
		SET ReturnValue = -50;
        LEAVE sp;
	ELSEIF @MemberLevel > MinimumLevel THEN
		SET ReturnValue = -30;
        LEAVE sp;
	ELSEIF @TotalPoints < MinimumPoints THEN
		SET ReturnValue = -40;
        LEAVE sp;
	END IF;
    
    SELECT Id INTO ConsumerUserId FROM ConsumerUser WHERE PhoneNumber = PhoneNumber;
    
    INSERT INTO RewardRedemption(ConsumerUserId, RewardTitle, PointsSpent, ExtraCashSpent, RedemptionDate)
    VALUES (ConsumerUserId, RewardTitle, MinimumPoints, IFNULL(ExtraCashSpent, 0), NOW());
    
    UPDATE ConsumerUser SET TotalRewardPoints = TotalRewardPoints - MinimumPoints
    WHERE PhoneNumber = PhoneNumber;
    
    SET ReturnValue = 0;
END $$

DELIMITER ;