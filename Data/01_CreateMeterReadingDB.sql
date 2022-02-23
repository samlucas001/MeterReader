USE [master]
GO

/****** Object:  Database [MeterReading]    Script Date: 22/02/2022 11:32:25 PM ******/
CREATE DATABASE [MeterReading]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MeterReading', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\MeterReading.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MeterReading_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\MeterReading_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeterReading].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [MeterReading] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [MeterReading] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [MeterReading] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [MeterReading] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [MeterReading] SET ARITHABORT OFF 
GO

ALTER DATABASE [MeterReading] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [MeterReading] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [MeterReading] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [MeterReading] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [MeterReading] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [MeterReading] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [MeterReading] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [MeterReading] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [MeterReading] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [MeterReading] SET  DISABLE_BROKER 
GO

ALTER DATABASE [MeterReading] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [MeterReading] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [MeterReading] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [MeterReading] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [MeterReading] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [MeterReading] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [MeterReading] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [MeterReading] SET RECOVERY FULL 
GO

ALTER DATABASE [MeterReading] SET  MULTI_USER 
GO

ALTER DATABASE [MeterReading] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [MeterReading] SET DB_CHAINING OFF 
GO

ALTER DATABASE [MeterReading] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [MeterReading] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [MeterReading] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [MeterReading] SET QUERY_STORE = OFF
GO

ALTER DATABASE [MeterReading] SET  READ_WRITE 
GO

