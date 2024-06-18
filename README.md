# event-reminder

## Description
This project is a .NET Core Web API for seamless event organization with configurable notification systems.

## Important Update: Migration to .NET 8
We have migrated the project from .NET 3.1 to .NET 8 and updated several dependencies. Below are the details of the changes made during this migration.

## Main Changes
1. **Migration from .NET 3.1 to .NET 8**
    - Updated the SDK in the project files to .NET 8 version.

2. **NuGet Updates**
    - All NuGet dependencies have been updated to their latest versions.

3. **Update MediatR in the Domain Layer**
    - Updated MediatR to the latest version.
    - Removed `MediatR.Extensions.Microsoft.DependencyInjection` as it is deprecated.

4. **Adaptation of `TransactionBehaviour.cs` and `ValidationBehaviour.cs`**
    - Necessary adaptations to be compatible with the latest version of MediatR.

5. **Configuration Changes**
    - Removed `Startup.cs`.
    - Created a single `Program.cs` in the Api and Notifications projects.
    - Modified `AddApplication()` and `AddBackgroundTasks()` to align with the changes in MediatR.

6. **Cryptography Update**
    - In `/Infrastructure/Cryptography/PasswordHasher`, replaced `new RNGCryptoServiceProvider();` with `RandomNumberGenerator.Create();` because the former is obsolete.

## Testing
- Ran all existing unit and integration tests to ensure all changes are functional.
- Manually tested key functionalities in the Api and Notifications projects.

## Developer Guide
### Updating Local Environments
To update your local environment, follow these steps:
1. Ensure you have the latest version of the .NET 8 SDK installed.
2. Update the NuGet dependencies using the `dotnet restore` command.
3. Review the changes in `Program.cs` and adapt any custom configuration you have in your environment.

### Additional Considerations
- Ensure all dependencies are updated to their latest versions to avoid conflicts.
- Review the MediatR documentation to understand the changes in the new version and how they affect your implementation.

## Credits
This project is a fork of the original repository created by [m-jovanovic](https://github.com/m-jovanovic) available at [event-reminder](https://github.com/m-jovanovic/event-reminder).

Thank you for your attention and collaboration in reviewing and adapting these changes,

[jmuinos](https://github.com/jmuinos)
