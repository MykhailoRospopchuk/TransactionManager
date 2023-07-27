# TransactionManager

## Prepare solution

After cloning the repository, go to the root of project directory
```TransactionManagement\TransactionManagement```

Use the commands from the Makefile to prepare the project:

```make help``` - get supporting information

```make add-migration``` - creating a Init migration

```make roll-migration``` - updating the database to the Init migration state

```make roll-back-migration``` - resetting the database to the default state

```make remove-migration``` - delete Init migrations

## OpenApi
```Auth``` Controller - This controller is provided for user authorization.

```Transaction``` Controller - This controller is responsible for retrieving filtered transaction data. It has been implemented user has the opportunity to filter transactions by type (perhaps several types at once), status (one status). Also, the user should be able to find the customer by text search by name. The user can also enable the service user to update the transaction status by id.

Access to this controllers is available only to an authorized user

```TransactionCsv``` Controller - After uploading the file, it will process the content and add data to the database by transaction_id, which should be in the Excel file. That is, if there is no record in the database with such a transaction_id, add such a record to the database, and if such a record is present, update the transaction status.

When requesting a list of transactions, user has the opportunity to filter transactions by type (perhaps several types at once), status (one status). Also, the user be able to find the customer by text search by name.

Access to this controllers is available only to an authorized user



## DataSet
Data from the ```DataSet``` catalog in the root of repository can be used for API testing

## Authorization
login: bigbos@gmail.com

password: automapper