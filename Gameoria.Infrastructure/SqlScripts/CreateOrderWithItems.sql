CREATE TYPE OrderItemTableType AS TABLE
(
    ProductId UNIQUEIDENTIFIER,
    Quantity INT,
    Price DECIMAL(18,2)
);
GO

CREATE PROCEDURE CreateOrderWithItems
    @OrderId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @StoreId UNIQUEIDENTIFIER,
    @TotalAmount DECIMAL(18,2),
    @Items OrderItemTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Orders(Id, UserId, StoreId, TotalAmount, CreatedDate)
    VALUES (@OrderId, @UserId, @StoreId, @TotalAmount, GETDATE());

    INSERT INTO OrderItems(OrderId, ProductId, Quantity, Price)
    SELECT @OrderId, ProductId, Quantity, Price
    FROM @Items;
END
GO
