
CREATE USER 'lib'@'localhost' IDENTIFIED BY 'rary';
GRANT ALL PRIVILEGES ON LibraryDb.* TO 'lib'@'localhost';


SELECT books.title, customers.name, customers.address, checkouts.due_date, checkouts.return_date
FROM ((books
INNER JOIN checkouts ON checkouts.book_id = books.id)
INNER JOIN customers ON checkouts.customer_id = customers.id);