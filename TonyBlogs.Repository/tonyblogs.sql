/*
Navicat MySQL Data Transfer


Date: 2017-08-12 21:08:32
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for blogarticle
-- ----------------------------
DROP TABLE IF EXISTS `blogarticle`;
CREATE TABLE `blogarticle` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(200) DEFAULT NULL,
  `Category` varchar(50) DEFAULT NULL,
  `Content` longtext,
  `Traffic` int(11) DEFAULT NULL,
  `CommentNum` int(11) DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Remark` varchar(500) DEFAULT NULL,
  `UserID` mediumtext,
  `RealName` varchar(50) DEFAULT NULL,
  `Summary` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=77 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for blogcomment
-- ----------------------------
DROP TABLE IF EXISTS `blogcomment`;
CREATE TABLE `blogcomment` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `BlogID` bigint(20) NOT NULL,
  `UserID` bigint(20) NOT NULL,
  `Content` varchar(500) DEFAULT NULL,
  `InsertTime` datetime DEFAULT NULL,
  `RealName` varchar(50) DEFAULT NULL,
  `ParentID` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for blogtrafficlog
-- ----------------------------
DROP TABLE IF EXISTS `blogtrafficlog`;
CREATE TABLE `blogtrafficlog` (
  `BlogID` bigint(20) NOT NULL,
  `IP` varchar(20) NOT NULL,
  `InsertTime` datetime DEFAULT NULL,
  PRIMARY KEY (`BlogID`,`IP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for purview
-- ----------------------------
DROP TABLE IF EXISTS `purview`;
CREATE TABLE `purview` (
  `PurviewID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Submitter` varchar(50) DEFAULT NULL,
  `PurviewTitle` varchar(50) DEFAULT NULL,
  `PurviewContent` varchar(200) DEFAULT NULL,
  `PurviewFuncIDs` varchar(1000) DEFAULT NULL,
  `InsertTime` datetime DEFAULT NULL,
  PRIMARY KEY (`PurviewID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for userfunction
-- ----------------------------
DROP TABLE IF EXISTS `userfunction`;
CREATE TABLE `userfunction` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `SortNum` int(11) DEFAULT NULL,
  `FuncLevel` int(11) DEFAULT NULL,
  `ParentID` bigint(20) NOT NULL,
  `FucTitle` varchar(50) DEFAULT NULL,
  `FuncContent` varchar(200) DEFAULT NULL,
  `AreaName` varchar(50) DEFAULT '',
  `ControllerName` varchar(50) DEFAULT '',
  `ActionName` varchar(50) DEFAULT '',
  `FuncType` int(11) DEFAULT NULL,
  `FuncStatus` int(11) DEFAULT NULL,
  `Icon` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `UserID` bigint(20) NOT NULL AUTO_INCREMENT,
  `LoginName` varchar(50) NOT NULL,
  `LoginPWD` varchar(50) NOT NULL,
  `RealName` varchar(50) DEFAULT NULL,
  `UserStatus` int(11) DEFAULT NULL,
  `PurviewID` bigint(11) NOT NULL,
  `InsertTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;


-- ----------------------------
-- Records of purview
-- ----------------------------
INSERT INTO `purview` VALUES ('1', null, '管理员', '管理员', '9,10,11,12,15,16,17', '2017-07-03 11:06:30');
INSERT INTO `purview` VALUES ('7', null, '博客作者', '博客作者', '15,16,17', '2017-07-03 19:10:20');

-- ----------------------------
-- Records of userfunction
-- ----------------------------
INSERT INTO `userfunction` VALUES ('9', '10', '1', '0', '系统管理', '系统管理', '', '', '', '1', '1', 'icon-home');
INSERT INTO `userfunction` VALUES ('10', '1', '2', '9', '功能管理', '功能管理', 'admin', 'function', 'index', '2', '1', '');
INSERT INTO `userfunction` VALUES ('11', '2', '2', '9', '权限管理', '权限管理', 'admin', 'purview', 'index', '2', '1', '');
INSERT INTO `userfunction` VALUES ('12', '3', '2', '9', '用户管理', '用户管理', 'admin', 'userinfo', 'index', '2', '1', '');
INSERT INTO `userfunction` VALUES ('14', '7', '1', '0', '测试', null, '', '', '', '1', '1', null);
INSERT INTO `userfunction` VALUES ('15', '9', '1', '0', '博客管理', '博客管理', '', '', '', '1', '1', 'icon-speech');
INSERT INTO `userfunction` VALUES ('16', '1', '2', '15', '录入编辑', '录入编辑', 'Admin', 'BlogArticle', 'AddOrEdit', '2', '1', null);
INSERT INTO `userfunction` VALUES ('17', '2', '2', '15', '博客列表', '博客列表', 'Admin', 'BlogArticle', 'Index', '2', '1', null);
INSERT INTO `userfunction` VALUES ('18', '11', '1', '0', null, null, '', '', '', '0', '9', null);
INSERT INTO `userfunction` VALUES ('19', '1', '2', '18', null, null, '', '', '', '0', '9', null);