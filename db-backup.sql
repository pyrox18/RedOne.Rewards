-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: redone_rewards
-- ------------------------------------------------------
-- Server version	8.0.23

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `adminuser`
--

DROP TABLE IF EXISTS `adminuser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adminuser` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adminuser`
--

LOCK TABLES `adminuser` WRITE;
/*!40000 ALTER TABLE `adminuser` DISABLE KEYS */;
INSERT INTO `adminuser` VALUES (1,'admin','AQAAAAEAACcQAAAAEOMlhiH3GqSTc78S48MmT2tM6egfloCOks3FiH17eoyP8DFmP+M03LNQ2OAyGTtoJw==');
/*!40000 ALTER TABLE `adminuser` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `banner`
--

DROP TABLE IF EXISTS `banner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `banner` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostCoverUrl` text NOT NULL,
  `PostTitle` varchar(255) NOT NULL,
  `PostShortDesc` text,
  `PostUrl` text NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `banner`
--

LOCK TABLES `banner` WRITE;
/*!40000 ALTER TABLE `banner` DISABLE KEYS */;
INSERT INTO `banner` VALUES (2,'https://www.redone.com.my/assets/uploads/FREE-1GB-DATA-webslider-Mobile-480x250-ENG.jpg','FREE 1GB Data Every Day!\r\n',NULL,'https://www.redone.com.my/promotion/free_1gb_everyday');
/*!40000 ALTER TABLE `banner` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `consumeruser`
--

DROP TABLE IF EXISTS `consumeruser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consumeruser` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PhoneNumber` varchar(12) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Name` varchar(80) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsRoamingActivated` tinyint(1) NOT NULL,
  `IsIDDActivated` tinyint(1) NOT NULL,
  `EmailAddress` varchar(255) NOT NULL,
  `TotalRewardPoints` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `PhoneNumber` (`PhoneNumber`),
  UNIQUE KEY `EmailAddress` (`EmailAddress`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consumeruser`
--

LOCK TABLES `consumeruser` WRITE;
/*!40000 ALTER TABLE `consumeruser` DISABLE KEYS */;
INSERT INTO `consumeruser` VALUES (1,'018009999','AQAAAAEAACcQAAAAECTAdsqA7vJ8xW+zIWZ/0rFqcUBN4KR2k7uZktBXcHh4p1ps0kpT07jY72MfyeVU9w==','Test User',1,0,0,'test@user.com',1400);
/*!40000 ALTER TABLE `consumeruser` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `memberlevel`
--

DROP TABLE IF EXISTS `memberlevel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `memberlevel` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Level` int NOT NULL,
  `LevelText` varchar(255) NOT NULL,
  `Threshold` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `memberlevel`
--

LOCK TABLES `memberlevel` WRITE;
/*!40000 ALTER TABLE `memberlevel` DISABLE KEYS */;
INSERT INTO `memberlevel` VALUES (1,1,'Platinum',1200),(2,2,'Gold',1000),(3,3,'Silver',500),(4,4,'Bronze',0);
/*!40000 ALTER TABLE `memberlevel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reward`
--

DROP TABLE IF EXISTS `reward`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reward` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) NOT NULL,
  `Description` text NOT NULL,
  `PointsRequired` int NOT NULL,
  `ExtraCashRequired` tinyint(1) NOT NULL,
  `ExtraCashAmount` int DEFAULT NULL,
  `ExpiryDate` timestamp NOT NULL,
  `MinimumMemberLevelId` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reward`
--

LOCK TABLES `reward` WRITE;
/*!40000 ALTER TABLE `reward` DISABLE KEYS */;
INSERT INTO `reward` VALUES (2,'Test Reward 2','Lorem ipsum',200,0,NULL,'2022-05-08 19:38:16',2);
/*!40000 ALTER TABLE `reward` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rewardredemption`
--

DROP TABLE IF EXISTS `rewardredemption`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rewardredemption` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ConsumerUserId` int NOT NULL,
  `RewardTitle` varchar(255) NOT NULL,
  `PointsSpent` int NOT NULL,
  `ExtraCashSpent` int NOT NULL,
  `RedemptionDate` timestamp NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rewardredemption`
--

LOCK TABLES `rewardredemption` WRITE;
/*!40000 ALTER TABLE `rewardredemption` DISABLE KEYS */;
INSERT INTO `rewardredemption` VALUES (1,1,'Test Reward 2',200,0,'2021-05-12 02:55:00'),(2,1,'Test Reward 2',200,0,'2021-05-12 03:03:38'),(3,1,'Test Reward 2',200,0,'2021-05-12 03:03:48');
/*!40000 ALTER TABLE `rewardredemption` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usage`
--

DROP TABLE IF EXISTS `usage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usage` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ConsumerUserId` int NOT NULL,
  `Title` varchar(255) NOT NULL,
  `CurrentUsage` int NOT NULL,
  `UsageLimit` int NOT NULL,
  `Unit` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usage`
--

LOCK TABLES `usage` WRITE;
/*!40000 ALTER TABLE `usage` DISABLE KEYS */;
INSERT INTO `usage` VALUES (1,1,'Internet',30,100,'GB'),(2,1,'Call',50,500,'minutes'),(3,1,'Text',20,100,'SMS');
/*!40000 ALTER TABLE `usage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'redone_rewards'
--
/*!50003 DROP PROCEDURE IF EXISTS `GetConsumerUserUsage` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `GetConsumerUserUsage`(
	IN PhoneNumber VARCHAR(12)
)
BEGIN
	SELECT u.Title, u.CurrentUsage, u.UsageLimit, u.Unit FROM `Usage` u
	JOIN ConsumerUser cu ON cu.Id = u.ConsumerUserId
	WHERE cu.PhoneNumber = PhoneNumber;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserRewardInfo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `GetUserRewardInfo`(
	IN PhoneNumber VARCHAR(12),
    OUT TotalPoints INT,
    OUT MemberLevel INT,
    OUT MemberLevelText VARCHAR(255)
)
BEGIN
	SELECT TotalRewardPoints INTO TotalPoints FROM ConsumerUser cu WHERE cu.PhoneNumber = PhoneNumber;
    SELECT `Level`, LevelText INTO MemberLevel, MemberLevelText FROM MemberLevel
    WHERE TotalPoints >= Threshold
    ORDER BY Threshold DESC
    LIMIT 1;
    
    SELECT TotalPoints, MemberLevel, MemberLevelText;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `RedeemReward` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `RedeemReward`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-05-12 11:14:01
