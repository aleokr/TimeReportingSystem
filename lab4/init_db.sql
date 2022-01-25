CREATE DATABASE activity;

USE activity;

CREATE TABLE Users (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Surname VARCHAR(100)
);

CREATE TABLE Projects (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Code VARCHAR(100),
    Budget INTEGER(100),
    Active BOOLEAN, 
    ManagerId INTEGER
);

ALTER TABLE Projects ADD CONSTRAINT f_manager_id FOREIGN KEY (ManagerId) REFERENCES Users(id);

CREATE TABLE Subprojects (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Code VARCHAR(100),
    ProjectId INTEGER 
);
ALTER TABLE Subprojects ADD CONSTRAINT f_project_id FOREIGN KEY (ProjectId) REFERENCES Projects(id);

CREATE TABLE Activities (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Date VARCHAR(100),
    Code VARCHAR(100),
    Subcode VARCHAR(100),
    Time INTEGER,
    Description VARCHAR(100),
    Confirm BOOLEAN,
    Accepted BOOLEAN,
    ProjectId INTEGER,
    UserId INTEGER
);

ALTER TABLE Activities ADD CONSTRAINT f_activity_manager_id FOREIGN KEY (UserId) REFERENCES Users(id);
ALTER TABLE Activities ADD CONSTRAINT f_activity_project_id FOREIGN KEY (ProjectId) REFERENCES Projects(id);


INSERT INTO Users (Name, Surname) VALUES ('Jan', 'Kowalski');
INSERT INTO Users (Name, Surname) VALUES ('Anna', 'Fijas');
INSERT INTO Users (Name, Surname) VALUES ('Karol', 'Zimowski');
INSERT INTO Users (Name, Surname) VALUES ('Joanna', 'Buczek');
INSERT INTO Users (Name, Surname) VALUES ('Michalina', 'Stępień');

INSERT INTO Projects (Code, Budget, Active, ManagerId) VALUES ('ALFA', 1500, true, 1);
INSERT INTO Projects (Code, Budget, Active, ManagerId) VALUES ('BETA', 2300, true, 1);
INSERT INTO Projects (Code, Budget, Active, ManagerId) VALUES ('GAMMA', 3300, true, 3);
INSERT INTO Projects (Code, Budget, Active, ManagerId) VALUES ('DELTA', 4200, true, 4);
