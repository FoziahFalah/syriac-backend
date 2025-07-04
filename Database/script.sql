USE [master]
GO
/****** Object:  Database [SyriacSources]    Script Date: 01/01/47 03:09:31 م ******/
CREATE DATABASE [SyriacSources]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SyriacSources', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SyriacSources.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SyriacSources_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SyriacSources_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SyriacSources] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SyriacSources].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SyriacSources] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SyriacSources] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SyriacSources] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SyriacSources] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SyriacSources] SET ARITHABORT OFF 
GO
ALTER DATABASE [SyriacSources] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SyriacSources] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SyriacSources] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SyriacSources] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SyriacSources] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SyriacSources] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SyriacSources] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SyriacSources] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SyriacSources] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SyriacSources] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SyriacSources] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SyriacSources] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SyriacSources] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SyriacSources] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SyriacSources] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SyriacSources] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SyriacSources] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SyriacSources] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SyriacSources] SET  MULTI_USER 
GO
ALTER DATABASE [SyriacSources] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SyriacSources] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SyriacSources] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SyriacSources] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SyriacSources] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SyriacSources] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SyriacSources] SET QUERY_STORE = ON
GO
ALTER DATABASE [SyriacSources] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SyriacSources]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationPermissions]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationPermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PolicyName] [nvarchar](200) NOT NULL,
	[NameEN] [nvarchar](200) NOT NULL,
	[NameAR] [nvarchar](200) NOT NULL,
	[ParentId] [int] NOT NULL,
	[IsModule] [bit] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationRolePermissions]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationRolePermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationRoleId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[ApplicationPermissionId] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationRolePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationRoles]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NormalizedRoleName] [nvarchar](100) NOT NULL,
	[NameEN] [nvarchar](100) NOT NULL,
	[NameAR] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUserRoles]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[ApplicationRoleId] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationUserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUsers]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullNameEN] [nvarchar](200) NOT NULL,
	[FullNameAR] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[UserType] [int] NOT NULL,
	[IdentityApplicationUserId] [int] NOT NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachments]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NOT NULL,
	[FileName] [nvarchar](200) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[FileExtension] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Centuries]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Centuries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Centuries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationUserId] [int] NOT NULL,
	[Details] [nvarchar](2000) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[ExcerptId] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoverPhotos]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoverPhotos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NOT NULL,
	[FileName] [nvarchar](200) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[FileExtension] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_CoverPhotos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DateFromats]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DateFromats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Format] [nvarchar](200) NOT NULL,
	[Period] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_DateFromats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExcerptDates]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExcerptDates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExcerptId] [int] NOT NULL,
	[DateFormatId] [int] NOT NULL,
	[FromYear] [int] NOT NULL,
	[ToYear] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_ExcerptDates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Excerpts]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Excerpts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NOT NULL,
	[AdditionalInfo] [nvarchar](2000) NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Excerpts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExcerptTexts]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExcerptTexts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExcerptId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[EditorId] [int] NOT NULL,
	[ReviewerId] [int] NOT NULL,
	[TranslatorId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_ExcerptTexts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Footnotes]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Footnotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommenterId] [int] NOT NULL,
	[Comment] [nvarchar](1000) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Footnotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](7) NOT NULL,
	[Code] [nvarchar](3) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publications]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Publications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceDates]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceDates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NOT NULL,
	[DateFormatId] [int] NOT NULL,
	[FromYear] [int] NOT NULL,
	[ToYear] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_SourceDates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceIntroductionEditors]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceIntroductionEditors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NOT NULL,
	[EditorId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_SourceInroductionEditors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sources]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[CenturyId] [int] NOT NULL,
	[Introduction] [nvarchar](2000) NOT NULL,
	[SourceTitleInArabic] [nvarchar](500) NOT NULL,
	[SourceTitleInSyriac] [nvarchar](500) NOT NULL,
	[SourceTitleInForeignLanguage] [nvarchar](500) NOT NULL,
	[IntroductionEditorId] [int] NULL,
	[AdditionalInfo] [nvarchar](2000) NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TodoItems]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TodoItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ListId] [int] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[Priority] [int] NOT NULL,
	[Reminder] [datetime2](7) NULL,
	[Done] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_TodoItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TodoLists]    Script Date: 01/01/47 03:09:31 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TodoLists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Colour_Code] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_TodoLists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationRolePermissions_ApplicationPermissionId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationRolePermissions_ApplicationPermissionId] ON [dbo].[ApplicationRolePermissions]
