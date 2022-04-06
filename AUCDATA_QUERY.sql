CREATE DATABASE AUCDATA
GO
USE AUCDATA

CREATE TABLE DATA_EXAMPLE(
ID INT IDENTITY(1,1),
NAME NVARCHAR(50),
CONTENT NTEXT)

INSERT INTO DATA_EXAMPLE(NAME, CONTENT) VALUES
(N'Cà phê Nam Shark', N'Được làm từ 100% Polime giấy và cà phê Vibra'),
(N'Máy tính Nam Shark', N'Sở hữu con chip siêu tối tân của Tony Stark và chất liệu được làm từ Vibranium của Wakanda'),
(N'Bình dựng nước Nam Shark', N'Lớp vỏ bên ngoài chịu được sức công 
phá của bom nhiệt hạch, lớp bên trong có thể giữ nhiệt được suốt 100 năm')