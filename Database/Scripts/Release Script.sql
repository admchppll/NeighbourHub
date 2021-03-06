/*
TITLE:		RELEASE SCRIPT
INSTRUCTIONS:	RUN WHEN THE DATABASE IS INITIALISED

STEPS:	1) Tag
	2) Skill

__________________________________________________________
	ADD INITIAL TAG VALUES TO DATABASE
________________________________________________________*/

IF (	SELECT	COUNT(*) 
	FROM	Tag) = 0 
BEGIN
	INSERT INTO 
		Tag (Name) 
	VALUES 
		('Gardening'),
		('Computer'),
		('Community'),
		('DIY'),
		('Cleaning'),
		('Driving')

	PRINT('Initial Tag Entries Successfully Added')
END
ELSE 
BEGIN
	PRINT('Tag Entries Not Added! - Entries already present in database!')
END

/*________________________________________________________
	ADD INITIAL SKILL VALUES TO DATABASE
________________________________________________________*/

IF (	SELECT	COUNT(*) 
	FROM	Skill) = 0 
BEGIN
	INSERT INTO 
		Skill (Label) 
	VALUES 
		('Design'),
		('Carpentry'),
		('Catering'),
		('Driving'),
		('Plumbing'),
		('Nursing')

	PRINT('Initial Skill Entries Successfully Added')
END
ELSE 
BEGIN
	PRINT('Skill Entries Not Added! - Entries already present in database!')
END