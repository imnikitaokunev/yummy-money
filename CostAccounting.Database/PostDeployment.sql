/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\Scripts\Membership\Roles.sql
:r .\Scripts\Membership\Users.sql
:r .\Scripts\Membership\UserRoles.sql
:r .\Scripts\Core\Categories.sql
:r .\Scripts\Core\Expenses.sql
:r .\Scripts\Core\Incomes.sql