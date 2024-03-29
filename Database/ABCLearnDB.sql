USE [master]
 
/****** Object:  Database [ABCLearn]    Script Date: 7/18/2023 7:28:53 AM ******/
CREATE DATABASE [ABCLearn]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ABCLearn', FILENAME = N'E:\SQL server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ABCLearn.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ABCLearn_log', FILENAME = N'E:\SQL server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ABCLearn_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
 
ALTER DATABASE [ABCLearn] SET COMPATIBILITY_LEVEL = 150
 
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ABCLearn].[dbo].[sp_fulltext_database] @action = 'enable'
end
 
ALTER DATABASE [ABCLearn] SET ANSI_NULL_DEFAULT OFF 
 
ALTER DATABASE [ABCLearn] SET ANSI_NULLS OFF 
 
ALTER DATABASE [ABCLearn] SET ANSI_PADDING OFF 
 
ALTER DATABASE [ABCLearn] SET ANSI_WARNINGS OFF 
 
ALTER DATABASE [ABCLearn] SET ARITHABORT OFF 
 
ALTER DATABASE [ABCLearn] SET AUTO_CLOSE OFF 
 
ALTER DATABASE [ABCLearn] SET AUTO_SHRINK OFF 
 
ALTER DATABASE [ABCLearn] SET AUTO_UPDATE_STATISTICS ON 
 
ALTER DATABASE [ABCLearn] SET CURSOR_CLOSE_ON_COMMIT OFF 
 
ALTER DATABASE [ABCLearn] SET CURSOR_DEFAULT  GLOBAL 
 
ALTER DATABASE [ABCLearn] SET CONCAT_NULL_YIELDS_NULL OFF 
 
ALTER DATABASE [ABCLearn] SET NUMERIC_ROUNDABORT OFF 
 
ALTER DATABASE [ABCLearn] SET QUOTED_IDENTIFIER OFF 
 
ALTER DATABASE [ABCLearn] SET RECURSIVE_TRIGGERS OFF 
 
ALTER DATABASE [ABCLearn] SET  DISABLE_BROKER 
 
ALTER DATABASE [ABCLearn] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
 
ALTER DATABASE [ABCLearn] SET DATE_CORRELATION_OPTIMIZATION OFF 
 
ALTER DATABASE [ABCLearn] SET TRUSTWORTHY OFF 
 
ALTER DATABASE [ABCLearn] SET ALLOW_SNAPSHOT_ISOLATION OFF 
 
ALTER DATABASE [ABCLearn] SET PARAMETERIZATION SIMPLE 
 
ALTER DATABASE [ABCLearn] SET READ_COMMITTED_SNAPSHOT OFF 
 
ALTER DATABASE [ABCLearn] SET HONOR_BROKER_PRIORITY OFF 
 
ALTER DATABASE [ABCLearn] SET RECOVERY FULL 
 
ALTER DATABASE [ABCLearn] SET  MULTI_USER 
 
ALTER DATABASE [ABCLearn] SET PAGE_VERIFY CHECKSUM  
 
ALTER DATABASE [ABCLearn] SET DB_CHAINING OFF 
 
ALTER DATABASE [ABCLearn] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
 
ALTER DATABASE [ABCLearn] SET TARGET_RECOVERY_TIME = 60 SECONDS 
 
ALTER DATABASE [ABCLearn] SET DELAYED_DURABILITY = DISABLED 
 
ALTER DATABASE [ABCLearn] SET ACCELERATED_DATABASE_RECOVERY = OFF  
 
EXEC sys.sp_db_vardecimal_storage_format N'ABCLearn', N'ON'
 
ALTER DATABASE [ABCLearn] SET QUERY_STORE = OFF
 
USE [ABCLearn]
 
/****** Object:  User [thong0189]    Script Date: 7/18/2023 7:28:53 AM ******/
CREATE USER [thong0189] FOR LOGIN [thong0189] WITH DEFAULT_SCHEMA=[dbo]
 
