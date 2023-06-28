CREATE TRIGGER trg_Enrollment_Insert
ON tblCourseOfStudent
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
go

CREATE TRIGGER trg_Enrollment_Delete
ON tblCourseOfStudent
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
GO

CREATE TRIGGER trg_DeleteStudent
ON tblStudent
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

DELETE tblStudent
WHERE IDStudent = 1038

INSERT INTO tblCourseOfStudent
Values(5 , 12 ,'2023-12-02 12:00:00.000')

