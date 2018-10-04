# Hair Salon
_by David Mortkowitz_

*Description*
_A program that allows you, a salon owner, to keep track of your stylist employees, and their clients, on a database._

## Hair Salon Specs
_This program allows you to create an organized list of stylists, and the clients they currently hold at your hair salon. You can choose to either add a newly hired stylist, add new clients to an existing stylist, or edit and remove a current stylist's clientbase._

## Directions for Installation
#### *Note*: This application was built using a Mac running OS X 10.13. If you are running Windows or a significantly earlier version of OS X, these instructions may vary based on your operating system.

* _To launch this application, please have MAMP and MySql installed and configured on your Mac._
* _Clone or download code from the Git Rep, located at:_
* https://github.com/dmortkowitz/HairSalon.git* 
* _Use a Database software such as MAMP, and turn on your server using your username and password._ 
* _Open Terminal, and launch MySql._
* _Once in MySql, run the following commands to create your database:_
```
  CREATE DATABASE david_mortkowitz;
  USE david_mortkowitz;
  CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE customers (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT(11));
  CREATE TABLE specialties (id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE stylist_specialty (id serial PRIMARY KEY, specialty_id INT(11), stylist_id INT(11));
```
* Next, in MySql, and run the following commands to create your test database:_
```
  CREATE DATABASE david_mortkowitz_test;
  USE david_mortkowitz_test;
  CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE customers (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT(11));
  CREATE TABLE specialties (id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE stylist_specialty (id serial PRIMARY KEY, specialty_id INT(11), stylist_id INT(11));
```
* _Once the database has been created, open your project folder in into your text editor of choice._
* _Then, from terminal, please navigate to the project folder HairSalon within HairSalon.Solution, and type:_
* dotnet restore
* dotnet run
* _Once the file is running, signaled by the notification in Terminal, you may locate the site at:_
* Localhost:5000 
* _in your browser of choice (Chrome is recommended) and navigate the Hair Salon database._


## Expected behaviors
* _View all stylists._
* _View all customers._
* _Using joined tables to share data of stylists and customers, within the company._
* _To be able to add, remove and transfer stylist-customer relationships._
* _To be able to add and remove stylist specialties._
* _User can add and remove stylists._
* _User can add and remove clients from within stylist listings._
* _As you add and remove items from the list, it should automatically reflect changes to data in the database._

## Technologies used
* C# 
* .NET Core App 1.1
* Mono
* Visual Studio
* Visual Studio Code
* Git
* Github

## Contact me

_Please contact me at dmortkowitz [at] gmail.com with any questions, critiques, or suggestions._

*Copyright* _David Mortkowitz. 2018._