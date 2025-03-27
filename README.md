# Back-End-Pizza-Store
Source giao diện xử lý Back End cho cửa hàng pizza store
### Database
Download and install docker: https://www.docker.com/products/docker-desktop/ <br />
To create local database: 
<br />
<code>
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Express" -p 5000:1433 -d mcr.microsoft.com/mssql/server:2019-latest
</code>
1. To add new migration:
<br />
<code>
dotnet ef migrations add name_of_migration --project dotnet ef migrations add name_of_migration --project WebPizza_API_BackEnd/WebPizza_API_BackEnd.csproj --startup-project WebPizza_API_BackEnd
</code>
2. To remove migration:
<br />
<code>
dotnet ef migrations remove --project dotnet ef migrations add name_of_migration --project WebPizza_API_BackEnd/WebPizza_API_BackEnd.csproj --startup-project WebPizza_API_BackEnd
</code>
3. To udpate migration:
<br />
<code>
dotnet ef database update --project WebPizza_API_BackEnd/WebPizza_API_BackEnd.csproj --startup-project WebPizza_API_BackEnd
</code>
