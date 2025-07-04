CREATE TABLE student(
	student_id SERIAL PRIMARY KEY, --SERIAL Auto increment
	Name VARCHAR(100) NOT NULL,
	Age INT CHECK (Age>0),
	Grade VARCHAR(5),
	Email VARCHAR(50) UNIQUE,
	Create_Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  --timestamped automatically
	Update_Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP   --timestamped automatically
);

--View Table Print All Data
SELECT * FROM student;


--Insert Data
INSERT INTO student (Name,Age,Grade,Email) VALUES
('Pala', 19, 'A', 'pala1@gmail.com'),
('jay', 22, 'C', 'jay@gmail.com'),
('kavy', 20, 'B', 'kavy@gmail.com'),
('bhargav', 21, 'A', 'bhargav23@gmail.com'),
('Ramesh',18,'D','ramesh4@gmail.com');


--Add New Column
ALTER TABLE student ADD COLUMN city VARCHAR(50);

--Add value to New Column
UPDATE student SET city='Rajkot' WHERE Name='Pala';
UPDATE student SET city='Surat' WHERE Age>20;
UPDATE student SET city='Baroda' WHERE Age=18 AND Age=19;

--Rename Table Column
ALTER TABLE student RENAME COLUMN email TO email_Address;

--Rename Table
ALTER TABLE student RENAME TO STD;
ALTER TABLE STD RENAME TO student; 

--Other Table
CREATE TABLE subject(
	Sub_Name VARCHAR(50),
	sub_id SERIAL PRIMARY KEY,
	student_id INTEGER NOT NULL REFERENCES student(student_id),
	Create_Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  --timestamped automatically
	Update_Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP   --timestamped automatically
);

--View subject Table
SELECT * FROM subject;

--Add Data in subject Table
INSERT INTO subject (Sub_Name,student_id,Create_Date,Update_Date) VALUES
('Maths',1,Now(),NULL),
('Chemistry',2,Now(),NULL),
('Physics',3,Now(),NULL),
('English',4,Now(),NULL),
('Hindi',5,Now(),NULL),
('Gujarati',4,Now(),NULL);

--Drop Table REMOVE  TABLE
-- DROP TABLE IF EXISTS subject;

--Single column select
SELECT Name FROM student;
SELECT Sub_Name FROM subject;

--Multiple column
SELECT Name,Age,city FROM student;
SELECT Sub_Name,student_id FROM subject;

--Order wise print
SELECT Sub_Name,student_id FROM subject ORDER BY student_id ASC; --Asending order
SELECT Name,Age,city FROM student ORDER BY Age DESC;  --Desending order
SELECT Name,Age,city FROM student ORDER BY Age DESC,Name ASC;

--WHERE use
SELECT Name FROM student WHERE Name='Pala';
SELECT Name,Age,city FROM student WHERE Name='Pala' AND Age=19; --Both True then print
SELECT Name,Age,city FROM student WHERE Name='Ram' OR Age=18;  --Only one true print
SELECT Sub_Name,student_id FROM subject WHERE Sub_Name IN('Maths','Gujarati','Hindi');
SELECT Sub_Name,student_id FROM subject WHERE Sub_Name LIKE '%ENGLISH%'; --Like means Case Insensitive
SELECT Sub_Name,student_id FROM subject WHERE Sub_Name ILIKE '%ENGLISH%'; --Like means Case sensitive

--Join Two Table 
SELECT * FROM student AS std INNER JOIN subject As sub ON std.student_id=sub.student_id;
SELECT * FROM student AS std LEFT JOIN subject AS sub ON std.student_id=sub.student_id;
SELECT * FROM student AS std RIGHT JOIN subject AS sub ON std.student_id=sub.student_id;

--GROP BY ID AND FIND AVERAGE AGE
SELECT AVG(Age) AS average_age
FROM student;

SELECT std.Name, AVG(std.Age) AS avg_Age
FROM student std
JOIN subject sub ON std.student_id = sub.student_id
GROUP BY std.Name;

--Having
SELECT std.Name, AVG(std.Age) AS avg_Age
FROM student std
JOIN subject sub ON std.student_id = sub.student_id
GROUP BY std.Name
HAVING AVG(std.Age) > 20;

--Update data
UPDATE student SET Name='harshil',Age=23 
WHERE student_id=1;

SELECT * FROM student;

--Sub Query
SELECT * FROM subject WHERE student_id IN (
  SELECT student_id FROM student WHERE Name='bhargav'
);

--DELETE single item
DELETE FROM subject WHERE student_id = 1;
DELETE FROM student WHERE student_id = 1;

