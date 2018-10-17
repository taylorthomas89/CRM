DROP DATABASE crm;
CREATE DATABASE crm;
\c crm;


CREATE TABLE "Details" (
    details_id          SERIAL PRIMARY KEY,
    preferred_contact   varchar(255)
);

INSERT INTO "Details" (preferred_contact)
VALUES 
('email'),
('cell');

CREATE TABLE "Customer" (
    customer_id         SERIAL PRIMARY KEY,
    name                varchar(50),
    email               varchar(25),
    phone               varchar(25),
    age                 integer,
    details_id          integer REFERENCES "Details" (details_id)
);

INSERT INTO "Customer" (name, email, phone, age, details_id)
VALUES
('Jane Smith', 'JaneDaySmith99@gmail.com', '949-675-4921', 28, 2),
('Taylor Smith', 'TSmith1@pm.me', '714-360-4991', 29, 1);

CREATE TABLE "Employee" (
    employee_id         SERIAL PRIMARY KEY,
    name                varchar(50),
    email               varchar(50),
    phone               varchar(50)
);

INSERT INTO "Employee" (name, email, phone)
VALUES
('Kevin Jay', 'kjay1@gmail.com', '949-321-4295'),
('John Ngo', 'johnNgo@gmail.com', '714-923-3928');

CREATE TABLE "StatusType" (
    status_id           SERIAL PRIMARY KEY,
    status              varchar(50)
);

INSERT INTO "StatusType" (status)
VALUES
('first contact'),
('closed out');

CREATE TABLE "PriorityType" (
    priority_id         SERIAL PRIMARY KEY,
    priority            varchar(50)
);

INSERT INTO "PriorityType" (priority)
VALUES
('top priority'),
('no priority');

CREATE TABLE "Lead" (
    lead_id             SERIAL PRIMARY KEY,
    last_contact        timestamp,
    status_id           integer REFERENCES "StatusType" (status_id),
    priority_id         integer REFERENCES "PriorityType" (priority_id),
    customer_id         integer REFERENCES "Customer" (customer_id),
    employee_id         integer REFERENCES "Employee" (employee_id)
);

INSERT INTO "Lead" (last_contact, status_id, priority_id, customer_id, employee_id)
VALUES 
('2018-09-15 17:01:36', 1, 1, 1, 1),
('2018-09-15 17:15:20', 2, 2, 2, 2);

CREATE TABLE "Interaction" (
    interaction_id      SERIAL PRIMARY KEY,
    comment             varchar(255),
    datetime            timestamp,
    duration            integer,
    lead_id             integer REFERENCES "Lead" (lead_id),
    employee_id         integer REFERENCES "Employee" (employee_id)
);

INSERT INTO "Interaction" (comment, datetime, duration, lead_id, employee_id)
VALUES
('Customer was satisfied, closed him out', '2018-09-15 17:15:20', 525, 2, 2),
('Needs further assistance', '2018-09-15 17:01:36', 815, 1, 1);
