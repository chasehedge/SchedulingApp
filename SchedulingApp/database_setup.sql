CREATE DATABASE IF NOT EXISTS scheduling_db;
USE scheduling_db;

CREATE TABLE country (
    countryId INT(10) PRIMARY KEY AUTO_INCREMENT,
    country VARCHAR(50) NOT NULL,
    createDate DATETIME NOT NULL,
    createdBy VARCHAR(40) NOT NULL,
    lastUpdate TIMESTAMP NOT NULL,
    lastUpdateBy VARCHAR(40) NOT NULL
);

CREATE TABLE city (
    cityId INT(10) PRIMARY KEY AUTO_INCREMENT,
    city VARCHAR(50) NOT NULL,
    countryId INT(10) NOT NULL,
    createDate DATETIME NOT NULL,
    createdBy VARCHAR(40) NOT NULL,
    lastUpdate TIMESTAMP NOT NULL,
    lastUpdateBy VARCHAR(40) NOT NULL,
    FOREIGN KEY (countryId) REFERENCES country(countryId)
);

CREATE TABLE address (
    addressId INT(10) PRIMARY KEY AUTO_INCREMENT,
    address VARCHAR(50) NOT NULL,
    address2 VARCHAR(50),
    cityId INT(10) NOT NULL,
    postalCode VARCHAR(10) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    createDate DATETIME NOT NULL,
    createdBy VARCHAR(40) NOT NULL,
    lastUpdate TIMESTAMP NOT NULL,
    lastUpdateBy VARCHAR(40) NOT NULL,
    FOREIGN KEY (cityId) REFERENCES city(cityId)
);

CREATE TABLE customer (
    customerId INT(10) PRIMARY KEY AUTO_INCREMENT,
    customerName VARCHAR(45) NOT NULL,
    addressId INT(10) NOT NULL,
    active TINYINT(1) NOT NULL,
    createDate DATETIME NOT NULL,
    createdBy VARCHAR(40) NOT NULL,
    lastUpdate TIMESTAMP NOT NULL,
    lastUpdateBy VARCHAR(40) NOT NULL,
    FOREIGN KEY (addressId) REFERENCES address(addressId)
);

CREATE TABLE user (
    userId INT PRIMARY KEY AUTO_INCREMENT,
    userName VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    active TINYINT NOT NULL,
    createDate DATETIME NOT NULL,
    createdBy VARCHAR(40) NOT NULL,
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40)
);

CREATE TABLE appointment (
    appointmentId INT(10) PRIMARY KEY AUTO_INCREMENT,
    customerId INT(10) NOT NULL,
    userId INT NOT NULL,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    location TEXT,
    contact TEXT,
    type TEXT,
    url VARCHAR(255),
    start DATETIME NOT NULL,
    end DATETIME NOT NULL,
    createDate DATETIME NOT NULL,
    createdBy VARCHAR(40) NOT NULL,
    lastUpdate TIMESTAMP NOT NULL,
    lastUpdateBy VARCHAR(40) NOT NULL,
    FOREIGN KEY (customerId) REFERENCES customer(customerId),
    FOREIGN KEY (userId) REFERENCES user(userId)
);

-- Insert test user
INSERT INTO user (userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy)
VALUES ('test', 'test', 1, NOW(), 'admin', NOW(), 'admin');