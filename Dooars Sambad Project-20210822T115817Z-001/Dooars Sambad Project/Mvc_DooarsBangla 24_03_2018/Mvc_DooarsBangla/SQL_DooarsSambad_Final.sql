create database DooarsBangla_DB_Final
use DooarsBangla_DB_Final
create table Admins
(
AdminID int primary key identity(20,1) not null,
AdminName varchar(30) not null,
AdminMbl varchar(10) not null,
AdminEmail varchar(40) not null,
AdminStatus varchar(10) not null,
PasswordStatus varchar(13) not null
)



create table MarqueeNews
(
MarqueeNewsID int primary key identity(1000,1) not null,
MarqueeDescription nvarchar(100) not null,
MarqueeStatus varchar(10) not null
)
select * from MarqueeNews
insert MarqueeNews values('','')
insert ScrollerImage values('','','','')

create table ScrollerImage
(
ScrImageID int primary key identity(10000,1) not null,
ImageHeader nvarchar(100) not null,
ImageDescription nvarchar(180) not null,
ImageAddress varchar(100) not null,
Position int not null
)



create table AllNewstable
(
NewsID varchar(20) primary key not null,
NewsTitle nvarchar(100) not null,
Category varchar(50) not null,
Section1Description nvarchar(4000) NOT NULL,
Section2Description nvarchar(3500) NOT NULL,
Section3Description nvarchar(3000) NOT NULL,
IsLatest varchar(3) not null,
IsPopular varchar(3) not null,
PriorityOfNews varchar(3),
MarqueeNewsID int foreign key references MarqueeNews(MarqueeNewsID) not null,
NewsDate Date not null,
ScrImageID  int foreign key references ScrollerImage(ScrImageID) not null
)


create table NewsPhoto
(
NewsID varchar(20) foreign key references AllNewstable(NewsID) not null,
Section int not null,
ImageAddress varchar(100) not null
)


create table NewsVideo
(
NewsID varchar(20) foreign key references AllNewstable(NewsID) not null,
Section int not null,
IFrameAddress varchar(500) not null
)

create table Gallery
(
ImageID int primary key identity(1,1) not null,
ImageTitle nvarchar(150) not null,
ImageAddress varchar(100) not null,
Position int not null
)

create table feedback
(
feedbackID int primary key identity(1,1),
name varchar(10) not null,
contactMobile varchar(10) not null,
feedbackMessage varchar(140) not null,
feedbackStatus varchar(10) not null
)


create table NewsPaperPage
(
PaperImageID int primary key identity(11111,1) not null,
ImageAddress varchar(100) not null,
PageNumber int not null
)