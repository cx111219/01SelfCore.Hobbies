# 01SelfCore.Hobbies
为了学习并整理.net相关的专业技能，我会通过实现自己感兴趣的项目记录遇到的技能点。

<img src="../01SelfCore.Hobbies/selftcore.png" style="zoom:50%;"/>

## 🐏技术栈

受DDD设计思想，以及开源框架的影响，对设计模式，架构有模糊概念。在后面一系列的项目中会進一步學習，清晰落实。

当前项目业务简单，使用自己最舒适的结构 -- 没有结构，怎么舒适怎么来！

backend：asp.net core(5.0) + efcore + mysql(5.7.32-mysql) 
frontend: angular + ng-alain

## 🦆业务说明

SelfCore.Hobbies - 记录个人兴趣：
  - 书籍
    提供CURD, download, preview.
  - 旅行
    提供CURD , upload,类似朋友圈模式
  - 美食

## 🐕文件结构
 - SelfCore.Hobbies.Domains  记录实体相关

 - SelfCore.Hobbies.Services  记录公共服务相关

 - SelfCore.Hobbies.WebApi 记录api

   

## ❓值得记录的问题

1. 使用反向工程

   之前工作常用CodeSmiths 模板化生成代码，这次尝试使用efcore包管理控制台工具生成models,简单业务可以尝试。

   使用条件：

   - 在进行反向工程之前，需要安装 [PMC 工具](https://docs.microsoft.com/zh-cn/ef/core/cli/powershell)（仅限 Visual Studio）或 [CLI 工具](https://docs.microsoft.com/zh-cn/ef/core/cli/dotnet)。 有关详细信息，请参阅链接。

   - 在要为其搭建基架的项目中安装NuGet包`Microsoft.EntityFrameworkCore.Design`。

   - 还需要为要进行反向工程的数据库架构安装适当的[数据库提供程序](https://docs.microsoft.com/zh-cn/ef/core/providers/)。

     


