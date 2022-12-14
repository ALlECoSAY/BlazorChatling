CREATE TABLE Users (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	tag VARCHAR(100) NOT NULL
);


CREATE TABLE Chats (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(100) NOT NULL
);


CREATE TABLE Chat_User (
	id_user INT NOT NULL,
	id_chat INT NOT NULL,
	FOREIGN KEY (id_user) REFERENCES Users(id) ON DELETE CASCADE,
	FOREIGN KEY (id_chat) REFERENCES Chats(id) ON DELETE CASCADE
);

CREATE TABLE Messages (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	id_chat INT NOT NULL,
	id_user INT NOT NULL,
	id_reply_message INT,
	id_reply_user INT,
	msg_text TEXT NOT NULL,
	msg_time DATETIME DEFAULT GETDATE(),
	is_edited BIT NOT NULL DEFAULT 'FALSE',
	is_visible_to_creator BIT NOT NULL DEFAULT 'TRUE',
	is_reply_visible_to_group BIT NOT NULL DEFAULT 'TRUE',
	FOREIGN KEY (id_user) REFERENCES Users(id),
	FOREIGN KEY (id_reply_message) REFERENCES Messages(id),
	FOREIGN KEY (id_chat) REFERENCES Chats(id) ON DELETE CASCADE
);
