create table books (
	id varchar(30),  
	title varchar(100),
	author varchar(50),
	genre varchar (75),
	primary key(id)
);
create index books_title_index on books (title);
create index books_author_index on books (author);

create table customers (
	id int,
	name varchar(255), 
	phone_number varchar(255), 
	address varchar(255), 
	primary key(id)
);
create index customers_name_index on customers (name);


create table checkouts (
	customer_id int,
    book_id varchar(30),
    due_date datetime,
    primary key(customer_id, book_id),
	foreign key (customer_id) references customers(id) on delete cascade,
	foreign key (book_id) references books(id) on delete cascade
);

create table staff (
	id int,
	name varchar(100),
	position varchar(50),
	shift_hours varchar(100),
	primary key (id)
);
create index staff_name_index on staff (name);

create table charges (
	customer_id int,
	book_id varchar(30),
	staff_id int,
	amount_due decimal(5,2),
	payment_date datetime,
	description varchar(50),
	primary key(customer_id, book_id),
	foreign key (customer_id) references customers(id) on delete cascade,
	foreign key (book_id) references books(id) on delete cascade
);
create index charges_description_index on charges(description);
