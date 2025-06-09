CREATE TABLE motorcycle(
	id varchar(100) UNIQUE PRIMARY KEY,
	model varchar(100),
	plate varchar(100) UNIQUE,
	year int
);

CREATE TABLE delivery_man(
	id varchar(100) UNIQUE PRIMARY KEY,
	name varchar(100),
	cnpj varchar(100) UNIQUE,
	birthday date,
	driver_license_type int,
	driver_license_number varchar(100) UNIQUE
);

CREATE TABLE rental(
	id varchar(100) UNIQUE PRIMARY KEY,
	motorcycle_id varchar(100) REFERENCES motorcycle,
	delivery_man_id varchar(100) REFERENCES delivery_man,
	start_date date,
	end_date date,
	effective_end_date date,
	max_days int
);

CREATE TABLE motorcycle_created(
	id varchar(100) UNIQUE PRIMARY KEY,
	motorcycle_year int
);