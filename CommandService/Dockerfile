# Get the base SDK image form microsoft
# we need this image to build the project
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build-env
# Specify the working dierctory to put our stuff into it
WORKDIR /app

# Copy the .csproj into the container
COPY *.csproj ./
# restore all the dependencies
RUN dotnet restore

# Copy the source code of the project
COPY . ./
# Get the compiled project into a folder called out
RUN dotnet publish -c Release -o out

# in order to make our image light, we don't need the build sdk image 
# Generate a run time image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
# Combine from the previous steps
COPY --from=build-env /app/out .
# when the container starts, what we need to run  ..
ENTRYPOINT [ "dotnet", "CommandService.dll" ]docker