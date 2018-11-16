create table dbo.Module
(
	ModuleId	int identity(1,1) not null primary key,
	Details		varchar(max)
)

create table dbo.Schedule
(
	SceduleID		int identity(1,1) not null primary key,
	ModuleId		int unique foreign key references dbo.Module(ModuleID),
	ModuleDay		date,
	ModuleHour		time,
	ModuleLocation	varchar(100),
)


create table dbo.Lesson
(
	LessonID		int identity(1,1) not null primary key,
	ModuleId		int foreign key references dbo.Module(ModuleID),
)

create table dbo.Presentation
(
	PresentationID			int identity(1,1) not null primary key,
	LessonId				int unique foreign key references dbo.Lesson(LessonID),
	PresentationData		varbinary(max),
	PresentationExtension	varchar(10),
	
)


create table dbo.Homework
(
	HomeworkID	int identity(1,1) not null primary key,
	LessonId	int unique foreign key references dbo.Lesson(LessonID),
	Comments	varchar(max),
	IsDone		bit
)

create table dbo.Resources
(
	ResourceID		int identity(1,1) not null primary key,
	LessonId		int foreign key references dbo.Lesson(LessonID),
	ResourceData	varchar(max)
)

create table dbo.ModuleUser
(
	UserID			int identity(1,1) not null primary key,
	FirstName		varchar(100),
	LastName		varchar(200),
	AspNetUserId	nvarchar(450) unique foreign key references dbo.AspNetUsers(Id), 
	Email			nvarchar(256),
	ConnectionId	nvarchar(256)	
)

create table dbo.ChatRoom
(
	RoomID	int identity(1,1) not null primary key,
	Name	nvarchar(500)
)

create table dbo.LoggedInUsers
(
	LoggedInUserID	int identity(1,1) not null primary key,
	UserId			int unique foreign key references dbo.ModuleUser(UserID),
	RoomId			int unique foreign key references dbo.ChatRoom(RoomID),
	ConnectionId	nvarchar(256)
)

create table dbo.ChatMessage
(
	MessageID	int identity(1,1) not null primary key,
	RoomId		int unique foreign key references dbo.ChatRoom(RoomID),
	UserId		int unique foreign key references dbo.ModuleUser(UserID),
	ToUserId	int unique foreign key references dbo.ModuleUser(UserID),
	Text		nvarchar(max),
	TimeStamp	datetime default(getdate())
)

create table dbo.PrivateMessage
(
	PrivateMessageID	int identity(1,1) not null primary key,
	UserId				int unique foreign key references dbo.ModuleUser(UserID),
	ToUserId			int unique foreign key references dbo.ModuleUser(UserID),
	Text				nvarchar(max),
	TimeStamp			datetime default(getdate())
)
