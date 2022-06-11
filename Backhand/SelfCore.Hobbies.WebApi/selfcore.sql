/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50732
 Source Host           : localhost:3306
 Source Schema         : selfcore

 Target Server Type    : MySQL
 Target Server Version : 50732
 File Encoding         : 65001

 Date: 11/06/2022 13:02:20
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for books
-- ----------------------------
DROP TABLE IF EXISTS `books`;
CREATE TABLE `books`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '书名',
  `Author` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '作者',
  `BookType` tinyint(4) NULL DEFAULT NULL COMMENT '类型',
  `Picture` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '插图',
  `Brief` tinytext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '简介',
  `Adress` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文档地址',
  `Creatime` datetime NULL DEFAULT NULL COMMENT '创建时间',
  `Creator` int(11) NULL DEFAULT NULL COMMENT '创建人',
  `IsDeleted` bit(1) NOT NULL COMMENT '删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '书籍' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for foods
-- ----------------------------
DROP TABLE IF EXISTS `foods`;
CREATE TABLE `foods`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Picture` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片',
  `Type` tinyint(4) NOT NULL COMMENT '来源',
  `TravelId` int(11) NULL DEFAULT NULL COMMENT '旅游主键',
  `Remark` tinytext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '感想',
  `Creatime` datetime NULL DEFAULT NULL COMMENT '创建时间',
  `Creator` int(11) NULL DEFAULT NULL COMMENT '创建人',
  `IsDeleted` bit(1) NOT NULL COMMENT '删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '美食' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for travels
-- ----------------------------
DROP TABLE IF EXISTS `travels`;
CREATE TABLE `travels`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `TravelType` tinyint(4) NOT NULL COMMENT '类型',
  `MianPic` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '主图',
  `Location` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '旅行地点',
  `TravelDate` date NOT NULL COMMENT '旅行时间',
  `Companion` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '同伴',
  `Cost` float(8, 2) NULL DEFAULT NULL COMMENT '费用',
  `Remark` tinytext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '感想',
  `Creatime` datetime NULL DEFAULT NULL COMMENT '创建时间',
  `Creator` int(11) NULL DEFAULT NULL COMMENT '创建人',
  `IsDeleted` bit(1) NOT NULL COMMENT '删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '旅游' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '昵称',
  `Code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `Psd` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
  `Gender` bit(1) NULL DEFAULT NULL COMMENT '性别',
  `Birthday` date NULL DEFAULT NULL COMMENT '生日',
  `Headshot` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像',
  `IsAdmin` bit(1) NULL DEFAULT NULL COMMENT '是否管理员',
  `Creatime` datetime NULL DEFAULT NULL COMMENT '创建时间',
  `Creator` int(11) NULL DEFAULT NULL COMMENT '创建人',
  `IsDeleted` bit(1) NULL DEFAULT NULL COMMENT '删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
