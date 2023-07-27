help:
## The following commands can be used:
	@sed -n 's/^##//p' ${MAKEFILE_LIST}

add-migration:
## add-migration:		create default migrations
	dotnet ef migrations add InitMigration --context TransactionDbContext

roll-migration:
## roll-migration:	roll default migrations
	dotnet ef database update InitMigration --context TransactionDbContext

roll-back-migration:
## roll-back-migration:	roll back to default migrations
	dotnet ef database update 0 --context TransactionDbContext

remove-migration:
## remove-migration:	remove default migrations
	dotnet ef migrations remove --context TransactionDbContext