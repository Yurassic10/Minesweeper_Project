Create Database MinesweeperDB;
Go

Use MinesweeperDB;
Go

-- Table of players 
Create Table Players(
	Id Int Primary Key Identity(1,1),
	Name NVarchar(100) Not Null,
	RegisteredAt Datetime Not Null Default GetDate()
);


-- Table of results
Create Table GameResults(
	Id Int Primary Key Identity(1,1),
	PlayerId Int Not Null,
	DurationSeconds Int Not Null,
	Result NVarchar(10) Not Null, -- 'win' or 'loss'
	DatePlayed DateTime Not Null Default GetDate(),
	Score Int Not Null,
	Foreign Key(PlayerId) references Players(Id)
);
GO