(
	[ApplicationPermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationRolePermissions_ApplicationRoleId_ApplicationPermissionId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ApplicationRolePermissions_ApplicationRoleId_ApplicationPermissionId] ON [dbo].[ApplicationRolePermissions]
(
	[ApplicationRoleId] ASC,
	[ApplicationPermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NormalizedRoleName]    Script Date: 01/01/47 03:09:31 م ******/
CREATE UNIQUE NONCLUSTERED INDEX [NormalizedRoleName] ON [dbo].[ApplicationRoles]
(
	[NormalizedRoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationUserRoles_ApplicationRoleId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUserRoles_ApplicationRoleId] ON [dbo].[ApplicationUserRoles]
(
	[ApplicationRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationUserRoles_ApplicationUserId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationUserRoles_ApplicationUserId] ON [dbo].[ApplicationUserRoles]
(
	[ApplicationUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ApplicationUserEmailAddress]    Script Date: 01/01/47 03:09:31 م ******/
CREATE UNIQUE NONCLUSTERED INDEX [ApplicationUserEmailAddress] ON [dbo].[ApplicationUsers]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ApplicationUserFullNameAR]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [ApplicationUserFullNameAR] ON [dbo].[ApplicationUsers]
(
	[FullNameAR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ApplicationUserFullNameEN]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [ApplicationUserFullNameEN] ON [dbo].[ApplicationUsers]
(
	[FullNameEN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 01/01/47 03:09:31 م ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 01/01/47 03:09:31 م ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Attachments_SourceId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Attachments_SourceId] ON [dbo].[Attachments]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AuthorName]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [AuthorName] ON [dbo].[Authors]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CenturyName]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [CenturyName] ON [dbo].[Centuries]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_ApplicationUserId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Comments_ApplicationUserId] ON [dbo].[Comments]
(
	[ApplicationUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_ExcerptId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Comments_ExcerptId] ON [dbo].[Comments]
(
	[ExcerptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CoverPhotoFileExtension]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [CoverPhotoFileExtension] ON [dbo].[CoverPhotos]
(
	[FileExtension] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CoverPhotoFileName]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [CoverPhotoFileName] ON [dbo].[CoverPhotos]
(
	[FileName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CoverPhotoFilePath]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [CoverPhotoFilePath] ON [dbo].[CoverPhotos]
(
	[FilePath] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CoverPhotos_SourceId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CoverPhotos_SourceId] ON [dbo].[CoverPhotos]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [DateFormatName]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [DateFormatName] ON [dbo].[DateFromats]
(
	[Format] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [DateFormatPeriod]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [DateFormatPeriod] ON [dbo].[DateFromats]
(
	[Period] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExcerptDates_ExcerptId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ExcerptDates_ExcerptId] ON [dbo].[ExcerptDates]
(
	[ExcerptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Excerpts_SourceId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Excerpts_SourceId] ON [dbo].[Excerpts]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExcerptTexts_EditorId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ExcerptTexts_EditorId] ON [dbo].[ExcerptTexts]
(
	[EditorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExcerptTexts_ExcerptId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ExcerptTexts_ExcerptId] ON [dbo].[ExcerptTexts]
(
	[ExcerptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExcerptTexts_LanguageId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ExcerptTexts_LanguageId] ON [dbo].[ExcerptTexts]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExcerptTexts_ReviewerId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ExcerptTexts_ReviewerId] ON [dbo].[ExcerptTexts]
(
	[ReviewerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExcerptTexts_TranslatorId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_ExcerptTexts_TranslatorId] ON [dbo].[ExcerptTexts]
(
	[TranslatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Footnotes_CommenterId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Footnotes_CommenterId] ON [dbo].[Footnotes]
(
	[CommenterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PublicationSourceId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [PublicationSourceId] ON [dbo].[Publications]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceDates_DateFormatId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_SourceDates_DateFormatId] ON [dbo].[SourceDates]
(
	[DateFormatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceDates_SourceId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_SourceDates_SourceId] ON [dbo].[SourceDates]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceInroductionEditors_EditorId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_SourceInroductionEditors_EditorId] ON [dbo].[SourceIntroductionEditors]
(
	[EditorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceInroductionEditors_SourceId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_SourceInroductionEditors_SourceId] ON [dbo].[SourceIntroductionEditors]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sources_AuthorId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Sources_AuthorId] ON [dbo].[Sources]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sources_CenturyId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Sources_CenturyId] ON [dbo].[Sources]
(
	[CenturyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sources_IntroductionEditorId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_Sources_IntroductionEditorId] ON [dbo].[Sources]
(
	[IntroductionEditorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [SourceTitleInArabic]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [SourceTitleInArabic] ON [dbo].[Sources]
(
	[SourceTitleInArabic] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TodoItems_ListId]    Script Date: 01/01/47 03:09:31 م ******/
CREATE NONCLUSTERED INDEX [IX_TodoItems_ListId] ON [dbo].[TodoItems]
(
	[ListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationRolePermissions] ADD  DEFAULT ((0)) FOR [ApplicationPermissionId]
GO
ALTER TABLE [dbo].[ApplicationUserRoles] ADD  DEFAULT ((0)) FOR [ApplicationRoleId]
GO
ALTER TABLE [dbo].[ApplicationUsers] ADD  DEFAULT ((0)) FOR [UserType]
GO
ALTER TABLE [dbo].[ApplicationUsers] ADD  DEFAULT ((0)) FOR [IdentityApplicationUserId]
GO
ALTER TABLE [dbo].[ApplicationRolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationRolePermissions_ApplicationPermissions_ApplicationPermissionId] FOREIGN KEY([ApplicationPermissionId])
REFERENCES [dbo].[ApplicationPermissions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationRolePermissions] CHECK CONSTRAINT [FK_ApplicationRolePermissions_ApplicationPermissions_ApplicationPermissionId]
GO
ALTER TABLE [dbo].[ApplicationRolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationRolePermissions_ApplicationRoles_ApplicationRoleId] FOREIGN KEY([ApplicationRoleId])
REFERENCES [dbo].[ApplicationRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationRolePermissions] CHECK CONSTRAINT [FK_ApplicationRolePermissions_ApplicationRoles_ApplicationRoleId]
GO
ALTER TABLE [dbo].[ApplicationUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserRoles_ApplicationRoles_ApplicationRoleId] FOREIGN KEY([ApplicationRoleId])
REFERENCES [dbo].[ApplicationRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserRoles] CHECK CONSTRAINT [FK_ApplicationUserRoles_ApplicationRoles_ApplicationRoleId]
GO
ALTER TABLE [dbo].[ApplicationUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserRoles_ApplicationUsers_ApplicationUserId] FOREIGN KEY([ApplicationUserId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserRoles] CHECK CONSTRAINT [FK_ApplicationUserRoles_ApplicationUsers_ApplicationUserId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_Sources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Sources] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Attachments] CHECK CONSTRAINT [FK_Attachments_Sources_SourceId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_ApplicationUsers_ApplicationUserId] FOREIGN KEY([ApplicationUserId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_ApplicationUsers_ApplicationUserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Excerpts_ExcerptId] FOREIGN KEY([ExcerptId])
REFERENCES [dbo].[Excerpts] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Excerpts_ExcerptId]
GO
ALTER TABLE [dbo].[CoverPhotos]  WITH CHECK ADD  CONSTRAINT [FK_CoverPhotos_Sources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Sources] ([Id])
GO
ALTER TABLE [dbo].[CoverPhotos] CHECK CONSTRAINT [FK_CoverPhotos_Sources_SourceId]
GO
ALTER TABLE [dbo].[ExcerptDates]  WITH CHECK ADD  CONSTRAINT [FK_ExcerptDates_Excerpts_ExcerptId] FOREIGN KEY([ExcerptId])
REFERENCES [dbo].[Excerpts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExcerptDates] CHECK CONSTRAINT [FK_ExcerptDates_Excerpts_ExcerptId]
GO
ALTER TABLE [dbo].[Excerpts]  WITH CHECK ADD  CONSTRAINT [FK_Excerpts_Sources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Sources] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Excerpts] CHECK CONSTRAINT [FK_Excerpts_Sources_SourceId]
GO
ALTER TABLE [dbo].[ExcerptTexts]  WITH CHECK ADD  CONSTRAINT [FK_ExcerptTexts_ApplicationUsers_EditorId] FOREIGN KEY([EditorId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
GO
ALTER TABLE [dbo].[ExcerptTexts] CHECK CONSTRAINT [FK_ExcerptTexts_ApplicationUsers_EditorId]
GO
ALTER TABLE [dbo].[ExcerptTexts]  WITH CHECK ADD  CONSTRAINT [FK_ExcerptTexts_ApplicationUsers_ReviewerId] FOREIGN KEY([ReviewerId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
GO
ALTER TABLE [dbo].[ExcerptTexts] CHECK CONSTRAINT [FK_ExcerptTexts_ApplicationUsers_ReviewerId]
GO
ALTER TABLE [dbo].[ExcerptTexts]  WITH CHECK ADD  CONSTRAINT [FK_ExcerptTexts_ApplicationUsers_TranslatorId] FOREIGN KEY([TranslatorId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
GO
ALTER TABLE [dbo].[ExcerptTexts] CHECK CONSTRAINT [FK_ExcerptTexts_ApplicationUsers_TranslatorId]
GO
ALTER TABLE [dbo].[ExcerptTexts]  WITH CHECK ADD  CONSTRAINT [FK_ExcerptTexts_Excerpts_ExcerptId] FOREIGN KEY([ExcerptId])
REFERENCES [dbo].[Excerpts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExcerptTexts] CHECK CONSTRAINT [FK_ExcerptTexts_Excerpts_ExcerptId]
GO
ALTER TABLE [dbo].[ExcerptTexts]  WITH CHECK ADD  CONSTRAINT [FK_ExcerptTexts_Languages_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ExcerptTexts] CHECK CONSTRAINT [FK_ExcerptTexts_Languages_LanguageId]
GO
ALTER TABLE [dbo].[Footnotes]  WITH CHECK ADD  CONSTRAINT [FK_Footnotes_ApplicationUsers_CommenterId] FOREIGN KEY([CommenterId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Footnotes] CHECK CONSTRAINT [FK_Footnotes_ApplicationUsers_CommenterId]
GO
ALTER TABLE [dbo].[Publications]  WITH CHECK ADD  CONSTRAINT [FK_Publications_Sources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Sources] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Publications] CHECK CONSTRAINT [FK_Publications_Sources_SourceId]
GO
ALTER TABLE [dbo].[SourceDates]  WITH CHECK ADD  CONSTRAINT [FK_SourceDates_DateFromats_DateFormatId] FOREIGN KEY([DateFormatId])
REFERENCES [dbo].[DateFromats] ([Id])
GO
ALTER TABLE [dbo].[SourceDates] CHECK CONSTRAINT [FK_SourceDates_DateFromats_DateFormatId]
GO
ALTER TABLE [dbo].[SourceDates]  WITH CHECK ADD  CONSTRAINT [FK_SourceDates_Sources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Sources] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SourceDates] CHECK CONSTRAINT [FK_SourceDates_Sources_SourceId]
GO
ALTER TABLE [dbo].[SourceIntroductionEditors]  WITH CHECK ADD  CONSTRAINT [FK_SourceInroductionEditors_ApplicationUsers_EditorId] FOREIGN KEY([EditorId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SourceIntroductionEditors] CHECK CONSTRAINT [FK_SourceInroductionEditors_ApplicationUsers_EditorId]
GO
ALTER TABLE [dbo].[SourceIntroductionEditors]  WITH CHECK ADD  CONSTRAINT [FK_SourceInroductionEditors_Sources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Sources] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SourceIntroductionEditors] CHECK CONSTRAINT [FK_SourceInroductionEditors_Sources_SourceId]
GO
ALTER TABLE [dbo].[Sources]  WITH CHECK ADD  CONSTRAINT [FK_Sources_ApplicationUsers_IntroductionEditorId] FOREIGN KEY([IntroductionEditorId])
REFERENCES [dbo].[ApplicationUsers] ([Id])
GO
ALTER TABLE [dbo].[Sources] CHECK CONSTRAINT [FK_Sources_ApplicationUsers_IntroductionEditorId]
GO
ALTER TABLE [dbo].[Sources]  WITH CHECK ADD  CONSTRAINT [FK_Sources_Authors_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO
ALTER TABLE [dbo].[Sources] CHECK CONSTRAINT [FK_Sources_Authors_AuthorId]
GO
ALTER TABLE [dbo].[Sources]  WITH CHECK ADD  CONSTRAINT [FK_Sources_Centuries_CenturyId] FOREIGN KEY([CenturyId])
REFERENCES [dbo].[Centuries] ([Id])
GO
ALTER TABLE [dbo].[Sources] CHECK CONSTRAINT [FK_Sources_Centuries_CenturyId]
GO
ALTER TABLE [dbo].[TodoItems]  WITH CHECK ADD  CONSTRAINT [FK_TodoItems_TodoLists_ListId] FOREIGN KEY([ListId])
REFERENCES [dbo].[TodoLists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TodoItems] CHECK CONSTRAINT [FK_TodoItems_TodoLists_ListId]
GO
USE [master]
GO
ALTER DATABASE [SyriacSources] SET  READ_WRITE 
GO
