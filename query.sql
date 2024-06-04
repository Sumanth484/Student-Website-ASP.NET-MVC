-- Create the sample database
CREATE DATABASE sample;
GO

-- Use the newly created database
USE SampleDatabase;
GO

-- Create the Students table
CREATE TABLE Students (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    age INT,
    city VARCHAR(100),
    section VARCHAR(50),
    gender VARCHAR(10) -- Added gender column
);
GO
