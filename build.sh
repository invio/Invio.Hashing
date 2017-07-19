#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

dotnet restore ./src/Invio.Hashing
dotnet restore ./test/Invio.Hashing.Tests

dotnet test ./test/Invio.Hashing.Tests/Invio.Hashing.Tests.csproj -c Release -f netcoreapp1.0

dotnet pack ./src/Invio.Hashing -c Release -o ../../artifacts
