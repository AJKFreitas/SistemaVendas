CREATE DATABASE  IF NOT EXISTS `db_vendas` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_vendas`;
-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: db_vendas
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20200212135513_Start_db','3.1.2'),('20200213184936_Add_user_role','3.1.2'),('20200213210229_Add_user_IsAdmin','3.1.2'),('20200214195246_Add_Fornecedor_e_Enum','3.1.2'),('20200217163055_Remover_campos_fornecedor','3.1.2'),('20200217185153_Campo_alteracao_cnpj','3.1.2'),('20200218181022_Produto_Fornecedor_many_to_many','3.1.2'),('20200219162438_Remove_isAdmin_usuario','3.1.2'),('20200219165802_Add_Cliente','3.1.2'),('20200219170143_Alter_name_ProdutoFornecedor','3.1.2'),('20200219224010_Add_Pedido_ItemPedido_Cliente_Produto','3.1.2'),('20200224172657_OrdemCompra_Produto','3.1.2'),('20200224200913_Add_Codigo_Produto','3.1.2'),('20200315131440_Ateracao_moment_para_DataVenda','3.1.2'),('20200317195853_Configure_ondelete_itemPedido','3.1.2'),('20200317200259_Configure_ondelete_Pedido','3.1.2'),('20200324222835_Atualizacao-Ordem-Compra','3.1.2'),('20200327192233_Add_Usuario_Logado_Compra_Venda','3.1.2');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_Cliente`
--

