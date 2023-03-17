CREATE TABLE ExistentItem (
    itemID INTEGER PRIMARY KEY NOT NULL UNIQUE,
    title TEXT NOT NULL,
    price REAL NOT NULL,
    storeName TEXT NOT NULL  
);