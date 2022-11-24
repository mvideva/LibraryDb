use LibraryDb;

create table books (
	isbn int,  
	title varchar(100),
	author varchar(50),
	genre varchar (75),
	primary key(isbn)
);

create table customers (
	id int,
	name varchar(255), 
	phone_number varchar(255), 
	address varchar(255), 
	primary key(id)
);

create table checkouts (
	customer_id int,
    book_id int,
    due_date datetime,
	return_date datetime,
    primary key(customer_id, book_id),
	foreign key (customer_id) references customers(id) on delete cascade,
	foreign key (book_id) references books(isbn) on delete cascade
);

create table staff (
	id int,
	name varchar(100),
	position varchar(50),
	shift_hours int,
	primary key (id)
);

create table charges (
	customer_id int,
	book_id int,
	staff_id int,
	amount_due decimal(3,2),
	payment_date datetime,
	description varchar(50),
	primary key(customer_id, book_id),
	foreign key (customer_id) references customers(id) on delete cascade,
	foreign key (book_id) references books(isbn) on delete cascade
);