DROP TABLE IF EXISTS `TB_Cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_Cliente` (
  `Id` char(36) NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CPF` bigint NOT NULL,
  `Telefone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Endereco` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_Cliente`
--

LOCK TABLES `TB_Cliente` WRITE;
/*!40000 ALTER TABLE `TB_Cliente` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_Cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_Fornecedor`
--

DROP TABLE IF EXISTS `TB_Fornecedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_Fornecedor` (
  `Id` char(36) NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Telefone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CNPJ` bigint NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_Fornecedor`
--

LOCK TABLES `TB_Fornecedor` WRITE;
/*!40000 ALTER TABLE `TB_Fornecedor` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_Fornecedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_ItemOrdemCompra`
--

DROP TABLE IF EXISTS `TB_ItemOrdemCompra`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_ItemOrdemCompra` (
  `Id` char(36) NOT NULL,
  `Preco` double NOT NULL,
  `Quantidade` bigint NOT NULL,
  `IdProduto` char(36) NOT NULL,
  `IdOrdemCompra` char(36) NOT NULL,
  `SubTotal` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_TB_ItemOrdemCompra_IdOrdemCompra` (`IdOrdemCompra`),
  KEY `IX_TB_ItemOrdemCompra_IdProduto` (`IdProduto`),
  CONSTRAINT `FK_TB_ItemOrdemCompra_TB_OrdemCompra_IdOrdemCompra` FOREIGN KEY (`IdOrdemCompra`) REFERENCES `TB_OrdemCompra` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TB_ItemOrdemCompra_TB_Produto_IdProduto` FOREIGN KEY (`IdProduto`) REFERENCES `TB_Produto` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_ItemOrdemCompra`
--

LOCK TABLES `TB_ItemOrdemCompra` WRITE;
/*!40000 ALTER TABLE `TB_ItemOrdemCompra` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_ItemOrdemCompra` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_ItemPedido`
--

DROP TABLE IF EXISTS `TB_ItemPedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_ItemPedido` (
  `Id` char(36) NOT NULL,
  `Quantidade` bigint NOT NULL,
  `Preco` double NOT NULL,
  `SubTotal` double NOT NULL,
  `IdProduto` char(36) NOT NULL,
  `IdPedido` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TB_ItemPedido_IdPedido` (`IdPedido`),
  KEY `IX_TB_ItemPedido_IdProduto` (`IdProduto`),
  CONSTRAINT `FK_TB_ItemPedido_TB_Pedido_IdPedido` FOREIGN KEY (`IdPedido`) REFERENCES `TB_Pedido` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_TB_ItemPedido_TB_Produto_IdProduto` FOREIGN KEY (`IdProduto`) REFERENCES `TB_Produto` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_ItemPedido`
--

LOCK TABLES `TB_ItemPedido` WRITE;
/*!40000 ALTER TABLE `TB_ItemPedido` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_ItemPedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_OrdemCompra`
--

DROP TABLE IF EXISTS `TB_OrdemCompra`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_OrdemCompra` (
  `Id` char(36) NOT NULL,
  `DataEntrada` datetime(6) NOT NULL,
  `Nota` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IdFornecedor` char(36) NOT NULL,
  `ValorTotal` double NOT NULL,
  `IdUsuarioLogado` char(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
  PRIMARY KEY (`Id`),
  KEY `IX_TB_OrdemCompra_IdFornecedor` (`IdFornecedor`),
  KEY `IX_TB_OrdemCompra_IdUsuarioLogado` (`IdUsuarioLogado`),
  CONSTRAINT `FK_TB_OrdemCompra_TB_Fornecedor_IdFornecedor` FOREIGN KEY (`IdFornecedor`) REFERENCES `TB_Fornecedor` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TB_OrdemCompra_TB_Usuario_IdUsuarioLogado` FOREIGN KEY (`IdUsuarioLogado`) REFERENCES `TB_Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_OrdemCompra`
--

LOCK TABLES `TB_OrdemCompra` WRITE;
/*!40000 ALTER TABLE `TB_OrdemCompra` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_OrdemCompra` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_Pedido`
--

DROP TABLE IF EXISTS `TB_Pedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_Pedido` (
  `Id` char(36) NOT NULL,
  `IdCliente` char(36) NOT NULL,
  `ValorTotal` double NOT NULL,
  `DataVenda` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `IdUsuarioLogado` char(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
  PRIMARY KEY (`Id`),
  KEY `IX_TB_Pedido_IdCliente` (`IdCliente`),
  KEY `IX_TB_Pedido_IdUsuarioLogado` (`IdUsuarioLogado`),
  CONSTRAINT `FK_TB_Pedido_TB_Cliente_IdCliente` FOREIGN KEY (`IdCliente`) REFERENCES `TB_Cliente` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TB_Pedido_TB_Usuario_IdUsuarioLogado` FOREIGN KEY (`IdUsuarioLogado`) REFERENCES `TB_Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_Pedido`
--

LOCK TABLES `TB_Pedido` WRITE;
/*!40000 ALTER TABLE `TB_Pedido` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_Pedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_Produto`
--

DROP TABLE IF EXISTS `TB_Produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_Produto` (
  `Id` char(36) NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Descricao` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Valor` double NOT NULL,
  `Codigo` bigint NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_Produto`
--

LOCK TABLES `TB_Produto` WRITE;
/*!40000 ALTER TABLE `TB_Produto` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_Produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_Produto_Fornecedor`
--

DROP TABLE IF EXISTS `TB_Produto_Fornecedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_Produto_Fornecedor` (
  `IdProduto` char(36) NOT NULL,
  `IdFornecedor` char(36) NOT NULL,
  `Id` char(36) NOT NULL,
  PRIMARY KEY (`IdFornecedor`,`IdProduto`),
  KEY `IX_TB_Produto_Fornecedor_IdProduto` (`IdProduto`),
  CONSTRAINT `FK_TB_Produto_Fornecedor_TB_Fornecedor_IdFornecedor` FOREIGN KEY (`IdFornecedor`) REFERENCES `TB_Fornecedor` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TB_Produto_Fornecedor_TB_Produto_IdProduto` FOREIGN KEY (`IdProduto`) REFERENCES `TB_Produto` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_Produto_Fornecedor`
--

LOCK TABLES `TB_Produto_Fornecedor` WRITE;
/*!40000 ALTER TABLE `TB_Produto_Fornecedor` DISABLE KEYS */;
/*!40000 ALTER TABLE `TB_Produto_Fornecedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TB_Usuario`
--

DROP TABLE IF EXISTS `TB_Usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TB_Usuario` (
  `Id` char(36) NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Senha` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Role` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TB_Usuario`
--

LOCK TABLES `TB_Usuario` WRITE;
/*!40000 ALTER TABLE `TB_Usuario` DISABLE KEYS */;
INSERT INTO `TB_Usuario` VALUES ('971225e8-c278-47ab-9dff-c14c86da60bb','Administrador','adm@adm.com','123456','Admin');
/*!40000 ALTER TABLE `TB_Usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'db_vendas'
--

--
-- Dumping routines for database 'db_vendas'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-03-27 16:36:20
