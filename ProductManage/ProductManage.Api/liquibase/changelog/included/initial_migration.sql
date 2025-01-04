-- Create Categories table
CREATE TABLE Categories (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(200),
    Status VARCHAR(20) NOT NULL,
    CreatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Create Products table
CREATE TABLE Products (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    Price NUMERIC(18,2) NOT NULL CHECK (Price > 0),
    CategoryId UUID NOT NULL,
    Status VARCHAR(20) NOT NULL,
    StockQuantity INT NOT NULL CHECK (StockQuantity >= 0),
    ImageUrl TEXT,
    CreatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_category FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);