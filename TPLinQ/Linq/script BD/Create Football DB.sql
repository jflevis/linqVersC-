USE [master]
GO
/****** Object:  Database [Football]    Script Date: 2021-03-23 10:57:05 ******/
CREATE DATABASE [Football]
GO
ALTER DATABASE [Football] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Football].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Football] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Football] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Football] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Football] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Football] SET ARITHABORT OFF 
GO
ALTER DATABASE [Football] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Football] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Football] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Football] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Football] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Football] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Football] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Football] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Football] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Football] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Football] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Football] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Football] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Football] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Football] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Football] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Football] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Football] SET RECOVERY FULL 
GO
ALTER DATABASE [Football] SET  MULTI_USER 
GO
ALTER DATABASE [Football] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Football] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Football] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Football] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Football] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Football', N'ON'
GO
ALTER DATABASE [Football] SET QUERY_STORE = OFF
GO
USE [Football]
GO
/****** Object:  Table [dbo].[Conference]    Script Date: 2021-03-23 10:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conference](
	[id_Conference] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Conference] PRIMARY KEY CLUSTERED 
(
	[id_Conference] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipe]    Script Date: 2021-03-23 10:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipe](
	[id_Equipe] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[AnneeFondation] [int] NOT NULL,
	[id_Ville] [int] NOT NULL,
	[id_Conference] [int] NOT NULL,
	[Surnom] [varchar](100) NULL,
 CONSTRAINT [PK_Equipe] PRIMARY KEY CLUSTERED 
(
	[id_Equipe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etat]    Script Date: 2021-03-23 10:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etat](
	[id_Etat] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Etat] PRIMARY KEY CLUSTERED 
(
	[id_Etat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Joueur]    Script Date: 2021-03-23 10:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Joueur](
	[id_Joueur] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Prenom] [varchar](50) NOT NULL,
	[DateNaissance] [date] NOT NULL,
 CONSTRAINT [PK_Joueur] PRIMARY KEY CLUSTERED 
(
	[id_Joueur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JoueurEquipe]    Script Date: 2021-03-23 10:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JoueurEquipe](
	[id_Joueur] [int] NOT NULL,
	[id_Equipe] [int] NOT NULL,
	[DateDebut] [int] NOT NULL,
	[DateFin] [int] NULL,
 CONSTRAINT [PK_JoueurEquipe] PRIMARY KEY CLUSTERED 
(
	[id_Joueur] ASC,
	[id_Equipe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ville]    Script Date: 2021-03-23 10:57:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ville](
	[id_Ville] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[id_Etat] [int] NOT NULL,
	[Population] [int] NOT NULL,
	[AnneeFondation] [int] NOT NULL,
 CONSTRAINT [PK_Ville] PRIMARY KEY CLUSTERED 
(
	[id_Ville] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Conference] ON 
GO
INSERT [dbo].[Conference] ([id_Conference], [Nom]) VALUES (1, N'American Football Conference')
GO
INSERT [dbo].[Conference] ([id_Conference], [Nom]) VALUES (2, N'National Football Conference')
GO
SET IDENTITY_INSERT [dbo].[Conference] OFF
GO
SET IDENTITY_INSERT [dbo].[Equipe] ON 
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (1, N'Chiefs', 1960, 1, 1, NULL)
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (2, N'Bears', 1919, 2, 2, N'The Monster of the Midway')
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (3, N'Broncos', 1960, 3, 1, N'Orange Crush')
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (4, N'Giants', 1925, 4, 2, N'Big Blue')
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (5, N'Bills', 1960, 5, 1, NULL)
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (6, N'Jets', 1961, 5, 1, N'Gros Jets')
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (7, N'Stealers', 1950, 6, 1, N'Big Pitts')
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (8, N'Eagles', 1951, 7, 2, N'Aigles')
GO
INSERT [dbo].[Equipe] ([id_Equipe], [Nom], [AnneeFondation], [id_Ville], [id_Conference], [Surnom]) VALUES (9, N'49ers', 1965, 8, 2, N'SF')
GO
SET IDENTITY_INSERT [dbo].[Equipe] OFF
GO
SET IDENTITY_INSERT [dbo].[Etat] ON 
GO
INSERT [dbo].[Etat] ([id_Etat], [Nom]) VALUES (1, N'Missouri')
GO
INSERT [dbo].[Etat] ([id_Etat], [Nom]) VALUES (2, N'Illinois')
GO
INSERT [dbo].[Etat] ([id_Etat], [Nom]) VALUES (3, N'Colorado')
GO
INSERT [dbo].[Etat] ([id_Etat], [Nom]) VALUES (4, N'New Jersey')
GO
INSERT [dbo].[Etat] ([id_Etat], [Nom]) VALUES (5, N'New York')
GO
INSERT [dbo].[Etat] ([id_Etat], [Nom]) VALUES (6, N'Pensylvanie')
GO
INSERT [dbo].[Etat] ([id_Etat], [Nom]) VALUES (7, N'Californie')
GO
SET IDENTITY_INSERT [dbo].[Etat] OFF
GO
SET IDENTITY_INSERT [dbo].[Joueur] ON 
GO
INSERT [dbo].[Joueur] ([id_Joueur], [Nom], [Prenom], [DateNaissance]) VALUES (1, N'Cassel', N'Matt', CAST(N'1982-05-17' AS Date))
GO
INSERT [dbo].[Joueur] ([id_Joueur], [Nom], [Prenom], [DateNaissance]) VALUES (2, N'Hillis', N'Peyton', CAST(N'1986-01-21' AS Date))
GO
INSERT [dbo].[Joueur] ([id_Joueur], [Nom], [Prenom], [DateNaissance]) VALUES (3, N'Manning', N'Peyton', CAST(N'1976-03-24' AS Date))
GO
INSERT [dbo].[Joueur] ([id_Joueur], [Nom], [Prenom], [DateNaissance]) VALUES (4, N'Manning', N'Eli', CAST(N'1981-01-03' AS Date))
GO
INSERT [dbo].[Joueur] ([id_Joueur], [Nom], [Prenom], [DateNaissance]) VALUES (6, N'Cutler', N'Jay', CAST(N'1983-04-29' AS Date))
GO
INSERT [dbo].[Joueur] ([id_Joueur], [Nom], [Prenom], [DateNaissance]) VALUES (7, N'Anderson', N'Mark', CAST(N'1983-05-26' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Joueur] OFF
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (1, 1, 2009, NULL)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (2, 1, 2012, NULL)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (2, 3, 2008, 2009)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (3, 3, 2012, NULL)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (4, 4, 2004, NULL)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (6, 2, 2009, NULL)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (6, 3, 2006, 2008)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (7, 2, 2006, 2010)
GO
INSERT [dbo].[JoueurEquipe] ([id_Joueur], [id_Equipe], [DateDebut], [DateFin]) VALUES (7, 5, 2012, NULL)
GO
SET IDENTITY_INSERT [dbo].[Ville] ON 
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (1, N'Kansas City', 1, 480448, 1853)
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (2, N'Chicago', 2, 2707120, 1833)
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (3, N'Denver', 3, 600158, 1858)
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (4, N'New York', 4, 8175133, 1624)
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (5, N'Buffalo', 5, 261310, 1832)
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (6, N'Pittsburg', 6, 1200000, 1800)
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (7, N'Philadelphie', 6, 100000, 1750)
GO
INSERT [dbo].[Ville] ([id_Ville], [Nom], [id_Etat], [Population], [AnneeFondation]) VALUES (8, N'San Francisco', 6, 1500000, 1820)
GO
SET IDENTITY_INSERT [dbo].[Ville] OFF
GO
ALTER TABLE [dbo].[Equipe]  WITH CHECK ADD  CONSTRAINT [FK_Equipe_Conference] FOREIGN KEY([id_Conference])
REFERENCES [dbo].[Conference] ([id_Conference])
GO
ALTER TABLE [dbo].[Equipe] CHECK CONSTRAINT [FK_Equipe_Conference]
GO
ALTER TABLE [dbo].[Equipe]  WITH CHECK ADD  CONSTRAINT [FK_Equipe_Ville] FOREIGN KEY([id_Ville])
REFERENCES [dbo].[Ville] ([id_Ville])
GO
ALTER TABLE [dbo].[Equipe] CHECK CONSTRAINT [FK_Equipe_Ville]
GO
ALTER TABLE [dbo].[JoueurEquipe]  WITH CHECK ADD  CONSTRAINT [FK_JoueurEquipe_Equipe] FOREIGN KEY([id_Equipe])
REFERENCES [dbo].[Equipe] ([id_Equipe])
GO
ALTER TABLE [dbo].[JoueurEquipe] CHECK CONSTRAINT [FK_JoueurEquipe_Equipe]
GO
ALTER TABLE [dbo].[JoueurEquipe]  WITH CHECK ADD  CONSTRAINT [FK_JoueurEquipe_Joueur] FOREIGN KEY([id_Joueur])
REFERENCES [dbo].[Joueur] ([id_Joueur])
GO
ALTER TABLE [dbo].[JoueurEquipe] CHECK CONSTRAINT [FK_JoueurEquipe_Joueur]
GO
ALTER TABLE [dbo].[Ville]  WITH CHECK ADD  CONSTRAINT [FK_Ville_Etat] FOREIGN KEY([id_Etat])
REFERENCES [dbo].[Etat] ([id_Etat])
GO
ALTER TABLE [dbo].[Ville] CHECK CONSTRAINT [FK_Ville_Etat]
GO
USE [master]
GO
ALTER DATABASE [Football] SET  READ_WRITE 
GO
