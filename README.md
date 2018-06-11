# PetStoreAPI
A sample project to spike TestServer for Integration Test .Net core WebAPI. 

a. Tests in TestsWithDB folder use same config as specified for PetStoreAPI project

b. Tests in InMemoryTest folder in memory DB

c. Tests in MockTests folder use same config as specified for PetStoreAPI project and uses Stubbery library to mock external service calls


Steps to run tests
1. Install Postgres DB and create "PetStore" Database
2. Create Tables as below

        CREATE TABLE public."Pets"
        (
          "Id" integer NOT NULL DEFAULT nextval('"Pets_id_seq"'::regclass),
          "Name" character varying,
          "Family" character varying,
          CONSTRAINT "Pets_pkey" PRIMARY KEY ("Id")
        )
        WITH (
          OIDS=FALSE
        );

        CREATE TABLE public."Owners"
        (
          "Id" integer NOT NULL DEFAULT nextval('"Owners_id_seq"'::regclass),
          "Name" character varying,
          CONSTRAINT "Owners_pkey" PRIMARY KEY ("Id")
        )
        WITH (
          OIDS=FALSE
        );
3. Build and Run Nunit Tests in IntegrationTests project

