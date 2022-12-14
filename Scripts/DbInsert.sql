INSERT INTO books (id, title, author, genre) VALUES 
('978-3-16-148410-0', 'Guardians', 'Brian Dwells', 'Mythical');
INSERT INTO books (id, title, author, genre) VALUES 
('977-3-16-148410-0', '12 Seals', 'Brianna Campbell', 'Non-Fiction');
INSERT INTO books (id, title, author, genre) VALUES 
('976-3-16-148410-0', 'Mickey Mouse Tales', 'Tony Griffin', 'Comedy');
INSERT INTO books (id, title, author, genre) VALUES 
('975-3-16-148410-0', 'Abounding Heights', 'Anna Spells', 'Drama');
INSERT INTO books (id, title, author, genre) VALUES 
('974-3-16-148410-0', 'Green Towers', 'Amy Towers', 'Mythical');
INSERT INTO books (id, title, author, genre) VALUES 
('979-3-17-148810-0', 'Hero', 'Steve Maxwell', 'Thriller');
INSERT INTO books (id, title, author, genre) VALUES 
('957-3-16-148610-0', 'My House', 'Tony Stills', 'Comedy');
INSERT INTO books (id, title, author, genre) VALUES 
('996-3-16-128410-0', 'Yellow Trees', 'Erika Harves', 'Non-Fiction');
INSERT INTO books (id, title, author, genre) VALUES 
('905-3-16-148310-0', 'Up Above', 'Lance Treedy', 'Thriller');
INSERT INTO books (id, title, author, genre) VALUES 
('914-3-16-146410-0', 'The Medow', 'Maddie Phase', 'Drama');

INSERT INTO customers (id, name, phone_number, address) VALUES 
(1, 'Alison Adams', '345-725-2681', '12 Main St.');
INSERT INTO customers (id, name, phone_number, address) VALUES 
(2, 'Brian Boyce', '324-764-2456', '34 Smith Rd.');
INSERT INTO customers (id, name, phone_number, address) VALUES 
(3, 'Charles Chase', '246-764-8654', '18 Hills Av.');
INSERT INTO customers (id, name, phone_number, address) VALUES 
(4, 'Dillon Deters', '475-876-3145', '9 Creek Rd.');
INSERT INTO customers (id, name, phone_number, address) VALUES 
(5, 'Elise Elberts', '986-345-5687', '45 Broad St.');

INSERT INTO charges (customer_id, book_id, staff_id, amount_due, payment_date, description) VALUES 
(1,'978-3-16-148410-0',1,10.00,'2022-1-15','lost');
INSERT INTO charges (customer_id, book_id, staff_id, amount_due, payment_date, description) VALUES 
(2,'977-3-16-148410-0',2,5.00,'2022-2-16','damaged');
INSERT INTO charges (customer_id, book_id, staff_id, amount_due, payment_date, description) VALUES 
(3,'976-3-16-148410-0',3,0.50,'2022-3-16','late');
INSERT INTO charges (customer_id, book_id, staff_id, amount_due, payment_date, description) VALUES 
(4,'975-3-16-148410-0',4,2.50,'2022-4-15','late');
INSERT INTO charges (customer_id, book_id, staff_id, amount_due, payment_date, description) VALUES 
(5,'974-3-16-148410-0',5,5.00,'2022-4-16','damaged');

INSERT INTO staff (id,name,position,shift_hours) VALUES 
(1,'Fiona Flanders','Manager','10:00-5:00 Mon-Fri');
INSERT INTO staff (id,name,position,shift_hours) VALUES 
(2,'George Garrison','Checkout/Stocking','10:00-1:30 Mon-Fri');
INSERT INTO staff (id,name,position,shift_hours) VALUES 
(3,'Haley Halders','Checkout/Stocking','1:30-5:00 Mon-Fri');
INSERT INTO staff (id,name,position,shift_hours) VALUES 
(4,'Ian Ivans','Checkout/Stocking','10:00-5:00 Mon-Fri');
INSERT INTO staff (id,name,position,shift_hours) VALUES 
(5,'Jim Jones','Janitor','10:00-6:00 Mon-Thurs');

INSERT INTO checkouts (customer_id,book_id,due_date) VALUES 
(1,'978-3-16-148410-0','2022-1-15');
INSERT INTO checkouts (customer_id,book_id,due_date) VALUES 
(2,'977-3-16-148410-0','2022-2-16');
INSERT INTO checkouts (customer_id,book_id,due_date) VALUES 
(3,'976-3-16-148410-0','2022-3-16');
INSERT INTO checkouts (customer_id,book_id,due_date) VALUES 
(4,'975-3-16-148410-0','2022-4-15');
INSERT INTO checkouts (customer_id,book_id,due_date) VALUES 
(5,'974-3-16-148410-0','2022-4-16');