/****** Object:  Table [dbo].[tblAdmin]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblAdmin](
	[IDAdmin] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](20) NULL,
 CONSTRAINT [PK_tblAdmin] PRIMARY KEY CLUSTERED 
(
	[IDAdmin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblCalendar]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblCalendar](
	[IDCalendar] [int] IDENTITY(1,1) NOT NULL,
	[IDCourse] [int] NULL,
	[TimeStart] [time](7) NULL,
	[TimeEnd] [time](7) NULL,
 CONSTRAINT [PK_tblCalendar] PRIMARY KEY CLUSTERED 
(
	[IDCalendar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblCheckTeaching]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblCheckTeaching](
	[IDCheck] [int] IDENTITY(1,1) NOT NULL,
	[IDLecturer] [int] NULL,
	[Count] [int] NULL,
 CONSTRAINT [PK_tblCheckTeaching'] PRIMARY KEY CLUSTERED 
(
	[IDCheck] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblCommentOfStudent]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblCommentOfStudent](
	[IDStudent] [int] NULL,
	[IDCourse] [int] NULL,
	[Comment] [text] NULL,
	[TimeDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblCourse]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblCourse](
	[IDCourse] [int] IDENTITY(1,1) NOT NULL,
	[IDLecturer] [int] NULL,
	[Title] [text] NULL,
	[Detail] [text] NULL,
	[Price] [float] NULL,
	[Sale] [float] NULL,
	[Status] [bit] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[IDCourse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblCourseOfStudent]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblCourseOfStudent](
	[IDStudent] [int] NULL,
	[IDCourse] [int] NULL,
	[PurchaseDate] [datetime] NULL
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblLecturer]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblLecturer](
	[IDLecturer] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[RoleID] [nchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Phone] [nchar](10) NULL,
	[Avatar] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblLecturer] PRIMARY KEY CLUSTERED 
(
	[IDLecturer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblNontification]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblNontification](
	[IDContification] [int] IDENTITY(1,1) NOT NULL,
	[Title] [text] NULL,
	[Detail] [text] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_tblContification] PRIMARY KEY CLUSTERED 
(
	[IDContification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblQuiz]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblQuiz](
	[IDQuiz] [int] IDENTITY(1,1) NOT NULL,
	[Question] [text] NULL,
	[AnswerA] [nvarchar](50) NULL,
	[AnswerB] [nvarchar](50) NULL,
	[AnswerC] [nvarchar](50) NULL,
	[AnswerD] [nvarchar](50) NULL,
	[CorrectAnswer] [nvarchar](50) NULL,
	[IDCourse] [int] NULL,
 CONSTRAINT [PK_tblQuiz] PRIMARY KEY CLUSTERED 
(
	[IDQuiz] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblStudent]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblStudent](
	[IDStudent] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[RoleID] [nchar](10) NULL,
	[Password] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nchar](10) NULL,
	[ConfirmEmail] [bit] NULL,
	[DateCreate] [datetime] NULL,
	[Gander] [nchar](10) NULL,
	[DOB] [date] NULL,
	[Avatar] [nvarchar](50) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[IDStudent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[tblTransactionHistory]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[tblTransactionHistory](
	[IDHistory] [int] IDENTITY(1,1) NOT NULL,
	[IDStudent] [int] NULL,
	[CourseTitle] [nvarchar](50) NULL,
	[Price] [float] NULL,
	[Date] [datetime] NULL,
	[Method] [nchar](10) NULL,
	[OrderID] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblTransactionHistory] PRIMARY KEY CLUSTERED 
(
	[IDHistory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
SET IDENTITY_INSERT [dbo].[tblAdmin] ON 

INSERT [dbo].[tblAdmin] ([IDAdmin], [Email], [Password]) VALUES (1, N'abclearn@gmail.com', N'abclearn123')
SET IDENTITY_INSERT [dbo].[tblAdmin] OFF
 
SET IDENTITY_INSERT [dbo].[tblCalendar] ON 

INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (2, 13, CAST(N'10:00:00' AS Time), CAST(N'11:30:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (3, 14, CAST(N'08:00:00' AS Time), CAST(N'09:30:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (4, 16, CAST(N'13:30:00' AS Time), CAST(N'15:00:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (5, 17, CAST(N'14:00:00' AS Time), CAST(N'15:30:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (6, 12, CAST(N'13:30:00' AS Time), CAST(N'15:00:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (7, 13, CAST(N'14:00:00' AS Time), CAST(N'15:30:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (8, 14, CAST(N'15:00:00' AS Time), CAST(N'16:30:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (1011, 15, CAST(N'07:00:00' AS Time), CAST(N'08:00:00' AS Time))
INSERT [dbo].[tblCalendar] ([IDCalendar], [IDCourse], [TimeStart], [TimeEnd]) VALUES (1012, 18, CAST(N'06:00:00' AS Time), CAST(N'07:30:00' AS Time))
SET IDENTITY_INSERT [dbo].[tblCalendar] OFF
 
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (12, 15, N'amazing  od job', CAST(N'2023-05-02T12:12:00.000' AS DateTime))
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (1, 15, N'Nice', CAST(N'2023-05-21T21:09:15.557' AS DateTime))
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (12, 12, N'Nice', CAST(N'2023-05-21T21:09:15.557' AS DateTime))
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (1, 13, N'Nice', CAST(N'2023-05-21T21:09:15.557' AS DateTime))
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (2, 14, N'Nice', CAST(N'2023-05-21T21:09:15.557' AS DateTime))
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (2, 16, N'Nice', CAST(N'2023-05-21T21:09:15.557' AS DateTime))
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (1, 17, N'Nice', CAST(N'2023-05-21T21:09:15.557' AS DateTime))
INSERT [dbo].[tblCommentOfStudent] ([IDStudent], [IDCourse], [Comment], [TimeDate]) VALUES (3, 18, N'Nice', CAST(N'2023-05-21T21:09:15.557' AS DateTime))
 
SET IDENTITY_INSERT [dbo].[tblCourse] ON 

INSERT [dbo].[tblCourse] ([IDCourse], [IDLecturer], [Title], [Detail], [Price], [Sale], [Status], [Quantity]) VALUES (12, 1007, N'Elementary communication', N'Get used to communicating in English', 200, 20, 1, 29)
INSERT [dbo].[tblCourse] ([IDCourse], [IDLecturer], [Title], [Detail], [Price], [Sale], [Status], [Quantity]) VALUES (13, 2, N'Elementary reading', N'Practice face recognition to be able to read English', 200, 0, 1, 0)
INSERT [dbo].[tblCourse] ([IDCourse], [IDLecturer], [Title], [Detail], [Price], [Sale], [Status], [Quantity]) VALUES (14, 1, N'Intermediate reading', N'Read articles in English to improve your vocabulary and mid-range words', 250, 0, 1, 1)
INSERT [dbo].[tblCourse] ([IDCourse], [IDLecturer], [Title], [Detail], [Price], [Sale], [Status], [Quantity]) VALUES (15, 3, N'Intermediate communication', N'Practice speaking with dialogues and the pronunciation of key sounds', 250, 0, 1, 1)
INSERT [dbo].[tblCourse] ([IDCourse], [IDLecturer], [Title], [Detail], [Price], [Sale], [Status], [Quantity]) VALUES (16, 4, N'Elementary listening', N'Practice listening to basic sounds, practice recognizing syllables', 200, 0, 1, 0)
INSERT [dbo].[tblCourse] ([IDCourse], [IDLecturer], [Title], [Detail], [Price], [Sale], [Status], [Quantity]) VALUES (17, 1007, N'Intermediate listening', N'Practice listening to articles and practice listening to long conversations', 250, 20, 1, 0)
INSERT [dbo].[tblCourse] ([IDCourse], [IDLecturer], [Title], [Detail], [Price], [Sale], [Status], [Quantity]) VALUES (18, 1, N'SWP', N'ABCLearn Website', 100, 0, 1, 29)
SET IDENTITY_INSERT [dbo].[tblCourse] OFF
 
INSERT [dbo].[tblCourseOfStudent] ([IDStudent], [IDCourse], [PurchaseDate]) VALUES (1, 12, CAST(N'2023-12-02T12:00:00.000' AS DateTime))
INSERT [dbo].[tblCourseOfStudent] ([IDStudent], [IDCourse], [PurchaseDate]) VALUES (1, 14, CAST(N'2023-12-02T12:00:00.000' AS DateTime))
INSERT [dbo].[tblCourseOfStudent] ([IDStudent], [IDCourse], [PurchaseDate]) VALUES (1, 15, CAST(N'2023-06-11T09:22:11.000' AS DateTime))
INSERT [dbo].[tblCourseOfStudent] ([IDStudent], [IDCourse], [PurchaseDate]) VALUES (2, 18, CAST(N'2023-12-02T12:00:00.000' AS DateTime))
INSERT [dbo].[tblCourseOfStudent] ([IDStudent], [IDCourse], [PurchaseDate]) VALUES (3, 18, CAST(N'2023-12-02T12:00:00.000' AS DateTime))
INSERT [dbo].[tblCourseOfStudent] ([IDStudent], [IDCourse], [PurchaseDate]) VALUES (4, 18, CAST(N'2023-12-02T12:00:00.000' AS DateTime))
INSERT [dbo].[tblCourseOfStudent] ([IDStudent], [IDCourse], [PurchaseDate]) VALUES (5, 18, CAST(N'2023-12-02T12:00:00.000' AS DateTime))
 
SET IDENTITY_INSERT [dbo].[tblLecturer] ON 

INSERT [dbo].[tblLecturer] ([IDLecturer], [FirstName], [LastName], [RoleID], [Email], [Password], [Phone], [Avatar]) VALUES (1, N'Minh', N'Thong', N'Lecturer  ', N'thongngu0189@gmail.com', N'Thong0189@@', N'0123456789', N'Lecturer-1.jpg')
INSERT [dbo].[tblLecturer] ([IDLecturer], [FirstName], [LastName], [RoleID], [Email], [Password], [Phone], [Avatar]) VALUES (2, N'Minh', N'Trien', N'Lecturer  ', N'trien123@gmail.com', N'trien123', N'0123456789', N'Lecturer-2.jpg')
INSERT [dbo].[tblLecturer] ([IDLecturer], [FirstName], [LastName], [RoleID], [Email], [Password], [Phone], [Avatar]) VALUES (3, N'Pham', N'Vu', N'Lecturer  ', N'vu123@gmail.com', N'vu123', N'0123456789', N'Lecturer-3.jpg')
INSERT [dbo].[tblLecturer] ([IDLecturer], [FirstName], [LastName], [RoleID], [Email], [Password], [Phone], [Avatar]) VALUES (4, N'Thai', N'Dat', N'Lecturer  ', N'dat123@gmail.com', N'dat123', N'0123456789', N'Lecturer-4.jpg')
INSERT [dbo].[tblLecturer] ([IDLecturer], [FirstName], [LastName], [RoleID], [Email], [Password], [Phone], [Avatar]) VALUES (5, N'Tran', N'Thuc', N'Lecturer  ', N'thuc123@gmail.com', N'thuc123', N'0123456789', N'Lecturer-5.jpg')
INSERT [dbo].[tblLecturer] ([IDLecturer], [FirstName], [LastName], [RoleID], [Email], [Password], [Phone], [Avatar]) VALUES (1006, N'thong', N'thong', N'Lecturer  ', N'Thong@gmail.com', N'thong', N'0123456789', N'Lecturer.png')
INSERT [dbo].[tblLecturer] ([IDLecturer], [FirstName], [LastName], [RoleID], [Email], [Password], [Phone], [Avatar]) VALUES (1007, N'thong', N'minht', N'Lecturer  ', N'thong123@gmail.com', N'1231231233', N'123456789 ', N'Lecturer.png')
SET IDENTITY_INSERT [dbo].[tblLecturer] OFF
 
SET IDENTITY_INSERT [dbo].[tblNontification] ON 

INSERT [dbo].[tblNontification] ([IDContification], [Title], [Detail], [StartTime], [EndTime]) VALUES (5, N'test', N'test', CAST(N'2023-06-13T00:00:00.000' AS DateTime), CAST(N'2023-06-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNontification] ([IDContification], [Title], [Detail], [StartTime], [EndTime]) VALUES (6, N'test2', N'test2', CAST(N'2023-06-11T00:00:00.000' AS DateTime), CAST(N'2023-06-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNontification] ([IDContification], [Title], [Detail], [StartTime], [EndTime]) VALUES (8, N'Wellcome Summer', N'Welcome summer break, communication courses are on sale from 10%-20% for new students of the center.', CAST(N'2023-06-13T00:00:00.000' AS DateTime), CAST(N'2023-07-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNontification] ([IDContification], [Title], [Detail], [StartTime], [EndTime]) VALUES (9, N'Back to school', N'Welcome, reading courses are on sale from 5%-10% for students of the center.', CAST(N'2023-06-13T00:00:00.000' AS DateTime), CAST(N'2023-07-30T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tblNontification] OFF
 
SET IDENTITY_INSERT [dbo].[tblQuiz] ON 

INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (4, N'They ________because it is a national holiday.', N'don’t work', N'won’t work', N'haven’t worked', N'aren’t working', N'don’t work', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (5, N'When I last saw him, he _____ in London.', N'has lived', N'is living', N'was living', N'has been living', N'was living', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (6, N'We _______ Dorothy since last Saturday.', N'don’t see', N'haven’t seen', N'didn’t see', N'hadn’t seen', N'haven’t seen', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (7, N'The train ______ half an hour a .', N'has been leaving', N'left', N'has left', N'had left', N'left', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (8, N'My sister ________ for you since yesterday.', N'is looking', N'was looking', N'has been looking', N'looked', N'has been looking', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (9, N'Christopher Columbus _______ American more than 500 years a .', N'discovered', N'has discovered', N'had discovered', N'had been discovering', N'discovered', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (10, N'We _______ there when our father died.', N'still lived', N'lived still', N'was still lived', N'was still living', N'still lived', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (11, N'They ______ table tennis when their father comes back home.', N'will play', N'will be playing', N'play', N'would play', N'will be playing', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (12, N'By Christmas, I _______ for Mr. Smith for six years.', N'will have been working', N'will work', N'have been working', N'will be working', N'will have been working', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (13, N'I ______ to New York three times this year.', N'have been', N'was', N'were', N'had been', N'have been', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (14, N'I’ll come and see you before I _______ for the States.', N'leave', N'will leave', N'have left', N'shall leave', N'leave', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (15, N'The little girl asked what _______ to her friend.', N'has happened', N'happened', N'had happened', N'would have been happened', N'had happened', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (16, N'I have been waiting for you ______ .', N'since early morning', N'since 9 a.m', N'for two hours', N'All are correct', N'All are correct', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (17, N'Almost everyone _______ for home by the time we arrived.', N'leave', N'left', N'leaves', N'had left', N'had left', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (18, N'By the age of 25, he ______ two famous novels.', N'wrote', N'writes', N'has written', N'had written', N'had written', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (19, N'When her husband was in the army, Mary ______ to him twice a week', N'was reading', N'wrote', N'was written', N'had written', N'wrote', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (20, N'I couldn’t cut the grass because the lawn mower ______ a few days previously.', N'broke down', N'has been broken', N'had broken down', N'breaks down', N'had broken down', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (21, N'Henry _______ into the restaurant when the writer was having dinner.', N'was  ing', N'went', N'has  ne', N'did  ', N'went', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (22, N'He will take the dog out for a walk as soon as he ______ dinner.', N'finish', N'finishes', N'will finish', N'finishing', N'finishes', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (23, N'Last week, my professor promised that he ________ today.', N'would come', N'will come', N'comes', N'coming', N'would come', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (24, N'I ______ to the same barber since last year.', N'am  ing', N'have been  ing', N' ', N'had  ne', N'have been  ing', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (25, N'He always ________ for a walk in the evening.', N' ', N'is  ing', N' es', N' ing', N' es', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (26, N'_______ raiding for camels was a significant part of Bedouin life has been documented in Wilfed Thesiger’s Arabian Sands.', N'That', N'Which', N'What', N'Where', N'That', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (27, N'The little boy pleaded _____ not to leave him alone in the dark.', N'on his mother', N'his mother', N'with his mother', N'at his mother', N'with his mother', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (28, N'_____, the people who come to this club are in their twenties and thirties.', N'By and large', N'Altogether', N'To a degree', N'Virtually', N'By and large', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (29, N'The TV station, in _______ to massive popular demand, decided not to discontinue the soap opera.', N'reaction', N'response', N'answer', N'rely', N'response', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (30, N'"Just think, ______ 2 years'' time, we''ll be 20 both."', N'under', N'in', N'after', N'over', N'over', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (31, N'It''s said that he has ______ friends of his age.', N'few', N'plenty', N'a little', N'little', N'few', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (32, N'The students ______ to be at school at 3 p.m.', N'told', N'have told', N'were told', N'tell', N'were told', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (33, N'Elizabeth is fond ______  ing to dances.', N'in', N'of', N'with', N'at', N'of', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (34, N'That is last year’s telephone directory. It’s ______ date now.', N'into', N'out of', N'besides', N'out', N'out of', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (35, N'Quan (start) (drive) to work 5 months a  because he (buy) a car.', N'starts – driving – will buy', N'started – driven – buys', N'has started – driving – bought', N'started – to drive – has bought', N'started – to drive – has bought', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (36, N'Prices ________ in 1995 but then _____ in 1996.', N'rised _ falled', N'rose _ fell', N'have risen _ have fallen', N'rose _ have fallen', N'rose _ have fallen', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (37, N'Her hometown _______________ a lot of hills and mountains.', N'has', N'gets', N'is', N'has  t', N'has', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (38, N'______ from a business trip to NewYork.', N'I come back', N'I came back', N'I never came back', N'I''ve just come back', N'I''ve just come back', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (39, N'I __________ his mother since I _________ 5 years old because she was my neighbour.', N'have known – be', N'have known – was', N'knew – was', N'knew – were', N'have known – was', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (40, N'I ___________ hot food before. It is quite strange to me.', N'haven’t try', N'haven’t tried', N'don’t try', N'didn’t try', N'haven’t tried', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (41, N'_____________ in Vietnamese?', N'Did you ever worked', N'Have you ever worked', N'Worked you', N'Didn''t you have worked', N'Have you ever worked', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (42, N'When ____________ to Ha Long?', N'have you joined', N'did you joined', N'did you join', N'have you ever joined', N'did you join', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (43, N'Jimmy sent his mother a ___________ of flowers for her birthday.', N'bar', N'bunch', N'pack', N'packet', N'bunch', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (44, N'My plans to travel around the world have _________ through because I couldn"t save enough money.', N'fallen', N'put', N'dropped', N'given', N'fallen', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (45, N'“I thought you bought a new fountain pen last week?” “Yes, I did but I left it at home. Can I borrow _____________ for a moment?”', N'the one of you', N'one of yours', N'one pen of you', N'the one of your pen', N'one of yours', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (46, N'A little farther down the street _______________.', N'is the inn I used to stay at', N'there is an inn where I used to stay in', N'the inn is the place where I used to stay', N'is there an inn in which I used to stay', N'there is an inn where I used to stay in', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (47, N'“I wonder if you could help me?” - “______________”', N'No, what is it?', N'Really? How nice.', N'Don’t mention it.', N'I’ll do my best. What’s the problem?', N'I’ll do my best. What’s the problem?', 13)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (48, N'You shouldn''t _________to your teacher like that. It was very rude.', N'have talked', N'talk', N'have be talked', N'talked', N'talk', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (49, N'As it was getting late, the boys decided to _______ the campfire and crept into their sleeping bags.', N'put up', N'put off', N'put out', N'put on', N'put out', 15)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (50, N'_______ is the study of the Earth''s physical features and the people, plants, and animals that live in different regions of the world.', N'Science', N'Geography', N'History', N'Technology', N'Geography', 16)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (51, N'“ Don’t stay anywhere near the railway station at night. It’s dangerous.” - “_____________.”', N'Ok. That will do.', N'They won’t like it, I bet.', N'I definitely won’t. Thank you.', N'Where on Earth have you been?', N'I definitely won’t. Thank you.', 17)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (52, N'Alan took a photograph of Sandra while she________', N'didn''t look', N'wasn''t looking', N'hasn''t looked', N'hadn''t looked', N'wasn''t looking', 12)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2014, N'They ________because it is a national holiday.', N'don’t work', N'won’t work', N'haven’t worked', N'aren’t working', N'don’t work', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2015, N'When I last saw him, he _____ in London.', N'has lived', N'is living', N'was living', N'has been living', N'was living', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2016, N'We _______ Dorothy since last Saturday.', N'don’t see', N'haven’t seen', N'didn’t see', N'hadn’t seen', N'haven’t seen', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2017, N'The train ______ half an hour a .', N'has been leaving', N'left', N'has left', N'had left', N'left', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2018, N'My sister ________ for you since yesterday.', N'is looking', N'was looking', N'has been looking', N'looked', N'has been looking', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2019, N'Christopher Columbus _______ American more than 500 years a .', N'discovered', N'has discovered', N'had discovered', N'had been discovering', N'discovered', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2020, N'We _______ there when our father died.', N'still lived', N'lived still', N'was still lived', N'was still living', N'still lived', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2021, N'They ______ table tennis when their father comes back home.', N'will play', N'will be playing', N'play', N'would play', N'will be playing', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2022, N'By Christmas, I _______ for Mr. Smith for six years.', N'will have been working', N'will work', N'have been working', N'will be working', N'will have been working', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2023, N'I ______ to New York three times this year.', N'have been', N'was', N'were', N'had been', N'have been', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2024, N'I’ll come and see you before I _______ for the States.', N'leave', N'will leave', N'have left', N'shall leave', N'leave', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2025, N'The little girl asked what _______ to her friend.', N'has happened', N'happened', N'had happened', N'would have been happened', N'had happened', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2026, N'I have been waiting for you ______ .', N'since early morning', N'since 9 a.m', N'for two hours', N'All are correct', N'All are correct', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2027, N'Almost everyone _______ for home by the time we arrived.', N'leave', N'left', N'leaves', N'had left', N'had left', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2028, N'By the age of 25, he ______ two famous novels.', N'wrote', N'writes', N'has written', N'had written', N'had written', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2029, N'When her husband was in the army, Mary ______ to him twice a week', N'was reading', N'wrote', N'was written', N'had written', N'wrote', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2030, N'I couldn’t cut the grass because the lawn mower ______ a few days previously.', N'broke down', N'has been broken', N'had broken down', N'breaks down', N'had broken down', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2031, N'Henry _______ into the restaurant when the writer was having dinner.', N'was  ing', N'went', N'has  ne', N'did  ', N'went', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2032, N'He will take the dog out for a walk as soon as he ______ dinner.', N'finish', N'finishes', N'will finish', N'finishing', N'finishes', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2033, N'Last week, my professor promised that he ________ today.', N'would come', N'will come', N'comes', N'coming', N'would come', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2034, N'I ______ to the same barber since last year.', N'am  ing', N'have been  ing', N' ', N'had  ne', N'have been  ing', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2035, N'He always ________ for a walk in the evening.', N' ', N'is  ing', N' es', N' ing', N' es', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2036, N'_______ raiding for camels was a significant part of Bedouin life has been documented in Wilfed Thesiger’s Arabian Sands.', N'That', N'Which', N'What', N'Where', N'That', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2037, N'The little boy pleaded _____ not to leave him alone in the dark.', N'on his mother', N'his mother', N'with his mother', N'at his mother', N'with his mother', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2038, N'_____, the people who come to this club are in their twenties and thirties.', N'By and large', N'Altogether', N'To a degree', N'Virtually', N'By and large', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2039, N'The TV station, in _______ to massive popular demand, decided not to discontinue the soap opera.', N'reaction', N'response', N'answer', N'rely', N'response', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2040, N'"Just think, ______ 2 years'' time, we''ll be 20 both."', N'under', N'in', N'after', N'over', N'over', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2041, N'It''s said that he has ______ friends of his age.', N'few', N'plenty', N'a little', N'little', N'few', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2042, N'The students ______ to be at school at 3 p.m.', N'told', N'have told', N'were told', N'tell', N'were told', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2043, N'Elizabeth is fond ______  ing to dances.', N'in', N'of', N'with', N'at', N'of', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2044, N'That is last year’s telephone directory. It’s ______ date now.', N'into', N'out of', N'besides', N'out', N'out of', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2045, N'Quan (start) (drive) to work 5 months a  because he (buy) a car.', N'starts – driving – will buy', N'started – driven – buys', N'has started – driving – bought', N'started – to drive – has bought', N'started – to drive – has bought', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2046, N'Prices ________ in 1995 but then _____ in 1996.', N'rised _ falled', N'rose _ fell', N'have risen _ have fallen', N'rose _ have fallen', N'rose _ have fallen', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2047, N'Her hometown _______________ a lot of hills and mountains.', N'has', N'gets', N'is', N'has  t', N'has', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2048, N'______ from a business trip to NewYork.', N'I come back', N'I came back', N'I never came back', N'I''ve just come back', N'I''ve just come back', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2049, N'I __________ his mother since I _________ 5 years old because she was my neighbour.', N'have known – be', N'have known – was', N'knew – was', N'knew – were', N'have known – was', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2050, N'I ___________ hot food before. It is quite strange to me.', N'haven’t try', N'haven’t tried', N'don’t try', N'didn’t try', N'haven’t tried', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2051, N'_____________ in Vietnamese?', N'Did you ever worked', N'Have you ever worked', N'Worked you', N'Didn''t you have worked', N'Have you ever worked', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2052, N'When ____________ to Ha Long?', N'have you joined', N'did you joined', N'did you join', N'have you ever joined', N'did you join', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2053, N'Jimmy sent his mother a ___________ of flowers for her birthday.', N'bar', N'bunch', N'pack', N'packet', N'bunch', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2054, N'My plans to travel around the world have _________ through because I couldn"t save enough money.', N'fallen', N'put', N'dropped', N'given', N'fallen', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2055, N'“I thought you bought a new fountain pen last week?” “Yes, I did but I left it at home. Can I borrow _____________ for a moment?”', N'the one of you', N'one of yours', N'one pen of you', N'the one of your pen', N'one of yours', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2056, N'A little farther down the street _______________.', N'is the inn I used to stay at', N'there is an inn where I used to stay in', N'the inn is the place where I used to stay', N'is there an inn in which I used to stay', N'there is an inn where I used to stay in', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2057, N'“I wonder if you could help me?” - “______________”', N'No, what is it?', N'Really? How nice.', N'Don’t mention it.', N'I’ll do my best. What’s the problem?', N'I’ll do my best. What’s the problem?', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2058, N'You shouldn''t _________to your teacher like that. It was very rude.', N'have talked', N'talk', N'have be talked', N'talked', N'talk', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2059, N'As it was getting late, the boys decided to _______ the campfire and crept into their sleeping bags.', N'put up', N'put off', N'put out', N'put on', N'put out', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2060, N'_______ is the study of the Earth''s physical features and the people, plants, and animals that live in different regions of the world.', N'Science', N'Geography', N'History', N'Technology', N'Geography', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2061, N'“ Don’t stay anywhere near the railway station at night. It’s dangerous.” - “_____________.”', N'Ok. That will do.', N'They won’t like it, I bet.', N'I definitely won’t. Thank you.', N'Where on Earth have you been?', N'I definitely won’t. Thank you.', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (2062, N'Alan took a photograph of Sandra while she________', N'didn''t look', N'wasn''t looking', N'hasn''t looked', N'hadn''t looked', N'wasn''t looking', 14)
INSERT [dbo].[tblQuiz] ([IDQuiz], [Question], [AnswerA], [AnswerB], [AnswerC], [AnswerD], [CorrectAnswer], [IDCourse]) VALUES (3014, N'Question', N'A', N'B', N'C', N'D', N'D', 14)
 
SET IDENTITY_INSERT [dbo].[tblQuiz] OFF
 
SET IDENTITY_INSERT [dbo].[tblStudent] ON 

INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (1, N'Minh', N'Thong', N'Student   ', N'Thong0189@', N'thong123@gmail.com', N'0123456888', 1, CAST(N'2023-06-26T07:28:17.000' AS DateTime), N'Male      ', CAST(N'2002-12-21' AS Date), N'Student-1.png')
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (2, N'Pham Van', N'Vu', N'Student   ', N'12345', N'vupv12@gmail.com', N'0973454309', 1, CAST(N'2023-06-26T07:28:17.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (3, N'Thai Thanh', N'Dat', N'Student   ', N'11111', N'dattt12@gmail.com', N'0948846632', 1, CAST(N'2023-06-26T07:28:17.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (4, N'Thuc', N'Tran', N'Student   ', N'11111', N'thuc12@gmail.com', N'0987234029', 1, CAST(N'2023-06-26T07:28:17.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (5, N'Trien Tran', N'Minh', N'Student   ', N'12121', N'trien2@gmail.com', N'0987987245', 1, CAST(N'2023-06-26T07:28:17.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (6, N'Dat nguyen', N'Van', N'Student   ', N'123456', N'DatNguyen@gmail.com', N'0987354098', 1, CAST(N'2023-06-17T07:28:17.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (7, N'Tran Anh', N'thuc', N'Student   ', N'thuc1212', N'Tranthuc12@gmail.com', N'0765123987', 1, CAST(N'2023-04-23T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (8, N'Nguyen Nhon', N'Tran', N'Student   ', N'nhon12345', N'nhon1212@gmail.com', N'0987343648', 1, CAST(N'2023-04-23T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (9, N'The', N'Toan', N'Student   ', N'thetoan12', N'thetoan12@gmail.com', N'0987876876', 1, CAST(N'2023-04-23T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (10, N'Hai', N'Yen', N'Student   ', N'haiyen12', N'haiyen12@gmail.com', N'0987876345', 1, CAST(N'2023-04-24T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (11, N'Nguyen', N'Xuan', N'Student   ', N'Nguyenxuan12', N'NguyenXuan@gmail.com', N'0987111222', 1, CAST(N'2023-04-24T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (12, N'Thai ', N'Hoang', N'Student   ', N'Thaihoang', N'Thaihoang12@gmail.com', N'0987777524', 1, CAST(N'2023-04-24T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (13, N'Xuan ', N'Long', N'Student   ', N'XuanLong11111', N'XuanLong12@gmail.com', N'0987654654', 1, CAST(N'2023-04-24T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (14, N'Duong', N'Khai', N'Student   ', N'DuongKhai11', N'DuongKhai12@gmail.com', N'0987111222', 1, CAST(N'2023-04-24T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (15, N'Testter', N'number1', N'Student   ', N'testStudent', N'student@gmail.com', N'0987464533', 1, CAST(N'2023-04-24T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (16, N'Duc', N'Luong', N'Student   ', N'DucLuong11', N'ducLuong111@gmail.com', N'0999333765', 1, CAST(N'2023-04-24T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (17, N'Hoang', N'Yen', N'Student   ', N'Hoang12Yen', N'hoangyen111@gmail.com', N'0986354123', 1, CAST(N'2023-04-25T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (18, N'Huong', N'Pham', N'Student   ', N'huong1212', N'huong1212@gmail.com', N'0948674888', 1, CAST(N'2023-04-25T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (19, N'Pham', N'Hao', N'Student   ', N'haopham12', N'haopham2@gmail.com', N'0987666544', 1, CAST(N'2023-04-25T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (20, N'Anh ', N'Tuan', N'Student   ', N'anh12', N'AnhTuan12@gmail.com', N'0984536722', 1, CAST(N'2023-04-25T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (21, N'Thu ', N'Hang', N'Student   ', N'thuhang12', N'Thuhang12@gmail.com', N'0987493784', 1, CAST(N'2023-04-25T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (22, N'Nguyen', N'Ha', N'Student   ', N'hanguyen12', N'NguyenHa12@gmail.com', N'0983453121', 1, CAST(N'2023-04-25T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (23, N'Pham ', N'Ly', N'Student   ', N'LyPham12', N'lypham12@gmail.com', N'0984655098', 1, CAST(N'2023-04-25T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (25, N'Quoc', N'Dat', N'Student   ', N'QuocDat12', N'QuocDat12@gmail.com', N'0987465122', 1, CAST(N'2023-04-26T12:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-01-02' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (1039, N'thong', N'minht', N'Student   ', N'Thong0189@', N'thong12322@gmail.com', N'0123456789', 0, CAST(N'2023-07-09T20:24:16.000' AS DateTime), N'Male      ', CAST(N'1212-12-12' AS Date), NULL)
INSERT [dbo].[tblStudent] ([IDStudent], [FirstName], [LastName], [RoleID], [Password], [Email], [Phone], [ConfirmEmail], [DateCreate], [Gander], [DOB], [Avatar]) VALUES (1043, N'thong', N'heheh', N'Student   ', N'123', N'thong@gmail.com', N'0123456789', 1, CAST(N'2023-07-09T00:00:00.000' AS DateTime), N'Male      ', CAST(N'2002-02-02' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[tblStudent] OFF
 
SET IDENTITY_INSERT [dbo].[tblTransactionHistory] ON 

INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (2, 1, N'Elementary reading', 200, CAST(N'2023-06-08T22:25:09.000' AS DateTime), N'VNPAY     ', N'549432146876132156')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (3, 1, N'Elementary reading', 200, CAST(N'2023-06-08T12:25:09.000' AS DateTime), N'MOMO      ', N'548613213584851321')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (4, 1, N'Reading', 250, CAST(N'2023-06-09T12:25:09.000' AS DateTime), N'MOMO      ', N'21354315345987845')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (5, 1, N'test', 100, CAST(N'2023-06-09T19:34:23.000' AS DateTime), N'MOMO      ', N'213435121545')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (1003, 1, N'Intermediate communication', 250, CAST(N'2023-06-11T09:22:11.000' AS DateTime), N'MOMO      ', N'638220469178425557')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (1004, 1, N'SWP', 100, CAST(N'2023-06-16T11:19:09.000' AS DateTime), N'MOMO      ', N'638224859076688752')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (1005, 1, N'SWP', 100, CAST(N'2023-06-16T11:21:59.000' AS DateTime), N'MOMO      ', N'638224860972835187')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (2007, 1, N'SWP', 100, CAST(N'2023-06-24T08:59:11.000' AS DateTime), N'VNPAY     ', N'638231687063369706')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (4008, 1, N'Intermediate listening', 250, CAST(N'2023-06-28T08:08:51.000' AS DateTime), N'MOMO      ', N'14051465')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (4009, 1, N'Intermediate listening', 250, CAST(N'2023-06-28T08:18:03.000' AS DateTime), N'MOMO      ', N'14051473')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (4010, 1, N'Intermediate listening', 250, CAST(N'2023-06-28T08:22:25.000' AS DateTime), N'VNPAY     ', N'14051482')
INSERT [dbo].[tblTransactionHistory] ([IDHistory], [IDStudent], [CourseTitle], [Price], [Date], [Method], [OrderID]) VALUES (4011, 1, N'SWP', 100, CAST(N'2023-06-28T09:52:30.000' AS DateTime), N'MOMO      ', N'638235175271001377')
SET IDENTITY_INSERT [dbo].[tblTransactionHistory] OFF
 
ALTER TABLE [dbo].[tblCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblCalendar_Course] FOREIGN KEY([IDCourse])
REFERENCES [dbo].[tblCourse] ([IDCourse])
 
ALTER TABLE [dbo].[tblCalendar] CHECK CONSTRAINT [FK_tblCalendar_Course]
 
ALTER TABLE [dbo].[tblCheckTeaching]  WITH CHECK ADD  CONSTRAINT [FK_tblCheckTeaching_tblLecturer] FOREIGN KEY([IDLecturer])
REFERENCES [dbo].[tblLecturer] ([IDLecturer])
 
ALTER TABLE [dbo].[tblCheckTeaching] CHECK CONSTRAINT [FK_tblCheckTeaching_tblLecturer]
 
ALTER TABLE [dbo].[tblCommentOfStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblCommentOfStudent_Course] FOREIGN KEY([IDCourse])
REFERENCES [dbo].[tblCourse] ([IDCourse])
 
ALTER TABLE [dbo].[tblCommentOfStudent] CHECK CONSTRAINT [FK_tblCommentOfStudent_Course]
 
ALTER TABLE [dbo].[tblCommentOfStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblCommentOfStudent_Student] FOREIGN KEY([IDStudent])
REFERENCES [dbo].[tblStudent] ([IDStudent])
 
ALTER TABLE [dbo].[tblCommentOfStudent] CHECK CONSTRAINT [FK_tblCommentOfStudent_Student]
 
ALTER TABLE [dbo].[tblCourse]  WITH CHECK ADD  CONSTRAINT [FK_tblCourse_tblLecturer] FOREIGN KEY([IDLecturer])
REFERENCES [dbo].[tblLecturer] ([IDLecturer])
 
ALTER TABLE [dbo].[tblCourse] CHECK CONSTRAINT [FK_tblCourse_tblLecturer]
 
ALTER TABLE [dbo].[tblCourseOfStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblCourseOfStudent_Course] FOREIGN KEY([IDCourse])
REFERENCES [dbo].[tblCourse] ([IDCourse])
 
ALTER TABLE [dbo].[tblCourseOfStudent] CHECK CONSTRAINT [FK_tblCourseOfStudent_Course]
 
ALTER TABLE [dbo].[tblCourseOfStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblCourseOfStudent_Student] FOREIGN KEY([IDCourse])
REFERENCES [dbo].[tblStudent] ([IDStudent])
 
ALTER TABLE [dbo].[tblCourseOfStudent] CHECK CONSTRAINT [FK_tblCourseOfStudent_Student]
 
ALTER TABLE [dbo].[tblQuiz]  WITH CHECK ADD  CONSTRAINT [FK_tblQuiz_tblCourse] FOREIGN KEY([IDCourse])
REFERENCES [dbo].[tblCourse] ([IDCourse])
 
ALTER TABLE [dbo].[tblQuiz] CHECK CONSTRAINT [FK_tblQuiz_tblCourse]
 
ALTER TABLE [dbo].[tblTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblTransactionHistory_tblStudent] FOREIGN KEY([IDStudent])
REFERENCES [dbo].[tblStudent] ([IDStudent])
 
ALTER TABLE [dbo].[tblTransactionHistory] CHECK CONSTRAINT [FK_tblTransactionHistory_tblStudent]
 
/****** Object:  Trigger [dbo].[trg_Enrollment_Delete]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TRIGGER [dbo].[trg_Enrollment_Delete]
ON [dbo].[tblCourseOfStudent]
AFTER DELETE
AS
BEGIN
    -- Cập nhật số lượng người học giảm đi 1 cho khóa học liên quan
    UPDATE tblCourse
    SET Quantity = Quantity - 1
    WHERE tblCourse.IDCourse IN (SELECT IDCourse FROM deleted);

    -- Kiểm tra và cập nhật trạng thái của khóa học nếu số lượng < 30
    UPDATE tblCourse
    SET Status = 1
    WHERE tblCourse.IDCourse IN (
        SELECT IDCourse
        FROM tblCourse
        WHERE Quantity < 30
    );
END;
 
ALTER TABLE [dbo].[tblCourseOfStudent] ENABLE TRIGGER [trg_Enrollment_Delete]
 
/****** Object:  Trigger [dbo].[trg_Enrollment_Insert]    Script Date: 7/18/2023 7:28:53 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TRIGGER [dbo].[trg_Enrollment_Insert]
ON [dbo].[tblCourseOfStudent]
AFTER INSERT
AS
BEGIN
    -- Cập nhật số lượng người học tăng lên 1 cho khóa học liên quan
    UPDATE tblCourse
    SET Quantity = Quantity + 1
    WHERE tblCourse.IDCourse IN (SELECT IDCourse FROM inserted);

    -- Kiểm tra và cập nhật trạng thái của khóa học nếu số lượng >= 30
    UPDATE tblCourse
    SET Status = 0
    WHERE tblCourse.IDCourse IN (
        SELECT IDCourse
        FROM tblCourse
        WHERE Quantity >= 30
    );
END;
 
ALTER TABLE [dbo].[tblCourseOfStudent] ENABLE TRIGGER [trg_Enrollment_Insert]
 
/****** Object:  Trigger [dbo].[trg_DeleteStudent]    Script Date: 7/18/2023 7:28:54 AM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TRIGGER [dbo].[trg_DeleteStudent]
ON [dbo].[tblStudent]
INSTEAD OF DELETE
AS
BEGIN
    -- Xóa dữ liệu từ bảng comment liên quan
    DELETE FROM tblCommentOfStudent
    WHERE IDStudent IN (SELECT IDStudent FROM deleted);

    -- Xóa dữ liệu từ bảng course liên quan
    DELETE FROM tblCourseOfStudent
    WHERE IDStudent IN (SELECT IDStudent FROM deleted);

	-- Xóa dữ liệu từ bảng transaction liên quan
    DELETE FROM tblTransactionHistory
    WHERE IDStudent IN (SELECT IDStudent FROM deleted);

	DELETE FROM tblStudent
    WHERE IDStudent IN (SELECT IDStudent FROM deleted);
END;
 
ALTER TABLE [dbo].[tblStudent] ENABLE TRIGGER [trg_DeleteStudent]
 
USE [master]
 
ALTER DATABASE [ABCLearn] SET  READ_WRITE 
 
