BEGIN TRAN POST
    GO
    DISABLE TRIGGER ALL On DATABASE; 
    GO
    -- Person
    insert into dbo.Person
    values ('Neelam','Andro'), ('Viktória','Wulfflæd'), ('Demetra','Júlia'),  ('Gorou','Mihai'),  ('Rashmi','Yrian');
    GO
    -- Address
    insert into dbo.Address
    values ('800 Cherry Camp Road', 'Chicago', 'IL', '60631'), ('4988 Concord Street', 'Charlotte', 'NC', '28134'), ('2962 Dale Avenue', 'Gig Harbor', 'WA', '98335'),
    ('2134 Wayside Lane', 'Oakland', 'CA', '94612'), ('1916 Twin Oaks Drive', 'Grand Rapids', 'MI', '49503'), ('3669 Sussex Court', 'Killeen', 'TX', '76541'),
    ('4859 Simpson Street', 'Rock Island', 'IL', '61201'), ('4105 Bloomfield Way', 'North Norway', 'ME', '04268'), ('87 Hayhurst Lane', 'Southfield', 'MI', '48075'),
    ('3977 Cambridge Drive', 'Litchfield Park', 'AZ', '85340');
    GO
    -- Company
    insert into dbo.Company
    values ('Advise Advise', 1), ('Law Schmaw', 2), ('Bogtrotter Unlimited', 3), ('Pitt Industries', 4), ('Crystal Engineer', 5)
    GO
    -- Employee
    GO
    insert into dbo.Employee
    values (6, 1, 'Advise Advise', 'Legacy Coordinator','Neelam Andro'), (7, 2, 'Law Schmaw', 'Forward Designer','Viktória Wulfflæd'),
    (8, 3, 'Bogtrotter Unlimited', 'Regional Facilitator','Demetra Júlia'), (9, 4, 'Pitt Industries', 'District Developer','Gorou Mihai'),
    (10, 5, 'Crystal Engineer', 'Senior Developer','Rashmi Yrian');
    GO
    ENABLE TRIGGER ALL On DATABASE; 
COMMIT TRAN POST