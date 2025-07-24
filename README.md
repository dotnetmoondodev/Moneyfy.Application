# Moneyfy.Application
Application layer for Moneyfy Solution

## Create and publish package
```powershell
$version="1.0.2"
$owner="dotnetmoondodev"
$gh_pat=""

dotnet pack .\application --configuration Release -p:PackageVersion=$version -p:RepositoryUrl=https://github.com/$owner/Moneyfy.Application -o packages

dotnet nuget push packages\Moneyfy.Application.$version.nupkg --api-key $gh_pat --source "github"
```