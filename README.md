# Use Azure Functions to process a CSV File and import data into Azure SQL

> NOTE: this hands on lab is currently under development. This note will be removed when it is final.

## Introduction

## Exercises

1. [Create an Azure Functions project](#exercise1)
2. [Test the project locally](#exercise2)
3. [Create the SQL database](#exercise3)
4. [Add and test the code to update the database](#exercise4)
5. [Create an Azure SQL Database](#exercise5)
6. [Migrate the database](#exercise6)
7. [Deploy the project to Azure](#exercise7)
8. [Test a file with valid data](#exercise8)
9. [Test a file with errors](#exercise9)

<a name="exercise1"></a>

## 1. Create an Azure Functions project

This exercise introduces you to Azure Functions along with the ability to emulate storage and debug functions locally. The Azure Functions host makes it possible to run a full version of the functions host on your development machine.

### Prerequisites

* Visual Studio 2017 15.5 or later
* The Azure Development workload
* Azure Functions and Web Jobs Tools (15 or later)

### Steps

1. Open Visual Studio 2017.
2. Select `File` then `New Project` and choose the `Azure Functions` template. Enter `FileProcessor` for the `Name`.

    ![Azure Functions Project](media/step-01-01-new-fn-project.png)
3. In the Azure Functions dialog, choose the `Azure Functions v1 (.NET Framework)` host and select the `Empty` project template. Make sure that `Storage Emulator` is selected for the `Storage Account`. (This will automatically set up connection strings for storage emulation.) Click `OK` and wait for the project to create.

    ![Empty Project](media/step-01-02-fn-v1.png)
4. Right-click on the project name in the Solution Explorer and choose `Add` then `New Item...` 

    ![Add New Item](media/step-01-03-add-new-item.png)
5. Select `Azure Function` for the item and give it the name `FileProcessFn.cs` and click `Add`.

    ![Azure Function Item](media/step-01-04-function.png)
6. In the next dialog, choose the `Blob trigger` template. You can leave `Connection` blank or populate it with `AzureWebJobsStorage`. Type `import` for the `Path`.

    ![Blob Trigger](media/step-01-05-blob-trigger.png)
7. After the class is created, ensure it looks like this (if you did not fill out the `Connection` in the previous step, you can add it here):

    ```csharp
    namespace FileProcessor
    {
        public static class FileProcessFn
        {
            [FunctionName("FileProcessFn")]
            public static void Run([BlobTrigger("import/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, TraceWriter log)
            {
                log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            }
        }
    }
    ```
8. In the Solution Explorer, open `local.settings.json`. It should have development storage set, like this:

    ```JavaScript
    {
        "IsEncrypted": false,
        "Values": {
            "AzureWebJobsStorage": "UseDevelopmentStorage=true",
            "AzureWebJobsDashboard": "UseDevelopmentStorage=true"
        }
    }
    ```

<a name="exercise2"></a>

## 2. Test the project locally

### Prerequisites

* Azure Storage Emulator
* Azure Storage Explorer

### Steps

1. Launch the Storage Emulator by following the directions [here](#link).
2. Open Storage Explorer and navigate to `Blob Containers` in developer storage.

    ![Blob Containers](media/step-02-01-blob-containers.png)
3. Right-click on `Blob Containers` and choose `Create Blob Container`. This will open a node that you can type the name for the container: `import`. Hit `ENTER` and the container details will load.

    ![Import Container](media/step-02-02-new-container.png)
4. In Visual Studio, click the debug button or press F5 to start debugging.

    ![Launch Debug](media/step-02-03-debug.png)
5. Wait for the functions host to start running. The console will show the text `Debugger listening on [::]:5858` (your port may be different.)
6. In the Storage Explorer window for the `import` container, click the `Upload` button and choose the `Upload folder...` option.

    ![Upload Folder](media/step-02-04-upload-folder.png)
7. In the Upload Folder dialog, select the `data` folder that is provided with this hands-on lab. Make sure `Blob type` is set to `Block blob` and `Upload to folder (optional)` is empty. Click `Upload`.

    ![Select Folder](media/step-02-05-select-folder.png)
8. Confirm the files in the folder were processed by checking the logs in the function host console window.

    ![Confirm Upload](media/step-02-06-confirm-upload.png)
9. Stop the debugging session

<a name="exercise3"></a>

## 3. Create the SQL Database

This exercise walks through creating the local SQL database for testing. 

### Prerequisites

* SQL Server Express (full version is fine)
* SQL Server Management Studio (SSMS)

### Steps 

1. Open SQL Server Management Studio and connect to your local server instance.
2. Right-click on the `Databases` node and choose `New Database...`

    ![Create New Database](media/step-03-01-create-database.png)
3. For the `Database name` type `todo`. Adjust any other settings you desire and click `OK`.

    ![Name the Database](media/step-03-02-todo.png)
4. Right-click on the `todo` database and choose `New Query`. In the window that opens, type the following:

    ```SQL
    CREATE TABLE TodoList (Id Int Identity, Task NVarChar(max), IsComplete Bit);
    INSERT TodoList(Task, IsComplete) VALUES ('Insert first record', 1);
    SELECT * FROM TodoList;
    ```
5. Confirm that a single result is returned with "Insert first record" as the task.

<a name="exercise4"></a>

## 4. Add and test the code to update the database

The local database is ready to test. In this exercise, you will use Entity Framework to insert the records you parse from the uploaded files into the SQL database.

### Steps 

1. Add the connection string for SQL Server to `local.json.settings`. It should look like this:


<a name="exercise5"></a>

## 5. Create the Azure SQL Database

The first exercise will create an Azure SQL database in the cloud. This exercise uses the [Azure portal](https://jlik.me/b7b).

### Prerequisites

* Azure Subscription

### Steps

1. Choose `Create a resource` and search for or select `SQL Database`.

    ![SQL Database](media/step-05-01-sql-database.png)
1. Enter a unique `Database name`.
2. Choose your `Azure subscription`.
3. Select the `Create new` option for `Resource group` and enter `my-todo-hol`.
4. Keep the default `Blank database` for `Select source`.
5. Click `Configure required settings` for `Server`.
6. Select `Create new server`.
7. Enter a unique `Server name`.
8. Provide a login and password. ***Note:** be sure to save your credentials!*
9. Pick your preferred `Location`.
10. Click the `Select` button.

    ![Configure server](media/step-05-02-configure-server.png)
1. Click `Pricing tier`.
2. Slide the `DTU` bar to the lowest level for this lab.

    ![Configure performance](media/step-05-03-dtu.png)
1. Tap `Apply`.
2. Check `Pin to dashboard`.
3. Click `Create`.

Wait for the deployment to complete (you will receive an alert) and then continue to the exercise.

<a name="exercise6"></a>

## 6. Migrate the database

<a name="exercise7"></a>

## 7. Deploy the project to Azure

<a name="exercise8"></a>

## 8. Test a file with valid data

<a name="exercise9"></a>

## 9. Test a file with errors