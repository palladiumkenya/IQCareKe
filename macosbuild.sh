#!/bin/bash 
ARRAY=(
   "Solutions/Entities/Entities.sln"
    "Solutions/Interfaces/Interfaces.sln"
    #"Solutions/Application/Application.sln"
    "Solutions/DataAccess/DataAccess.sln"
    "Solutions/BusinessProcess/BusinessProcess.sln"
    "Solutions/Presentation/Presentation.sln"
    "Solutions/IQCare.Library/IQCare.Library.sln"
    "Solutions/IQLookup/IQCare.Lookup.sln"
    "Solutions/Billing/IQCare.Billing.sln"
    #"Solutions/IQCare.CCC/IQCare.CCC.sln"
    "Solutions/IQCare.PSmart/IQCare.PSmart.sln"
    "Solutions/IQCare.Web.API/IQCare.Web.API.sln"
    "Solutions/IQCareService/IQCareService.sln"
    "Solutions/IQCare Management/IQCare Management.sln"
    "Solutions/IQCare.Release/IQCare.Release.sln"
    )

echo "Build started..."

for i in "${ARRAY[@]}"
do
   :
    msbuild $i -v:m
done

echo "Build completed"

    